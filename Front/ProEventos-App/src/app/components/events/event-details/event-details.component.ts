import { Component, OnInit, TemplateRef } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

import { EventService } from '@app/services/event.service';
import { Event } from '@app/models/Event'
import { Observable } from 'rxjs';
import { Lote } from '@app/models/Lote';
import { LoteService } from '@app/services/lote.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-event-details',
  templateUrl: './event-details.component.html',
  styleUrls: ['./event-details.component.scss']
})
export class EventDetailsComponent implements OnInit {
  modalRef?: BsModalRef;

  event = {} as Event;
  form!: FormGroup;
  saveState = 'post';
  eventId: number = 0;
  lotId: number = 0;
  currentLot = {id: 0, name: '', index: 0};
  imageUrl = 'assets/image/upload.png';
  imageFile!: File;

  get editMode(): boolean {
    return this.saveState === 'put'
  }

  get lotes(): FormArray {
    return this.form.get('lotes') as FormArray;
  }

  get f(): any {
    return this.form.controls;
  }

  get bsConfig(): any {
    return {
      isAnimated: true,
      adaptivePosition: true,
      dateInputFormat: 'MMMM DD, YYYY hh:mm A',
      containerClass: 'theme-default',
      showWeekNumbers: false
    }
  }
  get bsConfigLot(): any {
    return {
      isAnimated: true,
      adaptivePosition: true,
      dateInputFormat: 'MMMM DD, YYYY hh:mm A',
      containerClass: 'theme-default',
      showWeekNumbers: false
    }
  }


  constructor(private fb: FormBuilder,
              private activatedRouter: ActivatedRoute,
              private modalService: BsModalService,
              private eventService: EventService,
              private loteService: LoteService,
              private spinner: NgxSpinnerService,
              private toastr: ToastrService,
              private router: Router) { }

  public loadingEvent(): void {
    this.eventId = +this.activatedRouter.snapshot.paramMap.get('id')!;

    if(this.eventId !== null && this.eventId !== 0){
      this.spinner.show();
      this.saveState = 'put';
      this.eventService.getEventById(+this.eventId).subscribe({
        next: (event: Event) => {
          this.event = {... event};
          this.form.patchValue(this.event);
          this.event.lotes.forEach(lot => {
            this.lotes.push(this.createLote(lot));
          })
        },
        error: (error: any) => {
          console.log(error);
          this.spinner.hide();
          this.toastr.error('Error loading event.', 'Error!');
        },
        complete: () => {
          this.spinner.hide()
        }
      });
    }

  }


  ngOnInit() {
    this.validation();
    this.loadingEvent();

  }

  public validation(): void {
    this.form = this.fb.group({
      subject: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      local: ['', Validators.required],
      eventDate: ['', [Validators.required]],
      qtyGuests: ['', [Validators.required, Validators.max(1000)]],
      phone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      imageURL: ['', Validators.required],
      lotes: this.fb.array([])
  });
  }

  addLote(): void {
    this.lotes.push(this.createLote({id: 0} as Lote));
  }

  createLote(lote: Lote): FormGroup {
    return this.fb.group({
      id: [lote.id,],
      name: [lote.name, Validators.required],
      price: [lote.price, Validators.required],
      quantity: [lote.quantity, [Validators.required, Validators.max(1000)]],
      dateStart: [lote.dateStart],
      dateEnd: [lote.dateEnd],
    })
  }

  public resetForm(): void{
    this.form.reset();
  }

  public cssValidator(field: FormControl | AbstractControl | null): any {
    return {'is-invalid': field?.errors && field?.touched};
  }

  public saveEvent(): void {
    this.spinner.show();
    let service = {} as Observable<Event>;

    if(this.saveState === 'post'){
      this.event = {... this.form.value };
      service = this.eventService.post(this.event);
    }
    else{
      this.event = { id: this.event.id, ... this.form.value };
      service = this.eventService.put(this.event);
    }
    var observer = {
      next: (eventReturn: Event) => {
        this.toastr.success('Event created successfully.', 'Success');
        this.router.navigate([`events/details/${eventReturn.id}`]);
      },
      error: (error: any) => {
        console.log(error);
        this.toastr.error('Error creating the event.', 'Error!');

      }
    }
    service.subscribe(observer).add(()=> this.spinner.hide());
  }

  public saveLots(): void {
    this.spinner.show();
    const observer = {
      next: () => {
        this.toastr.success('Lots saved successfully.', 'Success');
      },
      error: (error: any) => {
        console.error(error);
        this.toastr.error('Error saving lots.', 'Error!');
      }
    }
    console.log(this.form.value.lotes)
    this.loteService.saveLote(this.eventId!, this.form.value.lotes).subscribe(observer).add(
      () => this.spinner.hide()
    );
  }

  openModal(template: TemplateRef<any>, index: number): void {
    this.currentLot.id = this.lotes.get(index + '.id')?.value;
    this.currentLot.name = this.lotes.get(index + '.name')?.value;
    this.currentLot.index = index;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

   confirm(): void {
    this.modalRef?.hide();
    this.spinner.show();
    this.loteService.deleteLot(this.eventId, this.currentLot.id).subscribe({
      next: () => {
        this.toastr.success('Lot deleted successfully.', 'Success');
        this.lotes.removeAt(this.currentLot.index);
      },
      error: (error: any) => {
        console.error(error);
        this.toastr.error('Error deleting lot.', 'Error!');
      },
    }).add(() => this.spinner.hide());
  }

  decline(): void {
    this.modalRef?.hide();
  }

  public returnTitle(value: string): string{
    return value === null || value === ''
    ? 'Lot name'
    : value
  }

  public onFileChanged(evento: any) : void {
    const reader = new FileReader();

    reader.onload = (event: any) => this.imageUrl = event.target.result;

    this.imageFile = evento.target.files[0];
    reader.readAsDataURL(this.imageFile)
  }

  //





}

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

import { EventService } from '@app/services/event.service';
import { Event } from '@app/models/Event'
import { Observable } from 'rxjs';

@Component({
  selector: 'app-event-details',
  templateUrl: './event-details.component.html',
  styleUrls: ['./event-details.component.css']
})
export class EventDetailsComponent implements OnInit {

  event = {} as Event;
  form!: FormGroup;
  saveState = 'post';

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

  constructor(private fb: FormBuilder,
              private router: ActivatedRoute,
              private eventService: EventService,
              private spinner: NgxSpinnerService,
              private toastr: ToastrService) { }

  public loadingEvent(): void {
    const eventIdParam = this.router.snapshot.paramMap.get('id');

    if(eventIdParam !== null){
      this.spinner.show();
      this.saveState = 'put';
      this.eventService.getEventById(+eventIdParam).subscribe({
        next: (event: Event) => {
          this.event = {... event};
          this.form.patchValue(this.event);
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
      imageURL: ['', Validators.required]
  });
  }

  public resetForm(): void{
    this.form.reset();
  }

  public cssValidator(field: FormControl): any {
    return {'is-invalid': field.errors && field.touched};
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
      next: () => {
        this.toastr.success('Event created successfully.', 'Success');
      },
      error: (error: any) => {
        console.log(error);
        this.toastr.error('Error creating the event.', 'Error!');

      }
    }
    service.subscribe(observer).add(()=> this.spinner.hide());
  }



}

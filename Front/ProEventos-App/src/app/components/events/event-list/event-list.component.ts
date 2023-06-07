import { Component, OnInit, TemplateRef } from '@angular/core';
import { EventService } from '@app/services/event.service';
import { Event } from '@app/models/Event';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { Router } from '@angular/router';

@Component({
  selector: 'app-event-list',
  templateUrl: './event-list.component.html',
  styleUrls: ['./event-list.component.css']
})
export class EventListComponent implements OnInit {
  modalRef?: BsModalRef;

  public eventsFiltered: Event[] = [];
  public events: Event[] = [];
  public widthImg: number = 130;
  public marginImg: number = 2;
  public showImg: boolean = true;
  private _listFilter: string = '';
  public eventId = 0;

  public get listFilter(): string{
    return this._listFilter;
  }
  public set listFilter(value: string){
    this._listFilter = value;
    this.eventsFiltered = this.listFilter? this.filterEvents(this.listFilter) : this.events;
  }

  public filterEvents(filterBy: string): Event[]{
    filterBy = filterBy.toLocaleLowerCase();
    return this.events.filter(
      (event: any) => event.subject.toLocaleLowerCase().indexOf(filterBy) !== -1 ||
      event.local.toLocaleLowerCase().indexOf(filterBy) !== -1
    );
  }

  constructor(
    private eventService: EventService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router,
    ){ }

  public ngOnInit(): void{
    /** spinner starts on init */
    this.spinner.show();
    this.getEvents();
  }

  public getEvents(): void {
    const observer = {
      next: (_events: Event[]) => {
        this.events = _events,
        this.eventsFiltered = this.events
      },
      error: (error: any) => {
        this.spinner.hide();
        this.toastr.error('Error loading events.', 'Error');
        console.log(error);
      },
      complete: () => this.spinner.hide()
    };
    this.eventService.getEvents().subscribe(observer);
  }

  public hiddenImg(): void{
    this.showImg = !this.showImg;
  }

  openModal(event: any, template: TemplateRef<any>, eventId: number): void {
    event.stopPropagation();
    this.eventId = eventId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef?.hide();
    this.spinner.show();
    const observer = {
      next: (result: any) => {
        if(result.message === "Deleted"){
          this.toastr.success('Event successfully deleted.', 'Deleted'),
          this.spinner.hide(),
          this.getEvents()
        }
      },
      error: (error: any) => {
        this.spinner.hide();
        this.toastr.error('Error deleting event.', 'Error');
        console.log(error);
      },
      complete: () => this.spinner.hide()
    };
    this.eventService.deleteEvent(this.eventId).subscribe(observer);
  }

  decline(): void {
    this.modalRef?.hide();
  }

  eventDetails(id: number): void {
    this.router.navigate([`events/details/${id}`]);

  }
}

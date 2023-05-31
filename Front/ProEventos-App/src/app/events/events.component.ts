import { Component } from '@angular/core';
import { EventService } from '../services/event.service';
import { Event } from '../models/Event';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss']
})
export class EventsComponent {

  public eventsFiltered: Event[] = [];
  public events: Event[] = [];
  public widthImg: number = 150;
  public marginImg: number = 2;
  public showImg: boolean = true;
  private _listFilter: string = '';

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

  constructor(private eventService: EventService){ }

  public ngOnInit(): void{
    this.getEvents();
  }

  public getEvents(): void {
    const observer = {
      next: (_events: Event[]) => {
        this.events = _events,
        this.eventsFiltered = this.events
      },
      error: (error: any) => console.log(error)
    };
    this.eventService.getEvents().subscribe(observer);
  }

  public hiddenImg(): void{
    this.showImg = !this.showImg;
  }
}

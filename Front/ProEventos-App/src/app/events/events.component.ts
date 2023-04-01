import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss']
})
export class EventsComponent {

  public eventsFiltered: any = [];
  public events: any = [];
  widthImg = 150;
  marginImg = 2;
  showImg = true;
  private _listFilter: string = '';

  public get listFilter(): string{
    return this._listFilter;
  }
  public set listFilter(value: string){
    this._listFilter = value;
    this.eventsFiltered = this.listFilter? this.filterEvents(this.listFilter) : this.events;
  }

  filterEvents(filterBy: string): any{
    filterBy = filterBy.toLocaleLowerCase();
    return this.events.filter(
      (event: any) => event.subject.toLocaleLowerCase().indexOf(filterBy) !== -1 ||
      event.local.toLocaleLowerCase().indexOf(filterBy) !== -1
    )
  }

  constructor(private http: HttpClient){ }

  ngOnInit(): void{
    this.getEvents();
  }

  public getEvents(): void {
    this.http.get('https://localhost:5001/api/Events').subscribe(
      response => {
        this.events = response,
        this.eventsFiltered = this.events
      },
      error => console.log(error),
    );
  }

  public hiddenImg(){
    this.showImg = !this.showImg;
  }
}

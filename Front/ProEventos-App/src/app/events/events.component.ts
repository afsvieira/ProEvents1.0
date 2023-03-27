import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss']
})
export class EventsComponent {

  public events: any = [];
  widthImg = 150;
  marginImg = 2;
  showImg = true;

  constructor(private http: HttpClient){ }

  ngOnInit(): void{
    this.getEvents();
  }

  public getEvents(): void {
    this.http.get('https://localhost:5001/api/Events').subscribe(
      response => this.events = response,
      error => console.log(error),
    );
  }

  public hiddenImg(){
    this.showImg = !this.showImg;
  }
}

import { Event } from '../models/Event';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, take } from 'rxjs';

@Injectable()
export class EventService {
  baseURL = 'https://localhost:5001/api/Events';

  constructor(private http: HttpClient) { }

  public getEvents(): Observable<Event[]> {
    return this.http
    .get<Event[]>(this.baseURL)
    .pipe(take(1));
  }

  public getEventsBySubject(subject: string): Observable<Event[]> {
    return this.http
    .get<Event[]>(`${this.baseURL}/${subject}/subject`)
    .pipe(take(1));
  }

  public getEventById(id: number): Observable<Event> {
    return this.http
    .get<Event>(`${this.baseURL}/${id}`)
    .pipe(take(1));
  }

  public post(event: Event): Observable<Event> {
    return this.http
    .post<Event>(this.baseURL, event)
    .pipe(take(1));
  }

  public put(event: Event): Observable<Event> {
    return this.http
    .put<Event>(`${this.baseURL}/${event.id}`, event)
    .pipe(take(1));
  }

  public deleteEvent(id: number): Observable<any> {
    return this.http
    .delete(`${this.baseURL}/${id}`, {})
    .pipe(take(1));
  }
}

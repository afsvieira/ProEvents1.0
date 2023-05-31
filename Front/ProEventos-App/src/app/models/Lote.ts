import { Event } from "./Event";

export interface Lote {
  id: number;
  name: string;
  price: number;
  dateStart?: Date;
  dateEnd?: Date;
  quantity: number;
  eventId: number
  event: Event;
}

import { Speaker } from "./Speaker";
import { Event } from "./Event";

export interface SocialMedia {
  id: number;
  name: string;
  uRL: string;
  eventId?: number;
  event: Event;
  speakerId?: number;
  speaker: Speaker;
}

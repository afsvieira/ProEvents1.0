import { SocialMedia } from "./SocialMedia";
import { Event } from "./Event";

export interface Speaker {
  id: number;
  name: string;
  miniResume: string;
  imageURL: string;
  phone: string;
  email: string;
  socialMedias: SocialMedia[];
  speakerEvents: Event[];
}

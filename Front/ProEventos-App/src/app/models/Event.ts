import { Lote } from "./Lote";
import { SocialMedia } from "./SocialMedia";
import { Speaker } from "./Speaker";

export interface Event {
  id: number;
  local: string;
  eventDate?: Date;
  subject: string;
  qtyGuests: number;
  imageURL: string;
  phone: string;
  email: string;
  lotes: Lote[];
  socialMedias: SocialMedia[];
  speakerEvents: Speaker[];
}

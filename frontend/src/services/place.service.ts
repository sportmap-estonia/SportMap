import { BaseService } from './base.service';
import type { PlaceDto } from '@/types/place';
export type { PlaceDto } from '@/types/place';

export class PlaceService extends BaseService<PlaceDto> {
  protected readonly endpoint = 'api/places';

  constructor() {
    const baseUrl = process.env.NEXT_PUBLIC_API_URL;
    super(baseUrl);
  }
}

export const placeService = new PlaceService();

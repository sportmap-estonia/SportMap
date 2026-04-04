import { BaseService } from './base.service';

export interface PlaceDto {
  id: string;
  name: string;
  description: string;
  placeTypeId: string;
  placeType?: PlaceTypeDto;
  latitude: number;
  longitude: number;
  address?: string;
  imageId?: string;
  image?: ImageDto;
  creatorId: string;
  creatorName: string;
  createdAt: string;
  updatedAt?: string;
  status: string;
}

export interface PlaceTypeDto {
  id: string;
  name: string;
  description: string;
}

export interface ImageDto {
  id: string;
  name: string;
  fileName?: string;
}

export class PlaceService extends BaseService<PlaceDto> {
  protected readonly endpoint = 'places';

  constructor() {
    const baseUrl = process.env.NEXT_PUBLIC_API_URL || '/api';
    super(baseUrl);
  }
}

export const placeService = new PlaceService();

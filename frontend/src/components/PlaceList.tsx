'use client';

import React from 'react';
import type { PlaceDto } from '@/services/place.service';

interface PlaceListProps {
  places: PlaceDto[];
  onPlaceClick: (place: PlaceDto) => void;
}

export default function PlaceList({ places, onPlaceClick }: PlaceListProps) {
  if (places.length === 0) {
    return (
      <div className="p-4 text-center text-gray-500">
        No places found
      </div>
    );
  }

  return (
    <div className="p-4 space-y-3 overflow-y-auto h-full">
      {places.map((place) => (
        <div
          key={place.id}
          onClick={() => onPlaceClick(place)}
          className="bg-white dark:bg-zinc-800 rounded-lg p-4 shadow-sm border border-gray-200 dark:border-zinc-700 cursor-pointer hover:bg-gray-50 dark:hover:bg-zinc-700 transition-colors"
        >
          <div className="flex items-start justify-between">
            <div className="flex-1">
              <h3 className="font-semibold text-gray-900 dark:text-white">
                {place.name}
              </h3>
              {place.placeType && (
                <span className="inline-block mt-1 text-xs px-2 py-0.5 bg-blue-100 dark:bg-blue-900 text-blue-800 dark:text-blue-200 rounded-full">
                  {place.placeType.name}
                </span>
              )}
              {place.address && (
                <p className="mt-2 text-sm text-gray-600 dark:text-gray-400">
                  {place.address}
                </p>
              )}
              {place.description && (
                <p className="mt-2 text-sm text-gray-500 dark:text-gray-500 line-clamp-2">
                  {place.description}
                </p>
              )}
            </div>
          </div>
        </div>
      ))}
    </div>
  );
}

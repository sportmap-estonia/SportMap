'use client';

import React, { useEffect, useRef, useState } from 'react';
import mapboxgl from 'mapbox-gl';
import 'mapbox-gl/dist/mapbox-gl.css';
import PlaceDetailSheet, { Place } from './PlaceDetailSheet';
import { RecenterButton } from './navigation/RecenterButton';
import { placeService, PlaceDto } from '@/services/place.service';

const TALLINN_CENTER: [number, number] = [24.7421, 59.4379];

// Convert API PlaceDto to frontend Place interface
function mapToPlace(dto: PlaceDto): Place {
  return {
    id: dto.id,
    name: dto.name,
    placeTypeId: dto.placeTypeId,
    placeType: dto.placeType ? {
      id: dto.placeType.id,
      name: dto.placeType.name,
      description: dto.placeType.description
    } : undefined,
    location: { lat: dto.latitude, lng: dto.longitude },
    address: dto.address,
    description: dto.description,
    imageId: dto.imageId || '',
    creatorId: dto.creatorId,
    createdAt: dto.createdAt,
    updatedAt: dto.updatedAt || '',
    status: dto.status
  };
}

export default function MapView() {
  const mapRef = useRef<HTMLDivElement>(null);
  const mapInstanceRef = useRef<mapboxgl.Map | null>(null);
  const [selectedPlace, setSelectedPlace] = useState<Place | null>(null);
  const [places, setPlaces] = useState<Place[]>([]);
  const [loading, setLoading] = useState(true);

  const handleReport = () => {
    alert('Report functionality would be implemented here');
  };

  const handleRecenter = () => {
    const map = mapInstanceRef.current;
    if (!map) return;

    if ('geolocation' in navigator) {
      navigator.geolocation.getCurrentPosition(
        (pos) => {
          const newCenter: [number, number] = [pos.coords.longitude, pos.coords.latitude];
          map.flyTo({
            center: newCenter,
            zoom: 14,
            essential: true
          });
        },
        (err) => {
          console.warn('Unable to get current location for recentering: ', err);
          // Fallback to Tallinn center if geolocation denied
          map.flyTo({
            center: TALLINN_CENTER,
            zoom: 14,
            essential: true
          });
        }
      );
    } else {
      // Fallback to Tallinn center if geolocation not supported
      map.flyTo({
        center: TALLINN_CENTER,
        zoom: 14,
        essential: true
      });
    }
  };

  // Fetch places from API
  useEffect(() => {
    async function fetchPlaces() {
      try {
        const result = await placeService.getAll();
        if (result.isSucceed && result.value) {
          setPlaces(result.value.map(mapToPlace));
        }
      } catch (error) {
        console.error('Failed to fetch places:', error);
      } finally {
        setLoading(false);
      }
    }
    fetchPlaces();
  }, []);

  useEffect(() => {
    if (!mapRef.current) return;

    // Set the access token if we are on the client
    if (typeof window !== 'undefined') {
      const token = process.env.NEXT_PUBLIC_MAPBOX_TOKEN;
      if (!token) {
        console.error('Mapbox access token is not defined in environment variables');
        return;
      }
      mapboxgl.accessToken = token;
    }

    let map: mapboxgl.Map | null = null;

    const initMap = async () => {
      let center: [number, number] = [24.7421, 59.4379]; // Default to Tallinn

      // Try to get the current position
      if ('geolocation' in navigator) {
        await new Promise<void>((resolve) => {
          navigator.geolocation.getCurrentPosition(
            (pos) => {
              center = [pos.coords.longitude, pos.coords.latitude] as [number, number];
              resolve();
            },
            (err) => {
              console.warn('Unable to get current location: ', err);
              resolve(); // Still resolve so we use the default
            },
            {
              enableHighAccuracy: true,
              timeout: 5000,
              maximumAge: 0
            }
          );
        });
      }

      if (!mapRef.current) return; // Double-check ref still exists

      try {
        map = new mapboxgl.Map({
          container: mapRef.current,
          style: 'mapbox://styles/mapbox/dark-v10',
          center: center,
          zoom: 14
        });

        // Add navigation control
        map.addControl(new mapboxgl.NavigationControl(), 'top-right');
        
        // Store map instance
        mapInstanceRef.current = map;

        // Add markers for places from API
        map.on('load', () => {
          if (places.length === 0) return;
          
          places.forEach((place) => {
            // Create a custom marker element
            const el = document.createElement('div');
            el.className = 'custom-marker';
            el.style.width = '30px';
            el.style.height = '30px';
            el.style.backgroundColor = place.placeType?.name === 'Park' ? '#22c55e' : 
                                      place.placeType?.name === 'Gym' ? '#3b82f6' : 
                                      place.placeType?.name === 'Stadium' ? '#f97316' : '#a855f7';
            el.style.borderRadius = '50%';
            el.style.border = '2px solid #fff';
            el.style.cursor = 'pointer';
            el.style.boxShadow = '0 0 10px rgba(0,0,0,0.5)';

            // Add click handler
            el.addEventListener('click', () => {
              setSelectedPlace(place);
            });

            // Add marker to map
            new mapboxgl.Marker(el)
              .setLngLat([place.location.lng, place.location.lat])
              .addTo(map!);
          });
        });
      } catch (error) {
        console.error('Error initializing Mapbox map:', error);
      }
    };

    initMap();

    return () => {
      if (map) {
        map.remove();
        mapInstanceRef.current = null;
      }
    };
  }, []);

  return (
    <div className="w-full h-full relative">
      <style>{`
        .mapboxgl-ctrl-top-right {
          top: 60px !important;
          right: 8px !important;
        }
        .mapboxgl-ctrl-group {
          background-color: white !important;
          border-radius: 4px !important;
          box-shadow: 0 0 10px rgba(0,0,0,0.3) !important;
        }
        .mapboxgl-ctrl-group button {
          width: 36px !important;
          height: 36px !important;
        }
      `}</style>
      <div ref={mapRef} className="w-full h-full" />
      <div className="absolute top-4 right-4 z-30">
        <RecenterButton onClick={handleRecenter} />
      </div>
      <PlaceDetailSheet
        place={selectedPlace}
        onClose={() => setSelectedPlace(null)}
        onReport={handleReport} />
    </div>
  );
}
'use client';

import React, { useEffect, useRef, useState } from 'react';
import mapboxgl from 'mapbox-gl';
import 'mapbox-gl/dist/mapbox-gl.css';
import PlaceDetailSheet, { Place } from './PlaceDetailSheet';
import { RecenterButton } from './navigation/RecenterButton';

const TALLINN_LOCATIONS: Place[] = [
  {
    id: '1',
    name: 'Toompark',
    placeTypeId: '1',
    placeType: { id: '1', name: 'Park', description: 'Green area for recreation' },
    location: { lat: 59.4379, lng: 24.7421 },
    address: 'Lossi plats, Tallinn Old Town',
    description: 'Historic park in the heart of Tallinn\'s Old Town with walking paths and sports facilities including table tennis.',
    imageId: '1',
    image: { id: '1', name: 'Toompark Image', url: 'https://images.unsplash.com/photo-1578662996442-48f60103fc96?w=800&q=80', entityId: '1' },
    creatorId: 'system',
    createdAt: '2023-01-01T00:00:00Z',
    updatedAt: '2023-01-01T00:00:00Z',
    status: 'verified',
    distance: '0.5 km from center',
    rating: 4.9,
    tags: ['Park', 'Walking', 'Table Tennis', 'Historic']
  },
  {
    id: '2',
    name: 'Männi Park',
    placeTypeId: '1',
    placeType: { id: '1', name: 'Park', description: 'Green area for recreation' },
    location: { lat: 59.3993, lng: 24.6759 },
    address: 'Keskuse 1, 12911 Tallinn',
    description: 'Family-friendly park with basketball court, table tennis, playground and restaurant nearby.',
    imageId: '2',
    image: { id: '2', name: 'Männi Park Image', url: 'https://images.unsplash.com/photo-1520250497591-112f2f40a3f4?w=800&q=80', entityId: '2' },
    creatorId: 'system',
    createdAt: '2023-01-01T00:00:00Z',
    updatedAt: '2023-01-01T00:00:00Z',
    status: 'verified',
    distance: '2.3 km from center',
    rating: 4.3,
    tags: ['Park', 'Basketball', 'Table Tennis', 'Playground', 'Restaurant']
  },
  {
    id: '3',
    name: 'Tondiraba Park',
    placeTypeId: '1',
    placeType: { id: '1', name: 'Park', description: 'Green area for recreation' },
    location: { lat: 59.4459, lng: 24.8526 },
    address: 'Varraku 16, 13917 Tallinn',
    description: 'Large sports and recreation area with ice rink, beach volleyball, bike paths and extensive trails.',
    imageId: '3',
    image: { id: '3', name: 'Tondiraba Park Image', url: 'https://images.unsplash.com/photo-1578662996442-48f60103fc96?w=800&q=80', entityId: '3' },
    creatorId: 'system',
    createdAt: '2023-01-01T00:00:00Z',
    updatedAt: '2023-01-01T00:00:00Z',
    status: 'verified',
    distance: '3.1 km from center',
    rating: 4.7,
    tags: ['Park', 'Ice Rink', 'Beach Volleyball', 'Bike Path', 'Running Trails', 'Dog Park']
  },
  {
    id: '4',
    name: 'MyFitness Viru',
    placeTypeId: '2',
    placeType: { id: '2', name: 'Gym', description: 'Fitness center' },
    location: { lat: 59.4365, lng: 24.7571 },
    address: 'Viru väljak 4, 10153 Tallinn',
    description: 'Modern fitness center in Viru keskus with state-of-the-art equipment and group training classes.',
    imageId: '4',
    image: { id: '4', name: 'MyFitness Viru Image', url: 'https://images.unsplash.com/photo-1578662996442-48f60103fc96?w=800&q=80', entityId: '4' },
    creatorId: 'system',
    createdAt: '2023-01-01T00:00:00Z',
    updatedAt: '2023-01-01T00:00:00Z',
    status: 'verified',
    distance: '0.3 km from center',
    rating: 4.7,
    tags: ['Gym', 'Weight Training', 'Cardio', 'Group Training', '24/7 Access']
  },
  {
    id: '5',
    name: 'Kalev Spa Fitness Center',
    placeTypeId: '2',
    placeType: { id: '2', name: 'Gym', description: 'Fitness center' },
    location: { lat: 59.4258, lng: 24.7512 },
    address: 'Ahtri tn 6b, 10151 Tallinn',
    description: 'Luxury fitness center with 50-meter pool, spa facilities and diverse workout programs.',
    imageId: '5',
    image: { id: '5', name: 'Kalev Spa Image', url: 'https://images.unsplash.com/photo-1578662996442-48f60103fc96?w=800&q=80', entityId: '5' },
    creatorId: 'system',
    createdAt: '2023-01-01T00:00:00Z',
    updatedAt: '2023-01-01T00:00:00Z',
    status: 'verified',
    distance: '1.8 km from center',
    rating: 4.6,
    tags: ['Gym', 'Pool', 'Spa', 'Yoga', 'Pilates', 'Cardio']
  },
  {
    id: '6',
    name: 'Kose Stadium / Kose Park',
    placeTypeId: '3',
    placeType: { id: '3', name: 'Stadium', description: 'Sports stadium' },
    location: { lat: 59.3109, lng: 24.9956 },
    address: 'Puhkekodu tee 55b, Kose vald, Harju maakond',
    description: 'Outdoor stadium with walking paths, benches, illumination and playground in Kose district.',
    imageId: '6',
    image: { id: '6', name: 'Kose Stadium Image', url: 'https://images.unsplash.com/photo-1578662996442-48f60103fc96?w=800&q=80', entityId: '6' },
    creatorId: 'system',
    createdAt: '2023-01-01T00:00:00Z',
    updatedAt: '2023-01-01T00:00:00Z',
    status: 'verified',
    distance: '12.4 km from center',
    rating: 4.4,
    tags: ['Stadium', 'Walking Paths', 'Playground', 'Illuminated', 'Outdoor Sports']
  }
];

export default function MapView() {
  const mapRef = useRef<HTMLDivElement>(null);
  const mapInstanceRef = useRef<mapboxgl.Map | null>(null);
  const [selectedPlace, setSelectedPlace] = useState<Place | null>(null);

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
            center: [24.7421, 59.4379],
            zoom: 14,
            essential: true
          });
        }
      );
    } else {
      // Fallback to Tallinn center if geolocation not supported
      map.flyTo({
        center: [24.7421, 59.4379],
        zoom: 14,
        essential: true
      });
    }
  };

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

        // Add markers for Tallinn locations
        map.on('load', () => {
          TALLINN_LOCATIONS.forEach((place) => {
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
    <div className="w-full h-[calc(100vh-4rem)] relative">
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
'use client';

import React, { useEffect, useRef } from 'react';
import mapboxgl from 'mapbox-gl';
import { PlaceDetailSheet } from './PlaceDetailSheet';
import { RecenterButton } from './navigation/RecenterButton';

export default function MapView() {
  const mapRef = useRef<HTMLDivElement>(null);
  const mapInstanceRef = useRef<mapboxgl.Map | null>(null);

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
          // Fallback to default center if geolocation denied
          map.flyTo({
            center: [-122.415, 37.775],
            zoom: 14,
            essential: true
          });
        }
      );
    } else {
      // Fallback to default center if geolocation not supported
      map.flyTo({
        center: [-122.415, 37.775],
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
      let center: [number, number] = [-122.415, 37.775]; // Default to San Francisco

      // Try to get the current position
      if ('geolocation' in navigator) {
        await new Promise<void>((resolve, reject) => {
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
    <div className="w-full h-[calc(100vh-4rem)]">
      <div ref={mapRef} className="w-full h-full" />
      <div className="absolute top-4 right-4 z-30">
        <RecenterButton onClick={handleRecenter} />
      </div>
      <PlaceDetailSheet
        place={null}
        onClose={() => {}}
        onReport={handleReport} />
    </div>
  );
}

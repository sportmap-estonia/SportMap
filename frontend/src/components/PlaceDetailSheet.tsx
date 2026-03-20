import React from 'react';
import { motion, AnimatePresence } from 'framer-motion';
import { X, MapPin, Star, Flag, Trophy, ExternalLink } from 'lucide-react';

export interface PlaceType {
  id: string;
  name: string;
  description: string;
}

export interface Image {
  id: string;
  name: string;
  url: string;
  entityId: string;
}

export interface Place {
  id: string;
  name: string;
  placeTypeId: string;
  placeType?: PlaceType;
  location: { lat: number; lng: number };
  address?: string;
  description?: string;
  imageId: string;
  image?: Image;
  creatorId: string;
  createdAt: string;
  updatedAt: string;
  status: string;
  reviewerId?: string;
  // UI-specific fields (calculated/not in DB)
  distance?: string;
  rating?: number;
  tags?: string[];
}

interface PlaceDetailSheetProps {
  place: Place | null;
  onClose: () => void;
  onReport: () => void;
}

export default function PlaceDetailSheet({
  place,
  onClose,
  onReport
}: PlaceDetailSheetProps) {
  if (!place) return null;

  // Get image URL - prioritize place.image.url, fallback to a default
  const imageUrl = place.image?.url || 
    'https://images.unsplash.com/photo-1534438327276-14e5300c3a48?w=800&q=80';

  return (
    <AnimatePresence>
      <>
        {/* Backdrop */}
        <motion.div
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          exit={{ opacity: 0 }}
          onClick={onClose}
          className="absolute inset-0 bg-black/60 backdrop-blur-sm z-40"
        />

        {/* Bottom Sheet */}
        <motion.div
          initial={{ y: '100%' }}
          animate={{ y: 0 }}
          exit={{ y: '100%' }}
          transition={{
            type: 'spring',
            damping: 35,
            stiffness: 300,
            mass: 0.8
          }}
          className="absolute bottom-0 left-0 right-0 bg-[#12121a] rounded-t-3xl border-t border-white/10 z-50 max-h-[85vh] overflow-y-auto"
        >
            {/* Handle */}
            <div className="sticky top-0 bg-[#12121a] z-20 pt-4 pb-2 rounded-t-3xl">
              <div className="w-12 h-1.5 bg-gray-700 rounded-full mx-auto" />
            </div>

            {/* Cover Image */}
            <div className="relative h-48 w-full">
              <img
                src={imageUrl}
                alt={place.name}
                className="w-full h-full object-cover"
              />
              <div className="absolute inset-0 bg-gradient-to-t from-[#12121a] to-transparent" />
              <button
                onClick={onClose}
                className="absolute top-4 right-4 p-2 rounded-full bg-black/40 backdrop-blur-md text-white hover:bg-black/60 transition-colors"
              >
                <X size={20} />
              </button>
              <button
                onClick={onReport}
                className="absolute top-4 left-4 p-2 rounded-full bg-black/40 backdrop-blur-md text-white hover:bg-black/60 transition-colors"
              >
                <Flag size={20} />
              </button>
            </div>

            <div className="px-6 pb-8 -mt-12 relative z-10">
              {/* Header Info */}
              <div className="flex justify-between items-start mb-4">
                <div>
                  <h2 className="text-2xl font-bold text-white mb-1">
                    {place.name}
                  </h2>
                  <div className="flex items-center text-gray-400 text-sm mb-2">
                    <MapPin size={14} className="mr-1 text-blue-400" />
                    <span>
                      {place.distance} •{' '}
                      {place.address || 'Tallinn, Estonia'}
                    </span>
                  </div>
                  <div className="flex flex-wrap gap-2">
                    {place.tags?.map((tag) => (
                      <span
                        key={tag}
                        className="px-2 py-0.5 rounded-md bg-white/5 border border-white/5 text-xs text-gray-300"
                      >
                        {tag}
                      </span>
                    )) || (
                      <span className="px-2 py-0.5 rounded-md bg-white/5 border border-white/5 text-xs text-gray-300">
                        {place.placeType?.name || 'Unknown'}
                      </span>
                    )}
                  </div>
                </div>
                <div className="flex flex-col items-end space-y-2">
                  <div className="flex items-center bg-yellow-500/10 px-2 py-1 rounded-lg border border-yellow-500/20">
                    <Star
                      size={14}
                      className="text-yellow-500 mr-1 fill-yellow-500"
                    />
                    <span className="text-yellow-500 font-bold text-sm">
                      {place.rating || 'N/A'}
                    </span>
                  </div>
                  <button className="flex items-center text-blue-400 text-xs font-medium hover:text-blue-300 transition-colors">
                    Get Directions <ExternalLink size={10} className="ml-1" />
                  </button>
                </div>
              </div>

              {/* Description */}
              <p className="text-gray-400 text-sm leading-relaxed mb-8">
                {place.description ||
                  'A popular spot for locals. Great facilities and friendly community. Open 24/7 for members.'}
              </p>

              {/* Actions Grid */}
              <div className="grid grid-cols-2 gap-3 mb-8">
                <button className="py-3 rounded-xl bg-gradient-to-r from-blue-600 to-cyan-500 text-white font-bold text-sm shadow-[0_0_20px_rgba(59,130,246,0.4)] hover:shadow-[0_0_30px_rgba(59,130,246,0.6)] transition-all active:scale-[0.98]">
                  Check In Now
                </button>
                <button className="py-3 rounded-xl bg-[#12121a] border border-white/10 text-white font-semibold text-sm hover:bg-white/5 transition-colors">
                  Add to Favorites
                </button>
              </div>

              {/* Active Challenges */}
              <div className="mb-8">
                <h3 className="text-sm font-semibold text-gray-400 uppercase tracking-wider mb-3 flex items-center justify-between">
                  Active Challenges
                  <span className="text-blue-400 text-xs cursor-pointer">
                    View All
                  </span>
                </h3>
                <div className="space-y-3">
                  {[1, 2].map((i) => (
                    <div
                      key={i}
                      className="bg-white/5 p-3 rounded-xl border border-white/5 flex items-center hover:bg-white/10 transition-colors cursor-pointer"
                    >
                      <div className="w-10 h-10 rounded-full bg-blue-500/20 flex items-center justify-center mr-3 text-blue-400">
                        <Trophy size={18} />
                      </div>
                      <div className="flex-1">
                        <p className="text-white font-medium text-sm">
                          Morning Sprint {i}
                        </p>
                        <p className="text-gray-500 text-xs">
                          24 participants • Ends in 2h
                        </p>
                      </div>
                      <button className="px-3 py-1 rounded-full bg-blue-500/20 text-blue-400 text-xs font-bold">
                        Join
                      </button>
                    </div>
                  ))}
                </div>
              </div>

              {/* Upcoming Events placeholder */}
              <div className="mb-8">
                <h3 className="text-sm font-semibold text-gray-400 uppercase tracking-wider mb-3">
                  Upcoming Events
                </h3>
                <div className="text-gray-500 text-sm">
                  No upcoming events at this location.
                </div>
              </div>

              {/* Place Feed placeholder */}
              <div className="mb-8">
                <h3 className="text-sm font-semibold text-gray-400 uppercase tracking-wider mb-3">
                  Recent Activity
                </h3>
                <div className="text-gray-500 text-sm">
                  No recent activity at this location.
                </div>
              </div>
            </div>
        </motion.div>
      </>
    </AnimatePresence>
  );
}
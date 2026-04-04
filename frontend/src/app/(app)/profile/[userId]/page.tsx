"use client";

import { useState, useEffect } from "react";
import { useParams } from "next/navigation";
import ImageComponent from "@/components/ImageComponent";
import { imageService } from "@/services/image.service";

export default function UserProfilePage() {
  const params = useParams<{ userId: string }>();
  const [imageId, setImageId] = useState<string | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    imageService.getProfilePictureIdForUser(params.userId).then((id) => {
      setImageId(id);
      setLoading(false);
    });
  }, [params.userId]);

  if (loading) {
    return (
      <div className="flex items-center justify-center h-full">
        <div className="w-32 h-32 rounded-full bg-gray-700 animate-pulse" />
      </div>
    );
  }

  return (
    <div className="flex flex-col items-center gap-6 p-8">
      <h1 className="text-xl font-semibold text-white">Profile</h1>
      <ImageComponent
        imageId={imageId}
        alt="Profile picture"
        size="full"
        showRemove={false}
      />
    </div>
  );
}

"use client";

import { useState, useEffect } from "react";
import ImageComponent from "@/components/ImageComponent";
import { imageService } from "@/services/image.service";

export default function ProfilePage() {
  const [imageId, setImageId] = useState<string | null>(null);
  const [loading, setLoading] = useState(true);
  const [removeError, setRemoveError] = useState<string | null>(null);
  const [removing, setRemoving] = useState(false);

  useEffect(() => {
    imageService.getOwnProfilePictureId().then((id) => {
      setImageId(id);
      setLoading(false);
    });
  }, []);

  const handleUploadSuccess = (newId: string) => {
    setImageId(newId);
    setRemoveError(null);
  };

  const handleRemove = async () => {
    if (removing) return;
    setRemoving(true);
    setRemoveError(null);
    try {
      const result = await imageService.deleteProfilePicture();
      if (result.isSucceed) {
        setImageId(null);
      } else {
        setRemoveError(result.message ?? "Failed to remove profile picture.");
      }
    } finally {
      setRemoving(false);
    }
  };

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
        showRemove={!!imageId}
        onRemove={handleRemove}
        onUploadSuccess={handleUploadSuccess}
      />
      {removeError && (
        <p role="alert" className="text-red-400 text-sm">
          {removeError}
        </p>
      )}
    </div>
  );
}

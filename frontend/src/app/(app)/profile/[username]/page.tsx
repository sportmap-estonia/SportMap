"use client";

import { useState, useEffect } from "react";
import { useParams } from "next/navigation";
import { notFound } from "next/navigation";
import ImageComponent from "@/components/ImageComponent";
import { imageService } from "@/services/image.service";
import { useCurrentUser } from "@/hooks/use-current-user";

export default function ProfilePage() {
  const params = useParams<{ username: string }>();
  const currentUser = useCurrentUser();
  const [imageId, setImageId] = useState<string | null>(null);
  const [loading, setLoading] = useState(true);
  const [userNotFound, setUserNotFound] = useState(false);
  const [removeError, setRemoveError] = useState<string | null>(null);
  const [removing, setRemoving] = useState(false);

  useEffect(() => {
    let cancelled = false;
    imageService.getProfilePictureByUsername(params.username).then((result) => {
      if (cancelled) return;
      if (result === "NOT_FOUND") {
        setUserNotFound(true);
      } else {
        setImageId(result);
      }
      setLoading(false);
    });
    return () => {
      cancelled = true;
    };
  }, [params.username]);

  // Called during render — Next.js catches the thrown error and renders not-found.tsx
  if (userNotFound) notFound();

  const isOwn = currentUser?.username === params.username;

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
      <h1 className="text-xl font-semibold text-white">{params.username}</h1>
      <ImageComponent
        imageId={imageId}
        alt={`${params.username}'s profile picture`}
        size="full"
        showRemove={isOwn && !!imageId}
        onRemove={isOwn ? handleRemove : undefined}
        onUploadSuccess={isOwn ? handleUploadSuccess : undefined}
      />
      {removeError && (
        <p role="alert" className="text-red-400 text-sm">
          {removeError}
        </p>
      )}
    </div>
  );
}

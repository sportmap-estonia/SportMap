"use client";

import { useState, useEffect } from "react";
import BottomNav from "./BottomNav";
import DesktopSidebar from "./DesktopSidebar";

export type Tab = "map" | "feed" | "events" | "profile";

export default function ResponsiveNav() {
  const [isMobile, setIsMobile] = useState(false);

  useEffect(() => {
    const checkMobile = () => {
      setIsMobile(window.innerWidth < 768);
    };

    checkMobile();
    window.addEventListener("resize", checkMobile);
    return () => window.removeEventListener("resize", checkMobile);
  }, []);

  if (isMobile) {
    return <BottomNav />;
  }

  return <DesktopSidebar />;
}

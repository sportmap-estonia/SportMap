import type { Metadata } from "next";
import "./globals.css";
import ResponsiveNav from "@/components/navigation/ResponsiveNav";

export const metadata: Metadata = {
  title: "SportMap",
  description: "The map-first sports social network",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body className="antialiased">
        {/* Responsive Navigation (fixed position) */}
        <ResponsiveNav />
        
        {/* Main Content Area */}
        {/* Mobile: full height minus bottom nav (80px = h-20) */}
        {/* Desktop: full height with left margin for sidebar */}
        <main className="h-[calc(100vh-5rem)] md:h-screen md:ml-16">
          {children}
        </main>
      </body>
    </html>
  );
}

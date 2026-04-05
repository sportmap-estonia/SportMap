import type { Metadata } from "next";
import "./globals.css";

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
        {children}
      </body>
    </html>
  );
}

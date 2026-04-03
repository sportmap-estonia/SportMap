import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  reactCompiler: true,
  output: "standalone",
  images: {
    remotePatterns: [
      { hostname: "**", pathname: "/api/images/**" },
    ],
  },
};

export default nextConfig;

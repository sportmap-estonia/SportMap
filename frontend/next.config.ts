import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  reactCompiler: true,
  output: "standalone",
  images: {
    remotePatterns: [
      { hostname: "**", pathname: "/api/images/**" },
    ],
  },
  async rewrites() {
    const backendUrl =
      process.env.services__server__https__0 ??
      process.env.services__server__http__0 ??
      "http://localhost:5000";
    return [
      {
        source: "/api/:path*",
        destination: `${backendUrl}/:path*`,
      },
    ];
  },
};

export default nextConfig;

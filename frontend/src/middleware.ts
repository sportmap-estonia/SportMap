import { NextRequest, NextResponse } from "next/server";

export function middleware(request: NextRequest) {
  const token = request.cookies.get("access_token");
  const isLoginPage = request.nextUrl.pathname === "/login";

  if (token && isLoginPage) {
    return NextResponse.redirect(new URL("/map", request.url));
  }

  // add the bearer to api requests
  if (token && request.nextUrl.pathname.startsWith("/api/")) {
    const headers = new Headers(request.headers);
    headers.set("Authorization", `Bearer ${token.value}`);
    return NextResponse.next({ request: { headers } });
  }

  return NextResponse.next();
}
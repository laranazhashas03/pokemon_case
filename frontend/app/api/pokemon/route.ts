import { NextRequest, NextResponse } from "next/server";

const API_URL = process.env.NEXT_PUBLIC_API_URL;

export async function GET(request: NextRequest) {
  const searchParams = request.nextUrl.searchParams;

  const response = await fetch(
    `${API_URL}/api/Pokemon?${searchParams.toString()}`
  );

  const data = await response.json();

  return NextResponse.json(data, {
    status: response.status,
  });
}
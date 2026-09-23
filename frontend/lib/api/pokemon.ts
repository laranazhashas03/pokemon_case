const API_URL = process.env.NEXT_PUBLIC_API_URL;

export async function getPokemons(
  limit: number = 20,
  offset: number = 0,
  search?: string
) {
  const params = new URLSearchParams({
    limit: limit.toString(),
    offset: offset.toString(),
  });

  if (search) {
    params.append("search", search);
  }

  const response = await fetch(
    `${API_URL}/api/Pokemon?${params.toString()}`
  );

  if (!response.ok) {
    throw new Error("Failed to fetch Pokemon.");
  }

  return response.json();
}
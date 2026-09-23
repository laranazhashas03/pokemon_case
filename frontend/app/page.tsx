"use client";

import { useEffect, useState } from "react";
import Link from "next/link";

type Pokemon = {
  id: number;
  name: string;
  imageUrl: string;
};

export default function Home() {
  const [pokemons, setPokemons] = useState<Pokemon[]>([]);
  const [search, setSearch] = useState("");
  const [page, setPage] = useState(0);
  const [loading, setLoading] = useState(true);

  const limit = 20;

  useEffect(() => {
    async function loadPokemons() {
      setLoading(true);

      const params = new URLSearchParams({
        limit: limit.toString(),
        offset: (page * limit).toString(),
      });

      if (search.trim()) {
        params.append("search", search.trim());
      }

      const response = await fetch(`/api/pokemon?${params.toString()}`);

      if (!response.ok) {
        throw new Error("Failed to fetch Pokemon.");
      }

      const data = await response.json();

      setPokemons(data);
      setLoading(false);
    }

    loadPokemons();
  }, [page, search]);

  function handleSearchChange(value: string) {
    setSearch(value);
    setPage(0);
  }

  return (
    <main className="min-h-screen bg-gray-100 p-8">
      <h1 className="mb-6 text-4xl font-bold">
        Pokémon
      </h1>

      <input
        type="text"
        placeholder="Search Pokémon..."
        value={search}
        onChange={(e) => handleSearchChange(e.target.value)}
        className="mb-8 w-full max-w-md rounded-lg border bg-white px-4 py-3"
      />

      {loading ? (
        <p>Loading Pokémon...</p>
      ) : (
        <>
          <div className="grid grid-cols-2 gap-4 sm:grid-cols-3 md:grid-cols-4 lg:grid-cols-5">
            {pokemons.map((pokemon) => (
              <Link
              key={pokemon.id}
              href={`/pokemon/${pokemon.id}`}
              className="rounded-xl bg-white p-4 shadow transition hover:scale-105"
            >
              <img
                src={pokemon.imageUrl}
                alt={pokemon.name}
                className="mx-auto h-32 w-32 object-contain"
              />

              <h2 className="mt-3 text-center text-lg font-semibold capitalize">
                {pokemon.name}
              </h2>

              <p className="text-center text-gray-500">
                #{pokemon.id}
              </p>
            </Link>
            ))}
          </div>

          <div className="mt-8 flex justify-center gap-4">
            <button
              onClick={() => setPage((current) => current - 1)}
              disabled={page === 0}
              className="rounded-lg bg-gray-800 px-5 py-2 text-white disabled:opacity-40"
            >
              Previous
            </button>

            <span className="flex items-center px-3">
              Page {page + 1}
            </span>

            <button
              onClick={() => setPage((current) => current + 1)}
              disabled={pokemons.length < limit}
              className="rounded-lg bg-gray-800 px-5 py-2 text-white disabled:opacity-40"
            >
              Next
            </button>
          </div>
        </>
      )}
    </main>
  );
}
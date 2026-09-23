"use client";

import { useEffect, useState } from "react";
import { useParams } from "next/navigation";
import Link from "next/link";

type PokemonDetail = {
  id: number;
  name: string;
  imageUrl: string;
  height: number;
  weight: number;
  baseStatTotal: number;
};

export default function PokemonDetailPage() {
  const params = useParams();
  const id = params.id as string;

  const [pokemon, setPokemon] = useState<PokemonDetail | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    async function loadPokemon() {
      const response = await fetch(`/api/pokemon/${id}`);

      if (!response.ok) {
        throw new Error("Pokemon not found.");
      }

      const data = await response.json();

      setPokemon(data);
      setLoading(false);
    }

    loadPokemon();
  }, [id]);

  if (loading) {
    return (
      <main className="min-h-screen bg-gray-100 p-8">
        <p>Loading Pokémon...</p>
      </main>
    );
  }

  if (!pokemon) {
    return (
      <main className="min-h-screen bg-gray-100 p-8">
        <p>Pokémon not found.</p>
      </main>
    );
  }

  return (
    <main className="min-h-screen bg-gray-100 p-8">
        <Link
        href="/"
        className="mb-4 inline-block rounded-lg bg-gray-800 px-4 py-2 text-white hover:bg-gray-700"
        >
        ← Back to Pokémon
        </Link>
      <div className="mx-auto max-w-2xl rounded-2xl bg-white p-8 shadow">
        <img
          src={pokemon.imageUrl}
          alt={pokemon.name}
          className="mx-auto h-64 w-64 object-contain"
        />

        <h1 className="text-center text-4xl font-bold capitalize">
          {pokemon.name}
        </h1>

        <p className="mt-2 text-center text-gray-500">
          #{pokemon.id}
        </p>

        <div className="mt-8 grid grid-cols-3 gap-4 text-center">
          <div className="rounded-lg bg-gray-100 p-4">
            <p className="text-sm text-gray-500">Height</p>
            <p className="mt-1 text-xl font-semibold">
              {pokemon.height}
            </p>
          </div>

          <div className="rounded-lg bg-gray-100 p-4">
            <p className="text-sm text-gray-500">Weight</p>
            <p className="mt-1 text-xl font-semibold">
              {pokemon.weight}
            </p>
          </div>

          <div className="rounded-lg bg-gray-100 p-4">
            <p className="text-sm text-gray-500">Base Stats</p>
            <p className="mt-1 text-xl font-semibold">
              {pokemon.baseStatTotal}
            </p>
          </div>
        </div>
      </div>
    </main>
  );
}
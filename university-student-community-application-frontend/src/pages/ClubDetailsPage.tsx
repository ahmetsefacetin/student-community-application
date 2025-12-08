import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import {clubService} from "../services/clubService";
import type { ClubResponseDto } from "../types/club";

const ClubDetailsPage = () => {
  const { id } = useParams<{ id: string }>();
  const [club, setClub] = useState<ClubResponseDto | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchClub = async () => {
      if (!id) return;
      try {
        const data = await clubService.getClubById(Number(id));
        setClub(data);
      } catch (err) {
        console.error(err);
        setError("Failed to load club details.");
      } finally {
        setLoading(false);
      }
    };

    fetchClub();
  }, [id]);

  if (loading) return <div>Loading...</div>;
  if (error) return <div className="text-red-500">{error}</div>;
  if (!club) return <div>Club not found.</div>;

  return (
    <div className="container mx-auto p-4">
      <h1 className="text-3xl font-bold mb-4">{club.name}</h1>
      <div className="bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4">
        <p className="text-gray-700 text-base mb-4">
          <strong>Description:</strong> {club.description || "No description available."}
        </p>
        <p className="text-gray-600 text-sm">
          <strong>Manager:</strong> {club.managerFullName}
        </p>
      </div>
    </div>
  );
};

export default ClubDetailsPage;
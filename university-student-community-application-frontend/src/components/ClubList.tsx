import { useEffect, useState } from "react";
import { clubService } from "../services/clubService";
import type { ClubResponseDto, UserClubRoleDto } from "../types/club";
import { useAuth } from "../hooks/useAuth";
import ClubCard from "./ClubCard";

const ClubList = () => {
  const { isAuthenticated } = useAuth();
  const [clubs, setClubs] = useState<ClubResponseDto[]>([]);
  const [roles, setRoles] = useState<Record<number, string>>({});
  const [loading, setLoading] = useState(true);

  const [error, setError] = useState("");

  const handleJoin = async (clubId: number) => {
    try {
      await clubService.joinClub(clubId);
      setRoles((prev) => ({ ...prev, [clubId]: "Member" }));
    } catch {
      setError("Kulübe katılırken bir hata oluştu.");
    }
  };

  const handleLeave = async (clubId: number) => {
    try {
      await clubService.leaveClub(clubId);
      setRoles((prev) => ({ ...prev, [clubId]: "None" }));
    } catch {
      setError("Kulüpten ayrılırken bir hata oluştu.");
    }
  };

  useEffect(() => {
    const load = async () => {
      try {
        const data = await clubService.getAllClubs();
        setClubs(data);

        if (isAuthenticated) {
          const map: Record<number, string> = {};
          for (const c of data) {
            try {
              const r: UserClubRoleDto = await clubService.getUserClubRole(c.id);
              map[c.id] = r.clubRole;
            } catch {
              map[c.id] = "None";
            }
          }
          setRoles(map);
        }
      } catch (err) {
        setError("Kulüp listesi alınırken bir hata oluştu.");
      } finally {
        setLoading(false);
      }
    };

    load();
  }, [isAuthenticated]);

  if (loading) return <p>Kulüpler yükleniyor...</p>;
  if (error) return <p style={{ color: "red" }}>{error}</p>;

  return (
    <div>
      {clubs.map((club) => (
        <ClubCard
          key={club.id}
          club={club}
          role={isAuthenticated ? roles[club.id] : undefined}
          onJoin={isAuthenticated ? handleJoin : undefined}
          onLeave={isAuthenticated ? handleLeave : undefined}
        />
      ))}
    </div>
  );
};

export default ClubList;

import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { clubService } from "../services/clubService";
import type { ClubResponseDto, ClubMemberDto } from "../types/club";
import { ClubRole } from "../types/club";

const ClubDetailsPage = () => {
  const { id } = useParams<{ id: string }>();
  const [club, setClub] = useState<ClubResponseDto | null>(null);
  const [members, setMembers] = useState<ClubMemberDto[]>([]);
  const [role, setRole] = useState<string>(""); // Manager / Officer / Member
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    if (!id) return;

    const load = async () => {
      try {
        const [clubData, roleData, membersData] = await Promise.all([
          clubService.getClubById(Number(id)),
          clubService.getUserClubRole(Number(id)),
          clubService.getMembers(Number(id)),
        ]);

        setClub(clubData);
        setRole(roleData.clubRole);
        setMembers(membersData);
      } finally {
        setLoading(false);
      }
    };

    load();
  }, [id]);

  if (loading) return <div>Loading...</div>;
  if (!club) return <div>Club not found.</div>;

  const officers = members.filter(m => m.role === ClubRole.Officer);
  const generalMembers = members.filter(m => m.role === ClubRole.Member);

  const makeOfficer = async (userId: string) => {
    await clubService.makeOfficer(Number(id), userId);
    const updated = await clubService.getMembers(Number(id));
    setMembers(updated);
  };

  const demoteOfficer = async (userId: string) => {
    await clubService.demoteOfficer(Number(id), userId);
    const updated = await clubService.getMembers(Number(id));
    setMembers(updated);
  };

  return (
    <div className="container mx-auto p-4">

      {/* CLUB INFO */}
      <h1 className="text-3xl font-bold mb-4">{club.name}</h1>

      <div className="bg-white shadow-md rounded p-6 mb-6">
        <p><strong>Description:</strong> {club.description}</p>
        <p><strong>Manager:</strong> {club.managerFullName}</p>

        {/* User role badge */}
        {role && (
          <div className="mt-3 inline-block px-3 py-1 bg-blue-100 text-blue-700 rounded">
            {role && (
                    <p style={{
                        fontStyle: "italic",
                        fontSize: "0.97rem",
                        color: role === "None" ? "#e53e3e" : "#38a169",
                        marginTop: "0.25rem"
                    }}>
                        {role === "None"
                            ? "Bu kulübe üye değilsiniz"
                            : <>Rolünüz: <strong>{role}</strong></>
                        }
                    </p>
                )}
          </div>
        )}

        {/* Update Club Button - Only for Manager and Officer */}
        {(role === "Manager" || role === "Officer") && (
          <div className="mt-4">
            <button
              onClick={() => navigate(`/clubs/${id}/edit`)}
              className="px-4 py-2 bg-blue-600 text-white rounded hover:bg-blue-700"
            >
              Kulüp Bilgilerini Güncelle
            </button>
          </div>
        )}
      </div>

      {/* MANAGER PANEL */}
      {role === "Manager" && (
        <div className="bg-white shadow-md rounded p-6">
          <h2 className="text-2xl font-semibold mb-4">Yönetici Paneli</h2>

          {/* OFFICERS LIST */}
          <h3 className="text-xl font-bold mb-2">Yetkililer</h3>
          {officers.length === 0 && <p>Yetkili yok.</p>}
          {officers.map(off => (
            <div key={off.userId} className="flex justify-between mb-2">
              <span>{off.fullName}</span>
              <button
                onClick={() => demoteOfficer(off.userId)}
                className="px-3 py-1 bg-red-500 text-white rounded"
              >
                Görevden Al
              </button>
            </div>
          ))}

          {/* MEMBERS LIST */}
          <h3 className="text-xl font-bold mt-6 mb-2">Üyeler</h3>
          {generalMembers.length === 0 && <p>Üye yok.</p>}
          {generalMembers.map(m => (
            <div key={m.userId} className="flex justify-between mb-2">
              <span>{m.fullName}</span>
              <button
                onClick={() => makeOfficer(m.userId)}
                className="px-3 py-1 bg-green-600 text-white rounded"
              >
                Yetkili Yap
              </button>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

export default ClubDetailsPage;

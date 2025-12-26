import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { clubService } from "../services/clubService";
import type { ClubResponseDto, ClubMemberDto } from "../types/club";
import { ClubRole } from "../types/club";
import { useAuth } from "../hooks/useAuth";
import { eventService } from "../services/eventService";
import type { ClubEvent, CreateClubEventDto } from "../types/event";

const ClubDetailsPage = () => {
  const { id } = useParams<{ id: string }>();
  const { isAuthenticated } = useAuth();
  const [club, setClub] = useState<ClubResponseDto | null>(null);
  const [members, setMembers] = useState<ClubMemberDto[]>([]);
  const [events, setEvents] = useState<ClubEvent[]>([]);
  const [eventForm, setEventForm] = useState<CreateClubEventDto>({
    title: "",
    description: "",
    location: "",
    startTime: "",
    endTime: "",
  });
  const [eventError, setEventError] = useState("");
  const [role, setRole] = useState<string>("");
  const [loading, setLoading] = useState(true);
  const [actionLoading, setActionLoading] = useState(false);
  const [actionError, setActionError] = useState("");
  const navigate = useNavigate();

  useEffect(() => {
    if (!id) return;

    const load = async () => {
      try {
        const [clubData, membersData, eventData] = await Promise.all([
          clubService.getClubById(Number(id)),
          clubService.getMembers(Number(id)),
          eventService.list(Number(id)),
        ]);

        setClub(clubData);
        setMembers(membersData);
        setEvents(eventData);

        if (isAuthenticated) {
          try {
            const roleData = await clubService.getUserClubRole(Number(id));
            setRole(roleData.clubRole);
          } catch {
            setRole("None");
          }
        } else {
          setRole("None");
        }
      } finally {
        setLoading(false);
      }
    };

    load();
  }, [id, isAuthenticated]);

  if (loading) return <div>Loading...</div>;
  if (!club) return <div>Club not found.</div>;

  const officers = members.filter((m) => m.role === ClubRole.Officer);
  const generalMembers = members.filter((m) => m.role === ClubRole.Member);
  const managerMembers = members.filter((m) => m.role === ClubRole.Manager);
  const roleLabels: Record<number, string> = {
    [ClubRole.Manager]: "Manager",
    [ClubRole.Officer]: "Officer",
    [ClubRole.Member]: "Member",
  };

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

  const joinClub = async () => {
    if (!id) return;
    if (!isAuthenticated) {
      navigate("/login", { state: { from: `/clubs/${id}` } });
      return;
    }
    setActionError("");
    try {
      setActionLoading(true);
      await clubService.joinClub(Number(id));
      setRole("Member");
      const updated = await clubService.getMembers(Number(id));
      setMembers(updated);
    } catch (err: any) {
      setActionError(err?.response?.data?.message ?? "Katilim sirasinda bir hata olustu.");
    } finally {
      setActionLoading(false);
    }
  };

  const leaveClub = async () => {
    if (!id) return;
    if (!isAuthenticated) {
      navigate("/login", { state: { from: `/clubs/${id}` } });
      return;
    }
    setActionError("");
    try {
      setActionLoading(true);
      await clubService.leaveClub(Number(id));
      setRole("None");
      const updated = await clubService.getMembers(Number(id));
      setMembers(updated);
    } catch (err: any) {
      setActionError(err?.response?.data?.message ?? "Ayrilma sirasinda bir hata olustu.");
    } finally {
      setActionLoading(false);
    }
  };

  const formatDate = (iso: string) => {
    try {
      return new Date(iso).toLocaleString();
    } catch {
      return iso;
    }
  };

  const createEvent = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!id) return;
    setEventError("");
    try {
      await eventService.create(Number(id), eventForm);
      const updated = await eventService.list(Number(id));
      setEvents(updated);
      setEventForm({
        title: "",
        description: "",
        location: "",
        startTime: "",
        endTime: "",
      });
    } catch (err: any) {
      setEventError(err?.response?.data?.message ?? "Etkinlik olusturulurken bir hata olustu.");
    }
  };

  return (
    <div
      style={{
        maxWidth: "1100px",
        margin: "0 auto",
        padding: "1.5rem",
      }}
    >
      <h1 style={{ fontSize: "2rem", fontWeight: 700, marginBottom: "1.25rem" }}>
        {club.name}
      </h1>

      <div
        style={{
          display: "grid",
          gridTemplateColumns: "320px 1fr",
          gap: "1.25rem",
          alignItems: "start",
        }}
      >
        {/* Members column */}
        <div
          style={{
            background: "#fff",
            borderRadius: "12px",
            boxShadow: "0 2px 10px rgba(0,0,0,0.08)",
            padding: "1rem",
          }}
        >
          <h3 style={{ fontSize: "1.1rem", fontWeight: 700, marginBottom: "0.75rem" }}>Uyeler</h3>
          {members.length === 0 && <p>Henuz uye yok.</p>}
          <div style={{ display: "flex", flexDirection: "column", gap: "0.6rem" }}>
            {managerMembers.map((m) => (
              <div
                key={m.userId}
                style={{
                  display: "flex",
                  justifyContent: "space-between",
                  alignItems: "center",
                  border: "1px solid #e2e8f0",
                  padding: "0.6rem 0.75rem",
                  borderRadius: "10px",
                }}
              >
                <span>{m.fullName || "Yonetici"}</span>
                <span style={{
                  fontSize: "0.8rem",
                  padding: "0.15rem 0.55rem",
                  borderRadius: "999px",
                  background: "#ebf8ff",
                  color: "#2b6cb0",
                  fontWeight: 700,
                }}>
                  {roleLabels[m.role]}
                </span>
              </div>
            ))}
            {officers.map((m) => (
              <div
                key={m.userId}
                style={{
                  display: "flex",
                  justifyContent: "space-between",
                  alignItems: "center",
                  border: "1px solid #e2e8f0",
                  padding: "0.6rem 0.75rem",
                  borderRadius: "10px",
                }}
              >
                <span>{m.fullName}</span>
                <span style={{
                  fontSize: "0.8rem",
                  padding: "0.15rem 0.55rem",
                  borderRadius: "999px",
                  background: "#f3e8ff",
                  color: "#6b46c1",
                  fontWeight: 700,
                }}>
                  {roleLabels[m.role]}
                </span>
              </div>
            ))}
            {generalMembers.map((m) => (
              <div
                key={m.userId}
                style={{
                  display: "flex",
                  justifyContent: "space-between",
                  alignItems: "center",
                  border: "1px solid #e2e8f0",
                  padding: "0.6rem 0.75rem",
                  borderRadius: "10px",
                }}
              >
                <span>{m.fullName}</span>
                <span style={{
                  fontSize: "0.8rem",
                  padding: "0.15rem 0.55rem",
                  borderRadius: "999px",
                  background: "#edf2f7",
                  color: "#4a5568",
                  fontWeight: 700,
                }}>
                  {roleLabels[m.role]}
                </span>
              </div>
            ))}
          </div>
        </div>

        {/* Right column: info + manager panel */}
        <div style={{ display: "flex", flexDirection: "column", gap: "1rem" }}>
          <div
            style={{
              background: "#fff",
              borderRadius: "12px",
              boxShadow: "0 2px 10px rgba(0,0,0,0.08)",
              padding: "1.25rem",
            }}
          >
            <p><strong>Description:</strong> {club.description}</p>
            <p><strong>Manager:</strong> {club.managerFullName}</p>

            {role && (
              <div style={{
                marginTop: "0.75rem",
                display: "inline-block",
                padding: "0.35rem 0.65rem",
                borderRadius: "8px",
                background: "#ebf8ff",
                color: "#2b6cb0",
                fontStyle: "italic",
              }}>
                {role === "None" ? "Bu kulube uye degilsiniz" : <>Rolunuz: <strong>{role}</strong></>}
              </div>
            )}

            {actionError && (
              <p style={{ color: "#e53e3e", marginTop: "0.35rem", fontSize: "0.9rem" }}>
                {actionError}
              </p>
            )}

            <div style={{ display: "flex", gap: "0.75rem", marginTop: "1rem" }}>
              {role === "None" && (
                <button
                  disabled={actionLoading}
                  onClick={joinClub}
                  style={{ background: "#38a169" }}
                >
                  {actionLoading ? "Katiliyor..." : "Katil"}
                </button>
              )}
              {(role === "Member" || role === "Officer") && (
                <button
                  disabled={actionLoading}
                  onClick={leaveClub}
                  style={{ background: "#e53e3e" }}
                >
                  {actionLoading ? "Ayriliyor..." : "Ayril"}
                </button>
              )}
            </div>

            {(role === "Manager" || role === "Officer") && (
              <div style={{ marginTop: "1rem" }}>
                <button onClick={() => navigate(`/clubs/${id}/edit`)}>
                  Kulup Bilgilerini Guncelle
                </button>
              </div>
            )}
          </div>

          <div
            style={{
              background: "#fff",
              borderRadius: "12px",
              boxShadow: "0 2px 10px rgba(0,0,0,0.08)",
              padding: "1.25rem",
            }}
          >
            <h3 style={{ fontSize: "1.1rem", fontWeight: 700, marginBottom: "0.75rem" }}>
              Etkinlikler
            </h3>
            {events.length === 0 && <p>Etkinlik bulunmuyor.</p>}
            <div style={{ display: "flex", flexDirection: "column", gap: "0.6rem" }}>
              {events.map((ev) => (
                <div
                  key={ev.id}
                  style={{
                    border: "1px solid #e2e8f0",
                    borderRadius: "10px",
                    padding: "0.75rem",
                  }}
                >
                  <div style={{ fontWeight: 700 }}>{ev.title}</div>
                  {ev.description && (
                    <div style={{ marginTop: "0.25rem", color: "#4a5568", fontSize: "0.95rem" }}>
                      {ev.description}
                    </div>
                  )}
                  <div style={{ marginTop: "0.35rem", fontSize: "0.9rem", color: "#2d3748" }}>
                    {formatDate(ev.startTime)} - {formatDate(ev.endTime)}
                  </div>
                  {ev.location && (
                    <div style={{ fontSize: "0.9rem", color: "#2d3748" }}>
                      Lokasyon: {ev.location}
                    </div>
                  )}
                </div>
              ))}
            </div>

            {(role === "Manager" || role === "Officer") && (
              <form onSubmit={createEvent} style={{ marginTop: "1rem", display: "grid", gap: "0.75rem" }}>
                <h4 style={{ fontWeight: 700 }}>Yeni Etkinlik</h4>
                {eventError && <p style={{ color: "#e53e3e", fontSize: "0.9rem" }}>{eventError}</p>}
                <div>
                  <label>Baslik</label>
                  <input
                    value={eventForm.title}
                    onChange={(e) => setEventForm({ ...eventForm, title: e.target.value })}
                    required
                  />
                </div>
                <div>
                  <label>Aciklama</label>
                  <textarea
                    value={eventForm.description ?? ""}
                    onChange={(e) => setEventForm({ ...eventForm, description: e.target.value })}
                    rows={2}
                  />
                </div>
                <div>
                  <label>Lokasyon</label>
                  <input
                    value={eventForm.location ?? ""}
                    onChange={(e) => setEventForm({ ...eventForm, location: e.target.value })}
                  />
                </div>
                <div style={{ display: "grid", gap: "0.5rem", gridTemplateColumns: "repeat(auto-fit, minmax(240px, 1fr))" }}>
                  <div>
                    <label>Baslangic</label>
                    <input
                      type="datetime-local"
                      value={eventForm.startTime}
                      onChange={(e) => setEventForm({ ...eventForm, startTime: e.target.value })}
                      required
                    />
                  </div>
                  <div>
                    <label>Bitis</label>
                    <input
                      type="datetime-local"
                      value={eventForm.endTime}
                      onChange={(e) => setEventForm({ ...eventForm, endTime: e.target.value })}
                      required
                    />
                  </div>
                </div>
                <div>
                  <button type="submit">Etkinlik Olustur</button>
                </div>
              </form>
            )}
          </div>

          {role === "Manager" && (
            <div
              style={{
                background: "#fff",
                borderRadius: "12px",
                boxShadow: "0 2px 10px rgba(0,0,0,0.08)",
                padding: "1.25rem",
              }}
            >
              <h2 style={{ fontSize: "1.15rem", fontWeight: 700, marginBottom: "0.75rem" }}>
                Yonetici Paneli
              </h2>

              <h3 style={{ fontWeight: 700, marginBottom: "0.5rem" }}>Yetkililer</h3>
              {officers.length === 0 && <p>Yetkili yok.</p>}
              {officers.map((off) => (
                <div key={off.userId} style={{ display: "flex", justifyContent: "space-between", marginBottom: "0.5rem" }}>
                  <span>{off.fullName}</span>
                  <button
                    onClick={() => demoteOfficer(off.userId)}
                    style={{ background: "#e53e3e" }}
                  >
                    Gorevden Al
                  </button>
                </div>
              ))}

              <h3 style={{ fontWeight: 700, marginTop: "0.75rem", marginBottom: "0.5rem" }}>Uyeler</h3>
              {generalMembers.length === 0 && <p>Uye yok.</p>}
              {generalMembers.map((m) => (
                <div key={m.userId} style={{ display: "flex", justifyContent: "space-between", marginBottom: "0.5rem" }}>
                  <span>{m.fullName}</span>
                  <button
                    onClick={() => makeOfficer(m.userId)}
                    style={{ background: "#38a169" }}
                  >
                    Yetkili Yap
                  </button>
                </div>
              ))}
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

export default ClubDetailsPage;

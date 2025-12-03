import { useEffect, useState, type FormEvent } from "react";
import { useParams } from "react-router-dom";
import { clubService } from "../services/clubService";
import { useAuth } from "../hooks/useAuth";

import type { ClubResponseDto, UpdateClubDto, UserClubRoleDto } from "../types/club";

const UpdateClubPage = () => {
  const { id } = useParams();
  const clubId = Number(id);

  const { user } = useAuth();

  const [club, setClub] = useState<ClubResponseDto | null>(null);
  const [clubRole, setClubRole] = useState<string>("Loading"); // Manager / Officer / Member / None
  const [loading, setLoading] = useState(true);
  const [loadingRole, setLoadingRole] = useState(true);
  const [saving, setSaving] = useState(false);

  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");

  const [accessDenied, setAccessDenied] = useState(false);

  const [form, setForm] = useState<UpdateClubDto>({
    name: "",
    description: "",
  });

  // 1) Kulüp bilgilerini çek
  useEffect(() => {
    const fetchClub = async () => {
      try {
        const data = await clubService.getClubById(clubId);
        setClub(data);

        setForm({
          name: data.name,
          description: data.description ?? "",
        });
      } catch (err) {
        console.error(err);
        setError("Kulüp bilgisi alınırken bir hata oluştu.");
      } finally {
        setLoading(false);
      }
    };

    fetchClub();
  }, [clubId]);

  // 2) Kullanıcının bu kulüpteki rolünü çek
  useEffect(() => {
    const fetchRole = async () => {
      try {
        const data: UserClubRoleDto = await clubService.getUserClubRole(clubId);
        setClubRole(data.clubRole);
      } catch (err) {
        console.error(err);
        setClubRole("None");
      } finally {
        setLoadingRole(false);
      }
    };

    fetchRole();
  }, [clubId]);

  // 3) Yetki kontrolü (Admin → her zaman serbest)
  useEffect(() => {
    if (!user) return; // Auth çözülmemiş olabilir
    if (loadingRole) return;

    const isAdmin = user.roles.includes("Admin");
    const isManager = clubRole === "Manager";
    const isOfficer = clubRole === "Officer";

    if (isAdmin || isManager || isOfficer) {
      setAccessDenied(false);
    } else {
      setAccessDenied(true);
    }
  }, [user, clubRole, loadingRole]);

  // Form input değişimi
  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    const { name, value } = e.target;
    setForm((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  // Form submit
  const handleSubmit = async (e: FormEvent) => {
    e.preventDefault();
    setError("");
    setSuccess("");

    try {
      setSaving(true);
      await clubService.updateClub(clubId, form);
      setSuccess("Kulüp bilgileri başarıyla güncellendi.");
    } catch (err: any) {
      console.error(err);
      const apiMessage =
        err?.response?.data?.message || "Güncelleme sırasında bir hata oluştu.";
      setError(apiMessage);
    } finally {
      setSaving(false);
    }
  };

  // Loading states
  if (loading || loadingRole) {
    return <p style={{ marginTop: "100px" }}>Yükleniyor...</p>;
  }

  // Access control
  if (accessDenied) {
    return (
      <p style={{ marginTop: "100px", color: "red", fontSize: "1.2rem" }}>
        Bu kulübü düzenleme yetkiniz yok.
      </p>
    );
  }

  if (!club) {
    return <p style={{ marginTop: "100px" }}>Kulüp bulunamadı.</p>;
  }

  return (
    <div style={{ maxWidth: "600px", margin: "100px auto" }}>
      <h1>Kulüp Bilgilerini Düzenle</h1>

      {error && (
        <p style={{ color: "red", marginTop: "1rem" }}>{error}</p>
      )}
      {success && (
        <p style={{ color: "green", marginTop: "1rem" }}>{success}</p>
      )}

      <form
        onSubmit={handleSubmit}
        style={{
          marginTop: "1.5rem",
          display: "flex",
          flexDirection: "column",
          gap: "1rem",
        }}
      >
        <div>
          <label style={{ display: "block", marginBottom: "0.25rem" }}>
            Kulüp Adı
          </label>
          <input
            type="text"
            value={form.name}
            name="name"
            onChange={handleChange}
            style={{ width: "100%", padding: "0.5rem" }}
          />
        </div>

        <div>
          <label style={{ display: "block", marginBottom: "0.25rem" }}>
            Açıklama
          </label>
          <textarea
            value={form.description ?? ""}
            name="description"
            onChange={handleChange}
            style={{ width: "100%", padding: "0.5rem", minHeight: "90px" }}
          />
        </div>

        <button
          type="submit"
          disabled={saving}
          style={{
            padding: "0.75rem",
            cursor: saving ? "not-allowed" : "pointer",
          }}
        >
          {saving ? "Kaydediliyor..." : "Kaydet"}
        </button>
      </form>
    </div>
  );
};

export default UpdateClubPage;

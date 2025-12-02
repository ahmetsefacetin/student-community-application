import { type FormEvent, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { userService } from "../services/userService";
import { clubService } from "../services/clubService";
import type { UserResponse } from "../types/user";
import type { CreateClubDto, ClubResponseDto } from "../types/club";

const AdminCreateClubPage = () => {
  const [users, setUsers] = useState<UserResponse[]>([]);
  const [form, setForm] = useState<CreateClubDto>({
    name: "",
    description: "",
    managerUserId: "",
  });

  const [loadingUsers, setLoadingUsers] = useState<boolean>(true);
  const [submitting, setSubmitting] = useState<boolean>(false);
  const [error, setError] = useState<string>("");
  const [success, setSuccess] = useState<string>("");
  const navigate = useNavigate();

  // Sayfa açıldığında tüm kullanıcıları çek
  useEffect(() => {
    const fetchUsers = async () => {
      try {
        const data = await userService.getAllUsers();
        setUsers(data);
      } catch (err) {
        console.error(err);
        setError("Kullanıcı listesi alınırken bir hata oluştu.");
      } finally {
        setLoadingUsers(false);
      }
    };

    fetchUsers();
  }, []);

  const handleInputChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>
  ) => {
    const { name, value } = e.target;

    setForm((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleSubmit = async (e: FormEvent) => {
    e.preventDefault();
    setError("");
    setSuccess("");

    if (!form.name.trim()) {
      setError("Kulüp adı zorunludur.");
      return;
    }

    if (!form.managerUserId) {
      setError("Kulüp yöneticisi seçmelisiniz.");
      return;
    }

    try {
      setSubmitting(true);

      const createdClub: ClubResponseDto = await clubService.createClub(form);

      setSuccess(`Kulüp başarıyla oluşturuldu: ${createdClub.name}`);
      // İstersen formu sıfırlayabilirsin:
      setForm({
        name: "",
        description: "",
        managerUserId: "",
      });

      // İstersen kısa bir süre sonra başka sayfaya yönlendirebilirsin:
      // setTimeout(() => navigate("/"), 1500);
    } catch (err: any) {
      console.error(err);

      // Eğer backend senin ErrorDetails formatında dönüyorsa:
      const apiMessage =
        err?.response?.data?.message || "Kulüp oluşturulurken bir hata oluştu.";
      setError(apiMessage);
    } finally {
      setSubmitting(false);
    }
  };

  if (loadingUsers) {
    return <p>Yöneticiyi seçmek için kullanıcılar yükleniyor...</p>;
  }

  return (
    <div style={{ maxWidth: "600px", margin: "100px auto 0" }}>
      <h1>Yeni Kulüp Oluştur</h1>

      {error && (
        <p style={{ color: "red", marginTop: "1rem" }}>
          {error}
        </p>
      )}

      {success && (
        <p style={{ color: "green", marginTop: "1rem" }}>
          {success}
        </p>
      )}

      <form onSubmit={handleSubmit} style={{ marginTop: "1.5rem", display: "flex", flexDirection: "column", gap: "1rem" }}>
        {/* Kulüp Adı */}
        <div>
          <label htmlFor="name" style={{ display: "block", marginBottom: "0.25rem" }}>
            Kulüp Adı
          </label>
          <input
            id="name"
            name="name"
            type="text"
            value={form.name}
            onChange={handleInputChange}
            style={{ width: "100%", padding: "0.5rem" }}
            placeholder="Örn: Yazılım ve Teknoloji Kulübü"
            required
          />
        </div>

        {/* Açıklama */}
        <div>
          <label htmlFor="description" style={{ display: "block", marginBottom: "0.25rem" }}>
            Açıklama (opsiyonel)
          </label>
          <textarea
            id="description"
            name="description"
            value={form.description ?? ""}
            onChange={handleInputChange}
            style={{ width: "100%", padding: "0.5rem", minHeight: "80px" }}
            placeholder="Kulüp hakkında kısa bir açıklama yazın..."
          />
        </div>

        {/* Yönetici Seçimi */}
        <div>
          <label htmlFor="managerUserId" style={{ display: "block", marginBottom: "0.25rem" }}>
            Kulüp Yöneticisi
          </label>
          <select
            id="managerUserId"
            name="managerUserId"
            value={form.managerUserId}
            onChange={handleInputChange}
            style={{ width: "100%", padding: "0.5rem" }}
            required
          >
            <option value="">Yönetici seçin...</option>
            {users.map((user) => (
              <option key={user.id} value={user.id}>
                {user.fullName || user.userName || user.email}
              </option>
            ))}
          </select>
        </div>

        {/* Submit */}
        <button
          type="submit"
          disabled={submitting}
          style={{
            padding: "0.75rem",
            marginTop: "0.5rem",
            cursor: submitting ? "not-allowed" : "pointer",
          }}
        >
          {submitting ? "Oluşturuluyor..." : "Kulübü Oluştur"}
        </button>
      </form>
    </div>
  );
};

export default AdminCreateClubPage;

import { type FC } from "react";
import type { ClubResponseDto } from "../types/club";
import { useNavigate } from "react-router-dom";

interface Props {
    club: ClubResponseDto;
    role?: string; // Manager | Officer | Member | None
    onJoin?: (clubId: number) => void | Promise<void>;
    onLeave?: (clubId: number) => void | Promise<void>;
}

const ClubCard: FC<Props> = ({ club, role, onJoin, onLeave }) => {
    const navigate = useNavigate();

    const renderAction = () => {
        if (!role) return null; // anonymous view
        if (role === "Manager") return null;

        if (role === "None") {
            return (
                <button
                    onClick={() => onJoin?.(club.id)}
                    style={{
                        padding: "0.6rem 1.4rem",
                        background: "#38a169",
                        color: "#fff",
                        border: "none",
                        borderRadius: "6px",
                        fontWeight: 600,
                        fontSize: "1rem",
                        cursor: "pointer",
                        boxShadow: "0 1px 4px rgba(56,161,105,0.15)",
                        transition: "background 0.2s",
                    }}
                    onMouseOver={e => (e.currentTarget.style.background = "#2f855a")}
                    onMouseOut={e => (e.currentTarget.style.background = "#38a169")}
                >
                    Katıl
                </button>
            );
        }

        return (
            <button
                onClick={() => onLeave?.(club.id)}
                style={{
                    padding: "0.6rem 1.4rem",
                    background: "#e53e3e",
                    color: "#fff",
                    border: "none",
                    borderRadius: "6px",
                    fontWeight: 600,
                    fontSize: "1rem",
                    cursor: "pointer",
                    boxShadow: "0 1px 4px rgba(229,62,62,0.15)",
                    transition: "background 0.2s",
                }}
                onMouseOver={e => (e.currentTarget.style.background = "#c53030")}
                onMouseOut={e => (e.currentTarget.style.background = "#e53e3e")}
            >
                Ayrıl
            </button>
        );
    };

    return (
        <div
            style={{
                padding: "1.5rem",
                border: "1px solid #e0e0e0",
                borderRadius: "12px",
                marginTop: "1.5rem",
                display: "flex",
                justifyContent: "space-between",
                alignItems: "center",
                boxShadow: "0 2px 8px rgba(0,0,0,0.07)",
                background: "#fff",
                transition: "box-shadow 0.2s",
                gap: "2rem",
            }}
        >
            <div style={{ flex: 1 }}>
                <h3 style={{
                    marginBottom: "0.5rem",
                    fontSize: "1.35rem",
                    color: "#2d3748",
                    fontWeight: 700,
                    letterSpacing: "0.5px"
                }}>
                    {club.name}
                </h3>

                <p style={{
                    marginBottom: "0.5rem",
                    color: "#4a5568",
                    fontSize: "1rem"
                }}>
                    {club.description || "Açıklama eklenmemiş"}
                </p>

                <p style={{
                    marginBottom: "0.5rem",
                    fontSize: "0.98rem",
                    color: "#718096"
                }}>
                    Yönetici: <strong style={{ color: "#2b6cb0" }}>{club.managerFullName}</strong>
                </p>

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

            <div style={{ display: "flex", gap: "0.75rem", alignItems: "center" }}>
                {renderAction()}
                <button
                    onClick={() => navigate(`/clubs/${club.id}`)}
                    style={{
                        padding: "0.6rem 1.4rem",
                        background: "#3182ce",
                        color: "#fff",
                        border: "none",
                        borderRadius: "6px",
                        fontWeight: 600,
                        fontSize: "1rem",
                        cursor: "pointer",
                        boxShadow: "0 1px 4px rgba(49,130,206,0.12)",
                        transition: "background 0.2s",
                    }}
                    onMouseOver={e => (e.currentTarget.style.background = "#2b6cb0")}
                    onMouseOut={e => (e.currentTarget.style.background = "#3182ce")}
                >
                    Detay
                </button>
            </div>
        </div>
    );
};

export default ClubCard;

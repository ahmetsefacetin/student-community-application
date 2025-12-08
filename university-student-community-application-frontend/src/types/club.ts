export interface CreateClubDto {
  name: string;
  description?: string | null;
  managerUserId: string;
}

export interface ClubResponseDto {
  id: number;
  name: string;
  description?: string | null;
  managerId: string;
  managerFullName: string;
}

export interface ClubMemberDto {
  userId: string;
  fullName: string;
  role: number; // "Manager" | "Officer" | "Member"
}

export interface UpdateClubDto {
  name: string;
  description?: string | null;
}

export interface UserClubRoleDto {
  clubRole: string; // Manager | Officer | Member | None
}

export const ClubRole = {
  Member: 1,
  Officer: 2,
  Manager: 3
}
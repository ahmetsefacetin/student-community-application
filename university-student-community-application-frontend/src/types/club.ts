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
  role: string; // "Manager" | "Officer" | "Member"
}
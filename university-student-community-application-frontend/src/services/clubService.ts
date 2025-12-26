import apiClient from "./apiClient";
import type { CreateClubDto, ClubResponseDto, UpdateClubDto, UserClubRoleDto, ClubMemberDto } from "../types/club";

export const clubService = {
  async createClub(dto: CreateClubDto): Promise<ClubResponseDto> {
    const response = await apiClient.post<ClubResponseDto>("/Club", dto);
    return response.data;
  },

  async updateClub(id: number, dto: UpdateClubDto): Promise<void> {
    await apiClient.put(`/Club/${id}`, dto);
  },

  async getClubById(id: number): Promise<ClubResponseDto> {
    const response = await apiClient.get<ClubResponseDto>(`/Club/${id}`);
    return response.data;
  },

  async getUserClubRole(clubId: number): Promise<UserClubRoleDto> {
    const response = await apiClient.get<UserClubRoleDto>(`/ClubRoles/${clubId}/membership`);
    return response.data;
  },

  async getAllClubs(): Promise<ClubResponseDto[]> {
    const response = await apiClient.get<ClubResponseDto[]>("/Club");
    return response.data;
  },

  async getMembers(clubId: number): Promise<ClubMemberDto[]> {
    const response = await apiClient.get<ClubMemberDto[]>(
      `/Club/${clubId}/members`
    );
    return response.data;
  },

  async joinClub(clubId: number): Promise<void> {
    await apiClient.post(`/Club/${clubId}/join`);
  },

  async leaveClub(clubId: number): Promise<void> {
    await apiClient.delete(`/Club/${clubId}/leave`);
  },

  async makeOfficer(clubId: number, userId: string): Promise<void> {
    await apiClient.put(`/ClubRoles/${clubId}/make-officer/${userId}`);
  },

  // 🔹 PUT: Yetkili görevden al (Manager)
  async demoteOfficer(clubId: number, userId: string): Promise<void> {
    await apiClient.put(`/ClubRoles/${clubId}/demote-officer/${userId}`);
  }
}

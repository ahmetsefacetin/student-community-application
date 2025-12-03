import apiClient from "./apiClient";
import type { CreateClubDto, ClubResponseDto, UpdateClubDto, UserClubRoleDto } from "../types/club";

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
    const response = await apiClient.get<UserClubRoleDto>(`/Club/${clubId}/membership`);
    return response.data;
  }
};


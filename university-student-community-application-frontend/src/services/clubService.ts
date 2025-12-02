import apiClient from "./apiClient";
import type { CreateClubDto, ClubResponseDto } from "../types/club";

export const clubService = {
  async createClub(dto: CreateClubDto): Promise<ClubResponseDto> {
    const response = await apiClient.post<ClubResponseDto>("/Club", dto);
    return response.data;
  },
};
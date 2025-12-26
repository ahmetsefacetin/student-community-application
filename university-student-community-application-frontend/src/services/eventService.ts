import apiClient from "./apiClient";
import type { ClubEvent, CreateClubEventDto } from "../types/event";

export const eventService = {
  async list(clubId: number): Promise<ClubEvent[]> {
    const res = await apiClient.get<ClubEvent[]>(`/Club/${clubId}/events`);
    return res.data;
  },

  async create(clubId: number, dto: CreateClubEventDto): Promise<ClubEvent> {
    const res = await apiClient.post<ClubEvent>(`/Club/${clubId}/events`, dto);
    return res.data;
  },

  async update(clubId: number, eventId: number, dto: CreateClubEventDto): Promise<ClubEvent> {
    const res = await apiClient.put<ClubEvent>(`/Club/${clubId}/events/${eventId}`, dto);
    return res.data;
  },

  async remove(clubId: number, eventId: number): Promise<void> {
    await apiClient.delete(`/Club/${clubId}/events/${eventId}`);
  },
};

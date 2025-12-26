export type ClubEvent = {
  id: number;
  clubId: number;
  title: string;
  description?: string | null;
  location?: string | null;
  startTime: string;
  endTime: string;
};

export type CreateClubEventDto = {
  title: string;
  description?: string | null;
  location?: string | null;
  startTime: string; // ISO string
  endTime: string;   // ISO string
};

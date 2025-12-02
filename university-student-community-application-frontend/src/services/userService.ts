import apiClient from "./apiClient";
import type { UserResponse } from "../types/user";

export const userService = {
  async getAllUsers(): Promise<UserResponse[]> {
    const response = await apiClient.get<UserResponse[]>("/User/all");
    return response.data;
  },
};
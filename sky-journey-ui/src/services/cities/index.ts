import { ApiClient } from "../api";

export const getCities = async () => {
  return ApiClient().cities();
};
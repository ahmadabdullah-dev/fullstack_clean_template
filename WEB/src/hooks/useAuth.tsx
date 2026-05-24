import { useQuery } from "@tanstack/react-query";
import { client } from "../api/client";

export const useAuth = () => {
  const { data, isLoading} = useQuery({
    queryKey: ["auth"],
    queryFn: () => client.get("/User"),
    retry: false,
  });

  return { user: data?.data, isLoading, isAuthenticated: !!data };
};
import { useState } from "react";
import { useMutation } from "@tanstack/react-query";
import { AxiosError } from "axios";
import { useNavigate } from "react-router-dom";
import { client } from "../api/client";
import type { LoginCredentials } from "../types/auth";

export const useLogin = () => {
  const navigate = useNavigate();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [rememberMe, setRememberMe] = useState(false);

  const { mutate, isPending, isSuccess, isError, error } = useMutation({
    mutationFn: (credentials: LoginCredentials) =>
      client.post("/Auth/login", credentials),
    onSuccess: () => setTimeout(() => navigate("/"), 1000),
  });

  const errorMessage =
    error instanceof AxiosError
      ? error.response?.data
      : "Login failed. Please try again.";

  const handleLogin = (e: React.FormEvent) => {
    e.preventDefault();
    mutate({ email, password, rememberMe });
  };

  return {
    email,
    setEmail,
    password,
    setPassword,
    rememberMe,
    setRememberMe,
    handleLogin,
    isPending,
    isSuccess,
    isError,
    errorMessage,
  };
};

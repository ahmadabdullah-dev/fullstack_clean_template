import { Navigate, Outlet } from "react-router-dom";
import { useAuth } from "../hooks/useAuth";
import Header from "../../Home/components/Header";

const ProtectedRoute = () => {
  const { isAuthenticated, isLoading } = useAuth();

  if (isLoading) return <p>Loading...</p>;

  if (!isAuthenticated) return <Navigate to="/login" replace />;

  return (
    <>
      <Header />
      <Outlet />
    </>
  );
};
export default ProtectedRoute;

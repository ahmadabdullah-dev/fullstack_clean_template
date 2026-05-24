import { Route, Routes } from "react-router-dom";
import Home from "../pages/Home";
import LoginForm from "../pages/LoginForm";
import Register from "../pages/Register";
import NotFound from "../components/NotFound";
import Settings from "../pages/Settings";
import Profile from "../components/Profile";
import ProtectedRoute from "../components/ProtectedRoute";
function AppRoutes() {
  return (
    <>
      <Routes>
        {/* public anyone can call */}
        <Route path="/login" element={<LoginForm />} />
        <Route path="/register" element={<Register />} />

        {/* protected can only called if user authenticated */}
        <Route element={<ProtectedRoute />}>
          <Route path="/" element={<Home />} />
          <Route path="/settings" element={<Settings />}>
            <Route path="profile" element={<Profile />} />
          </Route>
        </Route>

        <Route path="*" element={<NotFound />} />
      </Routes>
    </>
  );
}

export default AppRoutes;

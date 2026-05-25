import { Route, Routes } from "react-router-dom";
import Home from "../../features/Home/pages/Home";
import LoginForm from "../../features/auth/pages/LoginForm";
import Register from "../../features/auth/pages/Register";
import NotFound from "../../util/NotFound";
import Settings from "../../features/user/Settings";
import Profile from "../../features/user/Profile";
import ProtectedRoute from "../../features/auth/components/ProtectedRoute";
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

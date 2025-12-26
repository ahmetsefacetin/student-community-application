import { Routes, Route } from "react-router-dom";
import HomePage from "../pages/HomePage";
import LoginPage from "../pages/LoginPage";
import RegisterPage from "../pages/RegisterPage";
import Layout from "../components/Layout"; // Import your Layout component
import ProtectedRoute from "./ProtectedRoute"; // Import ProtectedRoute if you want to secure these pages
import AdminCreateClubPage from "../pages/AdminCreateClubPage";
import UpdateClubPage from "../pages/UpdateClubPage";
import ClubDetailsPage from "../pages/ClubDetailsPage";

const AppRouter = () => {
  return (
    <Routes>

      {/* Wrap routes with Layout to include Navbar and other common components */}
      <Route element={<Layout />}>

        <Route path="/" element={<HomePage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
        

        {/* Admin routes */}
        <Route element={<ProtectedRoute roles={["Admin"]} />}>
          <Route path="/admin/clubs/create" element={<AdminCreateClubPage />} />
        </Route>

        {/* Club details accessible to everyone; edit is protected */}
        <Route path="/clubs/:id" element={<ClubDetailsPage />} />

        <Route element={<ProtectedRoute />}>
          <Route path="/clubs/:id/edit" element={<UpdateClubPage />} />
        </Route>

      </Route>

    </Routes>
  );
};

export default AppRouter;


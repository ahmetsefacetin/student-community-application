import { Routes, Route } from "react-router-dom";
import HomePage from "../pages/HomePage";
import LoginPage from "../pages/LoginPage";
import RegisterPage from "../pages/RegisterPage";
import Layout from "../components/Layout"; // Import your Layout component
import ProtectedRoute from "./ProtectedRoute"; // Import ProtectedRoute if you want to secure these pages

const AppRouter = () => {
  return (
    <Routes>

      {/* Wrap routes with Layout to include Navbar and other common components */ }
      <Route element={<Layout />}>
        
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route path="/" element={<HomePage />} />

        <Route element={<ProtectedRoute />}>
          {/* Protected routes go here */}
          {/* now there are no protected routes */}

        </Route>

      </Route>

    </Routes>
  );
};

export default AppRouter;


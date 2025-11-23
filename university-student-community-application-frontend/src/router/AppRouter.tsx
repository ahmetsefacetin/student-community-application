import { Routes, Route } from "react-router-dom";
import ComingSoonPage from "../pages/ComingSoonPage";
import HomePage from "../pages/HomePage";
import LoginPage from "../pages/LoginPage";
import RegisterPage from "../pages/RegisterPage";
import Layout from "../components/Layout"; // Import your Layout component
import ProtectedRoute from "./ProtectedRoute"; // Import ProtectedRoute if you want to secure these pages

const AppRouter = () => {
  return (
    <Routes>
      {/* 1. Pages WITHOUT Navbar */}
      {/* These are standalone pages */}
      <Route path="/login" element={<LoginPage />} />
      <Route path="/register" element={<RegisterPage />} />
      <Route path="/" element={<HomePage />} />

      {/* 2. Pages WITH Navbar */}
      {/* This Route acts as a wrapper. It renders Layout, which contains the Navbar */}
      <Route element={<Layout />}>
        
        <Route element={<ProtectedRoute />}>
            {/* These pages will appear inside the Layout's <Outlet /> */}
            <Route path="/coming" element={<ComingSoonPage />} />
        </Route>

      </Route>
      
    </Routes>
  );
};

export default AppRouter;


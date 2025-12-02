import React from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../hooks/useAuth';

const Navbar: React.FC = () => {
  const { logout, isAuthenticated, user } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate('/');
  };

  return (
    <nav style={{
      position: 'fixed',
      top: 0,
      left: 0,
      width: '100%',
      zIndex: 1000,
      padding: '1rem',
      borderBottom: '1px solid',
      display: 'flex',
      justifyContent: 'space-between',
      alignItems: 'center',
      boxSizing: 'border-box'
    }}>
      <div className="nav-links">
        {/* <Link to="/" style={{ marginRight: '1rem' }}>Home</Link> */}
        {/* Add other navigation links here */}
      </div>

      <div style={{ display: 'flex', gap: '10px' }}>
        {isAuthenticated && user?.roles.includes("Admin") && (
          <button onClick={() => navigate("/admin/clubs/create")}>
            Yeni Kulüp Oluştur
          </button>
        )}
        {isAuthenticated ? (
          <button onClick={handleLogout}>Logout</button>
        ) : (
          <>
            <button onClick={() => navigate('/login')}>Login</button>
            <button onClick={() => navigate('/register')}>Register</button>
          </>
        )}
      </div>
    </nav>
  );
};

export default Navbar;
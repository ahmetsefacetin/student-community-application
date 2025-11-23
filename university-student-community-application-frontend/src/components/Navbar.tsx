import React from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { useAuth } from '../hooks/useAuth';

const Navbar: React.FC = () => {
  const { logout } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate('/');
  };

  return (
    <nav style={{ 
      position: 'fixed', // Fixes the navbar to the top
      top: 0,
      left: 0,
      width: '100%', // Spans the full width
      zIndex: 1000, // Ensures it stays on top of other elements
    //   backgroundColor: '#ffffff', // Background color is needed so content doesn't show through
      padding: '1rem', 
      borderBottom: '1px solid', 
      display: 'flex', 
      justifyContent: 'space-between', 
      alignItems: 'center',
      boxSizing: 'border-box' // Ensures padding doesn't break the 100% width
    }}>
      <div className="nav-links">
        {/* <Link to="/" style={{ marginRight: '1rem' }}>Home</Link> */}
        {/* Add other navigation links here */}
      </div>
      <button onClick={handleLogout}>Logout</button>
    </nav>
  );
};

export default Navbar;
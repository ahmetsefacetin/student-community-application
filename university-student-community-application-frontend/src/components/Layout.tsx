import React from 'react';
import { Outlet } from 'react-router-dom';
import Navbar from './Navbar';

const Layout: React.FC = () => {
  return (
    <div>
      <Navbar />
      {/* paddingTop is added here to push content down below the fixed Navbar */}
      <main style={{ padding: '1rem', paddingTop: '80px' }}>
        <Outlet />
      </main>
    </div>
  );
};

export default Layout;
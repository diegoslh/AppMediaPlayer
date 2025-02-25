import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";

import HomeAdmin from "./views/HomeAdmin.jsx";
import HomeContent from "./views/HomeContent.jsx";
// import CreateBT from "./components/CreateBT.jsx";
// import CreateVT from "./components/CreateVT.jsx";


const PrivateRoute = ({ element }) => {
  const user_sesion = sessionStorage.getItem("ID_SESSION");
  const isAuthenticated = !!user_sesion;

  if (!isAuthenticated) {
    return <Navigate to="/" />;
  }

  return element;
};

function Router() {
  return (
    <BrowserRouter>
      <Routes>
        <Route
          path="/"
          element={<HomeContent />}
        />
        <Route
          path="/admin"
          element={<PrivateRoute element={<HomeAdmin />} />}
        />
        {/* <Route
          path="/admin/create/bt"
          element={<PrivateRoute element={<CreateBT />} />}
        />
        <Route
          path="/admin/create/vt"
          element={<PrivateRoute element={<CreateVT />} />}
        />
        <Route
          path="/admin/create/vbl"
          element={<PrivateRoute element={<HomeAdmin />} />}
        /> */}
      </Routes>
    </BrowserRouter>
  );
}

export default Router;
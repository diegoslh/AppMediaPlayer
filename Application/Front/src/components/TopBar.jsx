import ModalLogin from "./ModalLogin.jsx";
import { useNavigate } from "react-router-dom";


function TopBar() {
  const navigate = useNavigate();

  const user_sesion = sessionStorage.getItem("ID_SESSION");
  const isAuthenticated = !!user_sesion;

  function logout() {
    sessionStorage.removeItem("ID_SESSION");
    navigate("/");
  }

  return (
    <nav className="navbar navbar-expand-lg navbar-dark">
      <div className="container-fluid">
        <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarTogglerDemo01" aria-controls="navbarTogglerDemo01" aria-expanded="false" aria-label="Toggle navigation">
          <span className="navbar-toggler-icon"></span>
        </button>
        <div className="collapse navbar-collapse" id="navbarTogglerDemo01">
          <a className="navbar-brand" href="/">AppMediaPlayer</a>
          <ul className="navbar-nav me-auto mb-2 mb-lg-0">
            {
              isAuthenticated && (
                <li className="nav-item">
                  <a className="nav-link" href="/admin">Dashboard</a>
                </li>
              )
            }
          </ul>
          <section className="d-flex">
            {
              isAuthenticated
                ? <button className="button_auth" onClick={() => logout()}><span>Logout</span></button>
                : <ModalLogin />
            }
          </section>
        </div>
      </div>
    </nav>
  )
}

export default TopBar
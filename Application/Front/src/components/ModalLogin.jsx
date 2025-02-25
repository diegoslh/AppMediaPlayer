import { useNavigate } from "react-router-dom";
import { API_URL } from "../helpers/config";
import { toast } from "sonner";
import { useState } from "react";
import axios from "axios";


function ModalLogin() {
  const navigate = useNavigate();
  const url = `${API_URL}/Authentication/Login`;

  const [formData, setFormData] = useState({
    Username: "",
    Password: "",
  });

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await axios.post(url, formData);
      console.log(response)
      if (!response.data.success) {
        toast.error(<h6 className="mb-0"><b>{response.data.message}</b></h6>)
        return;
      }

      sessionStorage.setItem("ID_SESSION", response.data.jwt);
      // if (dataUser.rol === 1) //no viene rol en la response
      document.body.classList.remove("modal-open");
      document.querySelectorAll(".modal-backdrop").forEach(el => el.remove());
      return navigate("/admin");

    } catch (error) {
      console.log("Error al enviar los datos", error);
      toast.error(<h6 className="mb-0"><b>No se pudo iniciar sesión</b></h6>)
    }
  };


  return (
    <>
      <button
        className="button_auth"
        type="button"
        data-bs-toggle="modal"
        data-bs-target="#loginModal">
        <span>Login</span>
      </button>

      <article className="modal" tabIndex="-1" id="loginModal">
        <div className="modal-dialog modal-dialog-centered modal-sm">
          <form className="modal-content" onSubmit={handleSubmit}>
            <header className="modal-header border-0">
              <h4 className="modal-title w-100 text-center">Bienvenido</h4>
              {/* <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button> */}
            </header>

            <section className="modal-body">
              <div className="form-floating mb-3">
                <input type="text" className="form-control" id="Username" name="Username" onChange={handleChange} placeholder="" required />
                <label htmlFor="Username">Usuario</label>
              </div>
              <div className="form-floating">
                <input type="password" className="form-control" name="Password" id="Password" onChange={handleChange} placeholder="" required />
                <label htmlFor="Password">Contraseña</label>
              </div>
            </section>

            <footer className="modal-footer border-0">
              <button type="submit" className="btn btn-outline-success mx-auto">
                Iniciar Sesion
              </button>
            </footer>
          </form>
        </div>
      </article>
    </>
  )
}

export default ModalLogin
import { API_URL } from "../helpers/config.js";
import api from "../helpers/axiosConfig.js";
import { useState } from "react";
import { toast } from "sonner";
import timerIcon from '../assets/images/icon-timer.png'


function ModalSetHour({ id, programation }) {
  const url = `${API_URL}/content/setcontent?id=${id}`;
  const idModal = `SetHourContenttModal${id}`;

  const [hourValue, setHourValue] = useState("");
  const handleChange = (e) => setHourValue(e.target.value);


  const handleSubmit = async (e) => {
    e.preventDefault();
    let validate = validateHour()
    if (!validate) return;

    try {
      const formattedHour = `${hourValue}:00`;
      const response = await api.post(url, { Hour: formattedHour });

      if (!response.data.success) {
        toast.error(<h6 className="mb-0">{response.data.message}</h6>)
        return;
      }

      toast.success(<h6 className="mb-0"><b>{response.data.message}</b></h6>)
    } catch (error) {
      console.log("Error al enviar los datos", error);
      toast.error(<h6 className="mb-0">Ups! No se pudo programar el contenido</h6>)
    }
  };

  const validateHour = () => {

    const horasProgramadas = programation.map((item) =>
      item.pcoHoraProgramada.split(":").slice(0, 2).join(":")
    );

    if (horasProgramadas.includes(hourValue)) {
      toast.info(<h6 className="mb-0">Esta hora ya está programada. Elige otra.</h6>)
      return false;
    }

    return true;
  }

  return (
    <>
      <button
        className="btn btn-sm btn-outline-danger"
        type="button"
        data-bs-toggle="modal"
        data-bs-target={`#${idModal}`}>
        <img src={timerIcon} alt="Set" height="30" />
      </button>

      <article className="modal" tabIndex="-1" id={idModal}>
        <div className="modal-dialog">
          <form className="modal-content" onSubmit={handleSubmit}>
            <header className="modal-header border-0">
              <h4 className="modal-title w-100 text-center">Programar Contenido</h4>
            </header>

            <section className="modal-body">
              <div className="col-lg-12 mt-2">
                <label className="form-label">Hora de programación: <code>*</code></label>
                <input type="time" name="" className="form-control" onChange={handleChange} required />
              </div>
            </section>

            <footer className="modal-footer">
              <button type="button" className="btn btn-secondary" data-bs-dismiss="modal">
                Cancelar
              </button>
              <button type="submit" className="btn btn-outline-success">
                Programar
              </button>
            </footer>
          </form>
        </div>
      </article>
    </>
  )
}

export default ModalSetHour
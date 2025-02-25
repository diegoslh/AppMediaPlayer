import { API_URL } from "../helpers/config";
import { toast } from "sonner";
import api from "../helpers/axiosConfig.js";
import iconDelete from '../assets/images/icon-delete.png'


function ModalDelete({ id }) {
  const url = `${API_URL}/content/delete`;
  const idModal = `deleteContentModal${id}`;

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await api.put(`${url}?id=${id}`);
      // console.log(response)
      if (!response.data.success) {
        toast.error(<h6 className="mb-0">{response.data.message}</h6>)
        return;
      }

      toast.success(<h6 className="mb-0"><b>{response.data.message}</b></h6>)
      setTimeout(() => {
        window.location.reload();
      }, 1000);

    } catch (error) {
      console.log("Error al enviar los datos", error);
      toast.error(<h6 className="mb-0">No se pudo eliminar el registro</h6>)
    }
  };


  return (
    <>
      <button
        className="btn btn-sm btn-outline-danger"
        type="button"
        data-bs-toggle="modal"
        data-bs-target={`#${idModal}`}>
        {/* Eliminar */}
        <img src={iconDelete} alt="" height="30"/>
      </button>

      <article className="modal" tabIndex="-1" id={idModal}>
        <div className="modal-dialog">
          <form className="modal-content" onSubmit={handleSubmit}>
            <header className="modal-header border-0">
              <h4 className="modal-title w-100 text-center text-danger">Eliminar Contenido</h4>
            </header>

            <section className="modal-body text-center">
              <p>Confirma eliminar el contenido con numero de registro: {id}</p>
            </section>

            <footer className="modal-footer">
              <button type="button" className="btn btn-secondary" data-bs-dismiss="modal">
                Cancelar
              </button>
              <button type="submit" className="btn btn-outline-success">
                Eliminar
              </button>
            </footer>
          </form>
        </div>
      </article>
    </>
  )
}

export default ModalDelete
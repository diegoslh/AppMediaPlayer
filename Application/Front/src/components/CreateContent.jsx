import { API_URL } from '../helpers/config.js';
import { useState } from 'react';
// import { Modal } from "bootstrap";
import api from '../helpers/axiosConfig.js';
import { toast } from 'sonner';
import viteLogo from '/vite.svg'


function CreateContent({ label, content, contentType }) {
  const url = `${API_URL}/content/create`;
  const idModal = `createContentModal${content}`;
  const MAX_FILE_SIZE = 30 * 1024 * 1024;
  // const modalRef = useRef(idModal);

  const initialState = {
    CtoTitulo: "",
    CtoTipoContenidoFk: contentType,
    CtoVideo: null,
    CtoBanner: null,
    CtoTextoBanner: "",
    CtoDurationBanner: ""
  };

  const [formData, setFormData] = useState(initialState);

  const handleChange = (e) => {
    const { name, value, type, files } = e.target;

    if (type === "file") {
      const file = e.target.files[0];
      if (file) {
        if (file.size > MAX_FILE_SIZE) {
          alert("El archivo es demasiado grande. Máximo permitido: 30MB.");
          e.target.value = "";
          return;
        }
      }
    }

    setFormData((prev) => ({
      ...prev,
      [name]: type === "file" ? files[0] : value,
    }));
  };

  const createFormData = () => {
    const data = new FormData();

    Object.entries(formData).forEach(([key, value]) => {
      if (value) data.append(key, value); // Solo agregar si tiene valor
    });
    return data;
  }

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const data = createFormData();
      const response = await api.post(url, data, {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      });

      // console.log(response)
      if (!response.data.success) {
        toast.error(<h6 className="mb-0">{response.data.message}</h6>)
        return;
      }

      setFormData(initialState);
      toast.success(<h6 className="mb-0"><b>{response.data.message}</b></h6>)
      setTimeout(() => {
        window.location.reload();
      }, 1000);
    } catch (error) {
      console.log("Error al enviar los datos", error);
      toast.error(<h6 className="mb-0">Ups! No se pudo guardar el contenido.</h6>)
    }
  };


  return (
    <>
      <button
        className="btn_content_create"
        type="button"
        data-bs-toggle="modal"
        data-bs-target={`#${idModal}`}>
        <div className="flex_center">
          <i>
            <img src={viteLogo} className="logo" alt="Vite logo" />
          </i>
          <h3>{label}</h3>
        </div>
      </button>

      <article className="modal" tabIndex="-1" id={idModal}>
        <div className="modal-dialog">
          <form className="modal-content" onSubmit={handleSubmit}>
            <header className="modal-header">
              <h4 className="modal-title w-100 text-center">Agregar {content}</h4>
            </header>

            <section className="modal-body">
              <div className="col-lg-12 mt-2">
                <label className="form-label">Título de Contenido <code>*</code></label>
                <input type="text" name="CtoTitulo" className="form-control" onChange={handleChange} placeholder="" required />
              </div>

              <div className="col-lg-12 mt-2">
                {
                  (content === "BT" || content === "VBL")
                  && (
                    <>
                      <label className="form-label mt-2">Imagen Banner {content == "BT" && <code>*</code>}</label>
                      <input
                        accept=".png, .jpg, .jpeg"
                        type="file"
                        name="CtoBanner"
                        className="form-control"
                        onChange={handleChange}
                        required={content === "BT"}
                      />
                      <code>Formatos .png, .jpg, .jpeg</code><br />
                    </>
                  )
                }
                {
                  (content === "VT" || content === "VBL")
                  && (
                    <>
                      <label className="form-label mt-2">Video <code>*</code></label>
                      <input
                        accept=".mp4"
                        type="file"
                        name="CtoVideo"
                        className="form-control"
                        onChange={handleChange}
                        required
                      />
                      <code>Formato .mp4 / Máximo tamaño 30MB</code>
                    </>
                  )
                }
              </div>

              {content !== "VT" && (
                <div className="col-lg-12 mt-2">
                  <label className="form-label">Descripción de Banner</label>
                  <textarea name="CtoTextoBanner" className="form-control" onChange={handleChange}></textarea>
                </div>
              )}

              {
                (content === "BT") && (
                  <div className="col-lg-12 mt-2">
                    <label className="form-label">Duración de Banner (segundos)<code>*</code></label>
                    <input type="number" name="CtoDurationBanner" className="form-control" onChange={handleChange} placeholder="" required />
                  </div>
                )
              }
            </section>

            <footer className="modal-footer">
              <button type="button" className="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
              <button type="submit" className="btn btn-outline-success">Enviar</button>
            </footer>
          </form>
        </div>
      </article>
    </>
  )
}

export default CreateContent
import { API_URL } from "../helpers/config.js";
import { useEffect, useState } from "react";
import imageIcon from '../assets/images/icon-image.png'
import videoIcon from '../assets/images/icon-video.png'
import ModalDelete from "./ModalDelete.jsx";
import ModalSetHour from "./ModalSetHour.jsx";

function ContentList() {
  const [content, setContent] = useState([]);
  const [programation, setProgramation] = useState([]);

  // 📍 Fetch programation content from API
  useEffect(() => {
    fetch(`${API_URL}/content/programation`)
      .then((res) => res.json())
      .then((data) => setProgramation(data))
      .catch((err) => console.error("Error cargando multimedia:", err));
  }, []);

  //📍 Fetch multimedia content from API
  useEffect(() => {
    fetch(`${API_URL}/content`)
      .then((res) => res.json())
      .then((data) => setContent(data))
      .catch((err) => console.error("Error cargando multimedia:", err));
  }, []);

  return (
    <article className="cont_list_content">
      {
        content.map((item, index) => (
          <section key={index} className="row list_content">
            <div className="col-2">
              <img src={item.ctoTipoContenidoFk === 1 ? imageIcon : videoIcon} alt="icono" height="52" />
            </div>
            <div className="col-8">
              <h4 className="ellipsis_text">{item.ctoIdContenidoPk} | {item.ctoTitulo}</h4>
              <p>{item.ctoTipoContenido}</p>
            </div>
            <div className="col-2">
              <ModalSetHour
                id={item.ctoIdContenidoPk}
                programation={programation}
              />
              <ModalDelete
                id={item.ctoIdContenidoPk}
              />
            </div>
          </section>
        ))
      }
    </article>
  )
}

export default ContentList
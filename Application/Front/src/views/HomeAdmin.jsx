import CreateContent from "../components/CreateContent.jsx";
import ContentList from "../components/ContentList.jsx";
import TopBar from "../components/TopBar.jsx"
import { Toaster } from 'sonner'

function HomeAdmin() {

  return (
    <>
      <TopBar />
      <main className="container-lg container_admin">
        <h2 className="mt-3 mb-2">
          <span className="text_gradient">AppMediaPlayer</span> | Dashboard de administración
        </h2>

        <section className="row cont_dashboard">
          <div className="col-lg-8">
            <h3 className="text-center mt-4 mb-0">Programación Contenido</h3>
            <ContentList />
          </div>
          <div className="col-lg-4 flex_center cont_btns_content">
            <h3>Crear contenido</h3>
            <CreateContent
              label="Banner con Título"
              content="BT"
              contentType={1}
            />
            <CreateContent
              label="Video con Título"
              content="VT"
              contentType={2}
            />
            <CreateContent
              label="Video + Banner Lateral"
              content="VBL"
              contentType={3}
            />
          </div>
        </section>
        <Toaster richColors position="bottom-center" />
      </main>
    </>
  )
}

export default HomeAdmin
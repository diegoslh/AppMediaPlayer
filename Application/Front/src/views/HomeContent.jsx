import TopBar from "../components/TopBar";
import MediaPlayer from "../components/MediaPlayer.jsx";
import { Toaster } from "sonner";
import { API_URL } from "../helpers/config.js";
import { useState, useEffect } from "react";

function HomeContent() {
  const [programation, setProgramation] = useState([]);

  // 📍 Fetch programation content from API
  useEffect(() => {
    fetch(`${API_URL}/content/programation`)
      .then((res) => res.json())
      .then((data) => setProgramation(data))
      .catch((err) => console.error("Error cargando multimedia:", err));
  }, []);


  return (
    <>
      <TopBar />
      <main className="container-lg container_home">
        <MediaPlayer
          programation={programation}
        />
      </main>
      <Toaster richColors position="bottom-center" />
    </>
  )
}


export default HomeContent
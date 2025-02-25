import { useState, useEffect, useRef } from "react";
import { API_URL } from "../helpers/config.js";
import BannerTitle from "./BannerTitle.jsx";
import VideoTitle from "./VideoTitle.jsx";
import VideoBannerTitle from "./VideoBannerTitle.jsx";


function MediaPlayer({ programation }) {
  const [mediaFiles, setMediaFiles] = useState([]);
  const [currentIndex, setCurrentIndex] = useState(0);
  const [timeLeft, setTimeLeft] = useState(0);
  const videoRef = useRef(null);
  const timeoutRef = useRef(null);
  const intervalRef = useRef(null);

  // 📍 Fetch multimedia content from API
  useEffect(() => {
    fetch(`${API_URL}/content`)
      .then((res) => res.json())
      .then((data) => setMediaFiles(data))
      .catch((err) => console.error("Error cargando multimedia:", err));
  }, []);

  // 📍 Management transition
  useEffect(() => {
    if (mediaFiles.length === 0) return;

    // 🔹 Obtener la hora actual en segundos
    const getCurrentTimeInSeconds = () => {
      const now = new Date();
      return now.getHours() * 3600 + now.getMinutes() * 60 + now.getSeconds();
    };

    // 🔹 Convertir a segundos
    const timeStringToSeconds = (timeStr) => {
      const [hh, mm, ss] = timeStr.split(":").map(Number);
      return hh * 3600 + mm * 60 + ss;
    };

    // 🔹 Verificar si hay un contenido programado en el rango de 40s
    const currentTimeInSeconds = getCurrentTimeInSeconds();
    const scheduledContent = programation.find((item) => {
      const scheduledTimeInSeconds = timeStringToSeconds(item.pcoHoraProgramada);
      return Math.abs(scheduledTimeInSeconds - currentTimeInSeconds) <= 40;
    });

    let newIndex = currentIndex;
    if (scheduledContent) {
      const scheduledIndex = mediaFiles.findIndex(
        (file) => file.ctoIdContenidoPk === scheduledContent.pcoIdContenidoFk
      );
      if (scheduledIndex !== -1) {
        newIndex = scheduledIndex;
      }
    }

    const currentFile = mediaFiles[newIndex];
    let duration = 5;

    // 🔹 Reiniciar temporizadores para evitar que se queden pegados
    clearTimeout(timeoutRef.current);
    clearInterval(intervalRef.current);

    // 🔹 Definir tiempos de duración
    if ([2, 3].includes(currentFile.ctoTipoContenidoFk) && videoRef.current) {
      videoRef.current.src = currentFile.ctoVideo;
      videoRef.current.load();
      videoRef.current.onloadedmetadata = () => {
        duration = Math.round(videoRef.current.duration) || 10;
        startCountdown(duration, newIndex);
        videoRef.current.play().catch((err) => console.log("Reproducción bloqueada:", err));
      };
    } else {
      duration = currentFile.ctoDurationBanner || 5;
      startCountdown(duration, newIndex);
    }

    // 🔹 Configuracion de cuenta regresiva
    function startCountdown(seconds, nextIndex) {
      setTimeLeft(seconds);

      intervalRef.current = setInterval(() => {
        setTimeLeft((prev) => {
          if (prev <= 1) {
            clearInterval(intervalRef.current);
            setCurrentIndex((prevIndex) => (prevIndex + 1) % mediaFiles.length);
            return 0;
          }
          return prev - 1;
        });
      }, 1000);

      timeoutRef.current = setTimeout(() => {
        clearInterval(intervalRef.current);
        setCurrentIndex((prevIndex) => (prevIndex + 1) % mediaFiles.length);
      }, seconds * 1000);
    }

    return () => {
      clearTimeout(timeoutRef.current);
      clearInterval(intervalRef.current);
    };
  }, [currentIndex, mediaFiles, programation]);

  // 📍 Archivo actual
  const currentFile = mediaFiles[currentIndex];

  return (
    <>
      {
        (mediaFiles.length === 0)
          ? (
            <div className="flex_center h-100">
              <h2 className="text-center text_gradient">Cargando...</h2>
            </div>
          )
          : (
            <>
              <div className="flex_center timeleft">
                {timeLeft}s
              </div>
              <article className="media_player text-center">
                <header className="content_header flex_center">
                  <h2 className="text_gradient">
                    {currentFile.ctoIdContenidoPk} -
                    {currentFile.ctoTitulo}
                  </h2>
                </header>
                <section className="content_body">
                  {
                    currentFile.ctoTipoContenidoFk === 1
                      ? <BannerTitle file={currentFile} />
                      : currentFile.ctoTipoContenidoFk === 2
                        ? <VideoTitle file={currentFile} videoRef={videoRef} currentIndex={currentIndex} />
                        : <VideoBannerTitle file={currentFile} videoRef={videoRef} currentIndex={currentIndex} />
                  }
                </section>
              </article>
            </>
          )
      }
    </>
  );
};

export default MediaPlayer;
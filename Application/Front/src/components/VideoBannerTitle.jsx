
function VideoBannerTitle({ file, videoRef, currentIndex }) {
  let activeClass = (file.ctoTextoBanner && file.ctoBanner)
    ? "cont_banner template_rows"
    : "cont_banner";

  return (
    <section className="row" style={{ height: "100%" }}>
      <div className="col-4">
        <section className={activeClass}>
          {file.ctoBanner != null && (
            <div className="banner_img flex_center cont_background">
              <img
                src={file.ctoBanner}
                alt="Img Banner"
                // width="500"
                height="400"
              />
            </div>
          )}

          {file.ctoTextoBanner != null && (
            <div className="banner_text flex_center">
              <p>{file?.ctoTextoBanner}</p>
            </div>
          )}
        </section>
      </div>
      <div className="col-8 cont_video cont_background">
        <video
          key={currentIndex}
          ref={videoRef}
          controls
          autoPlay
          loop
          muted
          width="300">
          <source
            src={file.ctoVideo}
            type="video/mp4"
          />
          Tu navegador no soporta el video.
        </video>
      </div>
    </section>
  )
}

export default VideoBannerTitle
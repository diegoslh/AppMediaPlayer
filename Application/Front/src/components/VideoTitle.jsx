
function VideoTitle({ file, videoRef, currentIndex }) {
  return (
    <section className="cont_video cont_background">
      <video
        key={currentIndex}
        ref={videoRef}
        controls
        autoPlay
        // loop
        muted
        width="300">
        <source
          src={file.ctoVideo}
          type="video/mp4"
        />
        Tu navegador no soporta el video.
      </video>
    </section>
  )
}

export default VideoTitle
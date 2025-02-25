

function BannerTitle({ file }) {
  let activeClass = file.ctoTextoBanner
    ? "cont_banner template_rows"
    : "cont_banner ";
  return (
    <section className={activeClass}>
      <div className="banner_img cont_background flex_center">
        <img
          src={file.ctoBanner}
          alt="Img Banner"
        />
      </div>

      {file.ctoTextoBanner != null && (
        <div className="banner_text flex_center">
          <p>{file?.ctoTextoBanner}</p>
        </div>
      )}
    </section>
  )
}

export default BannerTitle
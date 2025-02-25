using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Shared;

namespace Models.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblBanner> TblBanners { get; set; }

    public virtual DbSet<TblContenido> TblContenidos { get; set; }

    public virtual DbSet<TblDatosPersona> TblDatosPersonas { get; set; }

    public virtual DbSet<TblDicRolesSistema> TblDicRolesSistemas { get; set; }

    public virtual DbSet<TblDicTipoContenido> TblDicTipoContenidos { get; set; }

    public virtual DbSet<TblProgramacionContenido> TblProgramacionContenidos { get; set; }

    public virtual DbSet<TblUsuario> TblUsuarios { get; set; }

    public virtual DbSet<TblVideo> TblVideos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:LocalConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SpContentDto>().HasNoKey();

        modelBuilder.Entity<TblBanner>(entity =>
        {
            entity.HasKey(e => e.BnrIdBannerPk).HasName("PK__TblBanne__426D162CE02E3A3D");

            entity.ToTable("TblBanner");

            entity.Property(e => e.BnrIdBannerPk).HasColumnName("bnr_IdBannerPk");
            entity.Property(e => e.BnrDuracion)
                //.HasDefaultValue(30)
                .HasColumnName("bnr_duracion");
            entity.Property(e => e.BnrEstado).HasColumnName("bnr_estado");
            entity.Property(e => e.BnrIdContenidoFk).HasColumnName("bnr_idContenidoFk");
            entity.Property(e => e.BnrRutaAcceso)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("bnr_rutaAcceso");
            entity.Property(e => e.BnrTexto)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("bnr_texto");

            entity.HasOne(d => d.BnrIdContenidoFkNavigation).WithMany(p => p.TblBanners)
                .HasForeignKey(d => d.BnrIdContenidoFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TblBanner__bnr_e__5070F446");
        });

        modelBuilder.Entity<TblContenido>(entity =>
        {
            entity.HasKey(e => e.CtoIdContenidoPk).HasName("PK__TblConte__11ED543B015314DD");

            entity.ToTable("TblContenido");

            entity.Property(e => e.CtoIdContenidoPk).HasColumnName("cto_IdContenidoPk");
            entity.Property(e => e.CtoEstado).HasColumnName("cto_estado");
            entity.Property(e => e.CtoTipoContenidoFk).HasColumnName("cto_tipoContenidoFk");
            entity.Property(e => e.CtoTitulo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cto_titulo");

            entity.HasOne(d => d.CtoTipoContenidoFkNavigation).WithMany(p => p.TblContenidos)
                .HasForeignKey(d => d.CtoTipoContenidoFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TblConten__cto_t__44FF419A");
        });

        modelBuilder.Entity<TblDatosPersona>(entity =>
        {
            entity.HasKey(e => e.DpIdDatosPersonaPk).HasName("PK__TblDatos__0B8745621CD910D3");

            entity.ToTable("TblDatosPersona");

            entity.Property(e => e.DpIdDatosPersonaPk).HasColumnName("dp_IdDatosPersonaPk");
            entity.Property(e => e.DpApellido1)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("dp_apellido1");
            entity.Property(e => e.DpApellido2)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("dp_apellido2");
            entity.Property(e => e.DpEstado)
                .HasDefaultValue(true)
                .HasColumnName("dp_estado");
            entity.Property(e => e.DpNombre1)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("dp_nombre1");
            entity.Property(e => e.DpNombre2)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("dp_nombre2");
        });

        modelBuilder.Entity<TblDicRolesSistema>(entity =>
        {
            entity.HasKey(e => e.RsIdRolSistemaPk).HasName("PK__TblDicRo__8FBF7D6F97F33995");

            entity.ToTable("TblDicRolesSistema");

            entity.Property(e => e.RsIdRolSistemaPk).HasColumnName("rs_IdRolSistemaPk");
            entity.Property(e => e.RsEstado).HasColumnName("rs_estado");
            entity.Property(e => e.RsNombreRol)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("rs_nombreRol");
        });

        modelBuilder.Entity<TblDicTipoContenido>(entity =>
        {
            entity.HasKey(e => e.TctoIdTipoContenidoPk).HasName("PK__TblDicTi__C738477F6652813B");

            entity.ToTable("TblDicTipoContenido");

            entity.Property(e => e.TctoIdTipoContenidoPk).HasColumnName("tcto_IdTipoContenidoPk");
            entity.Property(e => e.TctoDescripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tcto_descripcion");
            entity.Property(e => e.TctoEstado).HasColumnName("tcto_estado");
            entity.Property(e => e.TctoTipoContenido)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("tcto_tipoContenido");
        });

        modelBuilder.Entity<TblProgramacionContenido>(entity =>
        {
            entity.HasKey(e => e.PcoIdProgramacionPk).HasName("PK__TblProgr__5AFE44BACCAC05ED");

            entity.ToTable("TblProgramacionContenido");

            entity.Property(e => e.PcoIdProgramacionPk).HasColumnName("pco_IdProgramacionPk");
            entity.Property(e => e.PcoEstado).HasColumnName("pco_estado");
            entity.Property(e => e.PcoHoraProgramada).HasColumnName("pco_horaProgramada");
            entity.Property(e => e.PcoIdContenidoFk).HasColumnName("pco_idContenidoFk");

            entity.HasOne(d => d.PcoIdContenidoFkNavigation).WithMany(p => p.TblProgramacionContenidos)
                .HasForeignKey(d => d.PcoIdContenidoFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TblProgra__pco_e__4D94879B");
        });

        modelBuilder.Entity<TblUsuario>(entity =>
        {
            entity.HasKey(e => e.UsIdUsuarioPk).HasName("PK__TblUsuar__EA793E97B5F5D1FD");

            entity.Property(e => e.UsIdUsuarioPk).HasColumnName("us_IdUsuarioPk");
            entity.Property(e => e.UsAlias)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("us_alias");
            entity.Property(e => e.UsContrasena)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("us_contrasena");
            entity.Property(e => e.UsDatosPersonaFk).HasColumnName("us_datosPersonaFk");
            entity.Property(e => e.UsEstado).HasColumnName("us_estado");
            entity.Property(e => e.UsRolSistemaFk).HasColumnName("us_rolSistemaFk");

            entity.HasOne(d => d.UsDatosPersonaFkNavigation).WithMany(p => p.TblUsuarios)
                .HasForeignKey(d => d.UsDatosPersonaFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TblUsuari__us_da__3F466844");

            entity.HasOne(d => d.UsRolSistemaFkNavigation).WithMany(p => p.TblUsuarios)
                .HasForeignKey(d => d.UsRolSistemaFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TblUsuari__us_ro__403A8C7D");
        });

        modelBuilder.Entity<TblVideo>(entity =>
        {
            entity.HasKey(e => e.VdoIdVideoPk).HasName("PK__TblVideo__150E869A49468549");

            entity.ToTable("TblVideo");

            entity.Property(e => e.VdoIdVideoPk).HasColumnName("vdo_IdVideoPk");
            entity.Property(e => e.VdoEstado).HasColumnName("vdo_estado");
            entity.Property(e => e.VdoIdContenidoFk).HasColumnName("vdo_idContenidoFk");
            entity.Property(e => e.VdoRutaAcceso)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("vdo_rutaAcceso");

            entity.HasOne(d => d.VdoIdContenidoFkNavigation).WithMany(p => p.TblVideos)
                .HasForeignKey(d => d.VdoIdContenidoFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TblVideo__vdo_id__47DBAE45");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

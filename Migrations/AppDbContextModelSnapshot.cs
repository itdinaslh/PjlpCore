﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PjlpCore.Data;

#nullable disable

namespace PjlpCore.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PjlpCore.Entity.Agama", b =>
                {
                    b.Property<int>("AgamaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NamaAgama")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("AgamaID");

                    b.ToTable("agama");
                });

            modelBuilder.Entity("PjlpCore.Entity.Bidang", b =>
                {
                    b.Property<Guid>("BidangID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("KepalaBidang")
                        .HasMaxLength(75)
                        .HasColumnType("varchar(75)");

                    b.Property<string>("NamaBidang")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("varchar(75)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("BidangID");

                    b.ToTable("bidang");
                });

            modelBuilder.Entity("PjlpCore.Entity.Divisi", b =>
                {
                    b.Property<Guid>("DivisiID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("BidangID")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NamaDivisi")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("DivisiID");

                    b.HasIndex("BidangID");

                    b.ToTable("divisi");
                });

            modelBuilder.Entity("PjlpCore.Entity.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("EventId");

                    b.ToTable("events");
                });

            modelBuilder.Entity("PjlpCore.Entity.FileType", b =>
                {
                    b.Property<int>("FileTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("FileTypeId");

                    b.ToTable("filetypes");
                });

            modelBuilder.Entity("PjlpCore.Entity.Jabatan", b =>
                {
                    b.Property<Guid>("JabatanID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("BidangID")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NamaJabatan")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("JabatanID");

                    b.HasIndex("BidangID");

                    b.ToTable("jabatan");
                });

            modelBuilder.Entity("PjlpCore.Entity.JenisPegawai", b =>
                {
                    b.Property<int>("JenisPegawaiID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("NamaJenis")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("JenisPegawaiID");

                    b.ToTable("jenis_pegawai");
                });

            modelBuilder.Entity("PjlpCore.Entity.Kabupaten", b =>
                {
                    b.Property<string>("KabupatenID")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<bool>("IsKota")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Latitude")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Longitude")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("NamaKabupaten")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("ProvinsiID")
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.HasKey("KabupatenID");

                    b.HasIndex("ProvinsiID");

                    b.ToTable("kabupaten");
                });

            modelBuilder.Entity("PjlpCore.Entity.Kecamatan", b =>
                {
                    b.Property<string>("KecamatanID")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("KabupatenID")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Latitude")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Longitude")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("NamaKecamatan")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.HasKey("KecamatanID");

                    b.HasIndex("KabupatenID");

                    b.ToTable("kecamatan");
                });

            modelBuilder.Entity("PjlpCore.Entity.Kelurahan", b =>
                {
                    b.Property<string>("KelurahanID")
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<string>("KecamatanID")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Latitude")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Longitude")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("NamaKelurahan")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.HasKey("KelurahanID");

                    b.HasIndex("KecamatanID");

                    b.ToTable("kelurahan");
                });

            modelBuilder.Entity("PjlpCore.Entity.LokasiKerja", b =>
                {
                    b.Property<int>("LokasiKerjaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NamaLokasi")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("LokasiKerjaID");

                    b.ToTable("lokasi_kerja");
                });

            modelBuilder.Entity("PjlpCore.Entity.Pegawai", b =>
                {
                    b.Property<Guid>("PegawaiID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("AddressIsSame")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("AgamaID")
                        .HasColumnType("int");

                    b.Property<string>("AlamatDom")
                        .HasColumnType("longtext");

                    b.Property<string>("AlamatKTP")
                        .HasColumnType("longtext");

                    b.Property<Guid>("BidangID")
                        .HasColumnType("char(36)");

                    b.Property<string>("CabangBank")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("GolDarah")
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)");

                    b.Property<int>("JenisPegawaiID")
                        .HasColumnType("int");

                    b.Property<string>("JurusanPendidikan")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<bool?>("Kelamin")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("KelurahanDomID")
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<string>("KelurahanID")
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<string>("KodePosDom")
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.Property<string>("KodePosKTP")
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.Property<string>("NIK")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<string>("NPWP")
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)");

                    b.Property<string>("NamaPegawai")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("NamaSekolah")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("NoHP")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("NoKK")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("NoRekening")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<int?>("PendidikanID")
                        .HasColumnType("int");

                    b.Property<string>("RtDom")
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<string>("RtKTP")
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<string>("RwDom")
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<string>("RwKTP")
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<int?>("StatusKawinID")
                        .HasColumnType("int");

                    b.Property<string>("TempatLahir")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<DateOnly?>("TglLahir")
                        .HasColumnType("date");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("PegawaiID");

                    b.HasIndex("AgamaID");

                    b.HasIndex("BidangID");

                    b.HasIndex("JenisPegawaiID");

                    b.HasIndex("KelurahanDomID");

                    b.HasIndex("KelurahanID");

                    b.HasIndex("PendidikanID");

                    b.HasIndex("StatusKawinID");

                    b.ToTable("pegawai");
                });

            modelBuilder.Entity("PjlpCore.Entity.Pelamar", b =>
                {
                    b.Property<Guid>("PelamarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("AgamaId")
                        .HasColumnType("int");

                    b.Property<string>("Alamat")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("BidangId")
                        .HasColumnType("char(36)");

                    b.Property<string>("CabangRekening")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DomAlamat")
                        .HasColumnType("longtext");

                    b.Property<string>("DomKelurahanId")
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<string>("DomKodePos")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("DomRT")
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.Property<string>("DomRW")
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<string>("GolonganDarah")
                        .HasMaxLength(4)
                        .HasColumnType("varchar(4)");

                    b.Property<bool>("IsNew")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("JabatanId")
                        .HasColumnType("char(36)");

                    b.Property<string>("JurusanSekolah")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<bool>("Kelamin")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("KelurahanId")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<string>("KodePos")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Nama")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("NamaSekolah")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("NoBPJS")
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.Property<string>("NoBPJSK")
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.Property<string>("NoKK")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("NoKTP")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<string>("NoNPWP")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("NoRekening")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("NoSIM")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<int>("PendidikanId")
                        .HasColumnType("int");

                    b.Property<string>("RT")
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.Property<string>("RW")
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.Property<string>("StatusBPJS")
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.Property<int>("StatusLamaranId")
                        .HasColumnType("int");

                    b.Property<string>("Tanggungan")
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)");

                    b.Property<string>("Telp")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("TempatLahir")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateOnly?>("TglAkhirSIM")
                        .HasColumnType("date");

                    b.Property<DateOnly?>("TglLahir")
                        .IsRequired()
                        .HasColumnType("date");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("PelamarId");

                    b.HasIndex("AgamaId");

                    b.HasIndex("BidangId");

                    b.HasIndex("DomKelurahanId");

                    b.HasIndex("EventId");

                    b.HasIndex("JabatanId");

                    b.HasIndex("KelurahanId");

                    b.HasIndex("PendidikanId");

                    b.HasIndex("StatusLamaranId");

                    b.ToTable("pelamar");
                });

            modelBuilder.Entity("PjlpCore.Entity.Pendidikan", b =>
                {
                    b.Property<int>("PendidikanID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NamaPendidikan")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("PendidikanID");

                    b.ToTable("pendidikan");
                });

            modelBuilder.Entity("PjlpCore.Entity.Persyaratan", b =>
                {
                    b.Property<int>("PersyaratanID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NamaPersyaratan")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("PersyaratanID");

                    b.ToTable("persyaratan");
                });

            modelBuilder.Entity("PjlpCore.Entity.Provinsi", b =>
                {
                    b.Property<string>("ProvinsiID")
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.Property<string>("HcKey")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("KodeNegara")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.Property<string>("Latitude")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Longitude")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("NamaProvinsi")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("ProvinsiID");

                    b.ToTable("provinsi");
                });

            modelBuilder.Entity("PjlpCore.Entity.Status", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NamaStatus")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("StatusId");

                    b.ToTable("status");
                });

            modelBuilder.Entity("PjlpCore.Entity.StatusKawin", b =>
                {
                    b.Property<int>("StatusKawinID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NamaStatus")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("StatusKawinID");

                    b.ToTable("statuskawin");
                });

            modelBuilder.Entity("PjlpCore.Entity.Tupoksi", b =>
                {
                    b.Property<Guid>("TupoksiID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("DivisiID")
                        .HasColumnType("char(36)");

                    b.Property<string>("NamaTupoksi")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("TupoksiID");

                    b.HasIndex("DivisiID");

                    b.ToTable("tupoksi");
                });

            modelBuilder.Entity("PjlpCore.Entity.Divisi", b =>
                {
                    b.HasOne("PjlpCore.Entity.Bidang", "Bidang")
                        .WithMany("Divisis")
                        .HasForeignKey("BidangID");

                    b.Navigation("Bidang");
                });

            modelBuilder.Entity("PjlpCore.Entity.Jabatan", b =>
                {
                    b.HasOne("PjlpCore.Entity.Bidang", "Bidang")
                        .WithMany("Jabatans")
                        .HasForeignKey("BidangID");

                    b.Navigation("Bidang");
                });

            modelBuilder.Entity("PjlpCore.Entity.Kabupaten", b =>
                {
                    b.HasOne("PjlpCore.Entity.Provinsi", "Provinsi")
                        .WithMany("Kabupatens")
                        .HasForeignKey("ProvinsiID");

                    b.Navigation("Provinsi");
                });

            modelBuilder.Entity("PjlpCore.Entity.Kecamatan", b =>
                {
                    b.HasOne("PjlpCore.Entity.Kabupaten", "Kabupaten")
                        .WithMany("Kecamatans")
                        .HasForeignKey("KabupatenID");

                    b.Navigation("Kabupaten");
                });

            modelBuilder.Entity("PjlpCore.Entity.Kelurahan", b =>
                {
                    b.HasOne("PjlpCore.Entity.Kecamatan", "Kecamatan")
                        .WithMany("Kelurahans")
                        .HasForeignKey("KecamatanID");

                    b.Navigation("Kecamatan");
                });

            modelBuilder.Entity("PjlpCore.Entity.Pegawai", b =>
                {
                    b.HasOne("PjlpCore.Entity.Agama", "Agama")
                        .WithMany("Pegawais")
                        .HasForeignKey("AgamaID");

                    b.HasOne("PjlpCore.Entity.Bidang", "Bidang")
                        .WithMany("Pegawais")
                        .HasForeignKey("BidangID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PjlpCore.Entity.JenisPegawai", "JenisPegawai")
                        .WithMany()
                        .HasForeignKey("JenisPegawaiID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PjlpCore.Entity.Kelurahan", "KelurahanDom")
                        .WithMany("Pegawais")
                        .HasForeignKey("KelurahanDomID");

                    b.HasOne("PjlpCore.Entity.Kelurahan", "Kelurahan")
                        .WithMany()
                        .HasForeignKey("KelurahanID");

                    b.HasOne("PjlpCore.Entity.Pendidikan", "Pendidikan")
                        .WithMany("Pegawais")
                        .HasForeignKey("PendidikanID");

                    b.HasOne("PjlpCore.Entity.StatusKawin", "StatusKawin")
                        .WithMany("Pegawais")
                        .HasForeignKey("StatusKawinID");

                    b.Navigation("Agama");

                    b.Navigation("Bidang");

                    b.Navigation("JenisPegawai");

                    b.Navigation("Kelurahan");

                    b.Navigation("KelurahanDom");

                    b.Navigation("Pendidikan");

                    b.Navigation("StatusKawin");
                });

            modelBuilder.Entity("PjlpCore.Entity.Pelamar", b =>
                {
                    b.HasOne("PjlpCore.Entity.Agama", "Agama")
                        .WithMany("Pelamars")
                        .HasForeignKey("AgamaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PjlpCore.Entity.Bidang", "Bidang")
                        .WithMany("Pelamars")
                        .HasForeignKey("BidangId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PjlpCore.Entity.Kelurahan", "KelurahanDom")
                        .WithMany("Pelamars")
                        .HasForeignKey("DomKelurahanId");

                    b.HasOne("PjlpCore.Entity.Event", "Event")
                        .WithMany("Pelamars")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PjlpCore.Entity.Jabatan", "Jabatan")
                        .WithMany("Pelamars")
                        .HasForeignKey("JabatanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PjlpCore.Entity.Kelurahan", "Kelurahan")
                        .WithMany()
                        .HasForeignKey("KelurahanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PjlpCore.Entity.Pendidikan", "Pendidikan")
                        .WithMany("Pelamars")
                        .HasForeignKey("PendidikanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PjlpCore.Entity.Status", "StatusLamaran")
                        .WithMany("Pelamars")
                        .HasForeignKey("StatusLamaranId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Agama");

                    b.Navigation("Bidang");

                    b.Navigation("Event");

                    b.Navigation("Jabatan");

                    b.Navigation("Kelurahan");

                    b.Navigation("KelurahanDom");

                    b.Navigation("Pendidikan");

                    b.Navigation("StatusLamaran");
                });

            modelBuilder.Entity("PjlpCore.Entity.Tupoksi", b =>
                {
                    b.HasOne("PjlpCore.Entity.Divisi", "Divisi")
                        .WithMany("Tupoksis")
                        .HasForeignKey("DivisiID");

                    b.Navigation("Divisi");
                });

            modelBuilder.Entity("PjlpCore.Entity.Agama", b =>
                {
                    b.Navigation("Pegawais");

                    b.Navigation("Pelamars");
                });

            modelBuilder.Entity("PjlpCore.Entity.Bidang", b =>
                {
                    b.Navigation("Divisis");

                    b.Navigation("Jabatans");

                    b.Navigation("Pegawais");

                    b.Navigation("Pelamars");
                });

            modelBuilder.Entity("PjlpCore.Entity.Divisi", b =>
                {
                    b.Navigation("Tupoksis");
                });

            modelBuilder.Entity("PjlpCore.Entity.Event", b =>
                {
                    b.Navigation("Pelamars");
                });

            modelBuilder.Entity("PjlpCore.Entity.Jabatan", b =>
                {
                    b.Navigation("Pelamars");
                });

            modelBuilder.Entity("PjlpCore.Entity.Kabupaten", b =>
                {
                    b.Navigation("Kecamatans");
                });

            modelBuilder.Entity("PjlpCore.Entity.Kecamatan", b =>
                {
                    b.Navigation("Kelurahans");
                });

            modelBuilder.Entity("PjlpCore.Entity.Kelurahan", b =>
                {
                    b.Navigation("Pegawais");

                    b.Navigation("Pelamars");
                });

            modelBuilder.Entity("PjlpCore.Entity.Pendidikan", b =>
                {
                    b.Navigation("Pegawais");

                    b.Navigation("Pelamars");
                });

            modelBuilder.Entity("PjlpCore.Entity.Provinsi", b =>
                {
                    b.Navigation("Kabupatens");
                });

            modelBuilder.Entity("PjlpCore.Entity.Status", b =>
                {
                    b.Navigation("Pelamars");
                });

            modelBuilder.Entity("PjlpCore.Entity.StatusKawin", b =>
                {
                    b.Navigation("Pegawais");
                });
#pragma warning restore 612, 618
        }
    }
}

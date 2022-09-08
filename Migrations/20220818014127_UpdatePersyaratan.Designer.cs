﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PjlpCore.Data;

#nullable disable

namespace PjlpCore.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220818014127_UpdatePersyaratan")]
    partial class UpdatePersyaratan
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PjlpCore.Entity.Agama", b =>
                {
                    b.Property<int>("AgamaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AgamaID"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NamaAgama")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("AgamaID");

                    b.ToTable("agama");
                });

            modelBuilder.Entity("PjlpCore.Entity.Bidang", b =>
                {
                    b.Property<Guid>("BidangID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("KepalaBidang")
                        .HasMaxLength(75)
                        .HasColumnType("character varying(75)");

                    b.Property<string>("NamaBidang")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("character varying(75)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("BidangID");

                    b.ToTable("bidang");
                });

            modelBuilder.Entity("PjlpCore.Entity.Divisi", b =>
                {
                    b.Property<Guid>("DivisiID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("BidangID")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NamaDivisi")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("DivisiID");

                    b.HasIndex("BidangID");

                    b.ToTable("divisi");
                });

            modelBuilder.Entity("PjlpCore.Entity.Jabatan", b =>
                {
                    b.Property<Guid>("JabatanID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("BidangID")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NamaJabatan")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("JabatanID");

                    b.HasIndex("BidangID");

                    b.ToTable("jabatan");
                });

            modelBuilder.Entity("PjlpCore.Entity.Kabupaten", b =>
                {
                    b.Property<string>("KabupatenID")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<bool>("IsKota")
                        .HasColumnType("boolean");

                    b.Property<string>("Latitude")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("Longitude")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("NamaKabupaten")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<string>("ProvinsiID")
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)");

                    b.HasKey("KabupatenID");

                    b.HasIndex("ProvinsiID");

                    b.ToTable("kabupaten");
                });

            modelBuilder.Entity("PjlpCore.Entity.Kecamatan", b =>
                {
                    b.Property<string>("KecamatanID")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<string>("KabupatenID")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<string>("Latitude")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("Longitude")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("NamaKecamatan")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.HasKey("KecamatanID");

                    b.HasIndex("KabupatenID");

                    b.ToTable("kecamatan");
                });

            modelBuilder.Entity("PjlpCore.Entity.Kelurahan", b =>
                {
                    b.Property<string>("KelurahanID")
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.Property<string>("KecamatanID")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<string>("Latitude")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("Longitude")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("NamaKelurahan")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.HasKey("KelurahanID");

                    b.HasIndex("KecamatanID");

                    b.ToTable("kelurahan");
                });

            modelBuilder.Entity("PjlpCore.Entity.Pendidikan", b =>
                {
                    b.Property<int>("PendidikanID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PendidikanID"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NamaPendidikan")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("PendidikanID");

                    b.ToTable("pendidikan");
                });

            modelBuilder.Entity("PjlpCore.Entity.Persyaratan", b =>
                {
                    b.Property<int>("PersyaratanID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PersyaratanID"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NamaPersyaratan")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("PersyaratanID");

                    b.ToTable("persyaratan");
                });

            modelBuilder.Entity("PjlpCore.Entity.Provinsi", b =>
                {
                    b.Property<string>("ProvinsiID")
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)");

                    b.Property<string>("HcKey")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("KodeNegara")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)");

                    b.Property<string>("Latitude")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("Longitude")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("NamaProvinsi")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("ProvinsiID");

                    b.ToTable("provinsi");
                });

            modelBuilder.Entity("PjlpCore.Entity.Tupoksi", b =>
                {
                    b.Property<Guid>("TupoksiID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("DivisiID")
                        .HasColumnType("uuid");

                    b.Property<string>("NamaTupoksi")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

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

            modelBuilder.Entity("PjlpCore.Entity.Tupoksi", b =>
                {
                    b.HasOne("PjlpCore.Entity.Divisi", "Divisi")
                        .WithMany("Tupoksis")
                        .HasForeignKey("DivisiID");

                    b.Navigation("Divisi");
                });

            modelBuilder.Entity("PjlpCore.Entity.Bidang", b =>
                {
                    b.Navigation("Divisis");

                    b.Navigation("Jabatans");
                });

            modelBuilder.Entity("PjlpCore.Entity.Divisi", b =>
                {
                    b.Navigation("Tupoksis");
                });

            modelBuilder.Entity("PjlpCore.Entity.Kabupaten", b =>
                {
                    b.Navigation("Kecamatans");
                });

            modelBuilder.Entity("PjlpCore.Entity.Kecamatan", b =>
                {
                    b.Navigation("Kelurahans");
                });

            modelBuilder.Entity("PjlpCore.Entity.Provinsi", b =>
                {
                    b.Navigation("Kabupatens");
                });
#pragma warning restore 612, 618
        }
    }
}
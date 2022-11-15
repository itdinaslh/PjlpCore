using Microsoft.EntityFrameworkCore;
using PjlpCore.Entity;

namespace PjlpCore.Data;

public class AppDbContext : DbContext {
    #nullable disable
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Agama> Agamas { get; set; }
    public DbSet<Bidang> Bidangs { get; set; }
    public DbSet<Provinsi> Provinsis { get; set; }
    public DbSet<Kabupaten> Kabupatens { get; set; }
    public DbSet<Kecamatan> Kecamatans { get; set; }
    public DbSet<Kelurahan> Kelurahans { get; set; }
    public DbSet<Divisi> Divisis { get; set; }
    public DbSet<Jabatan> Jabatans { get; set; }
    public DbSet<Pendidikan> Pendidikans { get; set; }
    public DbSet<StatusKawin> StatusKawins { get; set; }
    public DbSet<Persyaratan> Persyaratans { get; set; }
    public DbSet<Tupoksi> Tupoksis { get; set; }
    public DbSet<LokasiKerja> LokasiKerjas { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<FileType> FileTypes { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Pelamar> Pelamars { get; set; }
    public DbSet<Pegawai> Karyawans { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        builder.Entity<Pelamar>()
            .HasOne(p => p.KelurahanDom)
            .WithMany(k => k.Pelamars)
            .HasForeignKey(p => p.DomKelurahanId);        

        builder.Entity<Pegawai>()
            .HasOne(p => p.KelurahanDom)
            .WithMany(k => k.Pegawais)
            .HasForeignKey(p => p.KelurahanDomID);
    }
}
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
}
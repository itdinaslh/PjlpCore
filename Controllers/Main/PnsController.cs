using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PjlpCore.Entity;
using PjlpCore.Models;
using PjlpCore.Repository;

namespace PjlpCore.Controllers;

public class PnsController : Controller
{
    private readonly IPegawai pegRepo;
    private readonly IFilePegawai fileRepo;

    public PnsController(IPegawai pegRepo, IFilePegawai fileRepo)
    {
        this.pegRepo = pegRepo;
        this.fileRepo = fileRepo;
    }

    [HttpGet("/pegawai/pns/details")]
    public async Task<IActionResult> Details(Guid bid, Guid pid)
    {
        Pegawai? peg = await pegRepo.Pegawais.Where(p => p.PegawaiID == pid)
            .Include(a => a.Agama)
            .Include(b => b.Bidang)
            .Include(p => p.Pendidikan)
            .Include(k => k.Kelurahan!.Kecamatan.Kabupaten.Provinsi)
            .Include(d => d.KelurahanDom!.Kecamatan.Kabupaten.Provinsi)
            .Include(x => x.DetailAsn)
            .FirstOrDefaultAsync();

        List<FilePegawai>? filePegawai = await fileRepo.FilePegawais
            .Where(x => x.PegawaiID == pid)
            .Include(p => p.Persyaratan)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

        string pasfoto = "";

        if (filePegawai is not null)
        {
            if (filePegawai.Any(x => x.PersyaratanID == 2))
            {
#nullable disable
                pasfoto = filePegawai.Where(x => x.PersyaratanID == 2).Select(x => x.FilePath + "/" + x.FileName).FirstOrDefault();
#nullable enable
            }
        }

        if (peg is not null)
        {
            string? lahir = peg.TglLahir.ToString();

            if (peg.DetailAsn is null)
            {
                peg.DetailAsn = new DetailAsn();
            }

            return View("~/Views/Main/Pegawai/ASN/Details.cshtml", new PnsVM
            {
                Pegawai = peg,
                NamaAgama = peg.Agama.NamaAgama,
                NamaBidang = peg.Bidang.NamaBidang,
                NamaPendidikan = peg.Pendidikan!.NamaPendidikan,
                TanggalLahir = DateTime.Parse(lahir!).ToString("dd-MM-yyyy"),
                Kelurahan = peg.KelurahanID is null ? "" : peg.Kelurahan!.NamaKelurahan,
                Kecamatan = peg.KelurahanID is null ? "" : peg.Kelurahan!.Kecamatan.NamaKecamatan,
                KecID = peg.KelurahanID is null ? "" : peg.Kelurahan!.KecamatanID,
                KabID = peg.KelurahanID is null ? "" : peg.Kelurahan!.Kecamatan.KabupatenID,
                Kabupaten = peg.KelurahanID is null ? "" : peg.Kelurahan!.Kecamatan.Kabupaten.NamaKabupaten,
                ProvID = peg.KelurahanID is null ? "" : peg.Kelurahan!.Kecamatan.Kabupaten.ProvinsiID,
                Provinsi = peg.KelurahanID is null ? "" : peg.Kelurahan!.Kecamatan.Kabupaten.Provinsi.NamaProvinsi,
                KelurahanDom = peg.KelurahanDomID is null ? "" : peg.KelurahanDom!.NamaKelurahan,
                KecDomID = peg.KelurahanDomID is null ? "" : peg.KelurahanDom!.KecamatanID,
                KecamatanDom = peg.KelurahanDomID is null ? "" : peg.KelurahanDom!.Kecamatan.NamaKecamatan,
                KabDomID = peg.KelurahanDomID is null ? "" : peg.KelurahanDom!.Kecamatan.KabupatenID,
                KabupatenDom = peg.KelurahanDomID is null ? "" : peg.KelurahanDom!.Kecamatan.Kabupaten.NamaKabupaten,
                ProvDomID = peg.KelurahanDomID is null ? "" : peg.KelurahanDom!.Kecamatan.Kabupaten.ProvinsiID,
                ProvinsiDom = peg.KelurahanDomID is null ? "" : peg.KelurahanDom!.Kecamatan.Kabupaten.Provinsi.NamaProvinsi,
                Files = filePegawai,
                IsSame = peg.AddressIsSame,
            });
        }

        return NotFound();
    }
}

$(document).ready(function () {
    loadTable();    
});

function loadContent() {
    var bid = $('#sBidang option:selected').val();
    var jab = $('#sJabatan option:selected').val();   
    var stat = $('#sStatus option:selected').val();
    loadTable(bid, jab, stat);
}

$('#btnDownload').click(function () {
    var datahref = '/pelamar/excel/download/?isNew=false';

    $.ajax({
        url: datahref,
        type: 'GET',
        xhrFields: {
            responseType: 'blob'
        },
        beforeSend: function () {
            $("main").addClass("loadgue");
        },
        success: function (result) {
            $('main').removeClass("loadgue");

            var blob = result;
            var downloadUrl = URL.createObjectURL(blob);
            var a = document.createElement("a");
            a.href = downloadUrl;
            a.download = "Data-Pelamar-Lama.xlsx";
            document.body.appendChild(a);
            a.click();
        }
    });
});

function loadTable(bid, jab, stat) {    
    $('#tblData').DataTable().clear().destroy();
    $('#tblData').DataTable({
        processing: false,
        serverSide: true,
        lengthMenu: [5, 10, 25, 50],
        responsive: true,
        filter: true,        
        orderMulti: false,
        ajax: {
            url: "/api/pelamar/lama",
            type: "POST",
            dataType: "json",
            data: {
                bidang: bid,
                jabatan: jab,
                status: stat
            }
        },
        columns: [
            { data: "nama", name: "nama", autoWidth: true },
            { data: "noktp", name: "noktp", autoWidth: true },
            { data: "bidang", name: "bidang", autoWidth: true },
            { data: "jabatan", name: "jabatan", autoWidth: true },
            { data: "usia", name: "usia", autoWidth: true, sortable: false },
            { data: "status", name: "status", autoWidth: true },
            {
                render: function (data, type, row) { return "<a class='btn btn-sm btn-outline-success mr-2' href='/pelamar/details/?bid=" + row.bidangID + "&pid=" + row.pelamarId + "'><i class='fal fa-edit'></i> Detail</a>" }
            }
        ],
        fnRowCallback: function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
            if (aData.blacklist == true) {
                $('td', nRow).addClass('bg-danger-200');
            }
            if (aData.umur > 55 && aData.isk2 == false) {
                $('td', nRow).addClass('bg-warning-200');
            }
            if (aData.isk2 == true && aData.blacklist != true) {
                $('td', nRow).addClass('bg-info-200');
            }
        }
    });
}
$(document).ready(function () {
    loadTable();
});

function loadContent() {
    loadTable();
}

function loadTable() {
    $('#tblDivisi').DataTable().clear().destroy();
    $('#tblDivisi').DataTable({
        processing: false,
        serverSide: true,
        lengthMenu: [5,10,25,50],
        filter: true,
        stateSave: true,
        orderMulti: false,
        ajax: {
            url: "/api/master/divisi",
            type: "POST",
            dataType: "json"
        },
        columns: [
            { data: "divisiID", name: "divisiID", autoWidth: true },
            { data: "namaDivisi", name: "namaDivisi", autoWidth: true },
            { data: "namaBidang", name: "namaBidang", autoWidth: true },
        ],
        order: [[0, "desc"]]
    })
}

$(document).on('shown.bs.modal', function () {
    $('#sBidang').select2({
        placeholder: 'Pilih Bidang...',
        dropdownParent: $('#myModal'),
        allowClear: true,
        ajax: {
            url: "/api/master/bidang/search",
            contentType: "application/json; charset=utf-8",
            data: function (params) {
                var query = {
                    term: params.term,
                };
                return query;
            },
            processResults: function (result) {
                return {
                    results: $.map(result, function (item) {
                        return {
                            text: item.namaBidang,
                            id: item.id
                        }
                    })
                }
            },
            cache: true
        }
    });
});
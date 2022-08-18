$(document).ready(function () {
    loadTable();
});

function loadContent() {
    loadTable();
}

function loadTable() {
    $('#tblTupoksi').DataTable().clear().destroy();
    $('#tblTupoksi').DataTable({
        processing: false,
        serverSide: true,
        lengthMenu: [5,10,25,50],
        filter: true,
        stateSave: true,
        orderMulti: false,
        ajax: {
            url: "/api/master/tupoksi",
            type: "POST",
            dataType: "json"
        },
        columns: [
            { data: "tupoksiID", name: "TupoksiID", autoWidth: true },
            { data: "namaTupoksi", name: "namaTupoksi", autoWidth: true },
            { data: "namaDivisi", name: "namaDivisi", autoWidth: true },
            {
                render: function (data, type, row) { return "<button type='button' class='btn btn-sm btn-outline-success mr-2 showMe' style='width:100%;' data-href='/master/tupoksi/edit/?tupoksiId=" + row.tupoksiID + "'><i class='fal fa-edit'></i> Edit</button>" }
            }
        ],
        order: [[0, "desc"]]
    })
}

$(document).on('shown.bs.modal', function () {
    $('#sDivisi').select2({
        placeholder: 'Pilih Divisi...',
        dropdownParent: $('#myModal'),
        allowClear: true,
        ajax: {
            url: "/api/master/divisi/search",
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
                            text: item.namaDivisi,
                            id: item.id
                        }
                    })
                }
            },
            cache: true
        }
    });
});
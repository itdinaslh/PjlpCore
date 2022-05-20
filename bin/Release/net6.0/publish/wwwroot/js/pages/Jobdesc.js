$(document).ready(function () {
    loadTable();
});

function loadContent() {
    loadTable();
}

function loadTable() {
    $('#tblJob').DataTable().clear().destroy();
    $('#tblJob').DataTable({
        processing: true,
        serverSide: true,
        lengthMenu: [5, 10, 15],
        filter: true,
        orderMulti: false,
        ajax: {
            url: "/api/job",
            type: "POST",
            dataType: "json"
        },
        columns: [
            { "data": "jobID", "name": "jobID" },
            { "data": "jobName", "name": "jobName" },
            { "data": "divisionName", "name": "divisionName" },
            {
                "render": function (data, type, row)
                { return "<button class='btn btn-sm btn-outline-success mr-2 showMe' style='width:100%' data-href='/job/edit/?jobID=" + row.jobID + "'>Edit</button>"; }
            }
        ],
        order: [[0, "desc"]]
    });
}

$(document).on('shown.bs.modal', function () {
    $('#sDiv').select2({
        placeholder: 'Pilih Divisi...',
        dropdownParent: $('#myModal'),
        allowClear: true,
        ajax: {
            url: "/api/division/searchdiv",
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
                            text: item.namadiv,
                            id: item.id
                        }
                    })
                }
            },
            cache: true
        }
    });
});

function showSuccessMessage() {
    Swal.fire(
        {
            position: 'top-end',
            type: 'success',
            title: 'Data berhasil disimpan!',
            showConfirmButton: false,
            timer: 1000
        }).then(function () {
            loadContent();
        });
}
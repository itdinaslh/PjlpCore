$(document).ready(function () {
    $('.dtpicker').datepicker({
        format: 'dd-mm-yyyy',
        orientation: 'bottom',
        todayHighlight: true,
    });
    $('.clockpicker').clockpicker()
        .find('input').change(function () {
            console.log(this.value);
    });
});
var todayDate = moment().startOf('day');
var YM = todayDate.format('YYYY-MM');
var YESTERDAY = todayDate.clone().subtract(1, 'day').format('YYYY-MM-DD');
var TODAY = todayDate.format('YYYY-MM-DD');
var TOMORROW = todayDate.clone().add(1, 'day').format('YYYY-MM-DD');

document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');

    var calendar = new FullCalendar.Calendar(calendarEl,
        {
            plugins: ['dayGrid', 'list', 'timeGrid', 'interaction', 'bootstrap'],
            //timeZone: 'Asia/Jakarta',
            locale: 'id',
            themeSystem: 'bootstrap',
            //dateAlignment: "month", //week, month
            //buttonText:
            //{today: 'today',month: 'month',week: 'week',day: 'day',list: 'list'},
            eventTimeFormat:
            {
                hour: 'numeric',
                minute: '2-digit',
                meridiem: 'short'
            },
            //navLinks: true,
            header:
            {
                //left: 'prev,next today addEventButton',
                left: '',
                center: 'title',
                right : ''
                //right: 'dayGridMonth,timeGridWeek,timeGridDay,listWeek'
            },
            footer:
            {
                left: '',
                center: '',
                right: ''
            },
            height: 700,
            editable: true,
            eventLimit: true, // allow "more" link when too many events
            views:
            {
                sevenDays:
                {
                    type: 'agenda',
                    buttonText: '7 Days',
                    visibleRange: function (currentDate) {
                        return {
                            start: currentDate.clone().subtract(2, 'days'),
                            end: currentDate.clone().add(5, 'days'),
                        };
                    },
                    duration:
                    {
                        days: 7
                    },
                    dateIncrement:
                    {
                        days: 1
                    },
                },
            },
            eventClick: function (info) {
                document.getElementById("Judul").innerHTML = myFunction("Edit Aktivitas");
                function myFunction(name) {
                    return name;
                }
                console.log(info.event);
                //tanggal
                tgl = (info.event.start.getDate());
                tgl = (tgl < 10) ? "0" + tgl : tgl;
                bln = (info.event.start.getMonth() + 1);
                bln = (bln < 10) ? "0" + bln : bln;
                thn = (info.event.start.getFullYear());
                //waktu masuk
                jam = (info.event.start.getHours());
                jam = ("0" + jam).slice(-2);
                menit = (info.event.start.getMinutes());
                menit = ("0" + menit).slice(-2);
                masuk = (jam + ":" + menit);
                //waktu keluar
                d1 = (info.event.end);
                if (typeof d1 === 'object' && d1 !== null && 'getHours' in d1) {
                    jamkeluar = (info.event.end.getHours());
                    jamkeluar = ("0" + jamkeluar).slice(-2);
                    menitkeluar = (info.event.end.getMinutes());
                    menitkeluar = ("0" + menitkeluar).slice(-2);
                    keluar = (jamkeluar + ":" + menitkeluar);
                    $('#Keluar').val(keluar);
                }
                $('#ID').val(info.event.id);
                $('#Title').val(info.event.title);
                $('#Tanggal').val(tgl + "-" + bln + "-" + thn);
                $('#Masuk').val(masuk);
                $('#deskripsi').val(info.event.extendedProps.desciption);
                $('#calendarModal').modal();
            },
            dateClick: function (info) {
                if (info.dateStr <= TODAY) {
                    document.getElementById("Judul").innerHTML = myFunction("Tambah Aktivitas");
                    function myFunction(name) {
                        return name;
                    }
                    const date = (info.dateStr);
                    const [year, month, day] = date.split('-');
                    const result = [day, month, year].join('-');
                    $('#Tanggal').val(result);
                    $('#calendarModal').modal();
                } else {
                    alert('Isi Hari Sekarang dan Sebelumnya !!!');
                }
            },
            events: [
                {
                    id: 999,
                    title: 'Repeating Event',
                    start: YM + '-09 16:00:00',
                    end: YM + '-09T16:00:00',
                    desciption: 'Test deskripsi kegiatan',
                    className: "bg-danger border-danger text-white",
                },
                {
                    id: 1000,
                    title: 'Repeating Event',
                    start: YM + '-16T06:00:00',
                    end: YM + '-16T16:00:00',
                    desciption: 'Test deskripsi kegiatan',
                    className: "bg-info text-white border-primary"
                },
                {
                    title: 'Meeting',
                    start: TODAY + 'T10:30:00',
                    end: TODAY + 'T12:30:00',
                    desciption: 'Test deskripsi kegiatan',
                    className: "bg-warning text-white border-primary"
                }
            ],
            
            /*viewRender: function(view) {
                localStorage.setItem('calendarDefaultView',view.name);
                $('.fc-toolbar .btn-primary').removeClass('btn-primary').addClass('btn-outline-secondary');
            },*/

        });
    calendar.setOption('locale', 'id');
    calendar.render();
    
    function postData(method) {
        report = {
            id: $('#Tanggal').val(info.dateStr),
            tanggal: $('#Tanggal').val(info.dateStr),
            JamMasuk: $('#Tanggal').val(info.dateStr),
            JamKeluar: $('#Tanggal').val(info.dateStr),
            Kegiatan: $('#Tanggal').val(info.dateStr),
        }
        console.log(report);
    }
});

$('#calendarModal').on('hidden.bs.modal', function (e) {
    $(this)
        .find("input,textarea,select")
        .val('')
        .end()
        .find("input[type=checkbox], input[type=radio]")
        .prop("checked", "")
        .end();
}) 

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
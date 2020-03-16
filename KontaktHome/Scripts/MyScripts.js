
$('#sandbox-container input').datepicker({
    language: 'az',
    todayBtn: "linked",
    clearBtn: true,
    todayHighlight: true,
    autoclose: true
});
function loadActiveOrders() {
    alert("TEST");
    

   
}
var data = {

}
$('#tableActiveOrders').DataTable({
    "destroy": true,
    "ajax": {
        "url": "../Order/GetActiveOrders",
        "type": 'POST',
        "data": data,
        "dataSrc": ""
    },
    "columns": [
        {
            "data": "0"
        },
        {
            "data": "1"
        },
        {
            "data": "2"
        },
        {
            "data": "3"
        },
        {
            "data": "4"
        },
        {
            "data": "5"
        },
        {
            "data": "6"
        },
        {
            "data": "7"
        },
        {
            data: null, render: function () {
                return "<a href='#' id='btnOrderEdit' class='btn btn-info btn-sm m-1' role='button' ><i class='fas fa-pencil-alt'></i > Düzəliş</a><a href='#' id='btnFaktura' class='btn btn-success btn-sm m-1' role='button'><i class='fas fa-user'></i> Vizitor Təyin Et</a><a href='#' id='btnFaktura' class='btn btn-danger btn-sm m-1' role='button'><i class='far fa-trash-alt'></i> Sil</a>";
            }
        }
    ],
    "language": {
        "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Azerbaijan.json"
    },
    "columnDefs": [
        {
            "targets": [8],
            "searchable": false
        }
    ],
    "processing": true,
    "dom": 'Bfrtip',    
    "buttons": [{
        extend: 'print',
        text: 'Print',
        title: 'Aktiv Sifarişlər',
        customize: function (win) {
            $(win.document.body)
                .css('font-size', '12pt');
            $(win.document.body).find('table')
                .addClass('compact')
                .css('font-size', 'inherit');
            $(win.document.body).find('h1').css('text-align', 'center');
        }
    }

    ]
});
$(document).ready(function () {
    $('body').on('click', '#btnOrderEdit', function () {       
        var currow = $(this).closest('tr');
        var sira = currow.children('td:eq(2)').text();
        window.location.href = "../Order/EditOrder" + '?Sira=' + sira;
    });
});

$('#tableVisitorOrders').DataTable({
    "destroy": true,
    "ajax": {
        "url": "../Order/GetVisitorActiveOrders",
        "type": 'POST',
        "data": data,
        "dataSrc": ""
    },
    "columns": [
        {
            "data": "0"
        },
        {
            "data": "1"
        },
        {
            "data": "2"
        },
        {
            "data": "3"
        },
        {
            "data": "4"
        },
        {
            "data": "5"
        },
        {
            "data": "6"
        },
        {
            "data": "7"
        },
        {
            data: null, render: function () {
                return "<a href='#' id='btnOrderInfo' class='btn btn-info btn-sm m-1' role='button' ><i class='fas fa-pencil-alt'></i > Ətraflı</a><a href='#' id='btnFaktura' class='btn btn-success btn-sm m-1' role='button'><i class='fas fa-user'></i> Vizitor Təyin Et</a><a href='#' id='btnFaktura' class='btn btn-danger btn-sm m-1' role='button'><i class='far fa-trash-alt'></i> Sil</a>";
            }
        }
    ],
    "language": {
        "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Azerbaijan.json"
    },
    "columnDefs": [
        {
            "targets": [8],
            "searchable": false
        }
    ],
    "processing": true,
    "dom": 'Bfrtip',
    "buttons": [{
        extend: 'print',
        text: 'Print',
        title: 'Aktiv Sifarişlər',
        customize: function (win) {
            $(win.document.body)
                .css('font-size', '12pt');
            $(win.document.body).find('table')
                .addClass('compact')
                .css('font-size', 'inherit');
            $(win.document.body).find('h1').css('text-align', 'center');
        }
    }

    ]
});
$(document).ready(function () {
    $('body').on('click', '#btnOrderInfo', function () {
        var currow = $(this).closest('tr');
        var sira = currow.children('td:eq(2)').text();
        window.location.href = "../Order/OrderInfo" + '?Sira=' + sira;
    });

    cmbSelectVistorChange();
});

function cmbSelectVistorChange() {
    if ($('#chkboxSetVisitor').is(":checked"))
        $("#containerVisitor").show();
    else
        $("#containerVisitor").hide();
}


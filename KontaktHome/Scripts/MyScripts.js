
$('#sandbox-container input').datepicker({
    language: 'az',
    todayBtn: "linked",
    clearBtn: true,
    todayHighlight: true,
    autoclose: true
});
$(document).ready(function () {
    $('body').on('click', '#btnOrderEdit', function () {
        var currow = $(this).closest('tr');
        var sira = currow.children('td:eq(2)').text();
        window.location.href = "../Order/EditOrder" + '?Sira=' + sira;
    });
    $('body').on('click', '#btnAddVisitor', function () {
        var currow = $(this).closest('tr');
        var sira = currow.children('td:eq(2)').text();
        window.location.href = "../Order/EditOrder" + '?Sira=' + sira;
    });
    $('body').on('click', '#btnOrderDeactivate', function () {
        var currow = $(this).closest('tr');
        var sira = currow.children('td:eq(2)').text();
        window.location.href = "../Order/EditOrder" + '?Sira=' + sira;
    });
    $('body').on('click', '#addNewItem', function () {
        addItemToVisitForm();
    });
    $('body').on('click', '#btnOrderInfo', function () {
        var currow = $(this).closest('tr');
        var sira = currow.children('td:eq(2)').text();
        window.location.href = "../Order/OrderInfo" + '?Sira=' + sira;
    });
    $('body').on('click', '#btnStartVisit', function () {
        var currow = $(this).closest('tr');
        var sira = currow.children('td:eq(2)').text();
        window.location.href = "../Order/CustomerVisit" + '?Sira=' + sira;
    });
    $('body').on('click', '#btnOrderSearch', function () {
        getActiveOrdersWithParametr();
    });
    $('body').on('click', '#btnVisitorOrderSearch', function () {       
        getVisitorActiveOrdersWithParametr();
    });
    $('body').on('click', '#btnAcceptOrder', function () {
        var currow = $(this).closest('tr');
        var sira = currow.children('td:eq(2)').text();
        window.location.href = "../Order/AcceptOrder" + '?Sira=' + sira+'&'+'Status=1';
    });
    $('body').on('click', '#btnAcceptDesigner', function () {
        var currow = $(this).closest('tr');
        var sira = currow.children('td:eq(2)').text();
        window.location.href = "../Order/AcceptOrder" + '?Sira=' + sira +'&'+'Status=2';
    });
    $('body').on('click', '#btnStartDesign', function () {
        var currow = $(this).closest('tr');
        var sira = currow.children('td:eq(2)').text();
        window.location.href = "../Order/DesignerEdit" + '?Sira=' + sira;
    });
    $('body').on('click', '#btnUserInfo', function () {
        var currow = $(this).closest('tr');
        var sira = currow.children('td:eq(1)').text();
        window.location.href = "../Admin/EditUser" + '?userid=' + sira;
    });
    $('body').on('click', '#btnDesignerOrderSearch', function () {
        alert("test");
        getDesignerActiveOrdersWithParametr();
    });
    
    cmbSelectVistorChange();
    cmbSelectDesignerChange();
});
function cmbSelectVistorChange() {
    if ($('#chkboxSetVisitor').is(":checked"))
        $("#containerVisitor").show();
    else
        $("#containerVisitor").hide();
}
function cmbSelectDesignerChange() {
    if ($('#chkboxSetDesigner').is(":checked"))
        $("#containerDesigner").show();
    else
        $("#containerDesigner").hide();
}
(function ($) {
    $.fn.inputFilter = function (inputFilter) {
        return this.on("input keydown keyup mousedown mouseup select contextmenu drop", function () {
            if (inputFilter(this.value)) {
                this.oldValue = this.value;
                this.oldSelectionStart = this.selectionStart;
                this.oldSelectionEnd = this.selectionEnd;
            } else if (this.hasOwnProperty("oldValue")) {
                this.value = this.oldValue;
                this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
            } else {
                this.value = "";
            }
        });
    };
}(jQuery));
$(document).ready(function () {
    $("#itemCount").inputFilter(function (value) {
        return /^\d*$/.test(value);
    });
    $("#dimensionWide").inputFilter(function (value) {
        return /^-?\d*[.]?\d*$/.test(value);
    });
    $("#dimensionLemght").inputFilter(function (value) {
        return /^-?\d*[.]?\d*$/.test(value);
    });
    $("#dimensionHeight").inputFilter(function (value) {
        return /^-?\d*[.]?\d*$/.test(value);
    });
    $("#orderPrice").inputFilter(function (value) {
        return /^-?\d*[.]?\d*$/.test(value);
    });
});
function getActiveOrdersWithParametr() {

    var isAllValid = true;
    var firstdate = new Date($('#firstDate').val().trim());
    var lastdate = new Date($('#lastDate').val().trim());
    if (firstdate > lastdate) {
        isAllValid = false;
        Swal.fire(
            'Başlanğıc tarixi bitiş tarixindən böyük olabilməz!',
            '',
            'error'
        )
    }
    else if ($('#firstDate').val().trim() == "" & $('#lastDate').val().trim() == "") {
        isAllValid = false;
        Swal.fire(
            'Tarixlər boş olabilməz!',
            '',
            'error'
        )
    } 
    if (isAllValid==true) {
        var xcheck = document.getElementById("chkAllOrders").checked;
        var data = {
            firstDate: $('#firstDate').val().trim(),
            lastDate: $('#lastDate').val().trim(),
            allorders: xcheck

        }

        $('#tableActiveOrders').DataTable({
            "destroy": true,
            "ajax": {
                "url": "../Order/GetActiveOrdersWithParametr",
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
                    "data": "8"
                },
                {
                    "data": "9"
                },
                {
                    "data": "10"
                },
                {
                    data: null, render: function (data, type, full) {                        
                        return '<a href="/Order/EditOrder' + full[10] + '" class="btn btn-info btn-sm mt-1 mb-1"><i class="fas fa-pencil-alt"></i> Düzəliş Et/Vizitor Təyin Et</a> <a href="/Order/EditOrder' + full[10] + '" class="btn btn-danger btn-sm  mt-1 mb-1"><i class="far fa-trash-alt"></i> Bağla</a>';
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
                },
                {
                    "targets": [10],
                    "visible": false
                }
            ],

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
    }

}
function getVisitorActiveOrdersWithParametr() {

    var isAllValid = true;
    var firstdate = new Date($('#visitfirstDate').val().trim());
    var lastdate = new Date($('#visitlastDate').val().trim());
    if (firstdate > lastdate) {
        isAllValid = false;
        Swal.fire(
            'Başlanğıc tarixi bitiş tarixindən böyük olabilməz!',
            '',
            'error'
        )
    }
    else if ($('#visitfirstDate').val().trim() == "" & $('#visitlastDate').val().trim() == "") {
        isAllValid = false;
        Swal.fire(
            'Tarixlər boş olabilməz!',
            '',
            'error'
        )
    }
    if (isAllValid == true) {
        var xcheck = document.getElementById("visitchkAllOrders").checked;
        var data = {
            firstDate: $('#visitfirstDate').val().trim(),
            lastDate: $('#visitlastDate').val().trim(),
            allorders: xcheck

        }

        $('#tableVisitorOrders').DataTable({
            "destroy": true,
            "ajax": {
                "url": "../Order/GetVisitorActiveOrdersWithParametr",
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
                    "data": "8"
                },
                {
                    "data": "9"
                },
                {
                    data: null, render: function (data, type, full) {
                        return '<a href="/Order/OrderInfo' + full[9] + '" class="btn btn-info btn-sm mt-1 mb-1"><i class="fas fa-pencil-alt"></i> Ətraflı</a> <a href="/Order/AcceptOrder' + full[9] + '" class="btn btn-primary btn-sm  mt-1 mb-1"><i class="fas fa-check"></i> Qəbul Et</a> <a href="/Order/CustomerVisit' + full[9] + '" class="btn btn-success btn-sm  mt-1 mb-1"><i class="fas fa-user"></i> Ziyarət Et</a>';
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
                },
                {
                    "targets": [9],
                    "visible": false
                }
            ],

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
    }

}
function getDesignerActiveOrdersWithParametr() {

    var isAllValid = true;
    var firstdate = new Date($('#designfirstDate').val().trim());
    var lastdate = new Date($('#designlastDate').val().trim());
    if (firstdate > lastdate) {
        isAllValid = false;
        Swal.fire(
            'Başlanğıc tarixi bitiş tarixindən böyük olabilməz!',
            '',
            'error'
        )
    }
    else if ($('#designfirstDate').val().trim() == "" & $('#designlastDate').val().trim() == "") {
        isAllValid = false;
        Swal.fire(
            'Tarixlər boş olabilməz!',
            '',
            'error'
        )
    }
    if (isAllValid == true) {
        var xcheck = document.getElementById("designchkAllOrders").checked;
        var data = {
            firstDate: $('#designfirstDate').val().trim(),
            lastDate: $('#designlastDate').val().trim(),
            allorders: xcheck

        }

        $('#tableDesignerOrders').DataTable({
            "destroy": true,
            "ajax": {
                "url": "../Order/GetDesignerActiveOrdersWithParametr",
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
                    "data": "8"
                },
                {
                    "data": "9"
                },
                {
                    //data: null, render: function () {
                    //    return "<a href='#' id='btnOrderInfo' class='btn btn-info btn-sm m-1' role='button' ><i class='fas fa-pencil-alt'></i > Ətraflı</a><a href='#' id='btnAcceptDesigner' class='btn btn-primary btn-sm m-1' role='button'><i class='fas fa-check'></i> Qəbul Et</a><a href='#' id='btnStartDesign' class='btn btn-success btn-sm m-1' role='button'><i class='fas fa-user'></i> Dizayn Et</a> ";
                    //}
                    data: null, render: function (data, type, full) {
                        return '<a href="/Order/OrderInfo' + full[9] + '" class="btn btn-info btn-sm mt-1 mb-1"><i class="fas fa-pencil-alt"></i> Ətraflı</a> <a href="/Order/AcceptDesignerOrder' + full[9] + '" class="btn btn-primary btn-sm  mt-1 mb-1"><i class="fas fa-check"></i> Qəbul Et</a> <a href="/Order/DesignerEdit' + full[9] + '" class="btn btn-success btn-sm  mt-1 mb-1"><i class="fas fa-user"></i> Dizayn Et</a>';
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
                },
                {
                    "targets": [9],
                    "visible": false
                }
            ],

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
    }

}
$('#tableUsers').DataTable({
    "destroy": true,
    "ajax": {
        "url": "../Admin/GetUsers",
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
            data: null, render: function () {
                return "<a href='#' id='btnUserInfo' class='btn btn-info btn-sm m-1' role='button' ><i class='fas fa-pencil-alt'></i > Ətraflı</a>";
            }
        }
    ],
    "language": {
        "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Azerbaijan.json"
    },
    "columnDefs": [
        {
            "targets": [7],
            "searchable": false
        }
    ],

    "dom": 'Bfrtip',
    "buttons": [{
        extend: 'print',
        text: 'Print',
        title: 'istifadəçilər',
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
$('#tableDesignerOrders').DataTable({
    "destroy": true,
    "ajax": {
        "url": "../Order/GetDesignerActiveOrders",
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
            "data": "8"
        },
        {
            "data": "9"
        },
        {
            //data: null, render: function () {
            //    return "<a href='#' id='btnOrderInfo' class='btn btn-info btn-sm m-1' role='button' ><i class='fas fa-pencil-alt'></i > Ətraflı</a><a href='#' id='btnAcceptDesigner' class='btn btn-primary btn-sm m-1' role='button'><i class='fas fa-check'></i> Qəbul Et</a><a href='#' id='btnStartDesign' class='btn btn-success btn-sm m-1' role='button'><i class='fas fa-user'></i> Dizayn Et</a> ";
            //}
            data: null, render: function (data, type, full) {
                return '<a href="/Order/OrderInfo' + full[9] + '" class="btn btn-info btn-sm mt-1 mb-1"><i class="fas fa-pencil-alt"></i> Ətraflı</a> <a href="/Order/AcceptDesignerOrder' + full[9] + '" class="btn btn-primary btn-sm  mt-1 mb-1"><i class="fas fa-check"></i> Qəbul Et</a> <a href="/Order/DesignerEdit' + full[9] + '" class="btn btn-success btn-sm  mt-1 mb-1"><i class="fas fa-user"></i> Dizayn Et</a>';
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
        },
        {
            "targets": [9],
            "visible": false
        }
    ],

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
var data = {}
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
            "data": "8"
        },
        {
            "data": "9"
        },        
        {
            "data": "10"
        },
        {
            data: null, render: function (data, type, full) {
                //return "<a href='#' id='btnOrderEdit' class='btn btn-info btn-sm m-1' role='button' ><i class='fas fa-pencil-alt'></i > Düzəliş Et/Vizitor Təyin Et</a><a href='#' id='btnOrderDeactivate' class='btn btn-danger btn-sm m-1' role='button'><i class='far fa-trash-alt'></i> Bağla</a>";               
                return '<a href="/Order/EditOrder' + full[10] + '" class="btn btn-info btn-sm mt-1 mb-1"><i class="fas fa-pencil-alt"></i> Düzəliş Et/Vizitor Təyin Et</a> <a href="/Order/EditOrder' + full[10] + '" class="btn btn-danger btn-sm  mt-1 mb-1"><i class="far fa-trash-alt"></i> Bağla</a>';
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
        },
        {
            "targets": [10],
            "visible": false
        }
    ],

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
            "data": "8"
        },
        {
            "data": "9"
        },
        {
            //data: null, render: function () {
            //    return "<a href='#' id='btnOrderInfo' class='btn btn-info btn-sm m-1' role='button' ><i class='fas fa-pencil-alt'></i > Ətraflı</a><a href='#' id='btnAcceptOrder' class='btn btn-primary btn-sm m-1' role='button'><i class='fas fa-check'></i> Qəbul Et</a><a href='#' id='btnStartVisit' class='btn btn-success btn-sm m-1' role='button'><i class='fas fa-user'></i> Ziyarət Et</a> ";
            //}
            data: null, render: function (data, type, full) {
                return '<a href="/Order/OrderInfo' + full[9] + '" class="btn btn-info btn-sm mt-1 mb-1"><i class="fas fa-pencil-alt"></i> Ətraflı</a> <a href="/Order/AcceptOrder' + full[9] + '" class="btn btn-primary btn-sm  mt-1 mb-1"><i class="fas fa-check"></i> Qəbul Et</a> <a href="/Order/CustomerVisit' + full[9] + '" class="btn btn-success btn-sm  mt-1 mb-1"><i class="fas fa-user"></i> Ziyarət Et</a>';
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
        },
        {
            "targets": [9],
            "visible": false
        }
    ],

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
﻿
$("#sandbox-container input").datepicker({
    language: 'az',
    todayBtn: "linked",
    clearBtn: true,
    todayHighlight: true,
    autoclose: true
});
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
$("#itemCount").inputFilter(function (value) {
    return /^\d*$/.test(value);
});
$("#userEditdate").inputFilter(function (value) {
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
$("#orderFinalPrice").inputFilter(function (value) {
    return /^-?\d*[.]?\d*$/.test(value);
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
    $('body').on('click', '#btnAcceptOrder', function () {
        var currow = $(this).closest('tr');
        var sira = currow.children('td:eq(2)').text();
        window.location.href = "../Order/AcceptOrder" + '?Sira=' + sira + '&' + 'Status=1';
    });
    $('body').on('click', '#btnAcceptDesigner', function () {
        var currow = $(this).closest('tr');
        var sira = currow.children('td:eq(2)').text();
        window.location.href = "../Order/AcceptOrder" + '?Sira=' + sira + '&' + 'Status=2';
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
    $("#DomainUser").change(function () {
        var selectedItemVal = $("#DomainUser option:selected").attr("value");
        var selectedItemText = $("#DomainUser option:selected").text();
        $("#userUserName").val(selectedItemVal);
        $("#userUserDisplayName").val(selectedItemText);
        //alert(selectedItemVal);
        //alert(selectedItemText);
    });
    $("#userStoreCode").change(function () {
        var selectedItemVal = $("#userStoreCode option:selected").attr("value");
        var selectedItemText = $("#userStoreCode option:selected").text();
        $("#userStoreName").val(selectedItemText);
        //alert(selectedItemVal);
        //alert(selectedItemText);
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
function getActiveOrdersWithParametr() {

    var isAllValid = true;
    var firstdate = new Date($('#firstDate').val().trim());
    var lastdate = new Date($('#lastDate').val().trim());
    var deletedCheck = document.getElementById("chkClosedOrders").checked;
    var activeCheck = document.getElementById("chkActiveOrders").checked;
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
    if (deletedCheck == false && activeCheck == false) {
        isAllValid = false;
        Swal.fire(
            'Mütləq bir sifariş statusu seçilməlidir!',
            '',
            'error'
        )
    }
    if (isAllValid == true) {
        var data = {
            firstDate: $('#firstDate').val().trim(),
            lastDate: $('#lastDate').val().trim(),
            deletedOrders: deletedCheck,
            activeOrders: activeCheck,
            sellerCode: document.getElementById('saticilar').value,
            storeCode: document.getElementById('magazalar').value
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
                    data: null, render: function (data, type, full) {
                        if (full[8] == 'Aktiv') {
                            return '<a href="/Order/OrderInfo' + full[7] + '" class="btn btn-primary btn-sm mt-1 mb-1"><i class="fas fa-search"></i> Ətraflı</a> <a href="/Order/EditOrder' + full[7] + '" class="btn btn-info btn-sm mt-1 mb-1"><i class="fas fa-pencil-alt"></i> Düzəliş Et</a> <a href="/Order/CloseOrder' + full[7] + '" class="btn btn-danger btn-sm  mt-1 mb-1"><i class="far fa-trash-alt"></i> İmtina</a> <a href="/Order/VisitInfo' + full[7] + '" class="btn btn-warning btn-sm mt-1 mb-1"><i class="fas fa-search"></i> Vizit Məlumatları</a>';
                        }
                        else {
                            return '<a href="/Order/OrderInfo' + full[7] + '" class="btn btn-primary btn-sm mt-1 mb-1"><i class="fas fa-search"></i> Ətraflı</a><a href="/Order/VisitInfo' + full[7] + '" class="btn btn-warning btn-sm mt-1 mb-1"><i class="fas fa-search"></i> Vizit Məlumatları</a>';
                        }

                    }
                }
            ],
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Azerbaijan.json"
            },
            "columnDefs": [
                {
                    "targets": [9],
                    "searchable": false
                },
                {
                    "targets": [7],
                    "visible": false
                }
            ],
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
    var chkactive = document.getElementById("chkVisitActiveOrders").checked;
    var chkdelete = document.getElementById("chkVisitClosedOrders").checked;
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
    if (chkactive == false && chkdelete == false) {
        isAllValid = false;
        Swal.fire(
            'Mütləq bir sifariş statusu seçilməlidir!',
            '',
            'error'
        )
    }
    if (isAllValid == true) {

        var data = {
            firstDate: $('#visitfirstDate').val().trim(),
            lastDate: $('#visitlastDate').val().trim(),
            activeOrders: chkactive,
            deletedOrders: chkdelete
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
                    data: null, render: function (data, type, full) {
                        if (full[7] >= 2 && full[7] < 4) {
                            return '<a href="/Order/VisitInfo' + full[6] + '" class="btn btn-info btn-sm mt-1 mb-1"><i class="fas fa-pencil-alt"></i> Ətraflı</a> <a href="/Order/CustomerVisit' + full[6] + '" class="btn btn-success btn-sm  mt-1 mb-1"><i class="fas fa-user"></i> Vizit Başla</a> ';
                        }
                        else if (full[7] == 1) {
                            return '<a href="/Order/VisitInfo' + full[6] + '" class="btn btn-info btn-sm mt-1 mb-1"><i class="fas fa-pencil-alt"></i> Ətraflı</a> <a href="/Order/AcceptOrder' + full[6] + '" class="btn btn-primary btn-sm  mt-1 mb-1"><i class="fas fa-check"></i> Qəbul Et</a> ';
                        }
                        else if (full[7] == 4) { return '<a href="/Order/VisitInfo' + full[6] + '" class="btn btn-info btn-sm mt-1 mb-1"><i class="fas fa-pencil-alt"></i> Ətraflı</a>'; }
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
                    "targets": [7, 6],
                    "visible": false
                }
            ]
        });
    }

}
function getDesignerActiveOrdersWithParametr() {
    var isAllValid = true;
    var firstdate = new Date($('#designfirstDate').val().trim());
    var lastdate = new Date($('#designlastDate').val().trim());
    var chkactive = document.getElementById("chkDesignActiveOrders").checked;
    var chkdelete = document.getElementById("chkDesignClosedOrders").checked;
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

    if (chkactive == false && chkdelete == false) {
        isAllValid = false;
        Swal.fire(
            'Mütləq bir sifariş statusu seçilməlidir!',
            '',
            'error'
        )
    }
    if (isAllValid == true) {

        var data = {
            firstDate: $('#designfirstDate').val().trim(),
            lastDate: $('#designlastDate').val().trim(),
            activeOrders: chkactive,
            deletedOrders: chkdelete

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
                    data: null, render: function (data, type, full) {

                        if (full[7] == 1) {
                            return '<a href="/Order/VisitInfo' + full[6] + '" class="btn btn-info btn-sm mt-1 mb-1"><i class="fas fa-pencil-alt"></i> Ətraflı</a> <a href="/Order/AcceptDesignerOrder' + full[6] + '" class="btn btn-primary btn-sm  mt-1 mb-1"><i class="fas fa-check"></i> Qəbul Et</a>';
                        }
                        else if (full[7] >= 2 && full[7] < 4) {
                            return '<a href="/Order/VisitInfo' + full[6] + '" class="btn btn-info btn-sm mt-1 mb-1"><i class="fas fa-pencil-alt"></i> Ətraflı</a> <a href="/Order/DesignerEdit' + full[6] + '" class="btn btn-success btn-sm  mt-1 mb-1"><i class="fas fa-user"></i> Dizayn Et</a>';
                        }
                        else if (full[7] == 4) {
                            return '<a href="/Order/VisitInfo' + full[6] + '" class="btn btn-info btn-sm mt-1 mb-1"><i class="fas fa-pencil-alt"></i> Ətraflı</a>';
                        }
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
                    "targets": [7, 6],
                    "visible": false
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
            //data: null, render: function () {
            //    return "<a href='#' id='btnUserInfo' class='btn btn-info btn-sm m-1' role='button' ><i class='fas fa-pencil-alt'></i > Ətraflı</a> ";
            //}
            data: null, render: function (data, type, full) {
                return '<a href="/Admin/EditUser?userid=' + full[1] + '" class="btn btn-info btn-sm mt-1 mb-1"><i class="fas fa-search"></i> Ətraflı</a> <a href="/Admin/DeleteUser?userid=' + full[1] + '" class="btn btn-danger btn-sm  mt-1 mb-1"><i class="far fa-trash-alt"></i> Sil</a>  <a href="/Admin/UserRoles?UserID=' + full[1] + '" class="btn btn-warning btn-sm  mt-1 mb-1"><i class="fas fa-user-shield"></i> Səlahiyyətlər</a>';

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
            data: null, render: function (data, type, full) {

                if (full[7] == 1) {
                    return '<a href="/Order/VisitInfo' + full[6] + '" class="btn btn-info btn-sm mt-1 mb-1"><i class="fas fa-pencil-alt"></i> Ətraflı</a> <a href="/Order/AcceptDesignerOrder' + full[6] + '" class="btn btn-primary btn-sm  mt-1 mb-1"><i class="fas fa-check"></i> Qəbul Et</a>';
                }
                else if (full[7] >= 2 && full[7] < 4) {
                    return '<a href="/Order/VisitInfo' + full[6] + '" class="btn btn-info btn-sm mt-1 mb-1"><i class="fas fa-pencil-alt"></i> Ətraflı</a> <a href="/Order/DesignerEdit' + full[6] + '" class="btn btn-success btn-sm  mt-1 mb-1"><i class="fas fa-user"></i> Dizayn Et</a>';
                }
                else if (full[7] == 4) {
                    return '<a href="/Order/VisitInfo' + full[6] + '" class="btn btn-info btn-sm mt-1 mb-1"><i class="fas fa-pencil-alt"></i> Ətraflı</a>';
                }
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
            "targets": [7, 6],
            "visible": false
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
            data: null, render: function (data, type, full) {
                if (full[8] == 'Aktiv') {
                    return '<a href="/Order/OrderInfo' + full[7] + '" class="btn btn-primary btn-sm mt-1 mb-1"><i class="fas fa-search"></i> Ətraflı</a> <a href="/Order/EditOrder' + full[7] + '" class="btn btn-info btn-sm mt-1 mb-1"><i class="fas fa-pencil-alt"></i> Düzəliş Et</a> <a href="/Order/CloseOrder' + full[7] + '" class="btn btn-danger btn-sm  mt-1 mb-1"><i class="far fa-trash-alt"></i> İmtina</a> <a href="/Order/VisitInfo' + full[7] + '" class="btn btn-warning btn-sm mt-1 mb-1"><i class="fas fa-search"></i> Vizit Məlumatları</a>';
                }
                else {
                    return '<a href="/Order/OrderInfo' + full[7] + '" class="btn btn-primary btn-sm mt-1 mb-1"><i class="fas fa-search"></i> Ətraflı</a> <a href="/Order/EditOrder' + full[7] + '" class="btn btn-info btn-sm mt-1 mb-1"><i class="fas fa-pencil-alt"></i> Düzəliş Et</a> <a href="/Order/VisitInfo' + full[7] + '" class="btn btn-warning btn-sm mt-1 mb-1"><i class="fas fa-search"></i> Vizit Məlumatları</a>';
                }

            }
        }
    ],
    "language": {
        "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Azerbaijan.json"
    },
    "columnDefs": [
        {
            "targets": [9],
            "searchable": false
        },
        {
            "targets": [7],
            "visible": false
        }
    ],
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
            data: null, render: function (data, type, full) {
                if (full[7] >= 2 && full[7] < 4) {
                    return '<a href="/Order/VisitInfo' + full[6] + '" class="btn btn-info btn-sm mt-1 mb-1"><i class="fas fa-pencil-alt"></i> Ətraflı</a> <a href="/Order/CustomerVisit' + full[6] + '" class="btn btn-success btn-sm  mt-1 mb-1"><i class="fas fa-user"></i> Vizit Başla</a> ';
                }
                else if (full[7] == 1) {
                    return '<a href="/Order/VisitInfo' + full[6] + '" class="btn btn-info btn-sm mt-1 mb-1"><i class="fas fa-pencil-alt"></i> Ətraflı</a> <a href="/Order/AcceptOrder' + full[6] + '" class="btn btn-primary btn-sm  mt-1 mb-1"><i class="fas fa-check"></i> Qəbul Et</a> ';
                }
                else if (full[7] == 4) { return '<a href="/Order/VisitInfo' + full[6] + '" class="btn btn-info btn-sm mt-1 mb-1"><i class="fas fa-pencil-alt"></i> Ətraflı</a>'; }
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
            "targets": [7, 6],
            "visible": false
        }
    ]
});
function getToday() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1;
    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd;
    }
    if (mm < 10) {
        mm = '0' + mm;
    }
    today = mm + '/' + dd + '/' + yyyy;
    return today;
}
var userroleid = $("#roleUSERID").val();
$('#tableRoles').DataTable({
    "destroy": true,
    "ajax": {
        "url": "../Admin/GetUserRoles",
        "type": 'POST',
        "data": { userid: userroleid },
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
            data: null, render: function (data, type, full) {
                //return '<a href="/Admin/DeleteUserRole?roleid=' + full[3] + '" type="submit" class="btn btn-danger btn-sm  mt-1 mb-1"><i class="far fa-trash-alt"></i> Sil</a>';
                return '<button type="submit" value="' + full[3] + '" name="roleMappingID" class="btn btn-danger btn-sm  mt-1 mb-1"><i class="far fa-trash-alt"></i> Sil</button >';
            }
        }
    ],
    "language": {
        "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Azerbaijan.json"
    },
    "columnDefs": [
        {
            "targets": [5],
            "searchable": false
        },
        {
            "targets": [3],
            "visible": false
        }
    ]
});
$('#tableStores').DataTable({
    "destroy": true,
    "ajax": {
        "url": "../Admin/GetStores",
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
            //data: null, render: function () {
            //    return "<a href='#' id='btnUserInfo' class='btn btn-info btn-sm m-1' role='button' ><i class='fas fa-pencil-alt'></i > Ətraflı</a> ";
            //}
            data: null, render: function (data, type, full) {
                if (full[3] == 'Aktiv') {
                    return `<button type="button" onclick="fancyConfirm('${full[0]}','${full[1]} ','${full[3]}');" class="btn btn-danger btn-sm  mt-1 mb-1"><i class="far fa-trash-alt"></i> Deaktiv Et</button>`;
                }
                else { return `<button type="button" onclick="fancyConfirm('${full[0]}','${full[1]} ','${full[3]}');" class="btn btn-primary btn-sm  mt-1 mb-1"><i class="far fa-trash-alt"></i> Aktiv Et</button>`; }

                //return '<a href="/Admin/DeleteStore?storeid=' + full[0] + '" class="btn btn-danger btn-sm  mt-1 mb-1"><i class="far fa-trash-alt"></i> Sil</a>';
            }
        }
    ],
    "language": {
        "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Azerbaijan.json"
    },
    "columnDefs": [
        {
            "targets": [3],
            "searchable": false
        },
        {
            "targets": [0],
            "visible": false
        }
    ]
});
function fancyConfirm(storeid, storecode, status) {
    if (status == 'Aktiv') {
        $.fancyConfirm({
            title: storecode + " kodlu mağaza deaktiv ediləcək!",
            message: "Davam etmək istəyirisinizmi?",
            okButton: 'Bəli',
            noButton: 'Xeyr',
            callback: function (value) {
                if (value) {
                    var url = '/Admin/DeaktivateStore?storeid=' + storeid;
                    window.location = url;
                } else {

                }
            }
        });
    }
    else {
        $.fancyConfirm({
            title: storecode + " kodlu mağaza aktiv ediləcək!",
            message: "Davam etmək istəyirisinizmi?",
            okButton: 'Bəli',
            noButton: 'Xeyr',
            callback: function (value) {
                if (value) {
                    var url = '/Admin/ActivateStore?storeid=' + storeid;
                    window.location = url;
                } else {

                }
            }
        });
    }


}
$.fancyConfirm = function (opts) {
    opts = $.extend(true, {
        title: 'Are you sure?',
        message: '',
        okButton: 'OK',
        noButton: 'Cancel',
        callback: $.noop
    }, opts || {});

    $.fancybox.open({
        type: 'html',
        src:
            '<div class="fc-content">' +
            '<h4>' + opts.title + '</h4>' +
            '<p>' + opts.message + '</p>' +
            '<p class="float-right">' +
            '<button data-value="0" data-fancybox-close class="btn btn-danger mr-3">' + opts.noButton + '</button>' +
            '<button data-value="1" data-fancybox-close class="btn btn-primary">' + opts.okButton + '</button>' +
            '</p>' +
            '</div>',
        opts: {
            animationDuration: 350,
            animationEffect: 'material',
            modal: true,
            baseTpl:
                '<div class="fancybox-container fc-container" role="dialog" tabindex="-1">' +
                '<div class="fancybox-bg"></div>' +
                '<div class="fancybox-inner">' +
                '<div class="fancybox-stage"></div>' +
                '</div>' +
                '</div>',
            afterClose: function (instance, current, e) {
                var button = e ? e.target || e.currentTarget : null;
                var value = button ? $(button).data('value') : 0;

                opts.callback(value);
            }
        }
    });
}

//datattable serverside processing
//$("#tableProducts").DataTable({   
//    "serverSide": true, // for process server side  
//    "filter": true, // this is for disable filter (search box)  
//    "orderMulti": false, // for disable multiple column at once  
//    "pageLength": 1,

//    "ajax": {
//        "url": "../Order/GetProducts",
//        "type": "POST",
//        "datatype": "json"
//    },

//    "columns": [
//        { "data": "product_code" },
//        { "data": "product_name" },
//        { "data": "product_price" },
//        { "data": "product_quantity" },      
//        {
//            "render": function (data, type, full, meta) { return '<a class="btn btn-info" href="/Demo/Edit/' + full.product_code + '">Edit</a>'; }
//        },
//        {
//            data: null, render: function (data, type, row) {
//                return "<a href='#' class='btn btn-danger' onclick=DeleteData('" + row.product_code + "'); >Delete</a>";
//            }
//        }

//    ],    
//    "language": {
//        "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Azerbaijan.json"
//    }

//});
function LoadProducts() {
    $('#tableProducts').DataTable({
        "destroy": "true",       
        "ajax": {
            "url": "../Order/GetProducts",
            "cache": "false",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "product_code" },
            { "data": "product_name" },
            { "data": "product_price" },
            { "data": "product_quantity" },
            {
                "render": function (data, type, full, meta) { return '<button type="button" class="btn btn-primary btn-sm" onclick="addfaqs(\'' + full.product_code + '\',\'' + full.product_name + '\',\'' + full.product_price + '\')">Əlavə Et</i></button>'; }
            }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Azerbaijan.json"
        }
    });
}


function calculateProduct(obj) {
    var currow = $(obj).closest('tr');   
    var price = currow.find('td:eq(2) input[type="text"]').val();
    var quamtity = currow.find('td:eq(3) input[type="text"]').val();
    var total = parseFloat(price) * parseFloat(quamtity);
    currow.find('td:eq(4)').html(total.toFixed(2));
    calculateTotal();
}
function calculateTotal() {
    var Cemi = 0.00;
    let table = document.querySelector("#faqs tbody");
    if (table.rows.length > 0) {
        for (let row of table.rows) {
            Cemi = Cemi + parseFloat(row.cells[4].innerText);
        }
    }
    var Faiz = document.getElementById("chargeValue").value;
    var CemFaizi = (Cemi * Faiz) / 100;
    var Total = Cemi + CemFaizi;
    $('#productTotal').html(Cemi.toFixed(2));
    $('#productSum').html(Total.toFixed(2));
}
var deletedItems = [];
function removeproduct(obj) {
    var currow = $(obj).closest('tr');
    var row_index = currow.index();
    var id = document.getElementById("tableProd").rows[row_index].cells[5].innerHTML;   
    if (id == 0) {        
        currow.remove();
    }
    else {
        deletedItems.push(id);
        currow.remove();
        //var form = $('#__AjaxAntiForgeryForm');
        //var token = $('input[name="__RequestVerificationToken"]', form).val();     
        //$.ajax({
        //    url: '/Order/DeleteProduct',
        //    type: "POST",
        //    data: {
        //        __RequestVerificationToken: token,
        //        id: id
        //    },
        //    success: function (d) {               
        //        if (d.status == true) {                   
        //            Swal.fire('Qeyd Silindi', '', 'success');     
        //            currow.remove();
        //        }
        //        else {
        //            alert('Qeyd silinmədi.');
        //            this.disabled = false;
        //        }
        //    },
        //    error: function () {
        //        alert('Error. Please try again.');
        //    }
        //});
    }
    calculateTotal();
}
$("#productCharge").change(function () {
    var selectedItemVal = $("#productCharge option:selected").attr("value");
    if (selectedItemVal == "") {
        selectedItemVal = 0;
    }
    $("#chargeValue").val(selectedItemVal);
    calculateTotal();
});

$('#recipeSave').click(function () {
    var recipeItems = [];
    var isAllValid = true;
    let table = document.querySelector("#faqs tbody");
    if (table.rows.length == 0) {
        $('#recipeitemAlert').replaceWith('<div id="recipeitemAlert" class="alert alert-danger col-md-12" role="alert">Stok seçilməyib.</div>');
        isAllValid = false;
    }
    else {
        $('#recipeitemAlert').replaceWith('<div id="recipeitemAlert"></div>');
        isAllValid = true;
    }
    if (isAllValid == true) {
        for (let row of table.rows) {
            recipeItems.push({
                ProductCode: row.cells[0].innerText,
                ProductName: row.cells[1].innerText,
                ProductPrice: row.cells[2].children[0].value,
                ProductQuantity: row.cells[3].children[0].value,
                ProductTotal: row.cells[4].innerText,
                ProductCharges: document.getElementById("chargeValue").value,
                Id: row.cells[5].innerText,
                OrderId: document.getElementById("orderNo").innerText,
                VisitId: document.getElementById("visitNo").innerText
            });
        }
        if (recipeItems.length > 0) {
            var form = $('#__AjaxAntiForgeryForm');
            var token = $('input[name="__RequestVerificationToken"]', form).val();
            var orderNo = document.getElementById("orderNo").innerText;
            var visitNo = document.getElementById("visitNo").innerText;
            var data = {
                order_No: orderNo,
                visit_No: visitNo,
                recipe_Items: recipeItems,
                deleted_items: deletedItems
            };
            $.ajax({
                url: '/Order/SaveRecipe',
                type: "POST",
                data: {
                    __RequestVerificationToken: token,
                    data: data
                },
                success: function (d) {
                    
                    if (d.status == true) {                        
                        Swal.fire('Qeyd Tamamlandı', '', 'success');                        
                        window.location.href = d.Url + d.link;
                    }
                    else {
                        var errortext = '';
                        for (var i = 0; i < d.errors.length; i++) {
                            errortext+=d.errors[i]+"<br>"
                        }
                        $('#errors').replaceWith('<div id="errors" class="alert alert-danger col-md-12" role="alert">' + errortext + '</div>');
                        Swal.fire('Xəta başverdi', ''+errortext+'', 'error');
                        this.disabled = false;
                    }
                },
                error: function () {
                    alert('Error. Please try again.');
                }
            });
        }
    }
});

function acceptOrder(orderId, visitId) {
    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();
    $.ajax({
        url: '/Order/SaveToMikro',
        type: "POST",
        data: {
            __RequestVerificationToken: token,
            orderid: orderId,
            visitid:visitId
        },
        success: function (d) {
            if (d.status == true) {
                Swal.fire('Qeyd Tamamlandı', '', 'success');
                window.location.href = d.Url + d.link;
            }
            else {                           
                var errortext = '';
                for (var i = 0; i < d.errors.length; i++) {
                    errortext += d.errors[i] + "<br>"
                }
                $('#errors').replaceWith('<div id="errors" class="alert alert-danger col-md-12" role="alert">' + errortext + '</div>');
                Swal.fire('Xəta başverdi', '' + errortext + '', 'error');
                this.disabled = false;
            }
        },
        error: function () {
            alert('Error. Please try again.');
        }
    });
}

function finishOrder(orderId) {
    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();
    $.ajax({
        url: '/Order/FinishOrder',
        type: "POST",
        data: {
            __RequestVerificationToken: token,
            orderid: orderId          
        },
        success: function (d) {
            if (d.status == true) {
                Swal.fire('Siafriş Tamamlandı', '', 'success');
                window.location.href = d.Url;
            }
            else {
                var errortext = '';
                for (var i = 0; i < d.errors.length; i++) {
                    errortext += d.errors[i] + "<br>"
                }
                $('#errors').replaceWith('<div id="errors" class="alert alert-danger col-md-12" role="alert">' + errortext + '</div>');
                Swal.fire('Xəta başverdi', '' + errortext + '', 'error');
                this.disabled = false;
            }
        },
        error: function () {
            alert('Error. Please try again.');
        }
    });
}


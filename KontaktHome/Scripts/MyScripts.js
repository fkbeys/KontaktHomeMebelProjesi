
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
    $("#DomainUser").change(function () {
        var selectedItemVal = $("#DomainUser option:selected").attr("value");
        var selectedItemText = $("#DomainUser option:selected").text();
        $("#userUserName").val(selectedItemVal);
        $("#userUserDisplayName").val(selectedItemText);
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
    if (isAllValid==true) {       
        var data = {
            firstDate: $('#firstDate').val().trim(),
            lastDate: $('#lastDate').val().trim(),
            deletedOrders: deletedCheck,
            activeOrders: activeCheck ,
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
                    "data": "10"
                },
                {
                    //data: null, render: function () {
                    //    return "<a href='#' id='btnOrderInfo' class='btn btn-info btn-sm m-1' role='button' ><i class='fas fa-pencil-alt'></i > Ətraflı</a><a href='#' id='btnAcceptDesigner' class='btn btn-primary btn-sm m-1' role='button'><i class='fas fa-check'></i> Qəbul Et</a><a href='#' id='btnStartDesign' class='btn btn-success btn-sm m-1' role='button'><i class='fas fa-user'></i> Dizayn Et</a> ";
                    //}
                    data: null, render: function (data, type, full) {

                        if (full[10] == 1) {
                            return '<a href="/Order/VisitInfo' + full[9] + '" class="btn btn-info btn-sm mt-1 mb-1"><i class="fas fa-pencil-alt"></i> Ətraflı</a> <a href="/Order/AcceptDesignerOrder' + full[9] + '" class="btn btn-primary btn-sm  mt-1 mb-1"><i class="fas fa-check"></i> Qəbul Et</a>';
                        }
                        else if (full[10] >= 2 && full[10] < 4) {
                            return '<a href="/Order/VisitInfo' + full[9] + '" class="btn btn-info btn-sm mt-1 mb-1"><i class="fas fa-pencil-alt"></i> Ətraflı</a> <a href="/Order/DesignerEdit' + full[9] + '" class="btn btn-success btn-sm  mt-1 mb-1"><i class="fas fa-user"></i> Dizayn Et</a>';
                        }
                        else if (full[10] == 4) {
                            return '<a href="/Order/VisitInfo' + full[9] + '" class="btn btn-info btn-sm mt-1 mb-1"><i class="fas fa-pencil-alt"></i> Ətraflı</a>';
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
                    "targets": [9, 10],
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
            //data: null, render: function () {
            //    return "<a href='#' id='btnUserInfo' class='btn btn-info btn-sm m-1' role='button' ><i class='fas fa-pencil-alt'></i > Ətraflı</a> ";
            //}
            data: null, render: function (data, type, full) {
                return '<a href="/Admin/EditUser?userid=' + full[1] + '" class="btn btn-info btn-sm mt-1 mb-1"><i class="fas fa-search"></i> Ətraflı</a> <a href="/Admin/DeleteUser?userid=' + full[1] +'" class="btn btn-danger btn-sm  mt-1 mb-1"><i class="far fa-trash-alt"></i> Sil</a>';            

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
            "data": "10"
        },
        {
            //data: null, render: function () {
            //    return "<a href='#' id='btnOrderInfo' class='btn btn-info btn-sm m-1' role='button' ><i class='fas fa-pencil-alt'></i > Ətraflı</a><a href='#' id='btnAcceptDesigner' class='btn btn-primary btn-sm m-1' role='button'><i class='fas fa-check'></i> Qəbul Et</a><a href='#' id='btnStartDesign' class='btn btn-success btn-sm m-1' role='button'><i class='fas fa-user'></i> Dizayn Et</a> ";
            //}
            data: null, render: function (data, type, full) {
               
                if (full[10] == 1) {
                    return '<a href="/Order/VisitInfo' + full[9] + '" class="btn btn-info btn-sm mt-1 mb-1"><i class="fas fa-pencil-alt"></i> Ətraflı</a> <a href="/Order/AcceptDesignerOrder' + full[9] + '" class="btn btn-primary btn-sm  mt-1 mb-1"><i class="fas fa-check"></i> Qəbul Et</a>';
                }
                else if (full[10] >= 2 && full[10] < 4) {
                    return '<a href="/Order/VisitInfo' + full[9] + '" class="btn btn-info btn-sm mt-1 mb-1"><i class="fas fa-pencil-alt"></i> Ətraflı</a> <a href="/Order/DesignerEdit' + full[9] + '" class="btn btn-success btn-sm  mt-1 mb-1"><i class="fas fa-user"></i> Dizayn Et</a>';
                }
                else if (full[10]==4) {
                    return '<a href="/Order/VisitInfo' + full[9] + '" class="btn btn-info btn-sm mt-1 mb-1"><i class="fas fa-pencil-alt"></i> Ətraflı</a>';
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
            "targets": [9,10],
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
            data: null, render: function (data, type, full) {                
                if (full[8]=='Aktiv') {
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
            "targets": [7,6],
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
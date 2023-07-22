var dataTable;

$(document).ready(function () {
    const url = window.location.search;

    if (url.includes("inprocess")) {
        loadDataTable("inprocess");
    }
    else if (url.includes("pending")) {
        loadDataTable("pending");
    }
    else if (url.includes("completed")) {
        loadDataTable("completed");
    }
    else if (url.includes("approved")) {
        loadDataTable("approved");
    }
    else {
        loadDataTable("all");
    }
});

function loadDataTable(orderStatus) {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "type": "GET",
            "url": "/Admin/Order/GetAll",
            "Content-Type": 'application/json',
            "data": {
                "status" : orderStatus
            },
            "cache": true
        },
        "columns": [
            { "data": "id", "width": "5%"},
            { "data": "name", "width": "15%"},
            { "data": "phoneNumber", "width": "15%"},
            { "data": "userDataModel.email", "width": "15%"},
            { "data": "orderStatus", "width": "15%"},
            { "data": "orderTotal", "width": "10%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                            <a href="/Admin/Order/Details?orderId=${data}"
                            class="btn btn-primary mx-2">
                                <i class="bi bi-info-square"></i>
                            </a>
                        </div>
                        `
                },
                "width": "5%"
            }
        ]
    });
}
var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Company/GetAll"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "streetAddress", "width": "15%" },
            { "data": "city", "width": "15%" },
            { "data": "postalCode", "width": "15%" },
            { "data": "phoneNumber", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                            <a class="btn btn-primary mx-2" href="/Admin/Company/Upsert?id=${data}">
                                <i class="bi bi-pencil-square"></i>Edit
                            </a>
                            <a class="btn btn-danger mx-2" href="/Admin/Company/Delete?id=${data}">
                                <i class="bi bi-eraser"></i>Delete
                            </a>
                        </div>
                        `
                },
                "width": "15%"
            }
        ]
    });
}
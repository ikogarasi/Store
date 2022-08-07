var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/CoverType/GetAll"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <a onClick=Delete('/Admin/Category/Delete/${data}')
                            class="btn btn-danger mx-2">
                            <i class="bi bi-eraser"></i>Delete
                            </a>
                        `
                },
                "width": "0.3%"
            }
        ]
    });
}
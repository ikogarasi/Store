var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "type": "GET",
            "url": "/Admin/Product/GetAll",
            "Content-Type": 'application/json',
            "cache": false
        },
        "columns": [
            { "data": "title", "width": "15%"},
            { "data": "isbn", "width": "15%"},
            { "data": "price", "width": "15%"},
            { "data": "author", "width": "15%"},
            { "data": "categoryModel.name", "width": "15%"},
            { "data": "coverTypeModel.name", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                            <a href="/Admin/Product/Upsert?id=${data}"
                            class="btn btn-primary mx-2">
                                <i class="bi bi-pencil-square"></i>Edit
                            </a>
                             <a onClick=Delete('/Admin/Product/Delete/${data}')
                            class="btn btn-danger mx-2">
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
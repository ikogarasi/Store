var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Product/GetAll"
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

function Delete(url) {
    Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        showCancelButton: true,
        confirmButtonColor: '#ff7851',
        cancelButtonColor: '#f3969a',
        confirmButtonText: 'Delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}
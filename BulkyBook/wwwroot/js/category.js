let dataTable;

$(document).ready(function () {
    loadDataTable()
});

function loadDataTable() {
    dataTable = $('#categories').DataTable({
        ajax: {
            url: '/Admin/Categories/GetAll'
        },
        columns: [
            { data: 'name', width: '25%' },
            { data: 'displayOrder', width: '25%' },
            {
                data: 'createdDateTime',
                render: DataTable.render.datetime(),
                width: '25%'
            },
            {
                data: 'id',
                render: function (data) {
                    return `
                        <div class="btn-group" role="group">
                            <a href="/Admin/Categories/Edit/${data}" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i>Edit</a>
                            <a class="btn btn-danger mx-2 js-delete" data-category-id="${data}"><i class="bi bi-trash3"></i>Delete</a>
                        </div>
                    `
                },
                width: '25%'
            }
        ]
    });
}

$(document).on('click', '.js-delete', function () {
    let button = $(this);

    swal({
        title: "Are you sure?",
        text: "You will not be able to recover this category!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then((result) => {
        if (result) {
            $.ajax({
                url: '/Admin/Categories/Delete/' + button.attr("data-category-id"),
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
})
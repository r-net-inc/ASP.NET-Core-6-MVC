let table;

$(document).ready(function () {
    loadTable()
});

function loadTable() {
    table = $('#categories').DataTable({
        ajax: {
            url: '/Admin/Categories/GetAll'
        },
        columns: [
            { data: 'name', "width": '55%' },
            { data: 'displayOrder', "width": '5%' },
            {
                data: 'createdDateTime',
                render: DataTable.render.datetime('lll'),
                "width": '20%'
            },
            {
                data: 'id',
                render: function (data) {
                    return `
                        <div class="btn-group" role="group">
                            <a href="/Admin/Categories/Upsert/${data}" class="btn btn-primary btn-sm mx-2"><i class="bi bi-pencil-square"></i>Edit</a>
                            <a class="btn btn-danger btn-sm mx-2 js-delete" data-category-id="${data}"><i class="bi bi-trash3"></i>Delete</a>
                        </div>
                    `
                },
                "width": '20%'
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
                        table.ajax.reload();
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
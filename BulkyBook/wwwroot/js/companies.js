let table;

$(document).ready(function () {
    loadTable()
});

function loadTable() {
    table = $('#companies').DataTable({
        ajax: {
            url: '/Admin/Companies/GetAll'
        },
        columns: [
            { data: 'name', "width": '35%' },
            { data: 'city', "width": '15%' },
            { data: 'province', "width": '15%' },
            { data: 'phoneNumber', "width": '15%' },
            {
                data: 'id',
                render: function (data) {
                    return `
                        <div class="btn-group" role="group">
                            <a href="/Admin/Companies/Upsert/${data}" class="btn btn-primary btn-sm mx-2"><i class="bi bi-pencil-square"></i>Edit</a>
                            <a class="btn btn-danger btn-sm mx-2 js-delete" data-company-id="${data}"><i class="bi bi-trash3"></i>Delete</a>
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
        text: "You will not be able to recover this company!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then((result) => {
        if (result) {
            $.ajax({
                url: '/Admin/Companies/Delete/' + button.attr("data-company-id"),
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
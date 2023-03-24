let dataTable;

$(document).ready(function () {
    loadDataTable()
});

function loadDataTable() {
    dataTable = $('#products').DataTable({
        ajax: {
            url: '/Admin/Products/GetAll'
        },
        columns: [
            { data: 'title', width: '15%' },
            { data: 'author', width: '15%' },
            { data: 'category.name', width: '15%' },
            { data: 'isbn', width: '15%' },
            { data: 'listPrice', width: '15%' },
            {
                data: 'id',
                render: function (data) {
                    return `
                        <div class="btn-group" role="group">
                            <a href="/Admin/Products/Upsert/${data}" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i>Edit</a>
                            <a class="btn btn-danger mx-2 js-delete" data-product-id="${data}"><i class="bi bi-trash3"></i>Delete</a>
                        </div>
                    `
                },
                width: '15%'
            }
        ]
    });
}

//function Delete(url) {
//    swal({
//        title: "Are you sure?",
//        text: "You will not be able to recover this product!",
//        icon: "warning",
//        buttons: true,
//        dangerMode: true,
//    }).then((result) => {
//        if (result) {
//            $.ajax({
//                url: url,
//                type: 'DELETE',
//                success: function (data) {
//                    if (data.success) {
//                        dataTable.ajax.reload();
//                        toastr.success(data.message);
//                    }
//                    else {
//                        toastr.error(data.message);
//                    }
//                }
//            })
//        }
//    })
//}

$(document).on('click', '.js-delete', function () {
    let button = $(this);

    swal({
        title: "Are you sure?",
        text: "You will not be able to recover this product!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then((result) => {
        if (result) {
            $.ajax({
                url: '/Admin/Products/Delete/' + button.attr("data-product-id"),
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

//$(document).on('click', '.js-delete', function () {
//    var button = $(this);

//    bootbox.confirm({
//        title: 'Delete Product?',
//        message: 'Are you sure you want to delete this product?',
//        buttons: {
//            cancel: {
//                label: '<i class="bi bi-x-square"></i>&nbsp; No',
//                className: 'btn-success'
//            },
//            confirm: {
//                label: '<i class="bi bi-check-square"></i>&nbsp; Yes',
//                className: 'btn-danger'
//            }
//        },
//        callback: function (result) {
//            if (result) {
//                window.location.assign("/Admin/Products/Delete/" + button.attr("data-product-id"));
//            }
//        }
//    });
//})
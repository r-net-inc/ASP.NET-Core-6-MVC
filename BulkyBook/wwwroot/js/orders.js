let table;

$(document).ready(function () {
    loadTable()
});

function loadTable() {
    table = $('#orders').DataTable({
        ajax: {
            url: '/Admin/Orders/GetAll'
        },
        columns: [
            { data: 'id', "width": '5%' },
            { data: 'name', "width": '25%' },
            { data: 'phoneNumber', "width": '15%' },
            { data: 'applicationUser.email', "width": '15%' },
            { data: 'orderStatus', "width": '15%' },
            { data: 'orderTotal', "width": '10%' },
            {
                data: 'id',
                render: function (data) {
                    return `
                        <div class="btn-group" role="group">
                            <a href="/Admin/Orders/Details/${data}" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i>Details</a>
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
//                        table.ajax.reload();
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
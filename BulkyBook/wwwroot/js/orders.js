$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("processing")) {
        loadTable("processing");
    }
    else {
        if (url.includes("completed")) {
            loadTable("completed");
        }
        else {
            if (url.includes("pending")) {
                loadTable("pending");
            }
            else {
                if (url.includes("approved")) {
                    loadTable("approved");
                }
                else {
                    loadTable("all");
                }
            }
        }
    }
});

function loadTable(status) {
    var table = $('#orders').DataTable({ 
        ajax: {
            url: "/Admin/Orders/GetAll?status=" + status
        },
        columns: [
            { data: 'id', "width": '5%' },
            { data: 'name', "width": '25%' },
            { data: 'phoneNumber', "width": '15%' },
            { data: 'applicationUser.email', "width": '20%' },
            { data: 'orderStatus', "width": '15%' },
            {
                data: 'orderTotal',
                render: DataTable.render.number(null, null, 2, '$'),
                "width": '10%'
            },
            {
                data: 'id',
                render: function (data) {
                    return `
                        <div class="btn-group" role="group">
                            <a href="/Admin/Orders/Details/${data}" class="btn btn-primary btn-sm mx-2"><i class="bi bi-pencil-square">Details</i></a>
                        </div>
                    `
                },
                "width": '10%'
            }
        ]
    });
}
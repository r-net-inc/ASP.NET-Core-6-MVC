$(document).ready(function () {
    loadDataTable()
});

function loadDataTable() {
    let datatable = $('#products').DataTable({
        'ajax': {
            'url': '/Admin/Products/GetAll'
        },
        'columns': [
            { 'data': 'title', 'width': '15%' },
            { 'data': 'author', 'width': '15%' },
            { 'data': 'isbn', 'width': '15%' },
            { 'data': 'price', 'width': '15%' },
        ]
    });
}
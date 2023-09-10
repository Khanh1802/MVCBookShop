

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/product/getall' },
        "columns":
            [
                { data: 'title', "width": "15%" },
                { data: 'isbn', "width": "15%" },
                { data: 'author', "width": "15%" },
                { data: 'price', "width": "5%" },
                { data: 'category.name', "width": "5%" },
                {
                    data: 'id',
                    "render": function (data) {
                        return `<div class="w-40 btn-group" role="group"></div>
                     <a href="/admin/product/createorupdate?id=${data}" class="btn btn-primary mx-1"> <i class="bi bi-pencil-square"></i>Update</a>
                     <a href="/admin/product/delete/${data}" class="btn btn-danger mx-1"><i class="bi bi-trash-fill">Delete </a>
                    `
                    },
                    "width": "15%"
                }
            ]
    });
}
var dtble; 
$(document).ready(function () {
    loaddata();
});

function loaddata() {
    dtble = $("#ProductTable").DataTable({
        "ajax": {
            "url": "/Admin/Product/Getdata"
        },
        "columns": [
            
            { "data": "productName" },
            { "data": "productDescription" },
            { "data": "productPrice" },     
            { "data": "category" },
            {
                "data": "productId",
                "render": function (data) {
                    return `
                    
                    <a type="button" class="btn btn-primary" href="/Admin/Product/Edit/${data}">Edit</a>

                     <a type="button" class="btn btn-primary"   onClick=deleteItem("/Admin/Product/Delete/${data}") > Delete </a>

                    `
                     
                }
            }
        ]
    });
}



function deleteItem(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "Delete",
                success: function (data) {
                    if (data.success) {
                        dtble.ajax.reload();
                        toastr.success(data.message);
                    } else {
                        toastr.error(data.message);
                    }
                }
            })
            Swal.fire({
                title: "Deleted!",
                text: "Your file has been deleted.",
                icon: "success"
            });
        }
    });
}




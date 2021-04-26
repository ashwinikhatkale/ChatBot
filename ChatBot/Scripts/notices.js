$(function () {
    $(".delete").on('click', function () {
        var id = $(this).data('id');
        var element = $(this).closest('tbody').find('tr[data-id=' + id + ']');

        $.ajax({
            type: "POST",
            url: '/Admin/Notice/Delete/' + id,
            contentType: "application/json; charset=utf-8",

            dataType: 'json',
            success: function (response) {
                if (response.isSuccess == true) {
                    alert('Notice deleted successfully!');
                    element.closest('tr').remove();                    
                }
            },
            error: function () {
                alert('Error occurred while deleting notice.')
            }
        });
    });
});
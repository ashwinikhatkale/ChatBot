

$(function () {

    $('.datetimepicker').datetimepicker({
        format: 'DD-MMM-YYYY'
    });

    $("#submit").on('click', function () {
        var fromDate = $("#FromDate").val();
        var toDate = $("#ToDate").val();

        if (fromDate <= toDate) {
            alert('From Date must be less than To Date.')
            return;
        }

        $.ajax({
            url: '/Student/NoticeBoard/Index',
            data: { fromDate: fromDate, toDate: toDate },
            contentType: "application/json; charset=utf-8",
            dataType: 'html',
            success: function (response) {
                $('.notices').html(response);
            },
            error: function (response) {
                alert('Error occurred.')
            }

        });
    });
});
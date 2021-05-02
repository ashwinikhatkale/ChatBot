
$(function () {

    $("#QuestionHint1").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/ChatBot/GetQuestions",
                dataType: "json",
                data: { noticeId: $('#NoticeBoardId').val(), prefix: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.Text, value: item.Value };
                    }));
                }
            })
        },
        messages: {
            noResults: '',
            results: function (resultsCount) { }
        },
        select: function (event, ui) {
            event.preventDefault();
            $("#QuestionHint1").val(ui.item.label);
            $("#Id").val(ui.item.value);
            var questionId = $('#Id').val();
            $('#Answer').val('');
            $('.message').addClass('hide');

            if (questionId > 0) {
                $.ajax({
                    url: '/ChatBot/GetAnswer',
                    data: { questionId: questionId },
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    success: function (response) {
                        if (response.answer != null && response.answer.length > 0) {
                            $('.answer').removeClass('hide');
                            $('#Answer').val(response.answer);
                            $('.submit-question').addClass('hide');
                            return;
                        }
                    }
                });
            }
            $('.answer').addClass('hide');
            $('.submit-question').removeClass('hide');
        }
    }).focus(function () {
        if ($(this).val().length == 0) {
            $(this).autocomplete("search", "%");
        }
    });

    $('#QuestionHint1').on('keyup', function () {
        $('#Answer').val('');
        $('.message').addClass('hide');
    });

    $('#QuestionHint1').on('blur', function () {
        if ($('#Answer').val().length == 0) {
            $('.answer').addClass('hide');
            $('.submit-question').removeClass('hide');
        }
        $('.message').addClass('hide');
    });
});
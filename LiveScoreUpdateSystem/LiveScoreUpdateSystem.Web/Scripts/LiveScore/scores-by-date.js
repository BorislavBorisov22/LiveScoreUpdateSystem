$('#scores-date').on('change', (ev) => {
    const date = kendo.toString($("#scores-date").data("kendoDatePicker").value(), 'd');
    const dateEncoded = encodeURI(date);

    const url = `/scores/bydate?date=${dateEncoded}`;

    $.get(url, (dataHtml) => {
        $('.matches_list_wrap').html(dataHtml);
        $('#scores-date').val(date);
    });
});

//const dateChanged = (ev) => {
    //    // const date = kendo.toString($("#scores-date").data("kendoDatePicker").value(), 'd');
    //    const dateEncoded = encodeURI(date);
    //    const url = `/scores/bydate?date=${dateEncoded}`

    //    $.get(url, (dataHtml) => {
    //        $('.matches_list_wrap').html(dataHtml);
    //        $('#scores-date').val(date);
    //    });
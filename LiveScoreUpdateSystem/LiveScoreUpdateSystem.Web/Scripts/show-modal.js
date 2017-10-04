function showModal(modalSelector, formTitle, action, method) {
    $(modalSelector).modal("show")
    $('.modal-title').html(formTitle);

    $("#modal-form")
        .attr("action", action)
        .attr("method", method);
}
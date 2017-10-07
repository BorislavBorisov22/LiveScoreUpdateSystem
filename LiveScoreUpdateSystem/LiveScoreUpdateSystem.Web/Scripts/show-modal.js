function showModal(modalSelector, formTitle) {
    $(modalSelector).modal("show")
    $('.modal-title').html(formTitle);
}

$("#btn-submit-form").on('click', () => {
    const $form = $('#add-data-form');
    $form.submit();
})
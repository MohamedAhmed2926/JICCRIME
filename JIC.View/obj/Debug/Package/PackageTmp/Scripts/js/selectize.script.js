//var $selectize = $('.selectize').selectize();

$(document).on('hidden.bs.modal', function (event) {
    $(this).find('form').each(function () {
        resetForm(this);
    });
});
//$(document).on('reset', 'form', function () {
//    $(this).find('select.selectize').each(function () {
//        if(this.selectize)
//            this.selectize.clear();
//    });
//});
function resetForm(form) {
    $(form).find('select.selectize').each(function () {
        if (this.selectize)
            this.selectize.destroy();
    });

    form.reset();
    // resets the form before continuing the function

    $(form).find('select.selectize').each(function () {
        $(this).selectize();
    });


    //reset unobtrusive validation summary, if it exists
    $(form).find("[data-valmsg-summary=true]")
        .removeClass("validation-summary-errors")
        .addClass("validation-summary-valid")
        .find("ul").empty();

    //reset unobtrusive field level, if it exists
    $(form).find("[data-valmsg-replace]")
        .removeClass("field-validation-error")
        .addClass("field-validation-valid")
        .empty();

    $(form).find('.field-validation-error')
        .removeClass('field-validation-error')
        .addClass('field-validation-valid');

    $(form).find('.input-validation-error')
        .removeClass('input-validation-error')
        .addClass('valid');
}
$("input[type='reset']").on("click", function (event) {
    var form = $(this).closest('form').get(0);
    if (form) {
        event.preventDefault();
        // stops the form from resetting after this function
        resetForm(form);
    }
});
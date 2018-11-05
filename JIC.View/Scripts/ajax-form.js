function formUpdated() {
    $(document).trigger("grid-update");
}
$(function () {
    $(".ajax-form").on('submit', function (e) {

        e.preventDefault() // prevent the form's normal submission
        var FormDiv = $(this);
        var dataToPost = FormDiv.serialize()
        $(".field-validation-error,.field-validation-valid").each(function () {
            $(this).text("");
        });
        FormDiv.find('input:submit').each(function () {
            $(this).attr('disabled', true);
        });
        $.post(FormDiv.data("url"), dataToPost)
            .done(function (response, status, jqxhr) {
                // this is the "success" callback
                console.log("Success" + response);
                FormDiv.find('input:submit').each(function () {
                    $(this).removeAttr('disabled');
                });

            })
            .fail(function (jqxhr, status, error) {
                // this is the ""error"" callback
                $.each(jqxhr.responseJSON, function (key, value) {
                    $("span[data-valmsg-for='" + key + "']").text(value);
                });
                FormDiv.find('input:submit').each(function () {
                    $(this).removeAttr('disabled');
                });
            })
    })

    $(document).on("grid-update", function (event) {
        $('.mvc-grid').mvcgrid({
            reload: true
        });
    });

    $('.mvc-grid').mvcgrid();
});

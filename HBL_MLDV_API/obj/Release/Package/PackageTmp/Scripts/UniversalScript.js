function SubmitForm(btn) {
    //$('body').find("input,select,textarea").each(function (i, v) {
    //    debugger;
    //    if ($(this).attr('type') != "hidden" && $(this).attr('type') != "checkbox" && $(this).valid() == false) {
    //        console.log($(this).attr('name'));
    //    }
    //});
    debugger;
    var val = "";
    if ($(btn).attr("data-status") == undefined) {
        val = btn;
    }
    else {
        val = $(btn).attr("data-status");
    }
    $('#sender').val(val);

    if ($("form").valid()) {
        var validate = true;
        if (validate) {
            $("form").submit();
        }
        else {
            Notify("error", "Please fill all required fields");
            e.preventDefault();
            return;
        }
    }
}
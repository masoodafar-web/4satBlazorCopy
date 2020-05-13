function createEducationalRecordModalContent() {
    $.get("/EducationalRecords/CreateEducationalRecordModalContent", function (data) {
        $('#EducationalRecordModalContent').html(data);
        $('#AddEducationalRecord').modal('show');

        $(function () {
            $('#StartDate').dropdownDatepicker();
        });
        $(function () {
            $('#EndDate').dropdownDatepicker();
        });


        $("#FieldId").val("").blur().focus();
        $("#OrientationId").val("").blur().focus();
        $("#UniversityId").val("").blur().focus();

        var form = $("#AddEducationalRecordAjax");
        form.removeData('validator');
        form.removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse(form);


    });


}

function editEducationalRecordModalContent(id) {
    $.get("/EducationalRecords/EditEducationalRecordModalContent/" + id, function (data) {
        $('#EducationalRecordModalContent').html(data);
        $('#AddEducationalRecord').modal('show');

        $(function () {
            $('#StartDate').dropdownDatepicker();
        });
        $(function () {
            $('#EndDate').dropdownDatepicker();
        });

        var form = $("#EditEducationalRecordAjax");
        form.removeData('validator');
        form.removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse(form);
    });
}
//متد ارسال شناسه سابقه تحصیلی کاربر برای حذف
function DeleteEducationalRecord(id) {
    $("#UserEducationalRecordId").val(id).blur().focus();
    $("#DeleteEducationalRecordAjax").submit();
}
//متد موفقیت آمیز بودن حذف سابقه تحصیلی
function deleteEducationalRecordOk(result) {
    $("#EducationalRecord_" + result.Id).fadeOut("slow");
    var notif = $("#popupNotificationGeneral").data("kendoNotification");
    notif.show("عملیات حذف " + result.University.Name + " با موفقیت انجام شد.");
}

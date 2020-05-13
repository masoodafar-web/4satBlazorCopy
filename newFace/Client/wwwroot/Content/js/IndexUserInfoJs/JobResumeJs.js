function createJobResumeModalContent() {
    $.get("/JobResumes/CreateJobResumeModalContent", function (data) {
        $('#JobResumeModalContent').html(data);
        $('#AddJobResume').modal('show');

        $(function () {
            $('#StartDate').dropdownDatepicker();
        });
        $(function () {
            $('#EndDate').dropdownDatepicker();
        });


        $("#JobPositionId").val("").blur().focus();
        $("#CompanyId").val("").blur().focus();
    

        var form = $("#AddJobResumeAjax");
        form.removeData('validator');
        form.removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse(form);


    });


}

function editJobResumeModalContent(id) {
    $.get("/JobResumes/EditJobResumeModalContent/" + id, function (data) {
        $('#JobResumeModalContent').html(data);
        $('#AddJobResume').modal('show');

        $(function () {
            $('#StartDate').dropdownDatepicker();
        });
        $(function () {
            $('#EndDate').dropdownDatepicker();
        });

        var form = $("#EditJobResumeAjax");
        form.removeData('validator');
        form.removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse(form);
    });
}
//متد ارسال شناسه سابقه شغلی کاربر برای حذف
function DeleteJobResume(id) {
    $("#UserJobResumeId").val(id).blur().focus();
    $("#DeleteJobResumeAjax").submit();
}
//متد موفقیت آمیز بودن حذف سابقه شغلی
function deleteJobResumeOk(result) {
    $("#JobResume_" + result.Id).fadeOut("slow");
    var notif = $("#popupNotificationGeneral").data("kendoNotification");
    notif.show("عملیات حذف " + result.JobTitle + " با موفقیت انجام شد.");
}


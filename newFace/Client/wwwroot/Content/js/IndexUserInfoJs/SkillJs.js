function SkillCreateOk() {
	$("#AddSkill").modal('hide');
	//$("#AddSkillAjax").trigger('reset');
	//RangeLable($("#rangsliderId"), 'rangAmount');
}


function skillCreatModalContent() {
	$.get("/Skills/CreateSkillModalContent", function (data) {
		$('#CreateSkillModalContent').html(data);
		$('#AddSkill').modal('show');
		$(function () {
			var SelectCategoryId = $("#CategoryId");
			SelectCategoryId.empty().append('<option selected="selected" value="0" disabled = "disabled">Loading.....</option>');
			$.ajax({
				type: "Get",
				url: "/Categories/GetSkillsinTree",
				data: '{}',
				contentType: "application/json; charset=utf-8",
				dataType: "json",
				success: function (response) {
					SelectCategoryId.empty().append('<option selected="selected" value="0">مهارت خود را انتخاب کنید..</option>');
					$.each(response, function () {
						SelectCategoryId.append($("<option></option>").val(this['Id']).html(this['Title']));
					});
				},
				failure: function (response) {
					alert(response.responseText);
				},
				error: function (response) {
					alert(response.responseText);
				}
			});
		});
		var form = $("#AddSkillAjax");
		form.removeData('validator');
		form.removeData('unobtrusiveValidation');
		$.validator.unobtrusive.parse(form);

	});
	
	//$("#AddSkillAjax").validate();
	//$("#AddSkillAjax").removeAttr("novalidate");
}
//متد ارسال شناسه مهارت کاربر برای حذف
function DeleteSkill(id) {
	$("#UserSkillId").val(id).trigger("change");
	$("#DeleteSkillAjax").submit();
}
//متد موفقیت آمیز بودن حذف مهارت
function deleteSkillOk(result) {
	$("#skillDiv_" + result.Id).fadeOut("slow");
	var notif = $("#popupNotificationGeneral").data("kendoNotification");
	notif.show("عملیات حذف" + result.Category.Title + " با موفقیت انجام شد.");
}
function createWorkSampleModalContent() {
	$.get("/WorkSamples/CreateWorkSampleModalContent", function (data) {
		$('#WorkSampleModalContent').html(data);
		$('#AddWorkSample').modal('show');

		$(function () {
			var SelectCategoryId = $("#CategoryId");
			SelectCategoryId.empty().append('<option selected="selected" value="0" disabled = "disabled">Loading.....</option>');
			$.ajax({
				type: "Get",
				url: "/Account/JobCategoryItems",
				data: '{}',
				contentType: "application/json; charset=utf-8",
				dataType: "json",
				success: function (response) {
					SelectCategoryId.empty().append('<option selected="selected" value="0">رسته شغلی خود را انتخاب کنید..</option>');
					response.forEach(function (value) {
						SelectCategoryId.append($("<option></option>").val(value.Value).html(value.Text));

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

		$(function () {
			$('#Date').dropdownDatepicker();
		});


		//$("#FieldId").val("").blur().focus();
		//$("#OrientationId").val("").blur().focus();
		//$("#UniversityId").val("").blur().focus();

		var form = $("#AddWorkSampleAjax");
		form.removeData('validator');
		form.removeData('unobtrusiveValidation');
		$.validator.unobtrusive.parse(form);


	});


}

function editWorkSampleModalContent(id) {
	$.get("/WorkSamples/EditWorkSampleModalContent/" + id, function (data) {
		$('#WorkSampleModalContent').html(data);
		$('#AddWorkSample').modal('show');
		$(function () {
			var SelectCategoryId = $("#CategoryId");
			var selectedCategory = $("#categoryEdit").val();
			SelectCategoryId.empty().append('<option selected="selected" value="0" disabled = "disabled">Loading.....</option>');
			$.ajax({
				type: "Get",
				url: "/Account/JobCategoryItems",
				data: '{}',
				contentType: "application/json; charset=utf-8",
				dataType: "json",
				success: function (response) {
					SelectCategoryId.empty().append('<option selected="selected" value="0">رسته شغلی خود را انتخاب کنید..</option>');
					response.forEach(function (value) {
						if (selectedCategory === value.Value) {
							SelectCategoryId.append($("<option selected='selected'></option>").val(value.Value).html(value.Text));
						} else {
							SelectCategoryId.append($("<option></option>").val(value.Value).html(value.Text));
						}

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
		$(function () {
			$('#Date').dropdownDatepicker();
		});


		var form = $("#EditWorkSampleAjax");
		form.removeData('validator');
		form.removeData('unobtrusiveValidation');
		$.validator.unobtrusive.parse(form);
	});
}
//متد ارسال شناسه سابقه تحصیلی کاربر برای حذف
function DeleteWorkSample(id) {
	$("#UserWorkSampleId").val(id).blur().focus();
	$("#DeleteWorkSampleAjax").submit();
}
//متد موفقیت آمیز بودن حذف سابقه تحصیلی
function deleteWorkSampleOk(result) {
	$("#WorkSample_" + result.Id).fadeOut("slow");
	var notif = $("#popupNotificationGeneral").data("kendoNotification");
	notif.show("عملیات حذف " + result.Title + " با موفقیت انجام شد.");
}

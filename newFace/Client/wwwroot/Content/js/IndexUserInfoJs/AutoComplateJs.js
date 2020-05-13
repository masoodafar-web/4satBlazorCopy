
//برای چک کردن گرایش و رشته و دانشگاه که انتخاب شده باشد از گزینه هایی که مورد قبول است اگر مورد صحیحی انتخاب شده باشد مقادیر تغییر میکند.
var OrientationIsValid = false;
var FieldIsValid = false;
var UniverValid = false;
var CompanyValid = false;
var JobPositionValid = false;
//---------------------------------------Field & Orientation--------------------------------------------------------------------------------
//این متد در رویداد (بدون مقدار)کندو اتوکمپلت است زمانی که مقدار تایپ شده وجود نداشته باشد این متد اجرا میشود
function addNewField(widgetId, value) {
    var widget = $("#" + widgetId).getKendoAutoComplete();
    var dataSource = widget.dataSource;
    var parentId = null;
    if (widgetId !== "AutoCompleteField") {
        parentId = $("#FieldId").val();
    }

    $.ajax({
        url: location.origin + '/FieldAndOrientation/Create',
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ Name: value, ParentId: parentId }),
        success: function (response) {
            dataSource.read();
            var ORGFildId = widgetId.replace("AutoComplete", "");

            if (ORGFildId === "Field") {
                FieldIsValid = true;
                $("#OrientationId").val("").blur().focus();
                $("#OrientationAutoComplete").val("");
                $("#OrientationAutoComplete").removeClass("k-state-disabled");

            } else if (ORGFildId === "Orientation") {
                OrientationIsValid = true;
            }

            $("#" + ORGFildId + "Id").val(response.Model.Id).blur().focus();

            if (ORGFildId === "Field") {
                FieldIsValid = true;
                $("#OrientationId").val("").blur().focus();
                $("#OrientationAutoComplete").val("");
                $("#OrientationAutoComplete").removeClass("k-state-disabled");

            } else if (ORGFildId === "Orientation") {
                OrientationIsValid = true;
            }

            alert(response.Message);
        },
        error: function (response) {
            alert(response.Message);
        },
        failure: function (response) {
            alert(response.Message);
        }
    });

    dataSource.read();
};
//------------------------------------------------------------------------------------------------------------
// متد فیلتر از سمت سرور است که هربار بعد از تایپ ، مقداری را به کنترولر پاس دهد
//الان در این متد متن تایپ شده خود فیلد پاس داده شده
function onAdditionalData(e) {
    if (e.filter.filters.length <= 0) {
        return {
            text: ""
        };
    } else {
        return {
            text: e.filter.filters[0].value
        };
    }

};
//این متد برای گرایش است که علاوه بر متن خودش مقدار آیدی رشته را نیز برای ثبت جدید پاس میدهد
function onAdditionalDataOrient() {
    return {
        parentId: $("#FieldId").val(),
        text: $("#OrientationAutoComplete").val()

    };
};
//------------------------------------------------------------------------------------------------------------
//هنگام انتخاب از لیست هردو (گرایش و رشته) اجرا شده و مقدا ایدی واقعیه مقادیر ارسال شده از کنترولر را با خو همراه دارد
function CustomSelect(e) {

    var DataItem = this.dataItem(e.item.index());
    var ORGFildId = e.sender.element[0].id.replace("AutoComplete", "");
    var dataSource;

    //اگر انتخاب از جانب رشته بود
    if (e.sender.element[0].id === "AutoCompleteField") {
        dataSource = $("#OrientationAutoComplete").getKendoAutoComplete().dataSource;
        //گرایش را دوباره بخوان
        dataSource.read();
        // متقیر چک کردن معتبر بودن رشته و گرایش از حالت غیر فعال خارج شود
        FieldIsValid = true;
        $("#OrientationId").val("").blur().focus();
        $("#OrientationAutoComplete").val("");
        $("#OrientationAutoComplete").removeClass("k-state-disabled");
    }
    //وگرنه ممکن است از جانب گرایش باشد پس متغیر مربوط را به دلیل این که در متد انتخاب هستیم تغییر بده
    else if (e.sender.element[0].id === "OrientationAutoComplete") {

        OrientationIsValid = true;
    }
    //وگرنه اگر از جانب دانشگاه است
    else if (e.sender.element[0].id === "UniversityAutoComplete") {
        UniverValid = true;
    }
    //وگرنه اگر از جانب سمت کاری
    else if (e.sender.element[0].id == "JobPositionAutoComplete") {
        JobPositionValid = true;
    }
    //وگرنه از جانب شرکت است
    else {
        CompanyValid = true;
    }

    //دلیل این که شرایط بالا دوبار تکرار شده این است که در همین خط جاری یک بار متد چنج(تغییر) صدا زده میشود بعد از این خط هم یک بار دیگر صدا زده میشود
    //به همین دلیل باید متغییر های چک کننده به درست یا ترو تبدیل شود
    $("#" + ORGFildId + "Id").val(DataItem.Id).blur().focus();

    //اگر انتخاب از جانب رشته بود
    if (e.sender.element[0].id === "AutoCompleteField") {
        dataSource = $("#OrientationAutoComplete").getKendoAutoComplete().dataSource;
        //گرایش را دوباره بخوان
        dataSource.read();
        // متقیر چک کردن معتبر بودن رشته و گرایش از حالت غیر فعال خارج شود
        FieldIsValid = true;
        $("#OrientationId").val("").blur().focus();
        $("#OrientationAutoComplete").val("");
        $("#OrientationAutoComplete").removeClass("k-state-disabled");
    }
    //وگرنه ممکن است از جانب گرایش باشد پس متغیر مربوط را به دلیل این که در متد انتخاب هستیم تغییر بده
    else if (e.sender.element[0].id === "OrientationAutoComplete") {

        OrientationIsValid = true;
    }
    //وگرنه اگر از جانب دانشگاه است
    else if (e.sender.element[0].id === "UniversityAutoComplete") {
        UniverValid = true;
    }
    //وگرنه اگر از جانب سمت کاری
    else if (e.sender.element[0].id == "JobPositionAutoComplete") {
        JobPositionValid = true;
    }
    //وگرنه از جانب شرکت است
    else {
        CompanyValid = true;
    }

}

function CustomChange(e) {
    var elementId;
    if (e.sender.element[0].id == "UniversityAutoComplete") {
        if (!UniverValid) {
            elementId = e.sender.element[0].id.replace("AutoComplete", "");
            $("#" + elementId + "Id").val(e.sender.element[0].value).blur().focus();
        }
        UniverValid = false;
    } else if (e.sender.element[0].id == "JobPositionAutoComplete") {
        if (!JobPositionValid) {
            elementId = e.sender.element[0].id.replace("AutoComplete", "");
            $("#" + elementId + "Id").val(e.sender.element[0].value).blur().focus();
        }
        JobPositionValid = false;
    } else {
        if (!CompanyValid) {
            elementId = e.sender.element[0].id.replace("AutoComplete", "");
            $("#" + elementId + "Id").val(e.sender.element[0].value).blur().focus();
        }
        CompanyValid = false;
    }

}
//-----------------------------------------------------------------------------------------------------------
//متدهای تغییر برای گرایش و رشته به صورت مجزا
function FieldChange(e) {
    //اگر متقیر از متد (انتخاب) عبور کرده پس حتما مقداری انتخاب شده ولی اگر
    // از گزینه های پیشنهاد داده شده انتخاب نکند صد در صد از متد(انتخاب) عبور نمیکند و مقدارش (درست) نمیشود
    if (!FieldIsValid) {
        var ORGFildId = e.sender.element[0].id.replace("AutoComplete", "");
        $("#" + ORGFildId + "Id").val(e.sender.element[0].value).blur().focus();
        $("#OrientationAutoComplete").empty();
        $("#OrientationAutoComplete").addClass("k-state-disabled");
    }
    FieldIsValid = false;
}
//اگر متقیر از متد (انتخاب) عبور کرده پس حتما مقداری انتخاب شده ولی اگر
// از گزینه های پیشنهاد داده شده انتخاب نکند صد در صد از متد(انتخاب) عبور نمیکند و مقدارش (درست) نمیشود
function OrientationChange(e) {
    if (!OrientationIsValid) {
        var ORGFildId = e.sender.element[0].id.replace("AutoComplete", "");
        $("#" + ORGFildId + "Id").val(e.sender.element[0].value).blur().focus();
    }
    OrientationIsValid = false;
}
//-------------------------------------------End F & O----------------------------------------------------------------
//----------------------------------------------General Method-------------------------------------------------------------------------
function addNew(widgetId, value) {
    var widget = $("#" + widgetId).getKendoAutoComplete();
    var dataSource = widget.dataSource;
    var type = widgetId.replace("AutoComplete", "");


    $.ajax({
        url: location.origin + '/Account/AddNewForResource',
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ Name: value, type: type }),
        success: function (response) {
            dataSource.read();
            if (widgetId == "UniversityAutoComplete") {
                UniverValid = true;
            } else if (widgetId == "JobPositionAutoComplete") {
                JobPositionValid = true;
            } else {
                CompanyValid = true;
            }
            var ORGFildId = widgetId.replace("AutoComplete", "");


            $("#" + ORGFildId + "Id").val(response.Model.Id).blur().focus();

            if (widgetId == "UniversityAutoComplete") {
                UniverValid = true;
            } else if (widgetId == "JobPositionAutoComplete") {
                JobPositionValid = true;
            } else {
                CompanyValid = true;
            }

            alert(response.Message);
        },
        error: function (response) {
            alert(response.Message);
        },
        failure: function (response) {
            alert(response.Message);
        }
    });

    dataSource.read();
};
        //-----------------------------------------End General Method-------------------------------------------------------------------------



//function showDetails(e) {
//    var grid = $("#" + $(e.delegateTarget).attr("id")).data("kendoGrid");
//    var filters = grid.dataSource.filter();
//    var allData = grid.dataSource.data();
//    var query = new kendo.data.Query(allData);
//    var data = query.filter(filters).data;
//    var columnNames = Object.keys(grid.dataSource._pristineData[0]);
//    var t = columnNames.map(function (name) {
//	    var isIdField = name.indexOf("ID") !== -1;
//	    return {
//		    field: name,
//		    width: (isIdField ? 40 : 200),
//		    title: (isIdField ? "Id" : name)
//	    };
//    });
//	//var grid = e.sender;
//	//var cellIndex = grid.select().index();
//    //var columnTitle = grid.thead.find('th')[cellIndex].getAttribute("data-title");
//    var r=grid.columns[0].field;
//   var tmpl = "";
//	$("script#template").empty();
//	e.preventDefault();
//	var fildname = [];
//	var filddisplayname = [];
//	var fildnameHTML = "";
//	for (var j = 0; j < columnNames.length; j++) {
//	    fildname.push(columnNames[j]);
//	    //filddisplayname.push(grid.columns[j].title);
//	}
//	////========بدست اوردن نام ستون ها=========
//	//$(e.delegateTarget).find("table > thead > tr:not(.k-filter-row) > th.k-with-icon").each(function () {
//	//	fildname.push($(this).attr("data-field"));
//	//	filddisplayname.push($(this).attr("data-title"));
//	//});

//	//=========ساختن اجزایه داخل صفحه جزئیات=========
//	for (var i = 0; i < fildname.length; i++) {
//		fildnameHTML += "<li><div class='row'><div class='col-md-6'><h5>" + filddisplayname[i] + ":</h5></div><div class='col-md-6'> #=" + fildname[i] + " # </div></div></li>";

//	}

//	//============افزودن اجراء به قالب اصلی========
//	tmpl =
//		"<div id='details-container'><div class='col-md-12'>" +
//		"<ul style='list-style: none'>" + fildnameHTML + "</ul>" +
//		"</div>";

//	///====ساخت تگ script======
//	var s = document.createElement("script");
//	s.type = "text/x-kendo-template";
//	s.setAttribute("id", "template");

//	///=====افزودن تگ به صفحه
//	$("body").append(s);

//    ///=====افزودن اجزاء و قالب به صفحه
//    $("script#template").append(tmpl);
//    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
//	var wnd = $("#Details").data("kendoWindow");

//	//======ساخت ویندوز جزئیات
//	var detailsTemplate = kendo.template($("#template").html());

//	//=======افزودن اجزاء به ویندوز و باز کردن آن
//	wnd.content(detailsTemplate(dataItem));
//	wnd.center().open();
//}
function showDetails(e) {
    e.preventDefault();
    var Gridtemplate = kendo.template($("#" + $(e.delegateTarget).attr("id") + "template").html());

    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    var wnd = $("#Details").data("kendoWindow");
    wnd.content(Gridtemplate(dataItem));
    wnd.center().open(WindowOpen());
}
//=======================Kendo Events======================
function error_handler(e) {
    if (e.errors) {
        var message = "Errors:\n";
        $.each(e.errors, function (key, value) {
            if ('errors' in value) {
                $.each(value.errors, function () {
                    message += this + "\n";
                });
            }
        });
        alert(message);
    }
};

function sync_handler(e) {
    this.read();
    if (window.location.pathname.toLowerCase() == "/account/indexuserinfo") {
	    GetUserVariableInfo();
    }
}
function popupaddtitle(e) {
   setTimeout(function () {

            $('.k-widget').each(function () {

          var aTagText = $(this).parent().find("a[href='http://www.telerik.com/purchase']").text();
                var aTag = $(this).parent().find("a[href='http://www.telerik.com/purchase']");
                if (aTagText != null && aTagText != "") {
for (i = 0; i < (aTagText.split("www.telerik.com/purchase").length-1); i++) {
  $(aTag[i].previousSibling).remove();
                    $(aTag[i].nextSibling).remove();
}
                   
                    $(this).parent().find("a[href='http://www.telerik.com/purchase']").remove();
                }
            });
        $('#popupNotification').each(function () {

          var aTagText = $(this).parent().find("a[href='http://www.telerik.com/purchase']").text();
                var aTag = $(this).parent().find("a[href='http://www.telerik.com/purchase']");
                if (aTagText != null && aTagText != "") {
                    for (i = 0; i < (aTagText.split("www.telerik.com/purchase").length-1); i++) {
  $(aTag[i].previousSibling).remove();
                    $(aTag[i].nextSibling).remove();
}
                    $(this).parent().find("a[href='http://www.telerik.com/purchase']").remove();
                }



            });
        }, 10);
if (e.model.isNew()) {

                    $(".k-grid-cancel").html('<span class=' + '"glyphicon glyphicon-minus-sign"' + 'style=' + '"font-size: 12px;"' + '></span>' + 'لغو');

                    $('.k-window-title').text("اضافه");
                    $('.k-grid-update').html('<span class=' + '"glyphicon glyphicon-plus"' + 'style=' + '"font-size: 12px;"' + '></span>' + 'اضافه');
                    $(".k-grid-update").attr("id", "k-grid-update");
                    var grid = $("#PersonGrid").data("kendoGrid");


                } else {
                    $('.k-window-title').text("ویرایش");

                    $(".k-grid-cancel").html('<span class=' + '"glyphicon glyphicon-minus-sign"' + 'style=' + '"font-size: 12px;"' + '></span>' + 'لغو');
                    $(".k-grid-update").attr("id", "k-grid-update");

                    var grid = $("#PersonGrid").data("kendoGrid");

                }
};

function popupaddtitleWorkSample(e) {
 
    if (e.model.isNew()) {
        $(".k-grid-cancel").html('<span class=' + '"glyphicon glyphicon-minus-sign"' + 'style=' + '"font-size: 12px;"' + '></span>' + 'لغو');

        $('.k-window-title').text("اضافه");
        $('.k-grid-update').html('<span class=' + '"glyphicon glyphicon-plus"' + 'style=' + '"font-size: 12px;"' + '></span>' + 'اضافه');
        $(".k-grid-update").attr("id", "k-grid-update");
        var grid = $("#PersonGrid").data("kendoGrid");


    } else {
        $('.k-window-title').text("ویرایش");

        $(".k-grid-cancel").html('<span class=' + '"glyphicon glyphicon-minus-sign"' + 'style=' + '"font-size: 12px;"' + '></span>' + 'لغو');
        $(".k-grid-update").attr("id", "k-grid-update");
        if ($("#ImgAddress").val().length == 0) {
            $(".WorkSamplesImg").attr("src", "/Content/img/NoImage.Png").trigger("change");

        } else {
	        $(".WorkSamplesImg").attr("src", $("#ImgAddress").val());

        }
        //var grid = $("#PersonGrid").data("kendoGrid");

    }
};

function popupaddtitleusergrid(e) {
    if (e.model.isNew()) {
        $(".k-grid-cancel").html('<span class=' +
            '"glyphicon glyphicon-minus-sign"' +
            'style=' +
            '"font-size: 12px;"' +
            '></span>' +
            'لغو');

        $('.k-window-title').text("اضافه");
        $('.k-grid-update').html('<span class=' +
            '"glyphicon glyphicon-plus"' +
            'style=' +
            '"font-size: 12px;"' +
            '></span>' +
            'اضافه');
        $(".k-grid-update").attr("id", "k-grid-update");
        //var grid = $("#PersonGrid").data("kendoGrid");


    } else {
        $('.k-window-title').html('<span style=' + '"overflow: unset;"' + '></span>' + 'ویرایش');

        $(".k-grid-cancel").html('<span class=' +
            '"glyphicon glyphicon-minus-sign"' +
            'style=' +
            '"font-size: 12px;"' +
            '></span>' +
            'لغو');
        $(".k-grid-update").html('<span class=' +
            '"glyphicon glyphicon-ok"' +
            'style=' +
            '"font-size: 12px;"' +
            '></span>' +
            'ویرایش');
        $(".k-grid-update").attr("id", "k-grid-update");
        $("#UserName").css("display", "none");
        $("#usernamelabel").css("display", "none");

        $("#passwordlabel").css("display", "none");
        
        $("#confirmpasswordlabel").css("display", "none");
        
        $("#Password").css("display", "none");
        
        $("#ConfirmPassword").css("display", "none");
        //var grid = $("#PersonGrid").data("kendoGrid");

    };
}


function WindowOpen(parameters) {
    var wnd = $(".k-popup-edit-form").data("kendoWindow");
    var wnd1 = $("#Details").data("kendoWindow");
    if (wnd != undefined) {
        var opts = wnd.options;
        opts.maxWidth = 600;
        opts.width = "90%";
        wnd.setOptions(opts);
        wnd.center();
    }else if (wnd1 != undefined) {
        var opts = wnd1.options;
        opts.maxWidth = 600;
        opts.width = "90%";
      wnd1.setOptions(opts);
        wnd1.center();
        }
	
	// Set "responsive" size, you may want to .center() on $(window).resize because the relative position between the window and the viewport will change.
	// The constraints above apply, however, and are most useful when the window is resizable by the end user. The example here demonstrates that you can use them
	// but in the provided configuration they may not be needed. You can find the full set of options the widget can take in the following article:
	// https://docs.telerik.com/kendo-ui/api/javascript/ui/window.
    // With the MVC wrapper, you need to set dimensions options through the setOptions() method.
	// Future versions may allow you to set width and height as percentage strings in the
    // wrapper methods too, so you can check that and you may be able to remove this function.
  
};
$(window).resize(function () {
	WindowOpen();
});
//=======================End Kendo Events======================
var notification = $("#notification").kendoNotification({
	position: {
		pinned: true,
		top: 30,
		right: 30
	},
	autoHideAfter: 0,
	stacking: "down",
	templates: [{
		type: "info",
		template: $("#emailTemplate").html()
	}, {
		type: "error",
		template: $("#errorTemplate").html()
	}, {
		type: "success",
		template: $("#successTemplate").html()
	}]

}).data("kendoNotification");

function RequestEnd(e) {
	if (e.type == "update" && !e.response.Errors) {
		notification.show({
		    message: "ویرایش با موفقیت انجام شد"
		}, "success");
	}

	if (e.type == "create" && !e.response.Errors) {
		notification.show({
		    message: "ایجاد با موفقیت انجام شد"
		}, "success");
	}
}
//The following code removes the 'Add child' button from the new records,
//because they will receive an ID after saving the changes, which means that
//no child records  could be added until that
function onDataBound(e) {
	var items = e.sender.items();
	for (var i = 0; i < items.length; i++) {
		var row = $(items[i]);
		var dataItem = e.sender.dataItem(row);
		if (dataItem.isNew()) {
			row.find("[data-command='createchild']").hide();
		}
		else {
			row.find("[data-command='createchild']").show();
		}
	}

}
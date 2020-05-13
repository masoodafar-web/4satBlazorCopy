function kFormatter(num) {
    return Math.abs(num) > 999
        ? Math.sign(num) * ((Math.abs(num) / 1000).toFixed(1)) + 'k'
        : Math.sign(num) * Math.abs(num);
}
$.fn.digits = function () {
    return this.each(function () {
        $(this).text($(this).text().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
    })
}

function GetlikePage(id) {
    $("#getLikedUser_" + id).submit();
}
/////درصد تکمیل پروفایل و اطلاعات صفحه اول------------------------------------------------------------------------


//$(document).ready(GetUserVariableInfo());

//function GetUserVariableInfo() {
//    $('[data-toggle="tooltip"]').tooltip();
//    $.ajax({
//        url: "/Home/LayoutAction",
//        type: "post",
//        global: false,
//        success: function (data) {
//            $("#userId").text(data.UserId);
//            $("#userName").text(data.UserName);
//            $("#userFullName").text(data.UserName);
//            $("#userNickName").text(data.NickName);
//            $("#cartCount").text(data.CartCount);
//            $("#pointuser").text(data.UserCreditWallet).digits();
//            if (data.UserCreditWallet > 100000) {
//                $("#pointuser").css("font-size", "0.6rem");
//            }
//            $("#crdit").text(kFormatter(data.UserCredit));
//            $("#BillCount").text(data.BillCount);
//            $("#ProductsCount").text(data.ProductsCount);
//            $("#ShareholderListCount").text(data.ShareholderListCount);
//            $("#countfaved").text(data.CountFaved);
//            $("#MySentGifts").text(data.MySentGifts);
//            $("#MyRecievedGifts").text(data.MyRecievedGifts);

//            if (data.unSeenCount != 0) {

//                $(".unSeenCount").text(data.unSeenCount);
//            }

//            $("#cartcount2").text(data.CartCount);

//            //----------------------------------------------------------------------------
//            $("#userpoint").text(data.Point);
//            $("#usercredit").text(kFormatter(data.UserCredit));

//            $("#basicinfoCOMPLT").css("display", data.basicinfoCOMPLT);
//            $("#SkillCOMPLT").css("display", data.SkillCOMPLT);
//            $("#EducationalRecordCOMPLT").css("display", data.EducationalRecordCOMPLT);
//            $("#JobResumeCOMPLT").css("display", data.JobResumeCOMPLT);
//            $("#WorkSampleCOMPLT").css("display", data.WorkSampleCOMPLT);
//            $("#socialnetworkCOMPLT").css("display", data.socialnetworkCOMPLT);

//            var className = "";
//            switch (true) {
//                case (data.UserInfoComplatePecent <= 25):
//                    className = "bg-danger";
//                    break;
//                case (data.UserInfoComplatePecent > 25 && data.UserInfoComplatePecent <= 50):
//                    className = "bg-warning";
//                    break;
//                case (data.UserInfoComplatePecent > 50 && data.UserInfoComplatePecent <= 75):
//                    className = "bg-primary";
//                    break;
//                case (data.UserInfoComplatePecent > 75 && data.UserInfoComplatePecent <= 100):
//                    className = "bg-success";
//                    break;
//            }
//            var UserInfoComplatePecent = Number(data.UserInfoComplatePecent).toFixed(0);
//            $("#UserInfoComplatePecent").text(UserInfoComplatePecent + "%");
//            $("#UserInfoComplatePecent").css("width", UserInfoComplatePecent + "%");
//            $("#UserInfoComplatePecent").attr("aria-valuenow", UserInfoComplatePecent);
//            var e = document.getElementById("UserInfoComplatePecent");
//            var q = e.className;
//            $("#UserInfoComplatePecent").removeClass(q);

//            $("#UserInfoComplatePecent").addClass("progress-bar progress-bar-striped " + className);

//            if (UserInfoComplatePecent >= 100) {
//                $("#ICONUserInfoComplate").css("display", "block");
//            } else {
//                $("#ICONUserInfoComplate").css("display", "none");

//            }
//            if (data.UserImg === "" || data.UserImg === null) {
//                $("#userAvatar").attr("src", "/Content/img/default_logo.jpg");

//            } else {
//                $("#userAvatar").attr("src", data.UserImg);

//            }
//            $("#userSkill").empty();
//            jQuery.each(data.SkilList, function (i, val) {
//                $("#userSkill").append("<li>" + val + "</li>");
//            });
//            $("#userfullName").text(data.FullName);
//            $("#userNickName").text(data.NickName);
//            $("#GotoProfile").attr("href", "/Home/Profile?UserName=" + data.UserName);
//            $("#VisionCount").text(data.AdviceVisionCount);
//            ////------------------------------------------------------------------
//        }
//    });
//}

////درصد تکمیل پروفایل----------------------------------End--------------------------------------

function valueMapper(options) {
    $.ajax({
        url: "https://demos.telerik.com/kendo-ui/service/Orders/ValueMapper",
        type: "GET",
        data: convertValues(options.value),
        success: function (data) {
            options.success(data);
        }
    });
}

function convertValues(value) {
    var data = {};

    value = $.isArray(value) ? value : [value];

    for (var idx = 0; idx < value.length; idx++) {
        data["values[" + idx + "]"] = value[idx];
    }

    return data;
}
////image zoom method----------------------------------start-----------------------------------------------------------

// Get the modal
var imagemodal = document.getElementById("imageModal");

// Get the image and insert it inside the modal - use its "alt" text as a caption
var modalImg = document.getElementById("img01");
var captionText = document.getElementById("imagecaption");

function openmodal(e) {
    // Get the modal
    imagemodal = document.getElementById("imageModal");
    // Get the image and insert it inside the modal - use its "alt" text as a caption
    modalImg = document.getElementById("img01");
    captionText = document.getElementById("imagecaption");

    imagemodal.style.display = "block";
    modalImg.src = e.src;
    captionText.innerHTML = e.alt;
};

window.onclick = function (event) {
    if (event.target === imagemodal) {
        imagemodal.style.display = "none";
    }
}
// When the user clicks on <span> (x), close the modal
function closemodal() {
    imagemodal.style.display = "none";
}


function imageZoom(imgID, resultID) {
    var img, lens, result, cx, cy;
    img = document.getElementById(imgID);
    result = document.getElementById(resultID);
    /*create lens:*/
    lens = document.createElement("DIV");
    lens.setAttribute("class", "img-zoom-lens");
    /*insert lens:*/
    img.parentElement.insertBefore(lens, img);
    /*calculate the ratio between result DIV and lens:*/
    cx = result.offsetWidth / lens.offsetWidth;
    cy = result.offsetHeight / lens.offsetHeight;
    /*set background properties for the result DIV:*/
    result.style.backgroundImage = "url('" + img.src + "')";
    result.style.backgroundSize = (img.width * cx) + "px " + (img.height * cy) + "px";
    /*execute a function when someone moves the cursor over the image, or the lens:*/
    lens.addEventListener("mousemove", moveLens);
    img.addEventListener("mousemove", moveLens);
    /*and also for touch screens:*/
    lens.addEventListener("touchmove", moveLens);
    img.addEventListener("touchmove", moveLens);
    function moveLens(e) {
        var pos, x, y;
        /*prevent any other actions that may occur when moving over the image:*/
        e.preventDefault();
        /*get the cursor's x and y positions:*/
        pos = getCursorPos(e);
        /*calculate the position of the lens:*/
        x = pos.x - (lens.offsetWidth / 2);
        y = pos.y - (lens.offsetHeight / 2);
        /*prevent the lens from being positioned outside the image:*/
        if (x > img.width - lens.offsetWidth) { x = img.width - lens.offsetWidth; }
        if (x < 0) { x = 0; }
        if (y > img.height - lens.offsetHeight) { y = img.height - lens.offsetHeight; }
        if (y < 0) { y = 0; }
        /*set the position of the lens:*/
        lens.style.left = x + "px";
        lens.style.top = y + "px";
        /*display what the lens "sees":*/
        result.style.backgroundPosition = "-" + (x * cx) + "px -" + (y * cy) + "px";
    }
    function getCursorPos(e) {
        var a, x = 0, y = 0;
        e = e || window.event;
        /*get the x and y positions of the image:*/
        a = img.getBoundingClientRect();
        /*calculate the cursor's x and y coordinates, relative to the image:*/
        x = e.pageX - a.left;
        y = e.pageY - a.top;
        /*consider any page scrolling:*/
        x = x - window.pageXOffset;
        y = y - window.pageYOffset;
        return { x: x, y: y };
    }
}
////image zoom method----------------------------------end-----------------------------------------------------------


function FailMessage(xhr) {

    var popupNotification = $("#popupNotification").data("kendoNotification");

    if (xhr.status === 305) {

        popupNotification.show({
            title: "پیغام خطا",
            message: xhr.responseText
        }, "error");
    }
    else {
        popupNotification.show({
            title: "پیغام",
            message: "خطای غیر منتظره سمت سرور"
        }, "error");
    }

}

//-----------------------------------------برای استفاده در مشاوره محصول استفاده میشود------------------------------------------------
//اسکریپت اقزودن به سبد خرید
//function addtocart(Id) {

//    var id = Id;
//    var CartType = 0;
//    $('#cartspinner').removeClass('d-none');
//    $.ajax({
//        url: '/Shops/AddToCart',
//        data: { id: id, CartType: CartType },
//        type: "post",
//        cache: false,
//        success: function (data) {

//            var msg = data[0].Message;
//            var stat = data[0].Statue;
//            $('#cartspinner').addClass('d-none');
//            if (stat === 1) {
//                var c1 = $('.cartcount1').text();
//                var addone = parseInt(c1);
//                addone = addone + 1;
//                $('.cartcount1').html(addone);
//                $('.cartcount2').html(addone);
//            }
//            var popupNotification = $("#popupNotification").data("kendoNotification");

//            if (stat === 1) {

//                popupNotification.show({
//                    title: "پیغام",
//                    message: msg
//                }, "success");
//            } else {

//                popupNotification.show({
//                    title: "پیغام",
//                    message: msg
//                }, "error");
//            }

//        },
//        error: function (xhr, ajaxOptions, thrownError) {
//            $('#cartspinner').addClass('d-none');
//            alert("خطایی رخ داده است");
//        }
//    });

//}

//scroll function-----------------------------------------------------------------------------------
function SaveScorll() {
    //localStorage.setItem('routeScrollPos',null);
    //var r=localStorage.getItem('routeScrollPos');
    var pathname = window.location.pathname;
    if (pathname == "/" || pathname == "/Home/Index") {
        var el = document.querySelector('#listView');
        var y = el.scrollTop;
        var x = el.scrollLeft;
    } else {
        var el = document.documentElement;
        if (/^((?!chrome|android).)*safari/i.test(navigator.userAgent)) {
            el = document.body;
        }
        var y = el.scrollTop;
        var x = el.scrollLeft;
    }

    var existing = localStorage.getItem('routeScrollPos');
    existing = existing != null ? JSON.parse(existing) : {};
    existing[pathname] = { x: x, y: y };
    localStorage.setItem('routeScrollPos', JSON.stringify(existing));
}

function GotoScroll(element, y, x) {
    if (typeof (element) != 'undefined' && element != null) {
        element.scrollTop = y;
        element.scrollLeft = x;
    }
}

function GetPathPosAndGo(ifkendoBound) {
    var pathname = window.location.pathname;
    var existing = localStorage.getItem('routeScrollPos');
    existing = JSON.parse(existing);
    var pos = existing[pathname];
    if (typeof (pos) != 'undefined' && pos != null) {
        var x = pos["x"];
        var y = pos["y"];
        if (pathname == "/" || pathname == "/Home/Index") {
            var el = document.querySelector('#listView');
            if (ifkendoBound) {
                GotoScroll(el, y, x);
            }
        } else {
            var el = document.documentElement;
            if (/^((?!chrome|android).)*safari/i.test(navigator.userAgent)) {
                el = document.body;
            }
            GotoScroll(el, y, x);
        }
    }
}
//End scroll function-----------------------------------------------------------------------------------

function goBack() {
    window.history.back();
}

function closePanel() {
    
}

function isNullOrEmpty(value) {
    return (!value || value == undefined || value == "" || value.length == 0);
}


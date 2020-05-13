$(".sidebar-dropdown > a").click(function () {
    $(".sidebar-submenu").slideUp(300);
    if (
        $(this)
            .parent()
            .hasClass("active")
    ) {
        $(".sidebar-dropdown").removeClass("active");
        $(this)
            .parent()
            .removeClass("active");
    } else {
        $(".sidebar-dropdown").removeClass("active");
        $(this)
            .next(".sidebar-submenu")
            .slideDown(300);
        $(this)
            .parent()
            .addClass("active");
    }
});

/*$("#close-sidebar").click(function() {
    $(".page-wrapper").removeClass("toggled");
});
$("#show-sidebar").click(function() {
    $(".page-wrapper").addClass("toggled");
});*/
$("#close-sidebar").click(function () {
    $(".page-wrapper").removeClass("toggled");
    $('#sidebar #chat #text-chat').animate({ right: '-400' }, 150);
    $('#sidebar #chat .back-to-search').animate({ right: '-400' }, 150);
    $('#shopping-cart').css('margin-right', '50px');
    $('#shopping-cart').css('transition', 'all 0.3s');

    clickHomeBtn();
});
$("#show-sidebar").click(function () {
    $(".page-wrapper").addClass("toggled");
    $('#sidebar #chat #text-chat').animate({ right: '0' }, 270);
    $('#sidebar #chat .back-to-search').animate({ right: '0' }, 270);
    if (window.innerWidth <= 1200) {
        $('#shopping-cart').css('margin-right', '50px');
        $('#shopping-cart').css('transition', 'all 0.3s');
    } else {
        $('#shopping-cart').css('margin-right', '300px');
        $('#shopping-cart').css('transition', 'all 0.3s');
    }
});


function isNullOrEmpty(value) {
return (!value || value == undefined || value == "" || value.length == 0);
}
//=====================showProfile()=================
function showProfile() {
    $('#show-profile').click();
    $('.navbar-logo').fadeOut(10);
    $('.sidebar-wrapper').css('margin-right', '200px');
    setTimeout(function () {
        $('.sidebar-wrapper').css('margin-right', '0');
        $('.navbar-logo').fadeIn(500);
    }, 300);
    $("#show-sidebar").click();

}
function showSidebarAlarm() {
    
    $("#show-sidebar").click();
    $("#show-search-person-sidebar").click();
}

function showSidebarAdviser() {
    $("#show-adviser-sidebar").click();
    $("#show-sidebar").click();
AviceVisionAndSuggesstproduct();
}
function clickHomeBtn() {
    $("#show-best-person").click();
}

function openSideCategory() {
    $("#show-sidebar").click();
    $("#show-category").click();
}


//===================================================
function shoppingCart() {
    $('#show-shopping-cart').click();
    $('.sidebar-wrapper').css('margin-right', '20px');
    setTimeout(function () {
        $('.sidebar-wrapper').css('margin-right', '0');
    }, 300);
    $("#show-sidebar").click();
}
$('.collaps-sort').slideUp();
function collapsSort() {
    $('.collaps-sort').slideToggle();
}

//==========================Book Shelf=========================
function showBookShelf() {
    $('#show-book-shelf').click();
    $('.sidebar-wrapper').css('margin-right', '20px');
    setTimeout(function () {
        $('.sidebar-wrapper').css('margin-right', '0');
    }, 300);
    $("#show-sidebar").click();
}

//==========================Book Mark=========================
function bookMark() {
    $('#show-book-mark').click();
    $('.sidebar-wrapper').css('margin-right', '20px');
    setTimeout(function () {
        $('.sidebar-wrapper').css('margin-right', '0');
    }, 300);
    $("#show-sidebar").click();
}
//==========================log in=========================
function showLoginSidebar() {
    $('#show-search-person-sidebar-2').click();
    $('.sidebar-wrapper').css('margin-right', '20px');
    setTimeout(function () {
        $('.sidebar-wrapper').css('margin-right', '0');
    }, 300);
    $("#show-sidebar").click();
}
//==========================register=========================
function showRegisterSidebar() {
    $('#show-register-sidebar').click();
    $('.sidebar-wrapper').css('margin-right', '20px');
    setTimeout(function () {
        $('.sidebar-wrapper').css('margin-right', '0');
    }, 300);
    $("#show-sidebar").click();
}
//==========================return=========================
function showReturnSidebar() {
    $('#show-return-sidebar').click();
    $('.sidebar-wrapper').css('margin-right', '20px');
    setTimeout(function () {
        $('.sidebar-wrapper').css('margin-right', '0');
    }, 300);
    $("#show-sidebar").click();
}

//=========================Show comment sidebar=================
function showCommentSidebar() {
    $('#show-comment-sidebar').click();
    $('.sidebar-wrapper').css('margin-right', '50px');
    setTimeout(function () {
        $('.sidebar-wrapper').css('margin-right', '0');
    }, 300);
}
function showCategorySidebar() {
    setTimeout(function () {
        if ($('#comment-modal').hasClass('show')) {

        } else {
            $('#show-category').click();
            $('.sidebar-wrapper').css('margin-right', '50px');
            setTimeout(function () {
                $('.sidebar-wrapper').css('margin-right', '0');
            }, 300);
        }
    }, 100);

}

//=======================City======================
$(document).ready(function () {

    

    loadprovince();
    $(".province").change(function () {
        $(this).closest('div').find('.city').addClass("temp09120217432class");
        loadCity($(this).val());
    });
});

function loadprovince() {
    selectValues = {
        "": "", "آذربایجان‌شرقی": "آذربایجان شرقی", "آذربایجان‌غربی": "آذربایجان غربی", "اردبیل": "اردبیل", "اصفهان": "اصفهان", "البرز": "البرز", "ایلام": "ایلام",
        "بوشهر": "بوشهر", "تهران": "تهران", "چهارمحال‌و‌بختیاری": "چهارمحال و بختیاری", "خراسان‌جنوبی": "خراسان جنوبی", "خراسان‌رضوی": "خراسان رضوی", "خراسان‌شمالی": "خراسان شمالی", "خوزستان": "خوزستان",
        "زنجان": "زنجان", "سمنان": "سمنان", "سیستان‌و‌بلوچستان": "سیستان و بلوچستان", "فارس": "فارس", "قزوین": "قزوین", "قم": "قم", "کردستان": "کردستان",
        "کرمان": "کرمان", "کرمانشاه": "کرمانشاه", "کهکیلویه‌و‌بویراحمد": "کهگیلویه و بویراحمد", "گلستان": "گلستان", "گیلان": "گیلان", "لرستان": "لرستان", "مازندران": "مازندران",
        "مرکزی": "مرکزی", "هرمزگان": "هرمزگان", "همدان": "همدان", "یزد": "یزد"
    };

    $.each(selectValues, function (key, value) {
        $('.province')
            .append($("<option></option>")
                .attr("value", key)
                .text(value));
    });
}

//Load city for selete

function loadCity(province) {
    $(".temp09120217432class").find('option').remove();

    switch (province) {
        case "آذربایجان‌شرقی":
            var selectValues = { "آذرشهر": "آذرشهر", "اسکو": "اسکو", "اهر": "اهر", "بستان‌آباد": "بستان‌آباد", "بناب": "بناب", "تبریز": "تبریز", "جلفا": "جلفا", "چاراویماق": "چاراویماق", "سراب": "سراب", "شبستر": "شبستر", "عجب‌شیر": "عجب‌شیر", "کلیبر": "کلیبر", "مراغه": "مراغه", "مرند": "مرند", "ملکان": "ملکان", "میانه": "میانه", "ورزقان": "ورزقان", "هریس": "هریس", "هشترود": "هشترود" };
            break;
        case "آذربایجان‌غربی":
            var selectValues = { "ارومیه": "ارومیه", "اشنویه": "اشنویه", "بوکان": "بوکان", "پیرانشهر": "پیرانشهر", "تکاب": "تکاب", "چالدران": "چالدران", "خوی": "خوی", "سردشت": "سردشت", "سلماس": "سلماس", "شاهین‌دژ": "شاهین‌دژ", "ماکو": "ماکو", "مهاباد": "مهاباد", "میاندوآب": "میاندوآب", "نقده": "نقده" };
            break;
        case "اردبیل":
            var selectValues = { "اردبیل": "اردبیل", "بیله‌سوار": "بیله‌سوار", "پارس‌آباد": "پارس‌آباد", "خلخال": "خلخال", "کوثر": "کوثر", "گرمی": "گرمی", "مشگین شهر": "مشگین شهر", "نمین": "نمین", "نیر": "نیر" };
            break;
        case "اصفهان":
            var selectValues = { "آران و بیدگل": "آران و بیدگل", "اردستان": "اردستان", "اصفهان": "اصفهان", "برخوردار و میمه": "برخوردار و میمه", "تیران و کرون": "تیران و کرون", "چادگان": "چادگان", "خمینی‌شهر": "خمینی‌شهر", "خوانسار": "خوانسار", "سمیرم": "سمیرم", "شهرضا": "شهرضا", "سمیرم سفلی": "سمیرم سفلی", "فریدن": "فریدن", "فریدون‌شهر": "فریدون‌شهر", "فلاورجان": "فلاورجان", "کاشان": "کاشان", "گلپایگان": "گلپایگان", "لنجان": "لنجان", "مبارکه": "مبارکه", "نائین": "نائین", "نجف‌آباد": "نجف‌آباد", "نطنز": "نطنز" };
            break;
        case "البرز":
            var selectValues = { "ساوجبلاغ": "ساوجبلاغ", "طالقان": "طالقان", "کرج": "کرج", "نظرآباد": "نظرآباد" };
            break;
        case "ایلام":
            var selectValues = { "آبدانان": "آبدانان", "ایلام": "ایلام", "ایوان": "ایوان", "دره‌شهر": "دره‌شهر", "دهلران": "دهلران", "شیروان و چرداول": "شیروان و چرداول", "مهران": "مهران" };
            break;
        case "بوشهر":
            var selectValues = { "بوشهر": "بوشهر", "تنگستان": "تنگستان", "جم": "جم", "دشتستان": "دشتستان", "دشتی": "دشتی", "دیر": "دیر", "دیلم": "دیلم", "کنگان": "کنگان", "گناوه": "گناوه" };
            break;
        case "تهران":
            var selectValues = { "ورامین": "ورامین", "فیروزکوه": "فیروزکوه", "شهریار": "شهریار", "شمیرانات": "شمیرانات", "ری": "ری", "رباط‌کریم": "رباط‌کریم", "دماوند": "دماوند", "تهران": "تهران", "پاکدشت": "پاکدشت", "اسلام‌شهر": "اسلام‌شهر" };
            break;
        case "چهارمحال‌و‌بختیاری":
            var selectValues = { "اردل": "اردل", "بروجن": "بروجن", "شهرکرد": "شهرکرد", "فارسان": "فارسان", "کوهرنگ": "کوهرنگ", "لردگان": "لردگان" };
            break;
        case "خراسان‌جنوبی":
            var selectValues = { "بیرجند": "بیرجند", "درمیان": "درمیان", "سرایان": "سرایان", "سربیشه": "سربیشه", "فردوس": "فردوس", "قائنات": "قائنات", "نهبندان": "نهبندان" };
            break;
        case "خراسان‌رضوی":
            var selectValues = { "بردسکن": "بردسکن", "تایباد": "تایباد", "تربت جام": "تربت جام", "تربت حیدریه": "تربت حیدریه", "چناران": "چناران", "خلیل‌آباد": "خلیل‌آباد", "خواف": "خواف", "درگز": "درگز", "رشتخوار": "رشتخوار", "سبزوار": "سبزوار", "سرخس": "سرخس", "فریمان": "فریمان", "قوچان": "قوچان", "کاشمر": "کاشمر", "کلات": "کلات", "گناباد": "گناباد", "مشهد": "مشهد", "مه ولات": "مه ولات", "نیشابور": "نیشابور" };
            break;
        case "خراسان‌شمالی":
            var selectValues = { "اسفراین": "اسفراین", "بجنورد": "بجنورد", "جاجرم": "جاجرم", "شیروان": "شیروان", "فاروج": "فاروج", "مانه و سملقان": "مانه و سملقان" };
            break;
        case "خوزستان":
            var selectValues = { "آبادان": "آبادان", "امیدیه": "امیدیه", "اندیمشک": "اندیمشک", "اهواز": "اهواز", "ایذه": "ایذه", "باغ‌ملک": "باغ‌ملک", "بندر ماهشهر": "بندر ماهشهر", "بهبهان": "بهبهان", "خرمشهر": "خرمشهر", "دزفول": "دزفول", "دشت آزادگان": "دشت آزادگان", "رامشیر": "رامشیر", "رامهرمز": "رامهرمز", "شادگان": "شادگان", "شوش": "شوش", "شوشتر": "شوشتر", "گتوند": "گتوند", "لالی": "لالی", "مسجد سلیمان": "مسجد سلیمان", "هندیجان": "هندیجان" };
            break;
        case "زنجان":
            var selectValues = { "ابهر": "ابهر", "ایجرود": "ایجرود", "خدابنده": "خدابنده", "خرمدره": "خرمدره", "زنجان": "زنجان", "طارم": "طارم", "ماه‌نشان": "ماه‌نشان" };
            break;
        case "سمنان":
            var selectValues = { "دامغان": "دامغان", "سمنان": "سمنان", "شاهرود": "شاهرود", "گرمسار": "گرمسار", "مهدی‌شهر": "مهدی‌شهر" };
            break;
        case "سیستان‌و‌بلوچستان":
            var selectValues = { "ایرانشهر": "ایرانشهر", "چابهار": "چابهار", "خاش": "خاش", "دلگان": "دلگان", "زابل": "زابل", "زاهدان": "زاهدان", "زهک": "زهک", "سراوان": "سراوان", "سرباز": "سرباز", "کنارک": "کنارک", "نیک‌شهر": "نیک‌شهر" };
            break;
        case "فارس":
            var selectValues = { "آباده": "آباده", "ارسنجان": "ارسنجان", "استهبان": "استهبان", "اقلید": "اقلید", "بوانات": "بوانات", "پاسارگاد": "پاسارگاد", "جهرم": "جهرم", "خرم‌بید": "خرم‌بید", "خنج": "خنج", "داراب": "داراب", "زرین‌دشت": "زرین‌دشت", "سپیدان": "سپیدان", "شیراز": "شیراز", "فراشبند": "فراشبند", "فسا": "فسا", "فیروزآباد": "فیروزآباد", "قیر و کارزین": "قیر و کارزین", "کازرون": "کازرون", "لارستان": "لارستان", "لامرد": "لامرد", "مرودشت": "مرودشت", "ممسنی": "ممسنی", "مهر": "مهر", "نی‌ریز": "نی‌ریز" };
            break;
        case "قزوین":
            var selectValues = { "آبیک": "آبیک", "البرز": "البرز", "بوئین‌زهرا": "بوئین‌زهرا", "تاکستان": "تاکستان", "قزوین": "قزوین" };
            break;
        case "قم":
            var selectValues = { "قم": "قم" };
            break;
        case "کردستان":
            var selectValues = { "بانه": "بانه", "بیجار": "بیجار", "دیواندره": "دیواندره", "سروآباد": "سروآباد", "سقز": "سقز", "سنندج": "سنندج", "قروه": "قروه", "کامیاران": "کامیاران", "مریوان": "مریوان" };
            break;
        case "کرمان":
            var selectValues = { "بافت": "بافت", "بردسیر": "بردسیر", "بم": "بم", "جیرفت": "جیرفت", "راور": "راور", "رفسنجان": "رفسنجان", "رودبار جنوب": "رودبار جنوب", "زرند": "زرند", "سیرجان": "سیرجان", "شهر بابک": "شهر بابک", "عنبرآباد": "عنبرآباد", "قلعه گنج": "قلعه گنج", "کرمان": "کرمان", "کوهبنان": "کوهبنان", "کهنوج": "کهنوج", "منوجان": "منوجان" };
            break;
        case "کرمانشاه":
            var selectValues = { "اسلام‌آباد غرب": "اسلام‌آباد غرب", "پاوه": "پاوه", "ثلاث باباجانی": "ثلاث باباجانی", "جوانرود": "جوانرود", "دالاهو": "دالاهو", "روانسر": "روانسر", "سرپل ذهاب": "سرپل ذهاب", "سنقر": "سنقر", "صحنه": "صحنه", "قصر شیرین": "قصر شیرین", "کرمانشاه": "کرمانشاه", "کنگاور": "کنگاور", "گیلان غرب": "گیلان غرب", "هرسین": "هرسین" };
            break;
        case "کهکیلویه‌و‌بویراحمد":
            var selectValues = { "بویراحمد": "بویراحمد", "بهمئی": "بهمئی", "دنا": "دنا", "کهگیلویه": "کهگیلویه", "گچساران": "گچساران" };
            break;
        case "گلستان":
            var selectValues = { "آزادشهر": "آزادشهر", "آق‌قلا": "آق‌قلا", "بندر گز": "بندر گز", "ترکمن": "ترکمن", "رامیان": "رامیان", "علی‌آباد": "علی‌آباد", "کردکوی": "کردکوی", "کلاله": "کلاله", "گرگان": "گرگان", "گنبد کاووس": "گنبد کاووس", "مراوه‌تپه": "مراوه‌تپه", "مینودشت": "مینودشت" };
            break;
        case "گیلان":
            var selectValues = { "آستارا": "آستارا", "آستانه": "آستانه", "اشرفیه": "اشرفیه", "املش": "املش", "بندر انزلی": "بندر انزلی", "رشت": "رشت", "رضوانشهر": "رضوانشهر", "رودبار": "رودبار", "رودسر": "رودسر", "سیاهکل": "سیاهکل", "شفت": "شفت", "صومعه‌سرا": "صومعه‌سرا", "طوالش": "طوالش", "فومن": "فومن", "لاهیجان": "لاهیجان", "لنگرود": "لنگرود", "ماسال": "ماسال" };
            break;
        case "لرستان":
            var selectValues = { "ازنا": "ازنا", "الیگودرز": "الیگودرز", "بروجرد": "بروجرد", "پل‌دختر": "پل‌دختر", "خرم‌آباد": "خرم‌آباد", "دورود": "دورود", "دلفان": "دلفان", "سلسله": "سلسله", "کوهدشت": "کوهدشت" };
            break;
        case "مازندران":
            var selectValues = { "آمل": "آمل", "بابل": "بابل", "بابلسر": "بابلسر", "بهشهر": "بهشهر", "تنکابن": "تنکابن", "جویبار": "جویبار", "چالوس": "چالوس", "رامسر": "رامسر", "ساری": "ساری", "سوادکوه": "سوادکوه", "قائم‌شهر": "قائم‌شهر", "گلوگاه": "گلوگاه", "محمودآباد": "محمودآباد", "نکا": "نکا", "نور": "نور", "نوشهر": "نوشهر" };
            break;
        case "مرکزی":
            var selectValues = { "آشتیان": "آشتیان", "اراک": "اراک", "تفرش": "تفرش", "خمین": "خمین", "دلیجان": "دلیجان", "زرندیه": "زرندیه", "ساوه": "ساوه", "شازند": "شازند", "کمیجان": "کمیجان", "محلات": "محلات" };
            break;
        case "هرمزگان":
            var selectValues = { "ابوموسی": "ابوموسی", "بستک": "بستک", "بندر عباس": "بندر عباس", "بندر لنگه": "بندر لنگه", "جاسک": "جاسک", "حاجی‌آباد": "حاجی‌آباد", "خمیر": "خمیر", "رودان": "رودان", "قشم": "قشم", "گاوبندی": "گاوبندی", "میناب": "میناب" };
            break;
        case "همدان":
            var selectValues = { "اسدآباد": "اسدآباد", "بهار": "بهار", "تویسرکان": "تویسرکان", "رزن": "رزن", "کبودرآهنگ": "کبودرآهنگ", "ملایر": "ملایر", "نهاوند": "نهاوند", "همدان": "همدان" };
            break;
        case "یزد":
            var selectValues = { "ابرکوه": "ابرکوه", "اردکان": "اردکان", "بافق": "بافق", "تفت": "تفت", "خاتم": "خاتم", "صدوق": "صدوق", "طبس": "طبس", "مهریز": "مهریز", "میبد": "میبد", "یزد": "یزد" };
            break;
        case "":
            var selectValues = { "": "" }

    }


    $.each(selectValues, function (key, value) {
        $(".temp09120217432class")
            .append($("<option></option>")
                .attr("value", key)
                .text(value));
    });
    $(".temp09120217432class").removeClass("temp09120217432class");
}



//===================================
$('.expand-button').on('click', function () {
    $(this).closest('section').find('.special-text').toggleClass('-expanded');

    if ($(this).closest('section').find('.special-text').hasClass('-expanded')) {
        $(this).html('<i class="fas fa-angle-double-up"></i>');
    } else {
        $(this).html('<i class="fas fa-angle-double-down"></i>');
    }
});

//===================================
function uploadVideo() {
    $('#customVideo').click();
}
function uploadCamera() {
    $('#customCamera').click();
}
function uploadFile() {
    $('#customFile').click();
}

function startPostCameraModal() {
    $("#createPostBtn").click();
    setTimeout(function () {

        $('#customCamera').click();


    }, 500);
}
function startPostVideoModal() {
    $("#createPostBtn").click();

    setTimeout(function () {

        $('#customVideo').click();


    }, 500);
}
function startPostFileModal() {
    $("#createPostBtn").click();

    setTimeout(function () {
        $('#customFile').click();

    }, 500);
}

function showSearchPerson() {
    $("#show-search-person-sidebar").click();
}
function feedShowComments() {
    $("#show-search-person-sidebar-1").click();

}

//===================================
$(document).on({
    mouseover: function (event) {
        var star = $(this).find('.star');
        $(this).find('.far').addClass('star-over');
        $(this).prevAll().find('.far').addClass('star-over');
    },
    success: function (data) {
        $("#userpoint").text(data.Point);
        $("#usercredit").text(data.Credit);
        var className = "";
        switch (true) {
            case (data.UserInfoComplatePecent <= 25):
                className = "bg-danger";
                break;
            case (data.UserInfoComplatePecent > 25 && data.UserInfoComplatePecent <= 50):
                className = "bg-warning";
                break;
            case (data.UserInfoComplatePecent > 50 && data.UserInfoComplatePecent <= 75):
                className = "bg-primary";
                break;
            case (data.UserInfoComplatePecent > 75 && data.UserInfoComplatePecent <= 100):
                className = "bg-success";
                break;
        }
        var UserInfoComplatePecent = Number(data.UserInfoComplatePecent).toFixed(0);
        $("#UserInfoComplatePecent").text(UserInfoComplatePecent + "%");
        $("#UserInfoComplatePecent").css("width", UserInfoComplatePecent + "%");
        $("#UserInfoComplatePecent").attr("aria-valuenow", UserInfoComplatePecent);
        $("#UserInfoComplatePecent").addClass(className);
        if (UserInfoComplatePecent >= 100) {
            $("#ICONUserInfoComplate").css("display", "block");
        } else {
            $("#ICONUserInfoComplate").css("display", "none");

        }
        if (data.UserImg == "" || data.UserImg == null) {
            $("#userAvatar").attr("src", "/Content/img/default_logo.jpg");

        } else {
            $("#userAvatar").attr("src", data.UserImg);
        }
    }
});
$(document).on('click', '.rate', function () {
    var star = $(this).find('.star');
    if (!$(this).hasClass('rate-active')) {
        $(this).siblings().find('.star').addClass('far').removeClass('fas rate-active').css('color', '#91a6ff');
        star.addClass('rate-active fas').removeClass('far star-over');
        $(this).prevAll().find('.star').addClass('fas').removeClass('far star-over').css('color', '#91a6ff');
    }
});

//===================scrollRowBooks===================

function scrollRowLeftBooks1() {
    document.getElementById("table-show-books-1").scrollBy(-1000, 0);
}
function scrollRowLeftBooks2() {
    document.getElementById("table-show-books-2").scrollBy(-1000, 0);
}
function scrollRowLeftBooks3() {
    document.getElementById("table-show-books-3").scrollBy(-1000, 0);
}
function scrollRowRightBooks1() {
    document.getElementById("table-show-books-1").scrollBy(1000, 0);
}
function scrollRowRightBooks2() {
    document.getElementById("table-show-books-2").scrollBy(1000, 0);
}
function scrollRowRightBooks3() {
    document.getElementById("table-show-books-3").scrollBy(1000, 0);
}
$(".emojionearea-editor").hasClass('focused', function () {
    alert('akjsdhkajh');
});

var timerOn = true;

function timer(remaining) {
    timerOn = true;
    var m = Math.floor(remaining / 60);
    var s = remaining % 60;

    m = m < 10 ? '0' + m : m;
    s = s < 10 ? '0' + s : s;
    if ($('#timer').length > 0) {
        document.getElementById('timer').innerHTML = m + ':' + s;
    }

    remaining -= 1;

    if (remaining >= 0 && timerOn) {
        setTimeout(function () {
            timer(remaining);
        }, 1000);
        return;
    }
    timerOn = false;

    if (!timerOn) {


        var btn_send = "<a href='#resend' class='btn btn-success' id='resend' onclick='ResendCode();'>ارسال مجدد کد</a>";

        $('#resendcode').append(btn_send);
        // Do validate stuff here
        return;
    }

    // Do timeout stuff here
    $('#btn-timer-verify').removeAttr('disabled');

}

timer(120);

function ResendCode() {
    var userid = document.getElementById('UserId').value;
    if (userid == "" || userid == null) {
        alert("خطای ارسال پارامتر نادرست");
        return 0;
    }


    $.ajax({
        url: '/Manage/ResendCode',
        data: { id: userid },
        type: "post",
        cache: false,
        success: function (data) {
            $('#resendcode').html(' ');
            timer(120);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('خطایی رخ داده است');
        }
    });

}

//document.getElementById("show-best-person").addEventListener("click", function (event) {
//    
//    return false;
//});

//var prev = 0;
//var $window = $('#divPostList');
//var nav = $('.tabbable');

//$window.on('scroll', function () {
//    var scrollTop = $window.scrollTop();
//    nav.toggleClass('hidden', scrollTop > prev);
//    prev = scrollTop;
//});


if ( $( "#listView" ).length ) {
 
 
}else{
$(document).ajaxStart(function (event, xhr, options) {

    $('.spinner').css('display', 'block');

}).ajaxComplete(function (event, xhr, options) {

    $('.spinner').css('display', 'none');

}).ajaxError(function (event, jqxhr, settings, exception) {

    $('.spinner').css('display', 'none');

});
$(window).on('load', function () {
	$('.spinner').css('display', 'none');
});
}

$(window).on('beforeunload', function () {
	$('.spinner').css('display', 'block');
});
$(document).ready(function() {
	$("form").on("submit", function () {
		var isvalid=$(this).valid();
		if (isvalid) {
			$('.spinner').css('display', 'block');
		} else {
			$('.spinner').css('display', 'none');
		}
		
	});
});


//$(document).ready(function() {
//	AviceVisionAndSuggesstproduct();
//});
   function AviceVisionAndSuggesstproduct() {
	     ///Advice Product
    $('.spinner').css('display', 'block');

                $.ajax({
                    url: "/AdviceProduct/GetProductLayout",
                    type: "post",
                    global: false,
                    success: function (data) {
                        if (data.length > 0) {
                            for (var i = 0; i < 4; i++) {
                                var skillString = "";
                                if (data[i].product.Type == 0) {
                                    $.each(data[i].SKills, function (index, value) {
                                        skillString = skillString + "<span class='badge badge-primary p-1'>" + value + " </span>";
                                    });
                                    var book =
                                        "<li class='mt-2 show-book'><div class='row no-gutters'><div class='col-3'><a href='/Shops/Product/" +
                                            data[i].product.Id +
                                            "'><img src='" +
                                            data[i].product.Img +
                                            "' class='img-fluid rounded p-1' alt=''></a></div><div class='col-9'><a href='/Shops/Product/" +
                                            data[i].product.Id +
                                            "'><div class='content-book p-2 mr-2'><p id='ProductTitle'><i class='fas fa-book-open'></i> " +
                                            data[i].product.Title +
                                            "</p><small id='ProducterOrDateOrTeacher'><i class='fas fa-user-edit' ></i> " +
                                            data[i].book.AuthorFullName +
                                            "</small><br><small id='PublishersOrDesc'><i class='fas fa-pen-nib'></i> " +
                                            data[i].book.PublishersFullName +
                                            "</small><br><small id='PublishersOrDesc'><i class='fas fa-cog'></i> " +
                                            skillString
                                            +
                                            "</small></div></div></a></div></li>";

                                    $("#AdviceProduct").append(book);
                                }
                                else if (data[i].product.Type == 1) {
                                    $.each(data[i].SKills, function (index, value) {
                                        skillString = skillString + "<span class='badge badge-primary p-1'>" + value + " </span>";
                                    });
                                    var course =
                                        "<li class='mt-2 show-book'><div class='row no-gutters'><div class='col-3'><a href='/Shops/Product/" +
                                            data[i].product.Id +
                                            "'><img src='" +
                                            data[i].product.Img +
                                            "' class='img-fluid rounded p-1' alt=''></a></div><div class='col-9'><a href='/Shops/Product/" +
                                            data[i].product.Id +
                                            "'><div class='content-book p-2 mr-2'><p><i class='fas fa-play-circle'></i> " +
                                            data[i].product.Title +
                                            "</p><small><i class='fas fa-user-tie'></i> " +
                                            data[i].course +
                                            "</small><br><small id='PublishersOrDesc'><i class='fas fa-cog'></i> " +
                                            skillString
                                            +
                                            "</small></div></div></a></div></li>";

                                    $("#AdviceProduct").append(course);
                                }
                                else if (data[i].product.Type == 2) {
                                    $.each(data[i].SKills, function (index, value) {
                                        skillString = skillString + "<span class='badge badge-primary p-1'>" + value + " </span>";
                                    });
                                    var exam =
                                        "<li class='mt-2 show-book'><div class='row no-gutters'><div class='col-3'><a href='/Shops/Product/" +
                                            data[i].product.Id +
                                            "'><img src='" +
                                            data[i].product.Img +
                                            "' class='img-fluid rounded p-1' alt=''></a></div><div class='col-9'><a href='/Shops/Product/" +
                                            data[i].product.Id +
                                            "'><div class='content-book p-2 mr-2'><p><i class='fas fa-file-signature'></i> " +
                                            data[i].product.Title +
                                            "</p><small><i class='fas fa-calendar-alt'></i> " +
                                            data[i].exam +
                                            "</small><br><small id='PublishersOrDesc'><i class='fas fa-cog'></i> " +
                                            skillString
                                            +
                                            "</small></div></div></a></div></li>";

                                    $("#AdviceProduct").append(exam);
                                }

                            }

    $('.spinner').css('display', 'none');

                        }

                    }
                });
                ///Advice Vision layout

                $.ajax({
                    url: "/Visions/GetVisionLayout",
                    type: "post",
                    global: false,
                    success: function (data) {
                        for (var i = 0; i < data.length; i++) {
                            var steps = "";
                            for (var j = 0; j < data[i].Categorys.length; j++) {


                                if (data[i].Skills.filter(w => w.CategoryId == data[i].Categorys[j].Id).length > 0) {

                                    steps = steps +
                                        "<svg id='svg' viewbox='0 0 100 100' title=" +
                                        data[i].Categorys[j].Title +
                                        " data-toggle='tooltip'><circle cx='50' cy='50' r='45' fill='#17a2b8' /><path fill='none' stroke-linecap='round' stroke-width='5' stroke='#fff'stroke-dasharray='" +
                                        (251.2 * data[i].Skills.filter(w => w.CategoryId == data[i].Categorys[j].Id)[0].Percent / 100) +
                                        "," +
                                        (251.2 * (100 - data[i].Skills.filter(w => w.CategoryId == data[i].Categorys[j].Id)[0].Percent) / 100) +
                                        "'d='M50 10a 40 40 0 0 1 0 80a 40 40 0 0 1 0 -80' /><text x='50' y='50' text-anchor='middle' dy='7' font-size='20'>" +
                                        data[i].Skills.filter(w => w.CategoryId == data[i].Categorys[j].Id)[0].Percent +
                                        "<text>%</text></text></svg>";
                                }
                                else {
                                    steps = steps +
                                        "<svg id='svg' viewbox='0 0 100 100' title='" +
                                        data[i].Categorys[j].Title +
                                        "' data-toggle='tooltip'><circle cx='50' cy='50' r='45' fill='#c4cbde' /><path fill='none' stroke-linecap='round' stroke-width='5' stroke='#fff'stroke-dasharray='0,251.2'd='M50 10a 40 40 0 0 1 0 80a 40 40 0 0 1 0 -80' /><text x='50' y='50' text-anchor='middle' dy='7' font-size='20'>%0</text></svg>";
                                }

                            }
                            var vision = "<div class='shop-book'><ul><li class='show-book'><div class='row no-gutters'><div class='col-3'><a><img src='" + data[i].Vision.Category.Img + "' class='img-fluid rounded p-1' alt=''></a></div><div class='col-9'><div class='content-book content-goals p-2 mr-2'><p class='mb-2'><i class='fas fa-bullseye'></i> " + data[i].Vision.Category.Title + "</p> " + steps + "</div></div> </div></li> </ul></div>";
                            steps = "";
                            $("#VisionLayout").append(vision);
                        }



                    }

                });


   }
if ((window.location.href.indexOf("Home/Index") > -1) || (window.location.pathname == "/")) {
        $("#searchBtnMenu").show();
        $("#sortBtnMenu").show();


    }else{
        $("#searchBtnMenu").hide();
        $("#sortBtnMenu").hide();
        $("#createPostBtn").css("pointer-events","none")
}

if (window.location.href.indexOf("Shops?productType") > -1) {
        $("#shopSearchBtnMenu").show();

    }else{
        $("#shopSearchBtnMenu").hide();

}


//file validate error methods
function validate(file, maxfilezieMB, type) {
    $("#file_error").html("");
    var file_size = $(file)[0].files[0].size;
    if (file_size > (parseInt(maxfilezieMB) * 1024 * 1024)) {
        file.value = null;
        $("#file_error").show();
        $("#file_error").html("حجم فایل مجاز نیست، حجم مجاز " + maxfilezieMB + " مگابایت");
        return false;
    }
if($(file)[0].files[0].type.indexOf("application/pdf") < 0  && $('#customFile').val() != null && $('#customFile').val() != ""){
        $("#file_error").show();
        $("#file_error").html("با انتخاب این گزینه <i class='fas fa-file-alt'></i> فقط میتوانید pdf آپلود کنید در صورت تمایل برای بارگزاری تصویر <i class='fas fa-camera-retro'></i>  یاویدئو <i class='fas fa-video'></i>   را انتخاب کنید.");
        $('#customFile').val(null);
        $('#customCamera').val(null);
        $('#customVideo').val(null);
        return false;

}

    $("#file_error").html("");
    $("#file_error").hide();
    $('#image_here').hide();
    $('#video_herev').hide();
    $('#doc_herei').hide();

    $('#image_here').attr('src', '');
    $('#video_herev').attr('src', '');
    $('#doc_herei').attr('src', '');

    readURL(file, type)
    return true;
}
function readURL(input, type) {
if ( /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)){
    $(".card").css("margin-top", "10px");
}else{
    $(".card").css("margin-top", "30px");

}
    $(".card").css("display", "block");
    $(".card").removeClass("d-none");

    if (input.files && input.files[0]) {
        var reader = new FileReader();
        if (type == "video") {
            reader.onload = function (e) {
                $('#video_herev').show();
                $('#image_here').hide();
                $('#image_here').hide();
                $('.croppie-container > .cr-boundary').remove();
                $('.croppie-container > .cr-slider-wrap').remove();
                $('#doc_herei').hide();
                $('#customCamera').val(null);
                $('#customFile').val(null);

                var video_object_url = URL.createObjectURL(input.files[0]);
                $('#video_herev').attr('src', video_object_url);
                $("#uploadsBtn > a").each(function () {
                    $(this).css("pointer-events", "none");
                });
            }
        }
        else if (type == "img") {
            reader.onload = function (e) {
                //$('#image_here').show();
$('.croppie-container > .cr-boundary').remove();
                $('.croppie-container > .cr-slider-wrap').remove();


                $('#doc_herei').hide();
                $('#video_herev').hide();
                $('#customVideo').val(null);
                $('#customFile').val(null);

if (Modernizr.mq('(max-width: 320px)')) {
    //...

var vEl = document.getElementById('image_here'),
			vanilla = new Croppie(vEl, {
			viewport: { width: 250, height: 220 },
			boundary: { width: 270, height: 300 },
            enableOrientation: true
		});
vanilla.bind({
            url: e.target.result,
            orientation: 1
        });


$('#rotateImg').on('click', function (ev) {
			 vanilla.rotate(90);

});
    //save crope img

$('#btn_SpeichernSenden').on('click', function (ev) {
vanilla.result({
				type: 'base64',
                size: 'original'
			}).then(function (base64) {
				$("#cropImg").attr("value",base64)
$("#cropImg").trigger("change");
			});
					});



} else {
    //...

var vEl = document.getElementById('image_here'),
			vanilla = new Croppie(vEl, {
			viewport: { width: 290, height: 260 },
			boundary: { width: 310, height: 340 },
            enableOrientation: true
		});
vanilla.bind({
            url: e.target.result,
            orientation: 1
        });


$('#rotateImg').on('click', function (ev) {
			 vanilla.rotate(90);

});
    //save crope img

$('#btn_SpeichernSenden').on('click', function (ev) {
vanilla.result({
				type: 'base64',
                size: 'original'
			}).then(function (base64) {
				$("#cropImg").attr("value",base64)
$("#cropImg").trigger("change");
			});
					});



}

 
                //$('#image_here').attr('src', e.target.result);
                $("#uploadsBtn > a").each(function () {
                    $(this).css("pointer-events", "none");
                });
            }
        }
        else if (type == "doc") {
            reader.onload = function (e) {
                $('#doc_herei').show();
                $('#image_here').hide();
$('.croppie-container > .cr-boundary').remove();
                $('.croppie-container > .cr-slider-wrap').remove();
                $('#video_herev').hide();
                $('#customCamera').val(null);
                $('#customVideo').val(null);

                var pdf_object_url = URL.createObjectURL(input.files[0]);
                $('#doc_herei').attr('src', pdf_object_url);
                $("#uploadsBtn > a").each(function () {
                    $(this).css("pointer-events", "none");
                });
            }
        }


        reader.readAsDataURL(input.files[0]);
    }


}

function closecard() {
    $(".card").css("display", "none");
$("#mediaContent").css("width","100%");
    $("#uploadsBtn > a").each(function () {
                    $(this).css("pointer-events", "bounding-box");
    });
}

//function removecommentbtn(e, postId) {
//    $("#start-post-modal_" + postId).find("#commentdetail").html($(e).parent().parent().html());
//    $("#start-post-modal_" + postId).find("#commentdetail").find(".reply-delete").remove();
//    $("#start-post-modal_" + postId).find("#commentdetail").find(".img-fluid").remove();
//    $("#start-post-modal_" + postId).find("#commentdetail").find(".media-body.py-1.px-2.border.bg-light.mb-3").find(".media-body.py-1.px-2.border").addClass("bg-white");
//    $("#start-post-modal_" + postId).find("#commentdetail").find(".media-body.py-1.px-2.border.bg-light.mb-3").addClass("overflow-auto");
//    $("#start-post-modal_" + postId).find("#commentdetail").find(".media-body.py-1.px-2.border.bg-light.mb-3").css("height", "200px");
//
//}


function SuccessMessage(e) {

    var resulthtml = $($.parseHTML(e));

    //add comment counter after add comment
    //var postcommentcounter = "postcommentcounter_" + resulthtml[4].value;
    //$('#' + postcommentcounter).text(parseInt($('#' + postcommentcounter).text(), 10) + 1);
    $("#updateCommentCount" + resulthtml[4].value).click();

    //remove comment textarea
    $("#commentDesc_" + resulthtml[4].value).val("");

}






function Successdeletecomment(e) {
    var resulthtml = $($.parseHTML(e));
if(resulthtml[4] !== null){
    //display none after delete comment
    $("#start-post-modal_" + resulthtml[4].value).modal('toggle');
    $("#updateCommentCount" + resulthtml[4].value).click();
}
if(resulthtml[3] !== null)
{
    $("#delete-product-commnet-modal_" + resulthtml[3].value).modal('toggle');
}

    //remove deleted comment div after delete comment
    //var commentIdval="commentlist_" + $("#commentdetail").find("#commentId").val();
    //$('#'+commentIdval).remove();

    //remove comment counter after delete comment
    //var postcommentcounter = "postcommentcounter_" + resulthtml[4].value;
    //$('#' + postcommentcounter).text(parseInt($('#' + postcommentcounter).text(), 10) - 1);
}
///LikeDislikeClick
function disLikeClick(e) {
$("#dislike-counter_" + e).parent().removeClass("text-muted").addClass("text-info");
$("#like-counter_" + e).parent().removeClass("text-info").addClass("text-muted");
    $("#dislikeForm_" + e).click();
}

function likeClick(e) {
$("#like-counter_" + e).parent().removeClass("text-muted").addClass("text-info");
$("#dislike-counter_" + e).parent().removeClass("text-info").addClass("text-muted");
    $("#likeForm_" + e).click();
}
///Like

function SuccessLike(e) {
    //var popupNotification = $("#popupNotification").data("kendoNotification");

    if (e.Statue === 1) {

        $("#like-counter_" + e.PostId).text(e.LikeValue);
        $("#dislike-counter_" + e.PostId).text(e.DisLikeValue);
        $("#rate-counter_" + e.PostId).text(e.RateValue);
        //popupNotification.show({
        //    title: "پیغام",
        //    message: e.Message
        //}, "success");
    } else {

        popupNotification.show({
            title: "پیغام",
            message: e.Message
        }, "error");
    }
}

//disLike

function SuccessDisLike(e) {
    //var popupNotification = $("#popupNotification").data("kendoNotification");

    if (e.Statue === 1) {

        $("#like-counter_" + e.PostId).text(e.LikeValue);
        $("#dislike-counter_" + e.PostId).text(e.DisLikeValue);
        $("#rate-counter_" + e.PostId).text(e.RateValue);

        //popupNotification.show({
        //    title: "پیغام",
        //    message: e.Message
        //}, "success");
    } else {

        popupNotification.show({
            title: "پیغام",
            message: e.Message
        }, "error");
    }
}

//reply comment
function replycommentbtn(e, postId, parentId, firstparentId) {
    $("#reply-comment-modal").find("#PostId").val(postId);
    $("#reply-comment-modal").find("#ProductId").val(postId);
    $("#reply-comment-modal").find("#ParentId").val(parentId);
    $("#reply-comment-modal").find("#FirstParentId").val(firstparentId);
    $("#reply-comment-modal").find("#commentDesc").text("");
    $("#reply-comment-modal").find("#commentDesc").val("");
}

function SuccessReply(e) {
    var resulthtml = $($.parseHTML(e));
    //add comment counter after reply comment
    //var postcommentcounter = "postcommentcounter_" + resulthtml[4].value;
    //$('#' + postcommentcounter).text(parseInt($('#' + postcommentcounter).text(), 10) + 1);
if(resulthtml[4] !== null){
 $("#updateCommentCount" + resulthtml[4].value).click();

    $("#reply-comment-modal_" + resulthtml[4].value).modal('toggle');
}
if(resulthtml[3] !== null)
{
    $("#reply-comment-modal_" + resulthtml[3].value).modal('toggle');
}
   
}
///is Exist Post
function SuccessIsExist(e) {

    $("#alarm-modal").modal('hide');
    var popupNotification = $("#popupNotification").data("kendoNotification");

    if (e.Statue === 1) {

        popupNotification.show({
            title: "پیغام",
            message: "گزارش موفقیت آمیز بود"
            //message: e.Message
        }, "success");
    } else {

        popupNotification.show({
            title: "پیغام",
            message: e.Message
        }, "error");
    }
}

//edit post
function SuccessOpenPostModal(e) {
    var resulthtml = $($.parseHTML(e));

    $("#start-post-modal").modal('toggle');
    if (resulthtml[7].value == 1 && resulthtml[9].value == 1) {
        $("#uploadsBtn > a").each(function () {
            $(this).css("pointer-events", "none");
        });
    }

 if (resulthtml[1].value == 1 && resulthtml[3].value == 1) {
        $("#uploadsBtn > a").each(function () {
            $(this).css("pointer-events", "none");
        });
    }



$("input[type='file']").each(function(){
    $(this).next("span").text($(this).next("span").text().replace("Select files...","انتخاب فایل ضمیمه..."));
  });
 




}

function editpostaction(e) {
    $("#editpostaction" + e).click();
}


//startpostmodal
$(".startpostmodal").on("click", function () {
    $("#createpost").trigger("click");
    $("#posttype").val($("#postTypecode").val());
    $("#posttype").text($("#postTypecode").val());

    setTimeout(function () {

        $("#start-post-modal").find(".k-upload-button").find("span").remove();
        $("#start-post-modal").find(".k-upload-button").append("<span>... انتخاب فایل</span>");

    }, 200);

});

//editcomment



//deletepost
function deletepostaction(e) {
    $("#postdetail").html($("#post_" + e).html());
    $("#postidfordelete").val(e);
    $("#postdetail").find(".card-header").remove();
    $("#postdetail").find(".post-collapse").remove();
    $("#postdetail").find(".like-post").remove();
    $("#postdetail").find(".expand-button").remove();
}

function SuccessDeletePost(e) {
    var popupNotification = $("#popupNotification").data("kendoNotification");



    if (e.Statue === 1) {
$("#listView").data().kendoListView.dataSource.read();
        popupNotification.show({
            title: "پیغام",
            message: e.Message
        }, "success");
    } else {

        popupNotification.show({
            title: "پیغام",
            message: e.Message
        }, "error");
    }
    $("#delete-post-modal").modal('toggle');
    $("#post_" + e.PostId).remove();

}


//kendo upload temp script

function formatFileSize(bytes, decimalPoint) {
    if (bytes === 0) return '0 KB';
    var k = 1000,
        dm = decimalPoint || 2,
        sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'],
        i = Math.floor(Math.log(bytes) / Math.log(k));
    return parseFloat((bytes / Math.pow(k, i)).toFixed(dm)) + ' ' + sizes[i];
}

///Post Lazy Loading
/*$(document).ready(function () {

    $(window).scroll(function () {

        var documentHeight = $(document).height();
        var windowHeight = $(window).height();
        var scrollTop = $(window).scrollTop();

        if ((scrollTop + windowHeight) === documentHeight) {
            if($("#FilterUserId").val() !== null)
            $("#filterUserId").val($("#FilterUserId").val());
            $("#filterCategoryId").val($("#FilterCategoryId").val());
            $("#filterLevelId").val($("#FilterLevelId").val());
            $("#filterAdsTypeId").val($("#FilterAdsTypeId").val());

           if($("#endpage").position() !== undefined ){
if($("#endpage").position().top !== 0){
            $("#postListLazyLoad").click();
}
}

        }

    });
});

function lazyLoadspinnerStart() {
    $("#lazyLoadspinner").addClass("d-flex");
    $("#lazyLoadspinner").removeClass("d-none");
}
function lazyLoadspinnerEnd() {
    $("#lazyLoadspinner").addClass("d-none");
    $("#lazyLoadspinner").removeClass("d-flex");
            $("#pageNumberProduct").val(parseInt($("#pageNumberProduct").val()) + 1);
 $("#pageNumberPost").val(parseInt($("#pageNumberPost").val()) + 1);

}
function SuccessPostList() {
    $(".button-inner-wrap").on("click", function () {
        $(this).parent().submit();
    });


}


//filter Posts
function SuccessFilter() {
    $(".button-inner-wrap").on("click", function () {
        $(this).parent().submit();
    });

//-----------------
$('.expand-button').on('click', function () {
    $(this).closest('section').find('.special-text').toggleClass('-expanded');

    if ( $(this).closest('section').find('.special-text').hasClass('-expanded')) {
        $(this).html('<i class="fas fa-angle-double-up"></i>');
    } else {
        $(this).html('<i class="fas fa-angle-double-down"></i>');
    }
});


}
*/


////-----------------------Product-----------------------//////
//creat comment
function SuccessProductComment()
{
    $("#commentDescription").val('');
}

//deletecomment
function removeProductcommentbtn(e,commentId)
{
    $("#delete-product-commnet-modal_"+commentId).find("#commentdetail").html($(e).closest('.comment-box').html());
    $("#delete-product-commnet-modal_"+commentId).find(".person-test").remove();
    $("#delete-product-commnet-modal_"+commentId).find(".reply-delete").remove();
    

}


///lazyload product commnet

function SuccessLazyComment()
{
}


///my profile function
$("#nav-home-tab-posts").click(function(){
    $("#postType1").val(0);
    $("#postType").val(0);
    $("#pageNumberPost").val(1);
$("#postListLazyLoad1").click();
});

$("#nav-profile-tab-articles").click(function(){
    $("#postType1").val(1);
    $("#postType").val(1);
    $("#pageNumberPost").val(1);

$("#postListLazyLoad1").click();
});

$("#nav-profile-tab-adds").click(function(){
    $("#postType1").val(2);
    $("#postType").val(2);
    $("#pageNumberPost").val(1);

$("#postListLazyLoad1").click();
});


function SuccessReportModal(postId)
{
    $("#alarm-modal_"+postId).modal('toggle');


//kendo crack
setTimeout(function () {
$("#div-Details").text("");
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
//kendo crack



}
function AlarmModalClick(postId)
{
    $('#postId').val(postId);
    $("#alarm-modal").modal('toggle');

    $('#alarm-modal').find("select").val("");
    $('#IsExistPostCheck').removeProp('checked');
}
function copyToClipboard() {
  /* Get the text field */
  var copyText = document.getElementById("postLinkAddress");

  /* Select the text field */
  copyText.select();
  copyText.setSelectionRange(0, 99999); /*For mobile devices*/

  /* Copy the text inside the text field */
  document.execCommand("copy");

  /* Alert the copied text */
var popupNotification = $("#popupNotification").data("kendoNotification");
popupNotification.show({
            title: "پیغام",
            message: "لینک کپی شد"
        }, "success");
}

function copyToClipboardReferral() {
  /* Get the text field */
  var copyText = document.getElementById("referralLinkAddress");

  /* Select the text field */
  copyText.select();
  copyText.setSelectionRange(0, 99999); /*For mobile devices*/

  /* Copy the text inside the text field */
  document.execCommand("copy");

  /* Alert the copied text */
var popupNotification = $("#popupNotification").data("kendoNotification");
popupNotification.show({
            title: "پیغام",
            message: "لینک کپی شد"
        }, "success");
}


//start dataBound----------------------------------------------------
  function dataBound(e){

	$.each( e.items, function( key, value ) {

        var id = value.Id;
		//Convert Credit To kilo------
        $("#UserSpcialCredit_" + id).text(kFormatter($("#UserSpcialCredit_" + id).text()));
		//----------------------------
        var docfile = value.DocumentFile;
if(docfile != null){
if ( $("#pdfViewer"+id).children().length == 0 ) {
     $("#pdfViewer"+id).kendoPDFViewer({
            pdfjsProcessing: {
            file: docfile
            },
            width: "99%",
            toolbar: {
                      items: [
                        "pager","spacer","zoom","toggleSelection","spacer","search"               
                      ]
                    },
            height: "26rem"
            });
 
        };
}
  $("#pdfViewer"+id).find(".k-toolbar").find('span[class="k-widget k-combobox"]').remove();
 $("#pdfViewer"+id).find(".k-toolbar").find('div[class="k-pager-wrap k-toolbar-first-visible"]').css("direction","rtl");
  $("#pdfViewer"+id).find(".k-toolbar").find('span[class="k-pager-input k-label"]').text($("#pdfViewer"+id).find(".k-toolbar").find('span[class="k-pager-input k-label"]').text().replace("of","از").replace("page","صفحه"));


        var type  = 0;
          
});
        $('.expand-button').on('click', function () {
            $(this).closest('section').find('.special-text').toggleClass('-expanded');

            if ($(this).closest('section').find('.special-text').hasClass('-expanded')) {
                $(this).html('<i class="fas fa-angle-double-up"></i>');
            } else {
                $(this).html('<i class="fas fa-angle-double-down"></i>');
            }
        });

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
//end dataBound--------------------------------------------------------------------

        function filterChange(e){
$(".k-filter-preview-operator" ).each(function( index ) {
   $( this ).text($( this ).text().replace("AND","و"));
});
if($("#filter").getKendoFilter().filterModel.filters.filter(v => v.value == "" || v.value == 0).length != $("#filter").getKendoFilter().filterModel.filters.length )
{
var filters=$("#filter").getKendoFilter().filterModel.filters;
$("#filter").getKendoFilter().filterModel.filters=$("#filter").getKendoFilter().filterModel.filters.filter(v => v.value != "" || v.value != 0);
        $("#filter").getKendoFilter().applyFilter();
$("#filter").getKendoFilter().filterModel.filters=filters;

}
        }
         
        function Successupload(e){
        var m=m;
        }
       
$(document).ready(function() {
            var url = window.location.pathname;
            var statusIntro = false;
            $.ajax({
                url: '/Home/IntroSave',
                data: { 'url': url },
                type: "post",
                cache: false,
                success: function (data) {
                    statusIntro = data.Status;
                    console.log(data.Text);

                    if (statusIntro == false) {
                        introJs().setOptions({ 'nextLabel': 'بعد', 'prevLabel': 'قبل', 'skipLabel': 'خروج', 'doneLabel': 'اتمام' }).start();
                        //var intro = new Anno([
                        //
                        //    {
                        //        target: '#createpost-intro',
                        //        content: "کاربر گرامی خوش آمدید، شما از این قسمت می توانید محتوای مورد نظر مربوط خود شامل تصویر، ویدئو، اسناد و توضیحات مرتبط را انتشار دهید. ",
                        //        position: 'bottom'
                        //    },
                        //    {
                        //        target: '#profile-cheaker-intro',
                        //        content: "با تکمیل مشخصات خود از این بخش به ما کمک می کنید، مشاوره بهتر و دقیق تری به شما بدهیم.",
                        //        position: 'bottom'
                        //    },
                        //    {
                        //        target: '#adviser',
                        //        content: "محصولاتی از فروشگاه با دسترسی آسان در این بخش به شما پیشنهاد داده می شود که در راستای رسیدن به اهداف و مهارت های مورد نظرتان، به شما مشاوره می دهد",
                        //        position: 'top'
                        //    },
                        //    {
                        //        target: '#shopping-cart',
                        //        content: "با ورود به این قسمت این امکان را دارید که اهدافی را انتخاب کنید تا از سیستم مشاوره بهره بیشتری ببرید.",
                        //        position: 'left'
                        //    },
                        //    {
                        //        target: '#store-intro',
                        //        content: "راه سریع دسترسی به فروشگاه های دوره آموزشی، آزمون و کتاب برای شما این بخش است.",
                        //        position: 'left'
                        //    }
                        //
                        //]);
                        //intro.show();



                        $.ajax({
                            url: '/Home/IntroSave',
                            data: { 'url': url },
                            type: "post",
                            cache: false,
                            success: function (data) {
                                //console.WriteLine(data);
                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                                console.WriteLine('Error Getting Data from Server!');
                            }
                        });
                    }




                },
                error: function (xhr, ajaxOptions, thrownError) {
                    console.log("Error Getting Data From Server!");
                }
            });





        });

  $(document).on('click','.favorites',function() {

      var id = $(this).attr('id').substr(9);
            var changeType = $(this).attr("changeType");
            var type  = 0;
                    var popupNotification = $("#popupNotification").data("kendoNotification");

            $('#cartspinner').removeClass('d-none');
            $.ajax({
                url: '/Favorite/AddRemoveFavorite',
                data: { id : id , type : type,changeType:changeType },
                type: "post",
                cache: false,
                success: function (data) {

                    var msg = data[0].Message;
                    var stat = data[0].Statue;
                    var changetype = data[0].Changetype;
                    var totalcount = data[0].TotalFavedCount;

                    $('#cartspinner').addClass('d-none');
                    if (stat == 1) {
                        if (changetype == 0) {
                            $('#favorite_'+id).attr("changeType","1").html('<i class="text-info fas fa-bookmark"></i> به علاقه مندی ها اضافه شد');
                        }
                        else if (changetype == 1) {
                            $('#favorite_'+id).attr("changeType","0").html('<i class="far fa-bookmark"></i> اضافه به علاقه مندی ها');
                        }
                        $('.favoritecount').html(totalcount);

                    }



                    if (stat == 1) {

                        popupNotification.show({
                            title: "پیغام",
                            message: msg
                        }, "success");
                    } else {

                        popupNotification.show({
                            title: "پیغام",
                            message: msg
                        }, "error");
                    }

                },
                error: function (xhr, ajaxOptions, thrownError) {
                    $('#cartspinner').addClass('d-none');

                    popupNotification.show({
                        title: "پیغام",
                        message: 'خطایی رخ داده است لطفا دوباره تلاش کنید'
                    }, "success");

                }
            });

        });

        $(document).ready(function() {
        $('a.embed').gdocsViewer({width: 360, height: 300 });
        });

//Post Sort
 $("#CDateSort").click(function(){

$("#CDateSort").addClass("active");
$("#RateSort").removeClass("active");
$("#SeenSort").removeClass("active");
$("#LikeSort").removeClass("active");

                var sort=$("#listView").data().kendoListView.dataSource._sort;

        if($("#CDateSort").hasClass("active"))
        {
        var field = {field: "CDate", dir: "desc"};
        sort.push(field);
        $("#listView").data().kendoListView.dataSource._sort=sort.filter(v => v.field == "CDate");
        
        }else
        {
        sort=sort.filter(v => v.field != "CDate");
        $("#listView").data().kendoListView.dataSource._sort=sort;
        
        }
        $("#listView").data().kendoListView.dataSource.read();

});

 $("#LikeSort").click(function(){

$("#LikeSort").addClass("active");
$("#RateSort").removeClass("active");
$("#SeenSort").removeClass("active");
$("#CDateSort").removeClass("active");


                var sort=$("#listView").data().kendoListView.dataSource._sort;

        if($("#LikeSort").hasClass("active"))
        {
        var field = {field: "Like", dir: "desc"};
        sort.push(field);
        $("#listView").data().kendoListView.dataSource._sort=sort.filter(v => v.field == "Like");
        
        }else
        {
        sort=sort.filter(v => v.field != "Like");
        $("#listView").data().kendoListView.dataSource._sort=sort;
        
        }
        $("#listView").data().kendoListView.dataSource.read();

});

 $("#SeenSort").click(function(){

$("#SeenSort").addClass("active");
$("#RateSort").removeClass("active");
$("#LikeSort").removeClass("active");
$("#CDateSort").removeClass("active");

               var sort=$("#listView").data().kendoListView.dataSource._sort;

        if($("#SeenSort").hasClass("active"))
        {
        var field = {field: "Seen", dir: "desc"};
        sort.push(field);
        $("#listView").data().kendoListView.dataSource._sort=sort.filter(v => v.field == "Seen");
        
        }else
        {
        sort=sort.filter(v => v.field != "Seen");
        $("#listView").data().kendoListView.dataSource._sort=sort;
        
        }
        $("#listView").data().kendoListView.dataSource.read();

});

 $("#RateSort").click(function(){

$("#RateSort").addClass("active");
$("#SeenSort").removeClass("active");
$("#LikeSort").removeClass("active");
$("#CDateSort").removeClass("active");

       var sort=$("#listView").data().kendoListView.dataSource._sort;

        if($("#RateSort").hasClass("active"))
        {
        var field = {field: "Rate", dir: "desc"};
        sort.push(field);
        $("#listView").data().kendoListView.dataSource._sort=sort.filter(v => v.field == "Rate");
        
        }else
        {
        sort=sort.filter(v => v.field != "Rate");
        $("#listView").data().kendoListView.dataSource._sort=sort;
        
        }
        $("#listView").data().kendoListView.dataSource.read();

});

 $("#nav-home-tab-posts").click(function(){
        var url=$("#listView").data().kendoListView.dataSource.transport.options.read.url;
        var creatBtnUrl=$("#createPostBtn").attr("href");
        var replaceurl=url.replace("postType=Ads","postType=Post").replace("postType=Knowledg","postType=Post");
        var replaceBtnurl=creatBtnUrl.replace("postType=Ads","postType=Post").replace("postType=Knowledg","postType=Post");
        $("#createPostBtn").attr("href",replaceBtnurl);
        $("#CreateBtnText").text("ایجاد پست جدید");
        $("#listView").data().kendoListView.dataSource.transport.options.read.url=replaceurl;
        $("#listView").data().kendoListView.dataSource.read();

});

$("#nav-profile-tab-articles").click(function(){
        var creatBtnUrl=$("#createPostBtn").attr("href");
        var url=$("#listView").data().kendoListView.dataSource.transport.options.read.url;
        var replaceurl=url.replace("postType=Post","postType=Knowledg").replace("postType=Ads","postType=Knowledg");
        var replaceBtnurl=creatBtnUrl.replace("postType=Post","postType=Knowledg").replace("postType=Ads","postType=Knowledg");
        $("#createPostBtn").attr("href",replaceBtnurl);
        $("#CreateBtnText").text("ایجاد مقاله جدید");
        $("#listView").data().kendoListView.dataSource.transport.options.read.url=replaceurl;
        $("#listView").data().kendoListView.dataSource.read();
});

$("#nav-profile-tab-adds").click(function(){
        var creatBtnUrl=$("#createPostBtn").attr("href");
        var url=$("#listView").data().kendoListView.dataSource.transport.options.read.url;
        var replaceurl=url.replace("postType=Post","postType=Ads").replace("postType=Knowledg","postType=Ads");
        var replaceBtnurl=creatBtnUrl.replace("postType=Post","postType=Ads").replace("postType=Knowledg","postType=Ads");
        $("#createPostBtn").attr("href",replaceBtnurl);
        $("#CreateBtnText").text("ایجاد تبلیغ جدید");
        $("#listView").data().kendoListView.dataSource.transport.options.read.url=replaceurl;
        $("#listView").data().kendoListView.dataSource.read();
        });



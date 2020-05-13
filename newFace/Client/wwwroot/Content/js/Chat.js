//$(document).ready(function () {

$("#message").focus();

$('.nav-link').click(function () {
    $('#back-sidebar,#chat-user-show').addClass('d-none');
});

var senderUserName = $("#userName").val();

$("#message").keypress(function (event) {
    if (event.keyCode === 13) {
        //event.preventDefault();
        $("#btnSend").click();
    }
});

var chat = $.connection.chatHub;

$.connection.hub.start().done(function () {


    $("#btnSend").click(function () {

        // var message = $(".emojionearea-editor").html();

        var message = $('#message').val();

        //بی اثر کردن کاراکتر اینتر
        message = message.replace(/(\r\n|\n|\r)/gm, "");

        if (message === null || message === "") {
            return false;
        }

        //var chatId = chat.server.sendPrivateMessage($("#userId").val(), $("#receiverId").val(), message);

        var randomId = Math.floor(Math.random() * 10000) + 1;

        chat.server.sendPrivateMessage($("#userId").val(), $("#receiverId").val(), message, $("#reply-parentId").val()).done(function (chat) {
            if (chat) {
                GetChat(chat.Id, false, randomId);
            }
        });

        // پلاگین ایموجی یک دیو درست میکند که موجب باگ خالی نشدن تکست باکس اصلی  پس از زدن دکمه ارسال پیام میشود
        // $(".emojionearea-editor").text("").focus();

        // دلیل اینکه اینجا از پارشیال چت استفاده نکردم صرفه جویی در درخواست به سرور است
        $("#show-chat").append("<div id='chat_" + randomId + "' class='chat media-me'><div class='media mt-2'><div class='media-body py-1 px-2'><small class='my-1 float-right'>" + message + "</small><i class='fas fa-clock text-small float-left'></i></div></div></div>");

        CancelReplyChat();

        $("#message").val("").focus();

    });


});

//این تابع مسئول دریافت پیام های جدید از سرور است
chat.client.onNewPrivateMessage = function (message, id, senderId) {

    //نمایش پیغام خطا
    if (message) {

        alert(message);

    }

    //در حالتی که در صفحه همان کاربری که پیام داده قرار داریم
    //در این حالن نباید شمارنده پیام افزایش یابد و پیام هم باید سین بخورد

    var seen = false;
    if ($("#chat").hasClass("active")) {

        if ($("#receiverId").val() === senderId) {
            seen = true;
            GetChat(id, seen);
            scrollBottom();
        }
        else {
            NotifiNewMessage();
        }

    }
    else if ($("#comments").hasClass("active")) {
        GetChatContact();
        NotifiNewMessage();
    }
    else {
        NotifiNewMessage();
    }

};

chat.client.connection = function (userId, online) {
    if (online === 1) {
        $('#avatar-' + userId).css("border-color", "#1ed01b");
    }
    else {
        $('#avatar-' + userId).css("border-color", "#616f98");
    }
};

chat.client.isDeleted = function (chatId) {
    if (chatId) {
        GetChat(chatId, false, chatId);
    }

};

//شکلک های چت 
// تکست باکس اصلی را هیدن میکن و خود یک دیو میسازد و بر روی ان کار میکند
// $("#message").emojioneArea();

//نمایش مخاطبان کاربر و سابقه پیام ها و ارسال عبارتی که جستجو شده
$("#show-search-person-sidebar").click(function () {

    $("#searchUserTxt").val("");
    GetChatContact();
    //$('.ی').css('display', 'none');

});
$("#searchUserBtn").click(function () {
    var search = $("#searchUserTxt").val();
    GetChatContact(search);
    //$('.spinner').css('display', 'none');

});

$('#back-sidebar').click(function () {
    $("#show-search-person-sidebar").click();
});

$('#OpenFileUpload').click(function () { $('#fileupload').trigger('click'); });
//});



function OpenMessageDialog(receiverId, receiverName, receiverAvatar) {


    $("#show-sidebar").click();
    $("#show-profile").click();
    $("#receiverId").val(receiverId);
    $("#receiverName").text(receiverName);
    $("#receiverAvatar").attr("src", receiverAvatar);



    $('#back-sidebar,#chat-user-show').removeClass('d-none');

    GetMessageHistory($("#userId").val(), receiverId);

}

function LoadMoreMessage(pageNumber) {

    $('#chatLazyLoadPageNumber').val(parseInt($('#chatLazyLoadPageNumber').val()) + 1);

    GetMessageHistory($("#userId").val(), $("#receiverId").val(), pageNumber);
}


function GetChatContact(search) {

    $('#chatContactLoading').show();

    $.ajax({
        url: "/Chats/GetChatContact",
        type: "post",
        data: {
            search: search
        },
        success: function (data) {

            $('#chatContactLoading').hide();
            $('#chatContact').html(data);

        }
    });
}

function GetChat(id, seen, show) {

    $.ajax({
        url: "/Chats/GetChat",
        type: "post",
        global: false,
        data: {
            id: id,
            seen: seen
        },
        success: function (data) {
            if (show) {
                $('#chat_' + show).replaceWith(data);

                //$('#' + show).remove();
            }
            else {
                $('#show-chat').append(data);
                scrollBottom();
            }

        }
    });
}

function DeleteChat(id) {

    $.ajax({
        url: "/Chats/DeleteChat",
        type: "post",
        global: false,
        data: {
            id: id
        },
        success: function (data) {

            $('#chat_' + id).html(data);
        }
    });
}

function ReplyChat(senderName, message,parentId) {

    $('#reply-chat').addClass('d-flex');

    $('#reply-sendername').text(senderName);
    $('#reply-message').text(message);

    $('#reply-parentId').val(parentId);

}

function CancelReplyChat() {
    $('#reply-chat').removeClass('d-flex');

    $('#reply-parentId').val('');
}


function GetMessageHistory(senderId, receiverId, pageNumber) {

    $('#chatHistoryLoading').show();

    $.ajax({
        url: "/Chats/GetMessageHistory",
        type: "post",
        global: false,
        data: {
            senderId: senderId,
            receiverId: receiverId,
            pageNumber: pageNumber
        },
        success: function (data) {

            $('#chatHistoryLoading').hide();

            if (pageNumber) {
                $('#show-chat').prepend(data);
                if (data) {
                    if ($('#scrollTo').val()) {
                        scrollToMessage($('#scrollTo').val());
                    }
                }
                else {
                    $('#chatLazyLoadMoreDiv').remove();
                }
            }
            else {
                $('#chatContainer').html(data);
                scrollBottom();
            }

            CheckUnSeenCount();
        }
    });
}

function CheckUnSeenCount() {

    $.ajax({
        url: "/Chats/CheckUnSeenCount",
        type: "post",
        global: false,
        data: {

        },
        success: function (data) {
            if (data !== 0) {
                $('.unSeenCount').html(data);
            }
            else {
                $('.unSeenCount').html("");
            }
        }
    });
}

function Upload(voice) {
    $("#text-chat>.progress").removeClass("d-none");
    var progress = $("#progressBar");
    progress.css({ "width": "0%" });
    progress.html("0%");

    formData = new FormData();

    formData.append('senderId', $('#userId').val());
    formData.append('receiverId', $('#receiverId').val());
    formData.append('parentId', $('#reply-parentId').val());


    if (voice) {
        formData.append("file", voice, "x.wav");
    }
    else {
        formData.append('file', $('#fileupload').get(0).files[0]);
    }


    //for (var i = 0; i < $('#imgList' + galleryId).get(0).files.length; ++i) {

    //   formData.append('imgList', $('#imgList' + galleryId).get(0).files[i]);
    //}

    //console.log(file);
    $.ajax({
        url: '/Chats/Upload/',
        type: 'POST',
        global: false,
        contentType: false,
        cache: false,
        processData: false,
        data: formData,
        xhr: function () {
            var jqXHR = null;
            if (window.ActiveXObject) {
                jqXHR = new window.ActiveXObject("Microsoft.XMLHTTP");
            } else {
                jqXHR = new window.XMLHttpRequest();
            }
            //Upload progress
            jqXHR.upload.addEventListener("progress",
                function (evt) {

                    if (evt.lengthComputable) {
                        var percentComplete = Math.round((evt.loaded * 100) / evt.total);

                        var progress = $('#progressBar');

                        progress.css({ "width": percentComplete + "%" });
                        progress.html(percentComplete + "%");
                    }
                },
                false);

            return jqXHR;
        },
        success: function (data) {
            $("#text-chat>.progress").addClass("d-none");
            CancelReplyChat();

            $('#show-chat').append(data);
            scrollBottom();

        },
        error: function () {
            alert("خطا در سیستم چت");
        }
    });
}

function scrollBottom() {
    document.getElementById("sidebar-content").scrollBy(0, 50000);
}

function scrollToMessage(id) {
    // Scroll
    $('#sidebar-content').animate({
        scrollTop: $("#chat_" + id).position().top - 200
    }, 1);

}


//وقتی تو تکست اریاء چت فکوس بشه اسکرول میخوره میره ته
$("#message").focus(function () {
    setTimeout(function () {
        scrollBottom();
    }, 200);
});


function NotifiNewMessage(title, message) {

    CheckUnSeenCount();
    document.getElementById("chatSound").play();
}

$("input[type=text], textarea").mouseover(zoomDisable).mousedown(zoomEnable);
function zoomDisable() {
    $('head meta[name=viewport]').remove();
    $('head').prepend('<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />');
}
function zoomEnable() {
    $('head meta[name=viewport]').remove();
    $('head').prepend('<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />');
}
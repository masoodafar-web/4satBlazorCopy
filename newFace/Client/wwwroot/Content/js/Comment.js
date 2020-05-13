


        function OpenComment(postId){
            $('#GetComments' + postId).submit();
        }

        function SuccessComment(id ,data) {
          $("#pageNumber_" + id).val( parseInt( $("#pageNumber_" + id).val() , 10) + 1   );

            if(!data){
                $("#moreBtn_" + id).hide();
            }
            else{
                $('#moreBtn_' + id).removeClass("d-none");
            }

            $("#commentsLoading_" + id).hide();
        }

        function BeginSendComment(id){

            $("#commentsLoading_" + id).show();
        }

        function SuccessCreateComment(id){
	        $('.emoji-wysiwyg-editor').empty();

            $("#pageNumber_" + id).val(0);

            $("#updateCommentCount" + id).click();

            $('#Desc').val("");

            $("#reply-comment-modal_" + id).modal('hide');

        }

        function RemoveComment(commentId) {

            $("#commentidfordelete").val(commentId);
        }

        function SuccessRemoveComment(data){


            $("#commentlist_" + data.CommentId).fadeOut(300, function() { $(this).remove(); });

            $("#delete-commend-modal").modal('hide');

            $("#updateCommentCount" + data.PostId).click();

        }

        function SuccessReplyComment(postId) {
	        $('.emoji-wysiwyg-editor').empty();
            $("#reply-comment-modal").modal('hide');
            $('#Desc').val("");
            $("#updateCommentCount" + postId).click();

        }

        function FailComment(xhr){

            if (xhr.status === 305) {
              var popupNotification = $("#popupNotification").data("kendoNotification");

              popupNotification.show({
                  title: "پیغام خطا",
                  message: xhr.responseText
              }, "error");
            }

        }


function SuccessEdit(e) {
    $("#edit-comment-modal").modal('toggle');

}
function SuccessOpenEditCommentModal(e) {
    $("#edit-comment-modal").modal('toggle');

}

function SuccessOpenReplyCommentModal(e) {
    $("#reply-comment-modal").modal('toggle');
}


function openEditCommnetModal(e) {

    $("#editcommentaction" + e).click();
}

function openReplyCommnetModal(e) {

    $("#replycommentaction" + e).click();
}
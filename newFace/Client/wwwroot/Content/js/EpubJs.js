  let anchorOffset;
        let focusOffset;
        let text;

        // region Meysam ♥


        
        $('#btn-add-tag').click(function (e) {
 UIkit.offcanvas("#offcanvas-slide").show();
         closeBottomSheet("bottom-sheet-add-tag");
        });

        $('#btn-add-note').click(function (e) {
           UIkit.offcanvas("#offcanvas-slide").show();
         closeBottomSheet("bottom-sheet-add-tag");
        });

        // endregion

        $(document).keyup(function (e) {
            if (e.key === "Escape") {
                if (isOpenBottomSheet("bottom-sheet-change-theme"))
                    closeBottomSheet("bottom-sheet-change-theme");
                if (isOpenBottomSheet("bottom-sheet-theme-font"))
                    closeBottomSheet("bottom-sheet-theme-font");
                if (isOpenBottomSheet("bottom-sheet-add-tag"))
                    closeBottomSheet("bottom-sheet-add-tag");
                if (isOpenBottomSheet("bottom-sheet-add-note"))
                    closeBottomSheet("bottom-sheet-add-note");
            }
        });

        window.addEventListener("hashchange", function (e) {
            if (isOpenBottomSheet("bottom-sheet-change-theme"))
                closeBottomSheet("bottom-sheet-change-theme");
            if (isOpenBottomSheet("bottom-sheet-theme-font"))
                closeBottomSheet("bottom-sheet-theme-font");
            if (isOpenBottomSheet("bottom-sheet-add-tag"))
                closeBottomSheet("bottom-sheet-add-tag");
            if (isOpenBottomSheet("bottom-sheet-add-note"))
                closeBottomSheet("bottom-sheet-add-note");
            else {
                e.preventDefault();
                return false;
            }
        });

        $('#add-tag').click(function (e) {
            $(".popup").hide();
           
            openBottomSheet("bottom-sheet-add-tag");
        });
$('#add-note').click(function (e) {
            $(".popup").hide();
           
            openBottomSheet("bottom-sheet-add-tag");
        });

        document.addEventListener("selectionchange", e => {
            if (e.stopPropagation)
                e.stopPropagation();
            if (e.preventDefault)
                e.preventDefault();
            e.cancelBubble = true;
            e.returnValue = false;
            return false;
        });

        $("#pages").on("tap", function () {
            $(this).hide();
        });





        // region Open BottomSheet
$('#tag').on('click', '.uk-card', function () {
var id=$(this).attr("title");
$("#tagId").val(id);
$("#form0").attr("data-ajax-update","#tag"+id);
$("#text-tag-message").val("");
    openBottomSheet("bottom-sheet-add-tag");
});

$('#note').on('click', '.uk-card', function () {
var id=$(this).attr("title");
$("#tagId").val(id);
$("#form0").attr("data-ajax-update","#tag"+id);
$("#text-tag-message").val("");
    openBottomSheet("bottom-sheet-add-tag");
});


$('body').on('click', '#add-note', function () {
    openBottomSheet("bottom-sheet-add-note");
});

$('body').on('click', '#change-theme', function () {
    openBottomSheet("bottom-sheet-change-theme");
});

$('body').on('click', '#change-font', function () {
    openBottomSheet("bottom-sheet-theme-font");
});
// endregion

// region Close BottomSheet
$('body').on('click', '#close-change-theme', function () {
    closeBottomSheet("bottom-sheet-change-theme");
});

$('body').on('click', '#close-change-font', function () {
    closeBottomSheet("bottom-sheet-theme-font");
});

$('body').on('click', '#close-add-tag', function () {
    closeBottomSheet("bottom-sheet-add-tag");
});

$('body').on('click', '#close-add-note', function () {
    closeBottomSheet("bottom-sheet-add-note");
});
// endregion

// if click outside BottomSheet close
$(document).on('click', 'html', function (e) {
    if (e.target.id == "bottom-sheet-change-theme" ||
        e.target.id == "bottom-sheet-theme-font" ||
        e.target.id == "bottom-sheet-add-tag" ||
        e.target.id == "bottom-sheet-add-note") {
        closeBottomSheet(e.target.id);
    }
});


        function isOpenBottomSheet(id) {
            return !$("#" + id).hasClass("bottom-sheet-dialog-close");
        }

        function openBottomSheet(id) {
        $(".popup").hide();
            $("#" + id).removeClass("bottom-sheet-dialog-close");
            $("#" + id + " .bottom-sheet-dialog-child .animated").addClass("slideInUp");
            $("#" + id + " .bottom-sheet-dialog-child .animated").removeClass("slideInDown");
        }

        function closeBottomSheet(id) {
            $("#" + id).addClass("bottom-sheet-dialog-close");
            $("#" + id + "  .bottom-sheet-dialog-child .animated").addClass("slideInUp");
            $("#" + id + " .bottom-sheet-dialog-child .animated").removeClass("slideInDown");
        }

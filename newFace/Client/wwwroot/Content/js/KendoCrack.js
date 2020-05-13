
        //kendo trial

        $(document).ready(function () {

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


    });


$(document).ready(function() {


   $('.productsTable').on('scroll',
       function() {

           var parent = $(this).parent();

           var scrollLeft = $(this).scrollLeft();

           if (scrollLeft === 0) {

               var pageNumber = parseInt(parent.find('.productsPageNumber').val(), 10) + 1;
 
               var categoryId = $('#CategoryId').val();

               parent.find('.lazyLoadspinnerShop').show();

               getProductsLazyLoad(pageNumber, parent.find('.getProductType').val() , categoryId);


           }

       });


    function getProductsLazyLoad(pageNumber , getProductType , categoryId) {
        var productType = $("#ProductType").val();


        $.ajax({
            url: "/Shops/GetProductsLazyLoad",
            type: "post",
            //global: false,
            data: {
                pageNumber: pageNumber,
                getProductType: getProductType,
                productType: productType,
                categoryId: categoryId

            },
            success: function(data) {
                if (getProductType === "0") {
                    $('#NewProductsList').append(data);
                    if (data !== "") {
                        $('#newProductsPageNumber').val(pageNumber);
                    }

                }
                if (getProductType === "1") {
                    $('#bestSellerProductsList').append(data);
                    if (data !== "") {
                        $('#bestSellerProductsPageNumber').val(pageNumber);
                    }

                }
                if (getProductType === "3"){
                    $('#RelatedProductsList').append(data);
                    if (data !== "") {
                        $('#relatedProductsPageNumber').val(pageNumber);
                    }
                }
                $('.lazyLoadspinnerShop').hide();
            }

        });
    }


    function GetRelatedProducts(postId){

        $('#GetComments' + postId).submit();


    }



});


//پاسخ اضافه به سبد
 function addtocartresult(data) {
            var dta = data.responseText;

            var parsed = $.parseJSON(dta);


            var msg = parsed[0].Message;
            var stat = parsed[0].Statue;
            if (stat === 1) {
                var c1 = $('.cartcount1').text();
                var addone = parseInt(c1);
                addone = addone + 1;
                $('.cartcount1').html(addone);
                $('.cartcount2').html(addone);
            }
            var popupNotification = $("#popupNotification").data("kendoNotification");

            if (stat === 1) {

                popupNotification.show({
                        title: "پیغام",
                        message: msg
                    },
                    "success");
            } else {

                popupNotification.show({
                        title: "پیغام",
                        message: msg
                    },
                    "error");


            }
            $("#modal-saham").modal('hide');

            if (stat === 1) {
                window.location.href = '/Shops/ShoppingCard';
            }

        }

//کپی لینک معرف
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
                },
                "success");
        }
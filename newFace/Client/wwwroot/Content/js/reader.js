window.epubfunc = () => {
    var productId = document.getElementById('ProductId').textContent.trim();
    var params = URLSearchParams && new URLSearchParams(document.location.search.substring(1));
    var url = params && params.get("url") && decodeURIComponent(params.get("url"));
    var bookUrl = document.getElementById("FileText").textContent;
    var controls = document.getElementById("controls");
    var currentPage = document.getElementById("current-percent");


    // Load the opf
    var book = ePub(url || bookUrl, {
        canonical: function (path) {
            return window.location.origin + window.location.pathname;
        }
    });
    var rendition = book.renderTo("pages", {
        ignoreClass: "annotator-hl",
        width: "100%",
        height: "100%",
        direction: "rtl"
    });
    rendition.themes.register("IRANSans", "/Content/css/font-style-IRANSans.css");
    rendition.themes.register("Shabnam-FD", "/Content/css/font-style-Shabnam-FD.css");

    var loc = window.location.href.indexOf("?loc=");


    // var hash = window.location.hash.slice(2);
    if (loc > -1) {
        var href = window.location.href.slice(loc + 5);
        var hash = decodeURIComponent(href);
    }
    rendition.display(hash || undefined);


    rendition.on("displayed", event => {
        const el = event.document.documentElement;
        el.oncontextmenu = function () { return false; }
        el.addEventListener("selectionchange", e => {
            if (e.stopPropagation)
                e.stopPropagation();
            if (e.preventDefault)
                e.preventDefault();
            e.cancelBubble = true;
            e.returnValue = false;
            alert("اجرا شد");

            return false;
        });
        el.addEventListener("mousedown touchstart", e => {
            if (e.stopPropagation)
                e.stopPropagation();
            if (e.preventDefault)
                e.preventDefault();
            e.cancelBubble = true;
            e.returnValue = false;
            alert("اجرا شد");
            return false;
        });

    });



    // var loc = window.location.href.indexOf("?loc=");
    var flaglastpage = false;
    var firstload = true;

    // var hash = window.location.hash.slice(2);
    // if (loc > -1) {
    //   var href = window.location.href.slice(loc + 5);
    //   var hash = decodeURIComponent(href);
    // }
    // rendition.display(hash || undefined);

    // function loadfonts() {
    //          rendition.getContents().forEach(c => {
    //        [
    //            "/Content/css/font-style-Shabnam-FD.css",
    //            "/Content/css/font-style-IRANSans.css"
    //        ].forEach(url => {
    //            let el = c.document.body.appendChild(c.document.createElement("link"));
    //            el.setAttribute("rel", "stylesheet");
    //            el.setAttribute("href", url);
    //        });
    //    });
    //           
    //               }
    //
    //           rendition.hooks.content.register(loadfonts);

    function applyFont(ffc, fsc) {
        if (fsc == "") {
            fsc = "11pt";
        }


        var userstrId = document.getElementById("UserId").textContent;

        $.ajax({
            url: '/Products/UpdateBookSetting',
            data: { ProductId: productId, userid: userstrId, FontFamily: ffc, FontSize: fsc },
            type: "post",
            cache: false,
            success: function (data) {

            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
        rendition.themes.select(ffc);
        rendition.themes.font(ffc);
        rendition.themes.fontSize(parseInt(fsc));

        //rendition.getContents().forEach(c => c.addStylesheetRules({
        //    "body": {
        //        "font-family": `${theme.ff} !important`,
        //        "font-size": `${theme.fs} !important`
        //  },
        //}));
    }

    $('#theme-font').on('click', 'li', function () {
        //console.log("Font:" + $(this).find('a').attr("id"));
        applyFont($(this).find('a').attr("id"), "")
    });
    //-------------------------------BackgroundColor--------------------------------------//
    function applyTheme(bgc, fgc) {
        let theme = {
            bg: bgc,
            fg: fgc
            //l: "#0B4085",
            //ff: "Shabnam-FD"
            //fs: "11pt",
            //lh: "1.4",
            //ta: "justify",
            //m: "0"
        };


        var userstrId = document.getElementById("UserId").textContent;

        $.ajax({
            url: '/Products/UpdateBookSetting',
            data: { ProductId: productId, userid: userstrId, Background: bgc, TextColor: fgc },
            type: "post",
            cache: false,
            success: function (data) {

            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });



        rendition.getContents().forEach(c => c.addStylesheetRules({
            "body": {
                "background": theme.bg,
                "color": theme.fg
                //"font-family": `${theme.ff} !important`
                //"font-size": `${theme.fs} !important`,
                //"line-height": `${theme.lh} !important`,
                //"text-align": `${theme.ta} !important`,
                //"padding-top": theme.m,
                //"padding-bottom": theme.m
            },
            // "a": {
            //     "color": "inherit !important",
            //     "text-decoration": "none !important",
            //     "-webkit-text-fill-color": "inherit !important"
            // },
            // "a:link": {
            //     "color": `${theme.l} !important`,
            //     "text-decoration": "none !important",
            //     "-webkit-text-fill-color": `${theme.l} !important`
            // },
            // "a:link:hover": {
            //     "background": "rgba(0, 0, 0, 0.1) !important"
            // },
            // "img": {
            //     "max-width": "100% !important"
            // },
        }));
    }


    //  var colortoggeled = document.getElementById("colortoggeled");
    //      colortoggeled.addEventListener("click", function (e) {
    //        e.preventDefault();
    //  if($("#bgcolorId").hasClass("d-flex"))
    //  {
    //  document.getElementById("bgcolorId").classList.add('d-none');
    //       document.getElementById("bgcolorId").classList.remove('d-flex');
    //   
    //  }else{
    //  
    //  document.getElementById("bgcolorId").classList.add('d-flex');
    //       document.getElementById("bgcolorId").classList.remove('d-none');
    //  }
    //      
    //      }, false);


    function openBottomSheet(id) {
        $(".popup").hide();
        $("#" + id).removeClass("bottom-sheet-dialog-close");
        $("#" + id + " .bottom-sheet-dialog-child .animated").addClass("slideInUp");
        $("#" + id + " .bottom-sheet-dialog-child .animated").removeClass("slideInDown");
    }

    class PageColor {
        constructor(name, active, background, text) {
            this._background = background;
            this._text = text;
            this._name = name;
            this._active = active;
        }

        get background() {
            return this._background;
        }

        get text() {
            return this._text;
        }

        get name() {
            return this._name;
        }

        get active() {
            return this._active;
        }
    }



    let anchorOffset;
    let focusOffset;
    let text;

    let pageColorList = [
        new PageColor("Dark", true, "#222222", "#ffffff"),
        new PageColor("Light", false, "#ffffff", "#363636"),

    ];

    // int to list theme colors
    pageColorList.forEach(item => {
        if (item.active)
            $("#theme-color").append(`<li class="uk-active"><a href="#" style="background-color: ${item.background}">${item.name}</a></li>`)
        else
            $("#theme-color").append(`<li><a href="#" style="background-color: ${item.background}">${item.name}</a></li>`)
    });

    // region Meysam ♥
    $('#theme-color').on('click', 'li', function () {
        let index = $(this).index();
        let pageColor = pageColorList[index];
        applyTheme(pageColor.background, pageColor.text);

    });


    //-------------------------------EndBackgroundColor--------------------------------------//




    var userstrId = document.getElementById("UserId").textContent;



    var next = document.getElementById("next");
    next.addEventListener("click", function (e) {
        rendition.next();
        e.preventDefault();
        flaglastpage = true;


    }, false);

    var prev = document.getElementById("prev");
    prev.addEventListener("click", function (e) {
        rendition.prev();
        e.preventDefault();
        flaglastpage = true;

    }, false);


    //      var nav = document.getElementById("navigation");
    //      var opener = document.getElementById("opener");
    //      opener.addEventListener("click", function (e) {
    //        nav.classList.add("open");
    //      }, false);
    //     
    //      var closer = document.getElementById("closer");
    //      closer.addEventListener("click", function (e) {
    //        nav.classList.remove("open");
    //      }, false);

    // Hidden
    var hiddenTitle = document.getElementById("hiddenTitle");

    function onmousedownbook(e) {
        alert("down");

    };

    rendition.on("rendered", function (section) {
        var current = book.navigation && book.navigation.get(section.href);


        if (current) {
            document.title = current.label;
        }

        // TODO: this is needed to trigger the hypothesis client
        // to inject into the iframe
        // requestAnimationFrame(function () {
        //   hiddenTitle.textContent = section.href;
        // })

        // var old = document.querySelectorAll('.active');
        // Array.prototype.slice.call(old, 0).forEach(function (link) {
        //   link.classList.remove("active");
        // })
        //
        // var active = document.querySelector('a[href="'+section.href+'"]');
        // if (active) {
        //   active.classList.add("active");
        // }
        // Add CFI fragment to the history
        // history.pushState({}, '', "?loc=" + encodeURIComponent(section.href));

        // window.location.hash = "#/"+section.href
    });

    var keyListener = function (e) {

        // Left Key
        if ((e.keyCode || e.which) == 37) {
            rendition.prev();
        }

        // Right Key
        if ((e.keyCode || e.which) == 39) {
            rendition.next();
        }

    };


    //-------------------------------note for pages--------------------------------------//

    $('.uk-navbar-right').on('click', 'li', function () {
        //let color = $(this).find("a").attr("style").replace("background-color:", "");
        var highlighterText = $(this).find("#add-note");
        if (highlighterText != null) {
            cfiRange = highlighterText.attr('value');
            if (highlighterText.attr('value')) {
                $(".popup").hide();

                var index = document.getElementById('current-page').textContent;



                if (highlighterText.attr('name') != "white") {
                    rendition.annotations.highlight(highlighterText.attr('value'), highlighterText.attr('name'), {}, (e) => {
                        console.log("highlight clicked", e.target);
                    });
                }

                var cfiRange = highlighterText.attr('value');
                var color = highlighterText.attr('name');
                var highlightText = highlighterText.attr('title');
                var productId = document.getElementById('ProductId').textContent.trim();
                var href = document.location.search;
                $.ajax({
                    url: '/Products/SaveUserHighlightOfBook',
                    data: { CfiRange: cfiRange, Href: href, ProductId: productId, IndexNumber: index, Color: color, Text: highlightText, userid: userstrId },
                    type: "post",
                    cache: false,
                    success: function (data) {
                        var note = "";
                        if (data.Note != null) {
                            note = data.Note;
                        }
                        $(".page-wrapper").addClass("toggled");
                        var divcard = document.createElement('div');
                        divcard.classList.add('card');
                        divcard.style.margin = '5px';
                        divcard.setAttribute("id", "tag" + data.Id);
                        divcard.innerHTML = "<div class='uk-card uk-card-default uk-text-right' title='" + data.Id + "'><div class='uk-card-header uk-padding-small uk-padding-remove-bottom'><div class='uk-flex-middle'><h5 class='uk-card-title uk-margin-small-bottom uk-text-bold'><span class='uk-label' style='background-color:" + data.HighlightColor + "!important'><a href='#" + data.CfiRange + "' id='gotocfi_" + data.Id + "'>" + data.HighlightText + "</a></span></h5><p class='uk-text-meta uk-margin-remove'><time datetime='2016-04-01T19:00'>" + data.CDate + "</time></p></div></div><div class='uk-card-body uk-padding-small uk-padding'><p>یادداشت ندارد</p></div></div>";
                        //divcard.innerHTML = "<div class='card-header'>" + data.CDate + "<i class='fas fa-tint' style='color:" + data.HighlightColor + "'></i></div><div class='card-body'><a href='#" + data.CfiRange + "' id='gotocfi_" + data.Id + "'>" + data.HighlightText + "</a></div><div class='card-footer'><div class='form-group'><form action='/Products/UpdateUserHighlightOfBook' data-ajax='true' data-ajax-method='Post' data-ajax-mode='replace' data-ajax-update='#result' id='form0' method='post'><div class='row'><div class='col-9'><textarea class='form-control' name='Note' id='Note' placeholder='یادداشت' rows='1'>" + note + "</textarea></div><div class='col-2'><input type='number' name='Id' value='" + data.Id + "' hidden=''><input class='btn btn-success' type='submit' value='ثبت'></div></div></div></form></div></div>";
                        var highlightdiv = document.getElementById("tag");
                        if (data.HighlightColor == "white") {
                            divcard.innerHTML = "<div class='uk-card uk-card-default uk-text-right' title='" + data.Id + "'><div class='uk-card-header uk-padding-small uk-padding-remove-bottom'><div class='uk-flex-middle'><h5 class='uk-card-title uk-margin-small-bottom uk-text-bold'><span class='uk-label' style='background-color:" + data.HighlightColor + "!important'><a style='color:#000 !important' href='#" + data.CfiRange + "' id='gotocfi_" + data.Id + "'>" + data.HighlightText + "</a></span></h5><p class='uk-text-meta uk-margin-remove'><time datetime='2016-04-01T19:00'>" + data.CDate + "</time></p></div></div><div class='uk-card-body uk-padding-small uk-padding'><p>یادداشت ندارد</p></div></div>";

                            highlightdiv = document.getElementById("note");

                            $("#tagId").val(data.Id);
                            $("#form0").attr("data-ajax-update", "#tag" + data.Id);
                            $("#text-tag-message").val("");

                            UIkit.offcanvas("#offcanvas-slide").show();
                            UIkit.switcher("#switcher-item").show(2);
                        }
                        highlightdiv.appendChild(divcard);

                        var gotonote = document.getElementById("gotocfi_" + data.Id);
                        gotonote.onclick = function () {
                            rendition.display(data.CfiRange);
                            $(".page-wrapper").removeClass("toggled");
                            flaglastpage = true;

                        };
                    },
                    error: function (xhr, ajaxOptions, thrownError) {



                    }
                });


            }
        }
    });
    //-------------------------------Highlight--------------------------------------//

    $('.popup').on('click', 'li,span', function () {
        //let color = $(this).find("a").attr("style").replace("background-color:", "");
        $(".popup").hide();
        UIkit.offcanvas("#offcanvas-slide").show();
        UIkit.switcher("#switcher-item").show(1);
        var index = document.getElementById('current-page').textContent;
        var highlighterText = $(this).find("a");
        if (highlighterText != null) {
            cfiRange = highlighterText.attr('value');
            if (highlighterText.attr('value') != "" && highlighterText.attr('value') != 0) {
                if (rendition.annotations._annotationsBySectionIndex[index] != undefined) {
                    if (rendition.annotations._annotationsBySectionIndex[index].filter(p => p != cfiRange)) {
                        flag = false;
                    }
                }


                if (flag) {
                    rendition.annotations.remove(highlighterText.attr('value'));
                    flag = false;
                } else {
                    if (highlighterText.attr('name') != "white") {
                        rendition.annotations.highlight(highlighterText.attr('value'), highlighterText.attr('name'), {}, (e) => {
                            console.log("highlight clicked", e.target);
                        });
                    }

                    var cfiRange = highlighterText.attr('value');
                    var color = highlighterText.attr('name');
                    var highlightText = highlighterText.attr('title');
                    var productId = document.getElementById('ProductId').textContent.trim();
                    var href = document.location.search;
                    $.ajax({
                        url: '/Products/SaveUserHighlightOfBook',
                        data: { CfiRange: cfiRange, Href: href, ProductId: productId, IndexNumber: index, Color: color, Text: highlightText, userid: userstrId },
                        type: "post",
                        cache: false,
                        success: function (data) {
                            var note = "";
                            if (data.Note != null) {
                                note = data.Note;
                            }
                            $(".page-wrapper").addClass("toggled");
                            var divcard = document.createElement('div');
                            divcard.classList.add('card');
                            divcard.style.margin = '5px';
                            divcard.setAttribute("id", "tag" + data.Id);
                            divcard.innerHTML = "<div class='uk-card uk-card-default uk-text-right' title='" + data.Id + "'><div class='uk-card-header uk-padding-small uk-padding-remove-bottom'><div class='uk-flex-middle'><h5 class='uk-card-title uk-margin-small-bottom uk-text-bold'><span class='uk-label' style='background-color:" + data.HighlightColor + "!important'><a href='#" + data.CfiRange + "' id='gotocfi_" + data.Id + "'>" + data.HighlightText + "</a></span></h5><p class='uk-text-meta uk-margin-remove'><time datetime='2016-04-01T19:00'>" + data.CDate + "</time></p></div></div><div class='uk-card-body uk-padding-small uk-padding'><p>یادداشت ندارد</p></div></div>";
                            //divcard.innerHTML = "<div class='card-header'>" + data.CDate + "<i class='fas fa-tint' style='color:" + data.HighlightColor + "'></i></div><div class='card-body'><a href='#" + data.CfiRange + "' id='gotocfi_" + data.Id + "'>" + data.HighlightText + "</a></div><div class='card-footer'><div class='form-group'><form action='/Products/UpdateUserHighlightOfBook' data-ajax='true' data-ajax-method='Post' data-ajax-mode='replace' data-ajax-update='#result' id='form0' method='post'><div class='row'><div class='col-9'><textarea class='form-control' name='Note' id='Note' placeholder='یادداشت' rows='1'>" + note + "</textarea></div><div class='col-2'><input type='number' name='Id' value='" + data.Id + "' hidden=''><input class='btn btn-success' type='submit' value='ثبت'></div></div></div></form></div></div>";
                            var highlightdiv = document.getElementById("tag");
                            if (data.HighlightColor == "white") {
                                divcard.innerHTML = "<div class='uk-card uk-card-default uk-text-right' title='" + data.Id + "'><div class='uk-card-header uk-padding-small uk-padding-remove-bottom'><div class='uk-flex-middle'><h5 class='uk-card-title uk-margin-small-bottom uk-text-bold'><span class='uk-label' style='background-color:" + data.HighlightColor + "!important'><a style='color:#000 !important' href='#" + data.CfiRange + "' id='gotocfi_" + data.Id + "'>" + data.HighlightText + "</a></span></h5><p class='uk-text-meta uk-margin-remove'><time datetime='2016-04-01T19:00'>" + data.CDate + "</time></p></div></div><div class='uk-card-body uk-padding-small uk-padding'><p>یادداشت ندارد</p></div></div>";

                                highlightdiv = document.getElementById("note");
                            }
                            highlightdiv.appendChild(divcard);

                            var gotonote = document.getElementById("gotocfi_" + data.Id);
                            gotonote.onclick = function () {
                                rendition.display(data.CfiRange);
                                $(".page-wrapper").removeClass("toggled");
                                flaglastpage = true;

                            };
                        },
                        error: function (xhr, ajaxOptions, thrownError) {



                        }
                    });

                    flag = true;
                }
            }
        }
    });

    var flag = false;


    function drawBorderAroundSelection(contents, cfiRange) {
        $(".popup").show();

        var selection = contents.window.getSelection(), // get the selection then
            range = selection.getRangeAt(0), // the range at first selection group
            rect = range.getBoundingClientRect(); // and convert this to useful data
        var selectText = range.startContainer.wholeText.substr(range.startOffset, (range.endOffset - range.startOffset));

        $(".popup .uk-dotnav > li > a").each(function (index) {
            var color = $(this).text();
            $(this).attr("value", cfiRange);
            $(this).attr("name", color);
            $(this).attr("title", selectText);
            console.log(index + ": " + $(this).text());
        });
        $(".popup >span> #add-tag").each(function (index) {
            var color = $(this).attr("data-color");
            $(this).attr("value", cfiRange);
            $(this).attr("name", color);
            $(this).attr("title", selectText);
            console.log(index + ": " + $(this).text());
        });

    }


    function onmousedownselected(e, contents) {
        contents.window.getSelection().removeAllRanges();
        $(".popup").hide();
    }


    function ontouchendselected(e) {
        $(".popup").css({ left: e.changedTouches[0].screenX - 280 });
        $(".popup").css({ top: e.changedTouches[0].pageY });

    }

    // Apply a class to selected text
    rendition.on("selected", function (cfiRange, contents) {
        $(".popup").hide();
        var selection = contents.window.getSelection().getRangeAt(0);
        drawBorderAroundSelection(contents, cfiRange)
        contents.window.onmousedown = function (e) { onmousedownselected(e, contents); };
        contents.window.ontouchend = function (e) { ontouchendselected(e); };

    });

    //-------------------------------EndHighlight--------------------------------------//


    // Illustration of how to get text from a saved cfiRange
    var highlights = document.getElementById('highlights');


    rendition.on("keyup", keyListener);
    document.addEventListener("keyup", keyListener, false);


    //-------------------------------Bodymousedown--------------------------------------//

    //           function onmousedownbody(e) {
    //                 var bubbleId = document.getElementById("bubbleId");
    //                 if (bubbleId) {
    //                   bubbleId.remove();
    //                 }
    //           $(".page-wrapper").removeClass("toggled");
    //           document.getElementById("bgcolorId").classList.add('d-none');
    //                document.getElementById("bgcolorId").classList.remove('d-flex');
    //               }
    //           function ontouchstart(e) {
    //           mousex=e.touches[0].screenX;
    //           
    //                 }
    //           function ontouchend(e) {
    //           var moseafterx=e.changedTouches[0].screenX;
    //           if(mousex > moseafterx)
    //           {
    //                 rendition.prev();
    //           
    //           }else if(mousex < moseafterx){
    //           rendition.next();
    //           }
    //                 }
    //           function getContentsEach(item, index) {
    //                 item.window.onmousedown = function (e) { onmousedownbody(e); };
    //                 item.window.ontouchstart = function (e) { ontouchstart(e); };
    //                 item.window.ontouchend = function (e) { ontouchend(e); };
    //               }
    //           
    //           function getContentsall() {
    //           rendition.getContents().forEach(getContentsEach)
    //                                  
    //           
    //               }
    //           rendition.hooks.content.register(getContentsall);
    //-------------------------------EndBodymousedown--------------------------------------//

    //function onmousedownselected() {
    //      var bubbleId = document.getElementById("bubbleId");
    //      if (bubbleId) {
    //        bubbleId.remove();
    //      }
    //      contents.window.getSelection().removeAllRanges();
    //    }
    //rendition.getContents()[0].window.onclick=function (e) { onmousedownselected(e); };
    book.ready.then(function () {
        var $viewer = document.getElementById("pages");
        $viewer.classList.remove("loading");

    });



    book.loaded.navigation.then(function (toc) {
        var $nav = document.getElementById("toc"),
            docfrag = document.createDocumentFragment();

        toc.forEach(function (chapter, index) {
            var item = document.createElement("li");
            var link = document.createElement("a");
            link.id = "chap-" + chapter.id;
            link.textContent = chapter.label;
            link.href = chapter.href;
            item.appendChild(link);
            docfrag.appendChild(item);

            link.onclick = function () {
                var url = link.getAttribute("href");
                flaglastpage = true;
                rendition.display(url);
                UIkit.offcanvas("#offcanvas-slide").hide();

                return false;
            };

        });

        $nav.appendChild(docfrag);


    });

    book.loaded.metadata.then(function (meta) {
        var $title = document.getElementById("title");
        var $author = document.getElementById("author");
        var $cover = document.getElementById("cover");
        var $bookcover = document.getElementById("book-cover");
        var $nav = document.getElementById('navigation');

        $title.textContent = meta.title;
        $author.textContent = meta.creator;
        if (book.archive) {
            book.archive.createUrl(book.cover)
                .then(function (url) {
                    $cover.src = url;
                    $bookcover.style.backgroundImage = "url('" + url + "')";
                })
        } else {
            $cover.src = book.cover;
            $bookcover.style.backgroundImage = "url('" + book.cover + "')";
        }

    });


    // When navigating to the next/previous page
    rendition.on('relocated', function (locations) {


        var cfiRange = locations.start.cfi;
        $("#add-note").attr("value", cfiRange);
        $("#add-note").attr("name", "white");
        $("#add-note").attr("title", "صفحه " + locations.start.displayed.page);





        var userstrId = document.getElementById("UserId").textContent;
        var productId = document.getElementById('ProductId').textContent.trim();
        if (firstload) {


            $.ajax({
                url: '/Products/BookSetting',
                data: { ProductId: productId, userid: userstrId },
                type: "post",
                cache: false,
                success: function (data) {

                    applyFont(data.FontFamily, data.FontSize);
                    var thems = data;
                    setTimeout(() => {
                        //LastPageOfBook
                        $.ajax({
                            url: '/Products/LastPageOfBook',
                            data: { ProductId: productId, userid: userstrId },
                            type: "post",
                            cache: false,
                            success: function (data) {
                                if (data) {
                                    var cfi = new ePub.CFI(data.BookLastseenPage).toString();
                                    if (cfi) {
                                        book.rendition.display(cfi);

                                    }
                                }
                                setTimeout(() => {
                                    applyTheme(thems.Background, thems.TextColor);
                                    $('.spinner').css('display', 'none');

                                }, 2000);

                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                            }
                        });

                        //LastPageOfBook
                    }, 2000);

                },
                error: function (xhr, ajaxOptions, thrownError) {
                }
            });

            firstload = false;
        }





        var currentPage = locations.start.displayed.page;
        var EndPage = locations.start.displayed.total;
        $("#current-page").text(currentPage);
        $("#total-page").text(EndPage);
        $("#tag").empty();
        //-------------------------------HighlightListOfBook--------------------------------------//
        // var productId = document.getElementById('ProductId').textContent.trim();
        //var userstrId=document.getElementById("UserId").textContent;
        var index = document.getElementById('current-page').textContent;

        $.ajax({
            url: '/Products/HighlightListOfBook',
            data: { ProductId: productId, userid: userstrId, pageNumber: index },
            type: "post",
            cache: false,
            success: function (data) {

                data.forEach(cfiFunction);
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });


        function cfiFunction(item, index) {
            var note = "";
            if (item.Note != null) {
                note = item.Note;
            }
            var divcard = document.createElement('div');
            divcard.classList.add('card');
            divcard.style.margin = '5px';
            divcard.setAttribute("id", "tag" + item.Id);
            //divcard.innerHTML = "<div class='card-header'>"+item.CDate+"<i class='fas fa-tint' style='color:"+item.HighlightColor+"'></i></div><div class='card-body'><a href='#"+item.CfiRange+"' id='gotocfi_"+item.Id+"'>"+item.HighlightText+"</a></div><div class='card-footer'><div class='form-group'><form action='/Products/UpdateUserHighlightOfBook' data-ajax='true' data-ajax-method='Post' data-ajax-mode='replace' data-ajax-update='#result' id='form0' method='post'><div class='row'><div class='col-9'><textarea class='form-control' name='Note' id='Note' placeholder='یادداشت' rows='1'>"+note+"</textarea></div><div class='col-2'><input type='number' name='Id' value='"+item.Id+"' hidden=''><input class='btn btn-success' type='submit' value='ثبت'></div></div></div></form></div></div>";
            var highlightdiv = document.getElementById("tag");
            if (item.HighlightColor == "white") {
                divcard.innerHTML = "<div class='uk-card uk-card-default uk-text-right' title='" + item.Id + "'><div class='uk-card-header uk-padding-small uk-padding-remove-bottom'><div class='uk-flex-middle'><h5 class='uk-card-title uk-margin-small-bottom uk-text-bold'><span class='uk-label' style='background-color:" + item.HighlightColor + "!important'><a style='color:#000 !important' href='#" + item.CfiRange + "' id='gotocfi_" + item.Id + "'>" + item.HighlightText + "</a></span></h5><p class='uk-text-meta uk-margin-remove'><time datetime='2016-04-01T19:00'>" + item.CDate + "</time></p></div></div><div class='uk-card-body uk-padding-small uk-padding'><p>" + note + "</p></div></div>";

                highlightdiv = document.getElementById("note");
            } else {
                divcard.innerHTML = "<div class='uk-card uk-card-default uk-text-right' title='" + item.Id + "'><div class='uk-card-header uk-padding-small uk-padding-remove-bottom'><div class='uk-flex-middle'><h5 class='uk-card-title uk-margin-small-bottom uk-text-bold'><span class='uk-label' style='background-color:" + item.HighlightColor + "!important'><a href='#" + item.CfiRange + "' id='gotocfi_" + item.Id + "'>" + item.HighlightText + "</a></span></h5><p class='uk-text-meta uk-margin-remove'><time datetime='2016-04-01T19:00'>" + item.CDate + "</time></p></div></div><div class='uk-card-body uk-padding-small uk-padding'><p>" + note + "</p></div></div>";

                rendition.annotations.highlight(item.CfiRange, item.HighlightColor, {}, (e) => {
                    console.log("highlight clicked", e.target);
                });
            }

            highlightdiv.appendChild(divcard);
            var gotonote = document.getElementById("gotocfi_" + item.Id);
            gotonote.onclick = function () {
                rendition.display(item.CfiRange);
                $(".page-wrapper").removeClass("toggled");
                flaglastpage = true;

            };

        }
        //-------------------------------EndHighlightListOfBook--------------------------------------//


        if (flaglastpage) {


            if (currentPage == EndPage) {
                var userId = document.getElementById("UserId").textContent;
                var productId = document.getElementById("ProductId").textContent;

                $.ajax({
                    url: "/api/CompleteReadProduct",
                    type: "post",
                    data: {
                        ProductId: productId,
                        UserId: userId
                    },
                    success: function (data) {

                        alert(data.Message);

                    },
                    error: function () {
                        alert("خطا");
                    }

                });


            }

            var productId = document.getElementById('ProductId').textContent.trim();
            var userstrId = document.getElementById("UserId").textContent;

            //var href="?loc="+section.href;
            var href = locations.start.cfi;

            $.ajax({
                url: '/Products/SaveLastPageOfBook',
                data: { Href: href, ProductId: productId, userid: userstrId },
                type: "post",
                cache: false,
                success: function (data) {



                },
                error: function (xhr, ajaxOptions, thrownError) {



                }
            });
        }

    });

    book.loaded.navigation.then(function () {







    });



    book.rendition.hooks.content.register(function (contents, view) {

        contents.window.addEventListener('scrolltorange', function (e) {
            var range = e.detail;
            var cfi = new ePub.CFI(range, contents.cfiBase).toString();
            if (cfi) {
                book.rendition.display(cfi);
            }
            e.preventDefault();
        });

    });
};

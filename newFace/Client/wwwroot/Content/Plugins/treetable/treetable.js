$(function () {
    var
        $table = $('#dataTables-example'),
        rows = $table.find('tr');

    rows.each(function (index, row) {
        var
            $row = $(row),
            level = $row.data('level'),
            id = $row.data('id'),
            $columnName = $row.find('td[data-column="name"]'),
            children = $table.find('tr[data-parent="' + id + '"]');

        if (children.length) {
            var expander = $columnName.append('' + '<span class="glyphicon glyphicon-chevron-left"></span>' + '<button type="button" class="btn glyphicon-chevron-left">زیرگروه ها</button>');

            children.hide();

            expander.on('click', function (e) {
                var $target = $(e.target);
                if ($target.hasClass('glyphicon-chevron-left')) {
                    $target
                        .removeClass('glyphicon-chevron-left')
                        .addClass('glyphicon-chevron-down');

                    $('span')
                        .removeClass('glyphicon-chevron-left')
                        .addClass('glyphicon-chevron-down');

                    children.show(1000);
                } else {
                    $target
                        .removeClass('glyphicon-chevron-down')
                        .addClass('glyphicon-chevron-left');

                    $('span')
                        .removeClass('glyphicon-chevron-down')
                        .addClass('glyphicon-chevron-left');

                    children.hide(1000);


                    //چند مرحله ای
                    //reverseHide($table, $row);
                }
            });
        }

        //$columnName.prepend('' +
        //    '<span class="treegrid-indent" style="width:' + 15 * level + 'px"></span>' +
        //    '');
    });

    // Reverse hide all elements
    reverseHide = function (table, element) {
        var
            $element = $(element),
            id = $element.data('id'),
            children = table.find('tr[data-parent="' + id + '"]');

        if (children.length) {
            children.each(function (i, e) {
                reverseHide(table, e);
            });

            $element
                .find('.glyphicon-chevron-down')
                .removeClass('glyphicon-chevron-down')
                .addClass('glyphicon-chevron-left');

            children.hide();
        }
    };
});

(function ($) {
    $.fn.Scrollable = function (options) {
        var defaults = {
            ScrollHeight: 300,
            Width: 0
        };
        var options = $.extend(defaults, options);
        return this.each(function () {
            var grid = $(this).get(0);
            var gridWidth = grid.offsetWidth;
            var gridHeight = grid.offsetHeight;
            var headerCellWidths = new Array();
            for (var i = 0; i < grid.getElementsByTagName("TH").length; i++) {
                headerCellWidths[i] = grid.getElementsByTagName("TH")[i].Width;
            }
            grid.parentNode.appendChild(document.createElement("div"));
            var parentDiv = grid.parentNode;

            var table = document.createElement("table");
            for (i = 0; i < grid.attributes.length; i++) {
                if (grid.attributes[i].specified && grid.attributes[i].name != "id") {
                    table.setAttribute(grid.attributes[i].name, grid.attributes[i].value);
                }
            }

            table.style.cssText = grid.style.cssText;

            //fix for the header width to adapt the grid rows.
            table.style.width = options.Width + "px";
            //------------------------//

            table.appendChild(document.createElement("tbody"));
            table.getElementsByTagName("tbody")[0].appendChild(grid.getElementsByTagName("TR")[0]);
            var cells = table.getElementsByTagName("TH");

            var gridRow = grid.getElementsByTagName("TR")[0];
            for (var i = 0; i < cells.length; i++) {
                var width;
                if (headerCellWidths[i] >= gridRow.getElementsByTagName("TD")[i].Width) {
                    width = headerCellWidths[i];
                }
                else {
                    width = gridRow.getElementsByTagName("TD")[i].Width;
                }

                cells[i].style.width = width + "px";
            }

            parentDiv.removeChild(grid);

            var dummyHeader = document.createElement("div");

            //fix for the header width to adapt the grid rows.
            dummyHeader.style.width = options.Width + "px";
            //-------------------------------------------//

            dummyHeader.appendChild(table);
            parentDiv.appendChild(dummyHeader);
            if (options.Width > 0) {
                gridWidth = options.Width;
            }
            var scrollableDiv = document.createElement("div");
            if (parseInt(gridHeight) > options.ScrollHeight) {
                gridWidth = parseInt(gridWidth) + 17;
            }
            scrollableDiv.style.cssText = "overflow:auto;max-height:" + options.ScrollHeight + "px;width:" + gridWidth + "px";
            scrollableDiv.appendChild(grid);
            parentDiv.appendChild(scrollableDiv);

            // cut the pagination row out of the scrollabe div and append it to the main div.
            var paginationDiv = document.createElement("div");
            paginationDiv.setAttribute("class", "pagination");

            var oldpaginationRow = scrollableDiv.getElementsByClassName("pagination")[0];
            if (oldpaginationRow != null) {
                paginationDiv.appendChild(oldpaginationRow.getElementsByTagName("table")[0]);
                paginationDiv.style.cssText = "width:" + gridWidth + "px";
                parentDiv.appendChild(paginationDiv);
            }
        });
    };
})(jQuery);
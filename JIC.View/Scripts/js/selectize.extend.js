
function Init() {
    window.selects = [];
    if ($('[data-type=selectize]').length == 0)
        return;
    $.each($('[data-type=selectize]'), function () {

        var select = $(this);
        var options = select.find('option');
        //alert[options];
        var selectedOptions = [];
        var prefetch = $(select).data('prefetch');
        var remote = $(select).data('remote');
        var wildcard = $(select).data('wildcard');
        var clear = $(select).data('clear') == 'False' ? false : true;
        var create = $(select).data('create') == 'False' ? false : true;
        var IsOneClick = $(select).data('isoneclick') == 'False' ? false : true;
        var newValueContainer = $(select).data('newValueContainer');
        if (!clear) {
            options.each(function (i) {
          //      alert(this.getAttribute("selected"));
                if (this.getAttribute("selected")) {
            //        alert($(this).val());
                    selectedOptions.push($(this).val());
                }
            });
        }
        var dataSource = null;
        if ($(select).data('prefetch') || $(select).data('remote') || options) {
            var dataSource = new Bloodhound({
                datumTokenizer: Bloodhound.tokenizers.obj.whitespace($(select).data('text')),
                queryTokenizer: Bloodhound.tokenizers.whitespace,
                local: options,
                limit: $(select).data('limit')
            });
            dataSource.prefetch = prefetch;
            if (remote) {
                dataSource.remote = {
                    url: remote,
                    wildcard: wildcard,
                    filter: function (data) {
                    }
                };
            }
        }
        if (dataSource)
            dataSource.initialize();
        var selectize = $(select).selectize({
            mode: $(select).data('mode'),
            maxItems: null,
            items: selectedOptions,
            maxOptions: $(select).data('limit'),
            valueField: $(select).data('value'),
            labelField: $(select).data('text'),
            searchField: $(select).data('search'),
            create: create,
            highlight: true,
            persist: false,
            load: function (query, callback) {
                if (!query.length || !dataSource) return callback();
                dataSource.get(query, function (data) {
                    callback(data);
                });

            },

            placeholder: $(select).attr('placeholder'),
            plugins: ['restore_on_backspace', 'remove_button', 'drag_drop'],
            onItemAdd: function (value, $item) {
                //$(select).attr('data-newValueContainer', value);
                $(newValueContainer).val(value);
                if ($(select).data('mode') == 'single') {
                    this.blur();
                    this.close();
                }
                
            },

            onChange: function (value) {
                //  $(select).attr('data-newValueContainer', value);
                $(newValueContainer).val(value);
            },

        });
        
        if (clear) {
            selectize[0].selectize.clear();
            window.selects.push({ id: $(select).attr('id'), clear: true });
            $(select).data('clear', 'False');
         }

    });
}

$(function () {
    Init();
});

$(function () {
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(function (sender, args) {
        window.selects.find(function (select) {
            if (select.id == args._postBackElement.id && select.clear) {
                args.set_cancel(true);
                select.clear = false;
            }
        });
    });
    prm.add_endRequest(function () {
        Init();
    });
});
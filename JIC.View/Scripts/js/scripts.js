$(function () {
    //text area maxlength
    $(document.body).on('onblur keyup', 'textarea', function () {
        if ($(this).attr("maxlength") == undefined)
            return true;
        var len = parseInt($(this).attr("maxlength"), 10);
        if ($(this).val().length > len) {
            $(this).val($(this).val().substr(0, len));
            return false;
        }
    });

    //accept chars from 0-9 and "-"
    $(document.body).on('keypress', 'input:text[data-type="int"]', function (event) {
        var chars = "-0123456789";
        var chr = String.fromCharCode(event.charCode == undefined ? event.keyCode : event.charCode);
        return event.ctrlKey || event.metaKey || (chr < ' ' || !chars || chars.indexOf(chr) > -1);
    })

    //accept chars from 0-9, "." and "-"
    $(document.body).on('keypress', 'input:text[data-type="double"]', function (event) {
        var chars = "-0123456789.";
        var chr = String.fromCharCode(event.charCode == undefined ? event.keyCode : event.charCode);
        return event.ctrlKey || event.metaKey || (chr < ' ' || !chars || chars.indexOf(chr) > -1);
    })

    //clear form button.
    $(document.body).on('click',
        'a[data-action="clear"]',
        function(event) {
            var parent = $('#' + $(this).attr("data-parent"));
            parent.find('input:text').val("");
            parent.find('textarea').val("");
            //clear the enabled controls only.
            parent.find('select[disabled!="disabled"]').val("-1");
            parent.find('input:radio').attr('checked', false);
            parent.find('input:checkbox').attr('checked', false);
            //check if the button should not post back.
            return $(this).attr("data-post-back") != 'false';
        });

    $(document.body).on('click', '.checkall',
   function () {

       // if check all is checked, all checkboxes are checked
       // if check all is unchecked, all checkboxes are cleared

       if ($('.checkall input').is(':checked')) {


           $('.Check input').each(
               function () {
                   $(this).attr('checked', 'checked');
               });
       }
       else {
           $('.Check input').each(
               function () {
                   $(this).removeAttr('checked');
               });
       }
   });

    $(document.body).on('click', '.checkallnd',
 function () {

     // if check all is checked, all checkboxes are checked
     // if check all is unchecked, all checkboxes are cleared

     if ($('.checkallnd input').is(':checked')) {


         $('.checknd input').each(
             function () {
                 $(this).attr('checked', 'checked');
             });
     }
     else {
         $('.checknd input').each(
             function () {
                 $(this).removeAttr('checked');
             });
     }
 });

    $(document.body).on('click', '[data-action="check-all"]',
function (e, s) {


    var that = $(this);
    var parentThat = $('#' + that.attr('data-list-id'));
    var checkList = parentThat.find('input[type="checkbox"]');

    if (that.find('input[type="checkbox"]').is(':checked')) {

        checkList.attr("checked", "checked");
        checkList.on('click',
            function () {
                var count = checkList.length;

                var checkedCount = checkList.filter(':checked').length;

                that.find('input').attr('checked', count == checkedCount);
            });
    }
    else {
        checkList.removeAttr("checked");
    }
});
})

var postbackElement = null;

jQuery(document).ready(function ($) {
    updateClock();
    setInterval('updateClock()', 1 * 1000)

    if ($('[data-show-popover="true"]').length > 0) {
        $('[data-show-popover="true"]').popover();
    }

    var prm = Sys.WebForms.PageRequestManager.getInstance();

    prm.add_beginRequest(function (source, args) {
        postbackElement = args.get_postBackElement();

        $('.ajax-loading').show();
    });

    prm.add_endRequest(function (sourc, args) {
        if (postbackElement != null && document.getElementById(postbackElement.id) != null)
            document.getElementById(postbackElement.id).focus();

        $('.ajax-loading').hide();
        $('[data-show-popover="true"]').popover();
        if (!$(".modal").is(':visible')) {
            $("body").removeClass('modal-open');
            $("body").css("padding-right", "");
        }
    });

    $(document.body).on('click', '[data-action=show-dialog]', function (evt) {
        var IsValid = true;
        if ($(this).attr("data-causes-validation") != undefined) {
            if (typeof (Page_ClientValidate) == 'function') {
                if ($(this).attr("data-validation-group") != undefined)
                    IsValid = Page_ClientValidate($(this).attr("data-validation-group"));
                else
                    IsValid = Page_ClientValidate();
            }
        }
        if (IsValid) {
            $('#' + $(this).data('id') + '-dialog').modal('show');
            $('#' + $(this).data('id') + '_hfArgumentData').val($(this).attr('data-argument'));
            evt.preventDefault();
        }
    });
});

function ValidateCheckBoxList(sender, args) {
    if ($('#' + $(sender).attr('data-list-id')).find('input:checkbox:checked').length == 0)
        args.IsValid = false;
    else
        args.IsValid = true;
}

function ValidateRadioButtonList(sender, args) {
    if ($('#' + $(sender).attr('data-list-id')).find('input:radio:checked').length == 0)
        args.IsValid = false;
    else
        args.IsValid = true;
}

function updateControlValidationHighlights() {
    if (typeof Page_Validators != 'undefined') {
        // A single control may have multiple validators. Its final validation state is the
        // logical and of all validator states
        var controlStates = {};
        $.each(Page_Validators, function () {
            if (controlStates.hasOwnProperty(this.controltovalidate)) {
                controlStates[this.controltovalidate] = controlStates[this.controltovalidate] && this.isvalid;
            } else {
                controlStates[this.controltovalidate] = this.isvalid;
            }
        });

        var cssClass = 'invalid';
        var firstTabID = null;

        for (controlName in controlStates) {
            if (controlStates.hasOwnProperty(controlName)) {
                var control = $('#' + controlName);
                if (controlStates[controlName]) {
                    control.removeClass(cssClass);
                } else {
                    if (!control.hasClass(cssClass)) {
                        control.addClass(cssClass);
                        // check if the control is in a tab control to activate the first tab which contains invalid controls.
                        if (firstTabID == null) {
                            if (control.parents('.tab-pane').length == 1) {
                                firstTabID = control.parents('div.tab-pane').attr("id");
                            }
                        }
                    }
                }
            }
        }

        // activate the first tab which contains invalid controls.
        if (firstTabID != null)
            $('.nav-tabs li a[href="#' + firstTabID + '"]').tab('show');
    }
}

function updateClock() {
    var currentTime = new Date();
    var currentHours = currentTime.getHours();
    var currentMinutes = currentTime.getMinutes();
    var currentSeconds = currentTime.getSeconds();
    currentMinutes = (currentMinutes < 10 ? "0" : "") + currentMinutes;
    currentSeconds = (currentSeconds < 10 ? "0" : "") + currentSeconds;
    var timeOfDay = (currentHours < 12) ? "ص" : "م";
    currentHours = (currentHours > 12) ? currentHours - 12 : currentHours;
    currentHours = (currentHours == 0) ? 12 : currentHours;
    var currentTimeString = timeOfDay + " " + currentHours + ":" + currentMinutes + ":" + currentSeconds;
    $(".clock").text(currentTimeString);
}

// AutoComplete 
window.store = {
    localStoreSupport: function () {
        try {
            return 'localStorage' in window && window['localStorage'] !== null;
        } catch (e) {
            return false;
        }
    },
    set: function (name, value, days) {
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            var expires = "; expires=" + date.toGMTString();
        }
        else {
            var expires = "";
        }
        if (this.localStoreSupport()) {
            localStorage.setItem(name, value);
        }
        else {
            document.cookie = name + "=" + value + expires + "; path=/";
        }
    },
    get: function (name) {
        if (this.localStoreSupport()) {
            return localStorage.getItem(name);
        }
        else {
            var nameEQ = name + "=";
            var ca = document.cookie.split(';');
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i];
                while (c.charAt(0) == ' ') c = c.substring(1, c.length);
                if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
            }
            return null;
        }
    },
    del: function (name) {
        if (this.localStoreSupport()) {
            localStorage.removeItem(name);
        }
        else {
            this.set(name, "", -1);
        }
    }
}
var openAuto = 0;
var isopen = false;
$(document).on("change keyup paste", ".auto", function () {
    //debugger;
    var current = $(this).getCursorPosition();
    var parent = $(this).parents('.textbox-parent-div');
    var hdJson = parent.find('.class');
    var obj = JSON.parse(hdJson.val());

    var serverValidSource = obj.serverValidSource;
    var localname = obj.localname;
    var ServiceName = obj.ServiceName;
    var isFilterJson = obj.isFilterJson;
    var FilterJson = obj.FilterJson;
    var txtboxAuto = "auto";
    LoadData(serverValidSource, ServiceName, localname);
    AutoCopMethod($(this), localname, isFilterJson, current);
});
function LoadData(validLoad, servName, localstore) {
    if (validLoad == 'False') {

        var r = store.get(localstore);

        var globalJsonVar;
        $.getJSON(servName, null, function (result) { // Note crucial ?callback=?
            globalJsonVar = JSON.stringify(result);
            store.set(localstore, globalJsonVar);
        });
    }
}
function AutoCopMethod(txtName, localname, isFilterJson, current) {

    // debugger;
    txtName

      .on("keydown", function (event) {

          if (event.keyCode === $.ui.keyCode.tab &&
                  $(this).autocomplete("instance").menu.active
                 ) {
              event.preventDefault();
          }
          if (event.keyCode === $.ui.keyCode.LEFT ||

              event.keyCode === $.ui.keyCode.RIGHT ||
  (event.keyCode === $.ui.keyCode.SPACE && !$(this).autocomplete("instance").menu.active) ||
              event.keyCode === $.ui.keyCode.ESCAPE ||
              (event.keyCode === $.ui.keyCode.ENTER && !$(this).autocomplete("instance").menu.active)) {
              //debugger;
              openAuto = $(this).getCursorPosition();
          }

      })
        .autocomplete({

            minLength: 0,
            source: function (request, response) {
                //debugger;

                var avilableTags;

                if (isFilterJson == "True") {

                    avilableTags = FilterCrytecal(JSON.parse(store.get(localname)), FilterJson);
                }
                else {
                    avilableTags = JSON.parse(store.get(localname));
                }

                // $(JSON.parse(avilableTags)).filter()
                var filterData = FilterCrytecalByPart(avilableTags, { TextPredections: SetCurrentString(request.term, current) })
                var resstr = JSON.stringify(filterData);
                response($.map(filterData, function (item) {
                    return {
                        label: item.TextPredections
                    }
                }));
            },
            open: function (event, ui) {

                if (!isopen) {
                    openAuto = $(this).getCursorPosition();
                    if (openAuto == 1) {
                        openAuto = 0;
                    }
                    isopen = true;


                }

            },
            close: function (event, ui) {

                return false;
            },
            focus: function (event, ui) {
                // prevent value inserted on focus
                return false;
            },
            select: function (event, ui) {
                //  debugger;
                var terms = split(this.value);
                var a = $(this).val();
                var b = ui.item.value;
                var position = $(this).getCursorPosition();
                var last = $(this).val().length;
                var result;

                result = a.substr(0, openAuto) + b + a.substr(position);


                $(this).val(result);
                terms.pop();
                terms.push(ui.item.value);
                this.value = terms.join(" ");
                openAuto = $(this).getCursorPosition();
                isopen = false;
                return false;
            }
        });
};

$.fn.getCursorPosition = function () {
    var input = this.get(0);
    if (!input) return; // No (input) element found
    if (document.selection) {
        // IE
        input.focus();
    }
    return 'selectionStart' in input ? input.selectionStart : '' || Math.abs(document.selection.createRange().moveStart('character', -input.value.length));
}
function filterValuePart(arr, part) {
    // debugger;
    return arr.filter(function (obj) {
        return Object.keys(obj)
                     .some(function (k) {
                         return obj[k].indexOf(part) !== -1;
                     });
    });
}
function FilterCrytecalByPart(my_object, my_criteria) {
    //debugger;

    return my_object.filter(function (obj) {
        return Object.keys(my_criteria).every(function (c) {
            return obj[c].indexOf(my_criteria[c]) !== -1;
        });
    });

}
function FilterCrytecal(my_object, my_criteria) {
    //  debugger;
    return my_object.filter(function (obj) {
        return Object.keys(my_criteria).some(function (c) {
            return obj[c] == my_criteria[c];
        });
    });

}
function split(val) {
    return val.split(/ \s*/);
}
function extractLast(term) {
    return split(term).pop();
}
function SetCurrentString(itemtext, currentpos) {
    //debugger;
    var position = currentpos;

    if (openAuto == "undefined") {
        openAuto = 0
    }
    var len = position - openAuto

    var part = itemtext.substr(openAuto, len).replace("\n", "");
    console.log(part);



    return itemtext.substr(openAuto, len).replace("\n", "")
}
//#end AutoComplete 
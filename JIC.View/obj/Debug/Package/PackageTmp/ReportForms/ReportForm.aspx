<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportForm.aspx.cs" Inherits="JIC.Crime.View.ReportForms.ReportForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/js/jquery.js"></script>
    <link href="../Content/css/bootstrap.css" rel="stylesheet" />
    <script>
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
                function (event) {
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

            //if ($('[data-show-popover="true"]').length > 0) {
            //    $('[data-show-popover="true"]').popover();
            //}

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_beginRequest(function (source, args) {
                postbackElement = args.get_postBackElement();

                $('.ajax-loading').show();
            });

            prm.add_endRequest(function (sourc, args) {
                if (postbackElement != null && document.getElementById(postbackElement.id) != null)
                    document.getElementById(postbackElement.id).focus();

                $('.ajax-loading').hide();
                //$('[data-show-popover="true"]').popover();
                //if (!$(".modal").is(':visible')) {
                //    $("body").removeClass('modal-open');
                //    $("body").css("padding-right", "");
                //}
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

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
              <div class="section">
          
            <div class="sub-section" runat="server" id="dvReportParameters" visible="false" >
                <h4 class="sub-section-title"><%= JIC.Base.Resources.Resources.ReportsPramter %></h4>
                <div class="row">
                    
                    <div class="col-md-6" runat="server" id="dvCircleByCurrentUser" visible="false">
                        <CustomControls:CDropDownList runat="server" ID="ddlCircuite" 
                            AddonAutoPostBack="true"
                            IncludeDefaultItem="true" 
                            LabelResourceKey="Circuit" 
                            IsRequired="true" DataValueField="ID" DataTextField="Name" AutoPostBack="true" 
                         OnSelectedIndexChanged="ddlCircuite_SelectedIndexChanged"
                            SelectMethod="ddlCircuite_GetData"
                            ></CustomControls:CDropDownList>

                         <%--  OnSelectedIndexChanged="ddlCircuite_SelectedIndexChanged"
                             --%>
                    </div>
                    <div class="col-md-6" runat="server" id="dvSessionByCurrentUser" visible="false">
                        <CustomControls:CDropDownList runat="server" ID="ddlRolls" 
                        SelectMethod="ddlRolls_GetData"
                            IncludeDefaultItem="true" LabelResourceKey="SessionDate" IsRequired="true" DataValueField="ID" DataTextField="Name"></CustomControls:CDropDownList>
                          <%--   --%>
                    </div>
                </div>
                <div class="row">
                        <div class="col-md-6" runat="server" id="DivButton" visible="false">
                        <CustomControls:CButton runat="server" ID="btnReport" Text="عرض التقرير" ButtonStyle="Primary" IconName="search" OnClick="btnReport_Click"></CustomControls:CButton>
                            <%--  OnClick="btnReport_Click" --%>
                    </div>
                </div>
            </div>
            <div class="sub-section" runat="server" id="dvReport" visible="false">
                <h4 class="sub-section-title"><%= JIC.Base.Resources.Resources.Reports %></h4>
                <div class="row">
                    <div class="col-md-12" style="max-width: 100%; height: 500px; max-height: 1000px; overflow: auto">
                        <Reporting:ReportViewer ID="rptViewer" runat="server" Font-Names="Tahoma" Width="100%" InternalBorderWidth="100%"
                            AsyncRendering="true" DocumentMapCollapsed="false" PageCountMode="Actual" SizeToReportContent="true" Font-Size="8pt"
                            interactivedeviceinfos="(Collection)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
                            ShowFindControls="true" KeepSessionAlive="false" ShowParameterPrompts="false" InteractivityPostBackMode="AlwaysAsynchronous"
                            ZoomPercent="100" ZoomMode="Percent">
                        </Reporting:ReportViewer>
                       
                    </div>
                </div>
            </div>
        </div>

            </ContentTemplate>
        </asp:UpdatePanel>
        <div>
        </div>
    </form>
</body>
</html>

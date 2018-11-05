using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIC.Base;
using JIC.Utilities.Helpers;
using JIC.View;

namespace JIC.Utilities.WebFormHelpers.JICControls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:CDateTimePicker runat=server ValidationGroup= Required=false></{0}:CDateTimePicker>")]
    public class CDateTimePicker : TextBox
    {
        #region Variables

        CRequiredFieldValidator reqValidator;
        CLabel lbl;
        string timeFormat = "HH:mm:ss";
        string bootstrapTimeFormat = "hh:mm:ss";

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value that indicates whether the control properties are loaded or not.
        /// The default is false.
        /// </summary>
        private bool PropertiesLoaded
        {
            get { return ViewState.Get<bool>("PropertiesLoaded", false); }
            set { ViewState.Set<bool>("PropertiesLoaded", value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the control is required to have value or not.
        /// The default is false.
        /// </summary>
        public bool IsRequired
        {
            get { return ViewState.Get<bool>("IsRequired", false); }
            set { ViewState.Set<bool>("IsRequired", value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the control will show a popover text when get focus or not.
        /// The default is false.
        /// </summary>
        public bool ShowPopover
        {
            get { return ViewState.Get<bool>("ShowPopover", false); }
            set { ViewState.Set<bool>("ShowPopover", value); }
        }

        /// <summary>
        /// Gets or sets the location of the popup text.
        /// default value is PopoverDiractions.Top.
        /// </summary>
        public PopoverDiractions PopoverDiraction
        {
            get { return ViewState.Get<PopoverDiractions>("PopoverDiraction", PopoverDiractions.Top); }
            set { ViewState.Set<PopoverDiractions>("PopoverDiraction", value); }
        }

        /// <summary>
        /// Gets or sets the resource key will be used to get the popup text.
        /// </summary>
        public string PopoverTextResourceKey
        {
            get { return ViewState.Get("PopoverTextResourceKey"); }
            set { ViewState.Set<string>("PopoverTextResourceKey", value); }
        }

        /// <summary>
        /// Gets or sets the resource key will be used to get the associated label text.
        /// </summary>
        public string LabelResourceKey
        {
            get { return ViewState.Get("LabelResourceKey"); }
            set { ViewState.Set<string>("LabelResourceKey", value); }
        }

        /// <summary>
        /// Gets or sets the text will be used for the associated label.
        /// </summary>
        public string LabelText
        {
            get { return ViewState.Get("LabelText"); }
            set { ViewState.Set<string>("LabelText", value); }
        }

        /// <summary>
        /// Gets or sets the size of the control.
        /// default value is ControlSizes.Large.
        /// </summary>
        public ControlSizes ControlSize
        {
            get { return ViewState.Get<ControlSizes>("ControlSize", ControlSizes.Large); }
            set { ViewState.Set<ControlSizes>("ControlSize", value); }
        }

        /// <summary>
        /// Gets or sets the calendar start date from Code behind not from HTML
        /// eg: clac.StartDate=DateTime.Now.AddDays(2);
        /// </summary>
        public DateTime? StartDate
        {
            get { return ViewState.Get<DateTime?>("StartDate"); }
            set { ViewState.Set<DateTime?>("StartDate", value); }
        }

        /// <summary>
        /// Gets or sets the selected date.
        /// </summary>
        public DateTime? SelectedDate
        {
            get { return GetSelectedDate(); }
            set { this.Text = value.HasValue ? value.Value.ToString(GetFullFormat()) : ""; }
        }

        /// <summary>
        /// Gets or sets the calendar view mode if days, months or years.
        /// Default is CalendarViewModes.Days.
        /// </summary>
        public CalendarViewModes ViewMode
        {
            get { return (ViewState.Get<CalendarViewModes>("ViewMode", CalendarViewModes.Days)); }
            set { ViewState.Set<CalendarViewModes>("ViewMode", value); }
        }

        /// <summary>
        /// Gets or sets the calendar min view mode if days, months or years.
        /// Default is CalendarViewModes.Days.
        /// </summary>
        public CalendarViewModes MinViewMode
        {
            get { return (ViewState.Get<CalendarViewModes>("MinViewMode", CalendarViewModes.Days)); }
            set { ViewState.Set<CalendarViewModes>("MinViewMode", value); }
        }

        /// <summary>
        /// Gets or sets the date format will be used in the control.
        /// Default is SystemConfigurations.DateTime_ShortDateFormat.
        /// </summary>
        public string DateFormat
        {
            get { return ViewState.Get<string>("DateFormat", SystemConfigurations.DateTime_ShortDateFormat); }
            set { ViewState.Set<string>("DateFormat", value); }
        }

        /// <summary>
        /// Gets or sets the calendar mode if, date, time or datetime
        /// Default is CalendarModes.Date.
        /// </summary>
        public CalendarModes Mode
        {
            get { return (ViewState.Get<CalendarModes>("CalendarMode", CalendarModes.Date)); }
            set { ViewState.Set<CalendarModes>("CalendarMode", value); }
        }

        /// <summary>
        /// Gets or sets the value indecates if script was registered or not.
        /// </summary>
        private bool ScriptRegistered
        {
            get
            {
                // When doing partial postbacks, we need to register the scripts once.

                var scriptManager = ScriptManager.GetCurrent(this.Page);
                if (scriptManager != null && scriptManager.IsInAsyncPostBack)
                    return (ViewState.Get<bool>("ScriptRegistered", false));

                // When doing full postbacks, we register every time.
                return false;
            }
            set
            {
                ViewState.Set<bool>("IsScriptRegister", value);
            }
        }

        public string OnChangeScript
        {
            get { return (ViewState.Get<string>("OnChangeScript", null)); }
            set { ViewState.Set<string>("OnChangeScript", value); }
        }

        #endregion

            #region Methods

        protected override void OnLoad(EventArgs e)
        {
            JIC.Crime.View.WebFormHelpers.Utilities.AddCssToPageHeader(this.Page.Header, "/Layouts/css/datepicker.css");
            JIC.Crime.View.WebFormHelpers.Utilities.AddJsToPageHeader(this.Page.Header, "/Layouts/js/date.js");
            JIC.Crime.View.WebFormHelpers.Utilities.AddJsToPageHeader(this.Page.Header, "/Layouts/js/bootstrap-datepicker.js");

            base.OnLoad(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.LabelResourceKey) || !string.IsNullOrEmpty(this.LabelText))
            {
                this.lbl = Utilities.CreateLabel(this.LabelResourceKey, this.LabelText);
                this.Controls.Add(lbl);
            }

            if (this.IsRequired)
            {
                this.reqValidator = Utilities.CreateRequiredFieldValidator(this.ID, this.ValidationGroup);
                reqValidator.Style.Add("padding", "0px 5px");
                this.Controls.Add(reqValidator);
            }

            if (!this.PropertiesLoaded)
            {
                if (this.ShowPopover)
                    Utilities.AddPopoverAttributes(this.ID, this.Attributes, this.PopoverDiraction, PopoverTriggers.Focus, this.PopoverTextResourceKey);

                this.CssClass = string.Concat(this.CssClass, " ", "form-control");
                this.Attributes.Add("aria-describedby", this.ID + "-addon");

                this.PropertiesLoaded = true;
            }

            base.OnPreRender(e);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            StringBuilder builder = new StringBuilder();

            #region Label

            if (!string.IsNullOrEmpty(this.LabelResourceKey) || !string.IsNullOrEmpty(this.LabelText))
            {
                this.lbl.RenderControl(new HtmlTextWriter(new StringWriter(builder)));
                writer.Write(string.Format("<div class='control-labels'>{0}{1}</div>", builder.ToString(), this.IsRequired ? "<span class='required'>*</span>" : string.Empty));
            }

            #endregion

            #region Calendar

            string _dateFormat = "";
            switch (this.MinViewMode)
            {
                case CalendarViewModes.Years:
                    _dateFormat = this.DateFormat.Replace("dd/", "").Replace("MM/", "");
                    break;
                case CalendarViewModes.Months:
                    _dateFormat = this.DateFormat.Replace("dd/", "");
                    break;
                case CalendarViewModes.Days:
                    _dateFormat = this.DateFormat;
                    break;
            }

            writer.Write(string.Format("<div id='{0}' class='input-group date {1}'>", "dp_" + this.ClientID, this.ControlSize.ToString().ToLower()));

            switch (this.Mode)
            {
                case CalendarModes.Date:
                    this.Attributes.Add("data-date-format", _dateFormat);
                    this.Attributes.Add("placeholder", _dateFormat);
                    break;

                case CalendarModes.Time:
                    this.Attributes.Add("data-date-format", this.bootstrapTimeFormat);
                    this.Attributes.Add("placeholder", this.bootstrapTimeFormat);
                    break;

                case CalendarModes.DateTime:
                    this.Attributes.Add("data-date-format", _dateFormat + " " + this.bootstrapTimeFormat);
                    this.Attributes.Add("placeholder", _dateFormat + " " + this.bootstrapTimeFormat);
                    break;
            }

            #region Validator

            if (this.IsRequired)
            {
                builder.Clear();
                reqValidator.Display = ValidatorDisplay.Dynamic;
                reqValidator.RenderControl(new HtmlTextWriter(new StringWriter(builder)));
            }

            #endregion

            base.Render(writer);
            writer.Write(string.Format("<span class='input-group-addon' id='{0}-addon'><i class='{1}'></i>{2}</span></div>", this.ID, (this.Mode == CalendarModes.Date ? "icon-calendar" : " icon-time"), this.IsRequired ? builder.ToString() : string.Empty));

            #endregion

            #region Script

            if (!this.ScriptRegistered && this.Enabled)
            {
                string startDate = "sdate_" + this.ClientID;
                string localDate = "localdate_" + this.ClientID;
                string script = @"var #sdate=Date.parseExact('_sdate','_format');
                                    $('#id').datetimepicker(
                                                                {
                                                                    format: '#dateformat#timeformat',
                                                                    pickTime:#pickTime,
                                                                    pickDate:#pickDate,
                                                                    viewMode:'#viewMode',
                                                                    minViewMode:'#minViewMode',
                                                                    startDate:#sdate
                                                                });";

                if (OnChangeScript != null)
                    script += @"$('#id').on(""changeDate"",function (e) {" + OnChangeScript + "});";

                if (!this.Page.IsPostBack)
                    script = script + @"var #localdate=#sdate?new Date(#sdate):null;
                                            #localdate = #localdate;
                                            if(#localdate)$('#id').data('datetimepicker').setLocalDate(#localdate);";

                script = script.Replace("#id", "#dp_" + ClientID)
                                .Replace("#viewMode", ViewMode.ToString().ToLower())
                                .Replace("#minViewMode", MinViewMode.ToString().ToLower())
                                .Replace("_sdate", StartDate.HasValue ? StartDate.Value.ToString(GetFullFormat()) : "")
                                .Replace("#sdate", startDate)
                                .Replace("#localdate", localDate)
                                .Replace("_format", GetFullFormat());


                switch (this.Mode)
                {
                    case CalendarModes.Date:
                        script = script.Replace("#dateformat", _dateFormat).Replace("#timeformat", "").Replace("#pickTime", "false").Replace("#pickDate", "true");
                        break;
                    case CalendarModes.Time:
                        script = script.Replace("#dateformat", "").Replace("#timeformat", this.bootstrapTimeFormat).Replace("#pickTime", "true").Replace("#pickDate", "false");
                        break;
                    case CalendarModes.DateTime:
                        script = script.Replace("#dateformat", _dateFormat).Replace("#timeformat", " " + this.bootstrapTimeFormat).Replace("#pickTime", "true").Replace("#pickDate", "true");
                        break;
                }

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), this.ClientID + "-script", script, true);
                this.ScriptRegistered = true;
            }

            #endregion
        }

        private DateTime? GetSelectedDate()
        {
            if (string.IsNullOrEmpty(this.Text))
                return new Nullable<DateTime>();

            DateTime currentDate;

            string fullformat = GetFullFormat();

            if (!DateTime.TryParseExact(this.Text.Trim(), fullformat, System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, out currentDate))
            {
                LogHelper.LogWarning(string.Format("DDateTimePicker.CurrentDate: Could not parse '{0}' as DateTime using format '{1}'",
                    this.Text, DateFormat));
            }

            return currentDate;
        }

        private string GetFullFormat()
        {
            string _dateformat = DateFormat;
            string _timeformat = timeFormat;
            switch (MinViewMode)
            {
                case CalendarViewModes.Years:
                    _dateformat = DateFormat.Replace("dd/", "").Replace("MM/", "");
                    break;
                case CalendarViewModes.Months:
                    _dateformat = DateFormat.Replace("dd/", "");
                    break;
                case CalendarViewModes.Days:
                    _dateformat = DateFormat;
                    break;
            }

            switch (Mode)
            {
                case CalendarModes.Date:
                    _timeformat = "";
                    break;
                case CalendarModes.Time:
                    _timeformat = timeFormat;
                    _dateformat = "";
                    break;
                case CalendarModes.DateTime:
                    _timeformat = " " + timeFormat;
                    break;
                default:
                    break;
            }

            return _dateformat + _timeformat;
        }

        #endregion
    }
}

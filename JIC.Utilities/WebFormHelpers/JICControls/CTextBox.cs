using System;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIC.Base;
using JIC.Base.Resources;
using JIC.View;
using JIC.View.Utilities.JICControls;

namespace JIC.Utilities.WebFormHelpers.JICControls
{
    [ToolboxData("<{0}:CTextBox runat=server PlaceholderResourceKey= LabelResourceKey=></{0}:CTextBox>")]
    public class CTextBox : TextBox
    {
        #region Variables

        public CRequiredFieldValidator reqValidator;
        CRegularExpressionValidator regValidator;
        CRangeValidator rngValidator;
        CCompareValidator cmprValidator;
        CLabel lbl;
        CRadioButton addonRadioButton;


        #region AotuCompelet Variable
        CHiddenField hdjson;
        //CHiddenField hdAutoCompleteConfigurations;
        //CHiddenField hdServiceName;
        //CHiddenField hdLocalStoreg;
        //CHiddenField hdSessionKey;
        //CHiddenField hdIsFilter;
        //CHiddenField hdFilterJson;
        #endregion


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
        /// Gets or sets the resource key will be used to get the control placeholder.
        /// </summary>
        public string PlaceholderResourceKey
        {
            get { return ViewState.Get("PlaceholderResourceKey"); }
            set { ViewState.Set<string>("PlaceholderResourceKey", value); }
        }

        /// <summary>
        /// Gets or sets the data type of the text box.
        /// default value is TextBoxDataTypes.String.
        /// </summary>
        public TextBoxDataTypes DataType
        {
            get { return ViewState.Get<TextBoxDataTypes>("DataType", TextBoxDataTypes.String); }
            set { ViewState.Set<TextBoxDataTypes>("DataType", value); }
        }
        public TextBoxDefaultData DefaultData
        {
            get { return ViewState.Get<TextBoxDefaultData>("DefaultData", TextBoxDefaultData.CurrentYear); }
            set { ViewState.Set<TextBoxDefaultData>("DefaultData", value); }
        }
        /// <summary>
        ///  Gets or sets the text content of the System.Web.UI.WebControls.TextBox control.
        /// </summary>
        public override string Text
        {
            get { return base.Text.Trim(); }
            set { base.Text = value; }
        }

        /// <summary>
        /// Gets or sets the min accepted value for the textbox.
        /// This property will be applied only when the DataType property is not DataType.String.
        /// </summary>
        public string MinValue
        {
            get { return ViewState.Get("MinValue", string.Empty); }
            set { ViewState.Set<string>("MinValue", value); }
        }

        /// <summary>
        /// Gets or sets the max accepted value for the textbox.
        /// This property will be applied only when the DataType property is not DataType.String.
        /// </summary>
        public string MaxValue
        {
            get { return ViewState.Get("MaxValue", string.Empty); }
            set { ViewState.Set<string>("MaxValue", value); }
        }

        /// <summary>
        /// Gets or sets a value that control should be disabled or not.
        /// The default is false.
        /// </summary>
        public bool Disabled
        {
            get { return ViewState.Get<bool>("Disabled", false); }
            set { ViewState.Set<bool>("Disabled", value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the control will render add-on or not.
        /// The default is false.
        /// </summary>
        public bool ShowInAddon
        {
            get { return ViewState.Get<bool>("ShowInAddon", false); }
            set { ViewState.Set<bool>("ShowInAddon", value); }
        }

        /// <summary>
        /// Gets or sets the type of the add-on to show the control in.
        /// </summary>
        public AddonTypes? AddonType
        {
            get { return ViewState.Get<AddonTypes?>("AddonType"); }
            set { ViewState.Set<AddonTypes?>("AddonType", value); }
        }

        /// <summary>
        /// Gets or sets the value of the add-on control checked property.
        /// The default is false.
        /// </summary>
        public bool AddonChecked
        {
            get { return ViewState.Get<bool>("AddonChecked", false); }
            set { ViewState.Set<bool>("AddonChecked", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the add-on state automatically posts back to the server when clicked.
        /// </summary>
        public bool AddonAutoPostBack
        {
            get { return ViewState.Get<bool>("AddonAutoPostBack", false); }
            set { ViewState.Set<bool>("AddonAutoPostBack", value); }
        }

        /// <summary>
        /// Gets or sets the name of the group which the add-on should be added to.
        /// </summary>
        public string AddonGroupName
        {
            get { return ViewState.Get("AddonGroupName", string.Empty); }
            set { ViewState.Set<string>("AddonGroupName", value); }
        }

        /// <summary>
        /// Gets or sets a value if is auto combelet textbox
        /// </summary>


        #region Auto Compelete
        public bool IsAutoCombelete
        {
            get { return ViewState.Get<bool>("AutoCombelete", false); }
            set { ViewState.Set<bool>("AutoCombelete", value); }
        }


        public string ServiceName
        {
            get { return ViewState.Get("ServiceName"); }
            set { ViewState.Set<string>("ServiceName", value); }
        }
        public string SessionKey
        {
            get { return ViewState.Get("SessionKey"); }
            set { ViewState.Set<string>("SessionKey", value); }
        }
        public bool IsInnerFilterType
        {
            get { return ViewState.Get<bool>("IsInnerFilterType", false); }
            set { ViewState.Set<bool>("IsInnerFilterType", value); }
        }
        public string InnerFilterJson
        {
            get { return ViewState.Get("InnerFilterJson"); }
            set { ViewState.Set<string>("InnerFilterJson", value); }
        }
        public string LocalStoregName
        {
            get { return ViewState.Get("LocalStoregName"); }
            set { ViewState.Set<string>("LocalStoregName", value); }
        }
        #endregion

        #endregion

        #region Events

        /// <summary>
        /// Occurs when the value of the add-on control Checked property changes between posts to the server.
        /// </summary>
        public event EventHandler AddonCheckedChanged;

        #endregion

        #region Methods

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            #region Add-on

            if (this.ShowInAddon)
            {
                if (!this.AddonType.HasValue)
                    throw new InvalidOperationException(string.Format("No add-on type assigned to control {0} to render.", this.ID));

                switch (this.AddonType.Value)
                {
                    #region RadioButton

                    case AddonTypes.RadioButton:
                        this.addonRadioButton = Utilities.CreateRadioButtonAddon(this.ID, this.AddonChecked, this.AddonAutoPostBack, this.AddonGroupName);
                        if (this.AddonAutoPostBack)
                        {
                            this.addonRadioButton.CheckedChanged += (s, ea) =>
                            {
                                this.AddonChecked = ((CRadioButton)s).Checked;
                                if (this.AddonCheckedChanged != null)
                                    AddonCheckedChanged(s, ea);
                            };
                            ScriptManager.GetCurrent(this.Page).RegisterAsyncPostBackControl(this.addonRadioButton);
                        }
                        break;

                    #endregion

                    default: throw new InvalidOperationException(string.Format("Add-on type {0] isn't supported in control {1} to render.", AddonType.Value.ToString(), this.ID));
                }
            }

            #endregion
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (this.ShowInAddon)
            {
                switch (this.AddonType.Value)
                {
                    case AddonTypes.RadioButton: this.Controls.Add(addonRadioButton); break;
                }
            }
            if (this.IsAutoCombelete)
            {
                if (string.IsNullOrEmpty(this.ServiceName) || string.IsNullOrEmpty(this.LocalStoregName)||string.IsNullOrEmpty(this.SessionKey) || string.IsNullOrEmpty(this.InnerFilterJson))
                {
                    throw new Exception("Missing prams");
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            #region Label
            if (!string.IsNullOrEmpty(this.LabelResourceKey) || !string.IsNullOrEmpty(this.LabelText))
            {
                this.lbl = Utilities.CreateLabel(this.LabelResourceKey, this.LabelText);
                this.Controls.Add(lbl);
            }
            #endregion

            #region Validations
            if (this.IsRequired)
            {
                this.reqValidator = Utilities.CreateRequiredFieldValidator(this.ID, this.ValidationGroup);
                this.Controls.Add(reqValidator);
            }

            if (this.DataType != TextBoxDataTypes.String)
            {
                string validationExpression = string.Empty;
                switch (this.DataType)
                {
                    case TextBoxDataTypes.Email: validationExpression = @"\w+([-+.]\w+)*@\w+([-.]\w{2,3})+"; break;
                    case TextBoxDataTypes.NationalID:
                        validationExpression = "(2|3)[0-9][0-9][0-1][1-9][0-3][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]";
                        this.MaxLength = 14;
                        break;
                    case TextBoxDataTypes.Year:
                        //validate that the year number is four numbers and starts with 2.
                        validationExpression = @"2\d{3}";
                        this.MaxLength = 4;
                        break;
                    case TextBoxDataTypes.Username:
                        validationExpression = "^[a-zA-Z0-9_]{4,40}$";
                        break;
                    case TextBoxDataTypes.Alphapet:
                        validationExpression = "^[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z_⁠⁠⁠أإءئؤآي]+[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z-_⁠⁠⁠أإءئؤآي]*$";
                        break;
                    case TextBoxDataTypes.Int:
                    case TextBoxDataTypes.Double:
                    case TextBoxDataTypes.Mobile:
                    case TextBoxDataTypes.Phone:
                        validationExpression = "^\\d+$" + (this.DataType == TextBoxDataTypes.Double ? "|^-?\\d*\\.\\d+$" : "");
                        if (this.DataType == TextBoxDataTypes.Mobile)
                        {
                            validationExpression = "^01[0125]{1}[0-9]{7,11}";
                            this.MaxLength = 11;
                        }
                        else if (this.DataType == TextBoxDataTypes.Phone)
                        {
                            validationExpression = "[+-]?[0-9]{5,11}";
                            this.MaxLength = 11;
                        }
                        if (this.MaxLength == 0)
                        {
                            if (this.DataType == TextBoxDataTypes.Double)
                                this.MaxLength = 10;
                            else if (this.DataType == TextBoxDataTypes.Int)
                                this.MaxLength = 7;
 
                        }
                        break;
                }
                this.regValidator = Utilities.CreateRegularExpressionValidator(this.ID, this.ValidationGroup, validationExpression, "InvalidValue");
                this.Controls.Add(regValidator);
            }

            if (!string.IsNullOrEmpty(this.MinValue) && !string.IsNullOrEmpty(this.MaxValue))
            {
                this.rngValidator = Utilities.CreateRangeValidator(this.ID, this.ValidationGroup, this.DataType == TextBoxDataTypes.Int ? ValidationDataType.Integer : ValidationDataType.Double, this.MinValue, this.MaxValue);
                this.Controls.Add(rngValidator);
            }
            else if (!string.IsNullOrEmpty(this.MinValue) && string.IsNullOrEmpty(this.MaxValue))
            {
                this.cmprValidator = Utilities.CreateCompareValidator(this.ID, this.ValidationGroup, this.DataType == TextBoxDataTypes.Int ? ValidationDataType.Integer : ValidationDataType.Double, this.MinValue, ValidationCompareOperator.GreaterThanEqual, "CompareValidatorGreaterThanErrorMessage");
                this.Controls.Add(cmprValidator);
            }
            else if (string.IsNullOrEmpty(this.MinValue) && !string.IsNullOrEmpty(this.MaxValue))
            {
                this.cmprValidator = Utilities.CreateCompareValidator(this.ID, this.ValidationGroup, this.DataType == TextBoxDataTypes.Int ? ValidationDataType.Integer : ValidationDataType.Double, this.MaxValue, ValidationCompareOperator.LessThanEqual, "CompareValidatorLessThanErrorMessage");
                this.Controls.Add(cmprValidator);
            }

            #endregion
            #region AutoCompelete
            if (this.IsAutoCombelete)
            {
                this.hdjson = new CHiddenField();
                this.Controls.Add(hdjson);
            }
            #endregion
            #region Properties

            if (!this.PropertiesLoaded)
            {
                if (this.ShowPopover)
                    Utilities.AddPopoverAttributes(this.ID, this.Attributes, this.PopoverDiraction, PopoverTriggers.Focus, this.PopoverTextResourceKey);

                if (this.IsAutoCombelete)
                    this.CssClass = string.Concat(this.CssClass, "auto");

                if (this.TextMode == TextBoxMode.MultiLine && this.MaxLength > 0)
                    this.Attributes.Add("maxlength", this.MaxLength.ToString());


                if (this.DataType != TextBoxDataTypes.String)

                    switch (this.DataType)
                    {
                        case TextBoxDataTypes.Double:
                            this.Attributes.Add("data-type", this.DataType.ToString().ToLower());
                            break;

                        case TextBoxDataTypes.Int:
                        case TextBoxDataTypes.Mobile:
                        case TextBoxDataTypes.Phone:
                        case TextBoxDataTypes.NationalID:
                            this.Attributes.Add("data-type", TextBoxDataTypes.Int.ToString().ToLower());
                            break;
                        case TextBoxDataTypes.Year:
                            this.Attributes.Add("data-type", TextBoxDataTypes.Int.ToString().ToLower());
                            if (this.DefaultData!=TextBoxDefaultData.empty)
                            {
                                this.Text = DateTime.Now.Year.ToString();
                            }
                          
                            break;
                    }


                if (!string.IsNullOrEmpty(this.PlaceholderResourceKey))
                    this.Attributes.Add("placeholder", Resources.ResourceManager.GetString(this.PlaceholderResourceKey));

                this.CssClass = string.Concat(this.CssClass, " ", this.ControlSize.ToString().ToLower());

                if (this.ShowInAddon)
                    this.CssClass = string.Concat(this.CssClass, " form-control");

                this.PropertiesLoaded = true;
            }

            if (this.Disabled && this.Attributes["disabled"] == null)
                this.Attributes.Add("disabled", "disabled");
            else if (!this.Disabled && this.Attributes["disabled"] != null)
                this.Attributes.Remove("disabled");

            if (this.AddonType.HasValue && this.AddonType.Value == AddonTypes.RadioButton)
                this.addonRadioButton.Checked = this.AddonChecked;

            #endregion

            base.OnPreRender(e);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (IsAutoCombelete)
            {
                var validationSource = JIC.Utilities.Helpers.SessionHelper.Get<bool>(JIC.Utilities.Helpers.SessionHelper.Key.IsLoadPredcyion, SessionKey).ToString();
                writer.Write("<div class='textbox-parent-div'>");
                writer.Write("<input type='hidden' class='class' value='{\"serverValidSource\":\""+validationSource+"\",\"localname\":\""+LocalStoregName+"\",\"ServiceName\":\""+ServiceName+"\",\"isFilterJson\":\""+IsInnerFilterType.ToString()+"\",\"FilterJson\":\""+InnerFilterJson+"\"}'/>");
            }
            if (!this.ShowInAddon)
            {
                if (!string.IsNullOrEmpty(this.LabelResourceKey) || !string.IsNullOrEmpty(this.LabelText))
                {
                    StringBuilder builder = new StringBuilder();
                    this.lbl.RenderControl(new HtmlTextWriter(new StringWriter(builder)));
                    writer.Write(string.Format("<div class='control-labels'>{0}{1}</div>", builder.ToString(), this.IsRequired ? "<span class='required'>*</span>" : string.Empty));
                }

                base.Render(writer);

                if (this.IsRequired)
                    this.reqValidator.RenderControl(writer);

                if (this.DataType != TextBoxDataTypes.String)
                    this.regValidator.RenderControl(writer);

                if (!string.IsNullOrEmpty(this.MinValue) && !string.IsNullOrEmpty(this.MaxValue))
                    this.rngValidator.RenderControl(writer);
                else if (!string.IsNullOrEmpty(this.MinValue) || !string.IsNullOrEmpty(this.MaxValue))
                    this.cmprValidator.RenderControl(writer);
            }
            else
            {
                writer.Write("<div class='input-group'><span class='input-group-addon'>");

                switch (this.AddonType.Value)
                {
                    case AddonTypes.RadioButton: this.addonRadioButton.Enabled = true; this.addonRadioButton.RenderControl(writer); break;
                }

                if (!string.IsNullOrEmpty(this.LabelResourceKey) || !string.IsNullOrEmpty(this.LabelText))
                {
                    this.lbl.RenderControl(writer);
                }

                if (this.IsRequired)
                {
                    writer.Write("<span class='required'>*</span>");
                    this.reqValidator.Display = ValidatorDisplay.Dynamic;
                    this.reqValidator.Style.Add("padding", "0px 5px");
                    this.reqValidator.RenderControl(writer);
                }

                if (this.DataType != TextBoxDataTypes.String)
                {
                    this.regValidator.Display = ValidatorDisplay.Dynamic;
                    this.regValidator.Style.Add("padding", "0px 5px");
                    this.regValidator.RenderControl(writer);
                }

                if (!string.IsNullOrEmpty(this.MinValue) && !string.IsNullOrEmpty(this.MaxValue))
                {
                    this.rngValidator.Display = ValidatorDisplay.Dynamic;
                    this.rngValidator.Style.Add("padding", "0px 5px");
                    this.rngValidator.RenderControl(writer);
                }
                else if (!string.IsNullOrEmpty(this.MinValue) || !string.IsNullOrEmpty(this.MaxValue))
                {
                    this.cmprValidator.Display = ValidatorDisplay.Dynamic;
                    this.cmprValidator.Style.Add("padding", "0px 5px");
                    this.cmprValidator.RenderControl(writer);
                }

                writer.Write("</span>");
                base.Render(writer);
                writer.Write("</div>");
            }
            if (IsAutoCombelete)
                writer.Write("</div>");
        }

        #endregion
    }
}

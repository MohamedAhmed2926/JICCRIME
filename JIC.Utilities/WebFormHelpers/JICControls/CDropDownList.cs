using System;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIC.Base;
using JIC.Base.Resources;
using JIC.Crime.View;

namespace JIC.Utilities.WebFormHelpers.JICControls
{
    public partial class CDropDownList : DropDownList
    {
        #region Variables

        CRequiredFieldValidator reqValidator;
        CLabel lbl;
        CRadioButton addonRadioButton;

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
        /// Gets or sets the error message will be used for the associated required field validator.
        /// </summary>
        public string RequiredErrorMessage
        {
            get { return ViewState.Get("RequiredErrorMessage"); }
            set { ViewState.Set<string>("RequiredErrorMessage", value); }
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
        /// Gets or sets the selected value property mode for the dropdownlist.
        /// This mode indecates if the dropdownlist should throw exception if the value isn't exists in the dropdownlist values or just clear the selection and don't throw exception.
        /// default value is DropDownListSelectedValueModes.ThrowException.
        /// </summary>
        public DropDownListSelectedValueModes SelectedValueMode
        {
            get { return ViewState.Get<DropDownListSelectedValueModes>("SelectedValueMode", DropDownListSelectedValueModes.ThrowException); }
            set { ViewState.Set<DropDownListSelectedValueModes>("SelectedValueMode", value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the drop down list should include a default item or not.
        /// The default is false.
        /// </summary>
        public bool IncludeDefaultItem
        {
            get { return ViewState.Get<bool>("IncludeDefaultItem", false); }
            set { ViewState.Set<bool>("IncludeDefaultItem", value); }
        }

        /// <summary>
        /// Gets the value of the selected item in the list control, or selects the item in the list control that contains the specified value.
        /// </summary>
        public override string SelectedValue
        {
            get { return base.SelectedValue; }
            set
            {
                if (this.Items.Count == 0)
                    this.DataBind();

                if (SelectedValueMode == DropDownListSelectedValueModes.ThrowException)
                {
                    base.SelectedValue = value;
                }
                else if (SelectedValueMode == DropDownListSelectedValueModes.ClearSelection)
                {
                    try
                    {
                        base.SelectedValue = value;
                    }
                    catch
                    {
                        base.SelectedValue = null;
                    }
                }
            }
        }

        ///// <summary>
        ///// Gets or sets the content type that will be used to load the drop down list items.
        ///// This property is for security purpose only to move content types blocking logic from the pages and add this common logic in one place.
        ///// </summary>
        //public Nullable<ContentTypes> ContentType
        //{
        //    get { return ViewState.Get<Nullable<ContentTypes>>("ContentType"); }
        //    set { ViewState.Set<Nullable<ContentTypes>>("ContentType", value); }
        //}

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

        private bool ContainsDefaultItem
        {
            get { return this.Items.FindByText(Resources.Choose) != null; }
        }

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
           // this.ApplyOnLoadFeatures();
        }

        protected override ControlCollection CreateControlCollection()
        {
            return new ControlCollection(this);
        }

        protected override void PerformDataBinding(System.Collections.IEnumerable dataSource)
        {
            base.PerformDataBinding(dataSource);
        }

        protected override void OnDataBound(EventArgs e)
        {
            base.OnDataBound(e);

            if (this.IncludeDefaultItem && !this.ContainsDefaultItem)
            {
                this.Items.Insert(0, new ListItem(Resources.Choose, ""));
                this.SelectedIndex = 0;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (!string.IsNullOrEmpty(this.LabelResourceKey) || !string.IsNullOrEmpty(this.LabelText))
            {
                this.lbl = Utilities.CreateLabel(this.LabelResourceKey, this.LabelText);
                this.Controls.Add(lbl);
            }

            if (this.IsRequired)
            {
                this.reqValidator = string.IsNullOrEmpty(this.RequiredErrorMessage) ? Utilities.CreateRequiredFieldValidator(this.ID, this.ValidationGroup) : Utilities.CreateRequiredFieldValidator(this.ID, this.ValidationGroup, this.RequiredErrorMessage);
                this.Controls.Add(reqValidator);
            }

            if (!this.PropertiesLoaded)
            {
                if (this.ShowPopover)
                    Utilities.AddPopoverAttributes(this.ID, this.Attributes, this.PopoverDiraction, PopoverTriggers.Focus, this.PopoverTextResourceKey);

                this.CssClass = string.Concat(this.CssClass, " ", this.ControlSize.ToString().ToLower());

                this.PropertiesLoaded = true;
            }

            if (this.IncludeDefaultItem && !this.ContainsDefaultItem)
            {
                this.Items.Insert(0, new ListItem(Resources.Choose, ""));
                this.SelectedIndex = 0;
            }

            if (this.Disabled && this.Attributes["disabled"] == null)
                this.Attributes.Add("disabled", "disabled");
            else if (!this.Disabled && this.Attributes["disabled"] != null)
                this.Attributes.Remove("disabled");

            if (this.AddonType.HasValue && this.AddonType.Value == AddonTypes.RadioButton)
                this.addonRadioButton.Checked = this.AddonChecked;

            //this.ApplyOnPreRendarFeatures();
        }

        protected override void Render(HtmlTextWriter writer)
        {
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

                writer.Write("</span>");
                base.Render(writer);
                writer.Write("</div>");
            }
        }

        #endregion
    }

  
}

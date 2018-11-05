using System;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIC.View;

namespace JIC.Utilities.WebFormHelpers.JICControls
{
    [ToolboxData("<{0}:CRadioButtonList runat=server LabelResourceKey=></{0}:CRadioButtonList>")]
    public class CRadioButtonList : RadioButtonList
    {
        #region Variables

        CLabel lbl;
        CCustomValidator cstmValidator;

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

        #endregion

        #region Methods

        protected override void OnPreRender(EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.LabelResourceKey) || !string.IsNullOrEmpty(this.LabelText))
            {
                this.lbl = Utilities.CreateLabel(this.LabelResourceKey, this.LabelText);
                this.Controls.Add(lbl);
            }

            if (this.IsRequired)
            {
                this.cstmValidator = Utilities.CreateCustomValidator(this.ID, this.ValidationGroup, "ValidateRadioButtonList", "RequiredErrorMessage");
                this.cstmValidator.Attributes.Add("data-list-id", this.ClientID);
                this.Controls.Add(cstmValidator);
            }

            if (!this.PropertiesLoaded)
            {
                this.CssClass = "rdio-list";
                this.PropertiesLoaded = true;
            }

            base.OnPreRender(e);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            string lbl = string.Empty, validator = string.Empty;
            StringBuilder builder = new StringBuilder();

            if (!string.IsNullOrEmpty(this.LabelResourceKey) || !string.IsNullOrEmpty(this.LabelText))
            {
                this.lbl.RenderControl(new HtmlTextWriter(new StringWriter(builder)));
                lbl = builder.ToString();
            }

            if (IsRequired)
            {
                builder.Clear();
                this.cstmValidator.RenderControl(new HtmlTextWriter(new StringWriter(builder)));
                validator = builder.ToString();
            }

            writer.Write(string.Format("<div class='control-labels'>{0}{1}{2}</div>", lbl, this.IsRequired ? "<span class='required'>*</span>" : string.Empty, validator));

            base.Render(writer);
        }

        #endregion
    }
}
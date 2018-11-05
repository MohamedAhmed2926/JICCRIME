using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIC.Base;
using JIC.Base.Resources;

namespace JIC.Utilities.WebFormHelpers.JICControls
{
    [ToolboxData("<{0}:CButton runat=server TextResourceKey= IconName=></{0}:CButton>")]
    public partial class CButton : LinkButton
    {
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
        /// Gets or sets the resource key will be used to get the control text.
        /// </summary>
        public string TextResourceKey
        {
            get { return ViewState.Get("TextResourceKey"); }
            set { ViewState.Set<string>("TextResourceKey", value); }
        }

        /// <summary>
        /// Gets or sets the resource key will be used to get the control ToolTip.
        /// </summary>
        public string ToolTipResourceKey
        {
            get { return ViewState.Get("ToolTipResourceKey"); }
            set { ViewState.Set<string>("ToolTipResourceKey", value); }
        }

        /// <summary>
        /// Gets or sets the type of the button.
        /// default value is ButtonTypes.Primary.
        /// </summary>
        public ButtonStyles ButtonStyle
        {
            get { return ViewState.Get<ButtonStyles>("ButtonStyle", ButtonStyles.Primary); }
            set { ViewState.Set<ButtonStyles>("ButtonStyle", value); }
        }

        /// <summary>
        /// Gets or sets the type of the button.
        /// default value is ButtonTypes.Button.
        /// </summary>
        public ButtonTypes ButtonType
        {
            get { return ViewState.Get<ButtonTypes>("ButtonType", ButtonTypes.Button); }
            set { ViewState.Set<ButtonTypes>("ButtonType", value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the icon will be large or normal.
        /// The default is true.
        /// </summary>
        public bool UserLargeIcon
        {
            get { return ViewState.Get<bool>("UserLargeIcon", true); }
            set { ViewState.Set<bool>("UserLargeIcon", value); }
        }

        /// <summary>
        /// Gets or sets the name of the icon to add to the button.
        /// </summary>
        public string IconName
        {
            get { return ViewState.Get("IconName"); }
            set { ViewState.Set<string>("IconName", value); }
        }

        #endregion

        #region Methods

        protected override void Render(HtmlTextWriter writer)
        {
            if (!this.PropertiesLoaded)
            {
                if (string.IsNullOrEmpty(this.TextResourceKey) && string.IsNullOrEmpty(this.IconName))
                    throw new ArgumentNullException(string.Format("Button {0} don't have resource key or icon name.", this.ID));

                if (this.ButtonType == ButtonTypes.Button)
                {
                    this.CssClass = string.Concat(this.CssClass, " btn btn-", this.ButtonStyle.ToString().ToLower());
                }
                string text = !string.IsNullOrEmpty(this.TextResourceKey) ? Resources.ResourceManager.GetString(this.TextResourceKey) : this.Text;

                string iconSizeClass = this.ButtonStyle == ButtonStyles.Badge ? "icon-2x" : UserLargeIcon ? "icon-lg" : string.Empty;

                this.Text = string.Format("<i class='icon-{0} {1} {2}'></i>{3}", this.IconName, iconSizeClass, !string.IsNullOrEmpty(text) ? "icon" : "", text);

                if (!string.IsNullOrEmpty(this.ToolTipResourceKey))
                    this.ToolTip = Resources.ResourceManager.GetString(this.ToolTipResourceKey);

                this.PropertiesLoaded = true;
            }
            base.Render(writer);
        }

        #endregion
    }
}

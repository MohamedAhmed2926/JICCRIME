using System;
using System.Web.UI.WebControls;
using JIC.Base;

namespace JIC.Utilities.WebFormHelpers.JICControls
{
    public class CRequiredFieldValidator : RequiredFieldValidator
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
        /// Gets or sets the resource key will be used to get the validator error message.
        /// Default is "RequiredErrorMessage".
        /// </summary>
        public string ErrorMessageResourceKey
        {
            get { return ViewState.Get<string>("ErrorMessageResourceKey", "RequiredErrorMessage"); }
            set { ViewState.Set<string>("ErrorMessageResourceKey", value); }
        }

        #endregion

        #region Methods

        protected override void OnPreRender(EventArgs e)
        {
            if (!this.PropertiesLoaded)
            {
                Utilities.AddPopoverAttributes(this.ID, this.Attributes, PopoverDiractions.Left, PopoverTriggers.Hover, this.ErrorMessageResourceKey, ErrorMessage);
                this.SetFocusOnError = true;
                this.CssClass = "icon-info-circle validator";
                this.Display = ValidatorDisplay.Static;
                this.PropertiesLoaded = true;
            }

            base.OnPreRender(e);
        }

        #endregion
    }
}

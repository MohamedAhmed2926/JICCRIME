using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIC.Base.Resources;
using JIC.View;

namespace JIC.Utilities.WebFormHelpers.JICControls
{
    [ToolboxData("<{0}:CLabel runat=server TextResourceKey=></{0}:CLabel>")]
    public class CLabel : Label
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
        
        #endregion

        #region Methods

        protected override void OnPreRender(EventArgs e)
        {
          
                if (!this.PropertiesLoaded)
                {
                    if (!string.IsNullOrEmpty(this.TextResourceKey))
                        this.Text = Resources.ResourceManager.GetString(this.TextResourceKey);

                    this.CssClass = "label";


                    PropertiesLoaded = true;
                }

                base.OnPreRender(e);
           
        }

       

        #endregion
    }
}

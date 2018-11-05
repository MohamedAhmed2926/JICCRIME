using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIC.Base.Resources;
using JIC.View;
namespace JIC.Utilities.WebFormHelpers.JICControls
{
    [ToolboxData("<{0}:CLabel runat=server TextResourceKey=></{0}:CLabel>")]
    public class CLabelInfo : Label
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
        public string HeaderText
        {
            get { return ViewState.Get("HeaderText"); }
            set { ViewState.Set<string>("HeaderText", value); }
        }
        #endregion

        #region Methods

      

        protected override void Render(HtmlTextWriter writer)
        {

            if (!string.IsNullOrEmpty(this.TextResourceKey))
                this.Text = Resources.ResourceManager.GetString(this.TextResourceKey);
            //this.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#003366");
            //this.Style.Add(HtmlTextWriterStyle.Color, "White");
            writer.Write(string.Format("<div class={0}>", "'panel panel-info'"));
            writer.Write(string.Format("<div class={0} style={1}>{2}</div>",
                "'panel-heading'", "'background-color: #003366; color: white;'", this.HeaderText));
            writer.Write(string.Format("<div class={0}> {1}  </div>", "'panel-body'", this.Text));
            writer.Write(string.Format("</div>"));
            
        }
    }

    #endregion
}

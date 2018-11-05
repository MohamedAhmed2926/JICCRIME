using System;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIC.View;

namespace JIC.Utilities.WebFormHelpers.JICControls
{
    [ToolboxData("<{0}:CCheckBox runat=server TextResourceKey=></{0}:CCheckBox>")]
    public class CCheckBox : CheckBox
    {
        #region Variables

        CLabel lbl;

        #endregion

        #region Properties

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

            base.OnPreRender(e);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);

            if (!string.IsNullOrEmpty(this.LabelResourceKey) || !string.IsNullOrEmpty(this.LabelText))
            {
                StringBuilder builder = new StringBuilder();
                this.lbl.RenderControl(new HtmlTextWriter(new StringWriter(builder)));
                writer.Write(builder.ToString());
            }
        }

        #endregion
    }
}
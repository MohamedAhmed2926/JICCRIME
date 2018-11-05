using System.Web.UI;
using System.Web.UI.WebControls;
using JIC.Base.Resources;
using JIC.View;

namespace JIC.Utilities.WebFormHelpers.JICControls
{
    public class CModalPopup : Panel, INamingContainer
    {
        #region Properties

        /// <summary>
        /// Gets or sets the diaog title resource key.
        /// </summary>
        public string TitleRecourseKey
        {
            get { return this.ViewState.Get("TitleRecourseKey"); }
            set { this.ViewState.Set<string>("TitleRecourseKey", value); }
        }

        /// <summary>
        /// Gets or sets the diaog title text.
        /// </summary>
        public string TitleText
        {
            get { return this.ViewState.Get("TitleText"); }
            set { this.ViewState.Set<string>("TitleText", value); }
        }

        /// <summary>
        /// Gets or sets the diaog icon name.
        /// </summary>
        public string IconName
        {
            get { return this.ViewState.Get("IconName"); }
            set { this.ViewState.Set<string>("IconName", value); }
        
        }

        /// <summary>
        /// Gets the id of the modal dialog div.
        /// </summary>
        public string ModalID
        {
            get { return this.ClientID + "-modal"; }
        }

        #endregion

        #region Methods

        protected override void RenderContents(System.Web.UI.HtmlTextWriter writer)
        {
            string ModalHeader =
            @"<div class='modal fade' id='{0}' tabindex='-1' role='dialog' aria-labelledby='modal-{1}-label' aria-hidden='true' data-backdrop='static' data-keyboard='true'>
                <div class='modal-dialog modal-lg'>
                    <div class='modal-content'>
                        <div class='modal-header'>
                            <button type='button' class='close' data-dismiss='modal' aria-label='Close'><span aria-hidden='true'>&times;</span></button>
                            <h4 class='modal-title' id='modal-{1}-label'><i class='icon-{2} icon-lg'></i>{3}</h4>
                        </div>
                        <div class='modal-body'>";

            string ModalFooter = string.Concat(
                         @"</div>
                    </div>
                </div>
            </div>");
            writer.Write(string.Format(ModalHeader, this.ModalID, this.ClientID, this.IconName, !string.IsNullOrEmpty(this.TitleText) ? this.TitleText : Resources.ResourceManager.GetString(this.TitleRecourseKey)));
            base.RenderContents(writer);
            writer.Write(ModalFooter);
        }
       
        #endregion
    }
}
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIC.Base.Resources;

namespace JIC.Utilities.WebFormHelpers.JICControls
{
    public class CGridViewFilterPanel : Panel, INamingContainer
    {
        #region Variables

        CButton btnHiddenFilter;

        #endregion

        #region Events

        /// <summary>
        /// Occurs when the "Filter" or "Reset" button are clicked;
        /// </summary>
        public event EventHandler FilterApplied;

        #endregion

        #region Methods

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            btnHiddenFilter = new CButton { ID = this.ID + "_btnHiddenFilter", CausesValidation = false, IconName = "filter", };
            btnHiddenFilter.Style.Add("display", "none");
            btnHiddenFilter.Click += (s, ea) =>
            {
                if (FilterApplied != null)
                    FilterApplied(s, ea);
            };
            this.Controls.Add(btnHiddenFilter);
        }

        protected override void RenderContents(System.Web.UI.HtmlTextWriter writer)
        {

            string ModalHeader =
@"<div class='modal fade' id='{0}-modal' tabindex='-1' role='dialog' aria-labelledby='modal-{0}-label' aria-hidden='true' data-backdrop='static' data-keyboard='true'>
                <div class='modal-dialog modal-lg'>
                    <div class='modal-content'>
                        <div class='modal-header'>
                            <button type='button' class='close' data-dismiss='modal' aria-label='Close'><span aria-hidden='true'>&times;</span></button>
                            <h4 class='modal-title' id='modal-{0}-label'><i class='icon-filter icon-lg'></i>{1}</h4>
                        </div>
                        <div class='modal-body' id='dv{0}'>";

            string ModalFooter = string.Concat(
                         @"</div>
                    <div class='modal-footer'>",
                            "<a class='btn btn-alt' data-action='clear' data-parent='dv{3}'><i class='icon-undo icon-lg icon'></i>{2}</a>",
                            "<a class='btn btn-primary' data-dismiss='modal' onclick=\"document.getElementById('{0}').click();\"><i class='icon-check icon-lg icon'></i>{1}</a>",
                        @"</div>
                    </div>
                </div>
            </div>");
            writer.Write(string.Format(ModalHeader, this.ID, Resources.Filter));
            base.RenderContents(writer);
            writer.Write(string.Format(ModalFooter, this.btnHiddenFilter.ClientID, Resources.Apply, Resources.Reset, this.ID));
        }

        #endregion
    }
}
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIC.Base.Resources;

namespace JIC.Utilities.WebFormHelpers.JICControls
{
    [ToolboxData("<{0}:CModalDialog runat=server></{0}:CModalDialog>")]
    public class CModalDialog : WebControl, INamingContainer
    {
        #region Variables

        CButton btnConfirm;
        CHiddenField hfArgumentData;

        #endregion

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
        /// Gets or sets the dialog message resource key.
        /// </summary>
        public string MessageRecourseKey
        {
            get { return this.ViewState.Get("MessageRecourseKey"); }
            set { this.ViewState.Set<string>("MessageRecourseKey", value); }
        }

        /// <summary>
        /// Gets or sets the diaog message text.
        /// </summary>
        public string MessageText
        {
            get { return this.ViewState.Get("MessageText"); }
            set { this.ViewState.Set<string>("MessageText", value); }
        }

        /// <summary>
        /// Gets or sets the confirm button resource key.
        /// </summary>
        public string ConfirmButtonResourceKey
        {
            get { return this.ViewState.Get("ConfirmButtonResourceKey"); }
            set { this.ViewState.Set<string>("ConfirmButtonResourceKey", value); }
        }

        /// <summary>
        /// Gets or sets the cancel button resource key.
        /// </summary>
        public string CancelButtonResourceKey
        {
            get { return this.ViewState.Get("CancelButtonResourceKey"); }
            set { this.ViewState.Set<string>("CancelButtonResourceKey", value); }
        }

        /// <summary>
        /// Gets or set the value which indicates if the cancelling by keyboard (pressing Esc) feature is enabled or disapled.
        /// Default is true.
        /// </summary>
        public bool EnableKeyboardClose
        {
            get { return this.ViewState.Get<bool>("EnableKeyboardClose", true); }
            set { this.ViewState.Set<bool>("EnableKeyboardClose", value); }
        }

        public string DialogID { get { return this.ClientID + "-dialog"; } }

        private string LabelID { get { return this.ClientID + "-label"; } }

        /// <summary>
        /// Gets or sets a value that indicates whether the control should be hidden if the period is closed.
        /// The default is false.
        /// </summary>
        public bool HideWhenPeriodIsClosed
        {
            get { return ViewState.Get<bool>("HideWhenPeriodIsClosed", false); }
            set { ViewState.Set<bool>("HideWhenPeriodIsClosed", value); }
        }


        #endregion

        #region Events

        /// <summary>
        /// This event is fired when the confirmation button is clicked.
        /// </summary>
        public event CommandEventHandler Confirm;

        #endregion

        #region Methods

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.btnConfirm = new CButton
            {
                IconName = "check",
                Text = string.IsNullOrEmpty(this.ConfirmButtonResourceKey) ? Resources.Accept : Resources.ResourceManager.GetString(this.ConfirmButtonResourceKey),
                CausesValidation = false,
            };

            btnConfirm.Command += (s, ea) =>
            {
                if (this.Confirm != null)
                    this.Confirm(s, new CommandEventArgs(string.Empty, hfArgumentData.Value));
                hfArgumentData.Value = "";
            };

            this.Controls.Add(this.btnConfirm);

            hfArgumentData = new CHiddenField
            {
                ClientIDMode = System.Web.UI.ClientIDMode.Static,
                ID = ClientID + "_hfArgumentData"
            };
            Controls.Add(hfArgumentData);
            this.RegisterScripts();
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            string Title = string.IsNullOrEmpty(this.TitleRecourseKey) ? Resources.Confirm : Resources.ResourceManager.GetString(this.TitleRecourseKey);
            string Message = "";
            if (!string.IsNullOrEmpty(this.MessageRecourseKey))
                Message = Resources.ResourceManager.GetString(this.MessageRecourseKey);
            else if (!string.IsNullOrEmpty(this.MessageText))
                Message = this.MessageText;
            else
                Message = Messages.ConfirmMessage;

            string CancelButtonLabel = string.IsNullOrEmpty(CancelButtonResourceKey) ? Resources.Cancel : Resources.ResourceManager.GetString(this.CancelButtonResourceKey);
            string ModalHeader = @"
                    <div id='{0}' class='modal fade' tabindex='-1' role='dialog' aria-labelledby='#{1}' aria-hidden='true'
                        data-backdrop='static' data-keyboard='{2}'>
                        <div class='modal-dialog'>
                            <div class='modal-content'>
                                <div class='modal-header'>
                                    <a href='' class='close' data-dismiss='modal' aria-hidden='true'>×</a>
                                    <h4 class='modal-title' id='{1}'><i class='icon-warning icon-lg'></i>{3}</h4>
                                </div>
                                <div class='modal-body'>
                                {4}
                                </div>
                                <div class='modal-footer'>
                                    <a href='' class='btn btn-alt' data-dismiss='modal' data-target='#{0}' aria-hidden='true'><i class='icon-close icon-lg icon'></i>{5}</a>";
            string footerTemplate = @"
                                </div>
                            </div>
                        </div>
                    </div>";

            writer.Write(string.Format(ModalHeader, this.DialogID, this.LabelID, this.EnableKeyboardClose, Title, Message, CancelButtonLabel));
            base.RenderContents(writer);
            writer.Write(footerTemplate);
        }

        private void RegisterScripts()
        {
            // 0: Confirm button ID
            // 1: Dialog ID
            // 2: Hidden field id

            var scriptTemplate = string.Concat(@"
                Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function (sender, args) {{
                    if (args.get_postBackElement() != null && args.get_postBackElement().id == '{0}') {{
                        $('#{1}').modal('hide');
                        $('#{2}').val('');
                    }}
                }});");

            var script = string.Format(scriptTemplate,
                this.btnConfirm.ClientID,
                this.DialogID,
                hfArgumentData.ClientID
                );
            ScriptManager.RegisterStartupScript(this, this.ViewState.GetType(), this.ID, script, addScriptTags: true);

        }

        #endregion
    }
}

using System;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIC.View;

namespace JIC.Utilities.WebFormHelpers.JICControls
{
    [ToolboxData("<{0}:CCheckBoxList runat=server LabelResourceKey=></{0}:CCheckBoxList>")]
    public class CCheckBoxList : CheckBoxList
    {
        #region Variables

        CLabel lbl;
        CCheckBox chkCheckAll;
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
        /// Gets or sets a value that indicates whether the control has checkbox check all.
        /// The default is false.
        /// </summary>
        public bool EnableCheckAll
        {
            get { return ViewState.Get<bool>("EnableCheckAll", false); }
            set { ViewState.Set<bool>("EnableCheckAll", value); }
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
        /// Gets or sets the content type that will be used to load the control list items.
        /// This property is for security purpose only to move content types blocking logic from the pages.
        /// </summary>
        //public Nullable<ContentTypes> ContentType
        //{
        //    get { return ViewState.Get<Nullable<ContentTypes>>("ContentType"); }
        //    set { ViewState.Set<Nullable<ContentTypes>>("ContentType", value); }
        //}

        #endregion

        #region Methods

        protected override void OnPreRender(EventArgs e)
        {
            if (this.EnableCheckAll)
            {
                this.chkCheckAll = new CCheckBox();
                this.CssClass = "checkall";
                this.chkCheckAll.Attributes.Add("data-action", "check-all");
                this.chkCheckAll.Attributes.Add("data-list-id", this.ClientID);
            }

            if (!string.IsNullOrEmpty(this.LabelResourceKey) || !string.IsNullOrEmpty(this.LabelText))
            {
                this.lbl = Utilities.CreateLabel(this.LabelResourceKey, this.LabelText);
                this.Controls.Add(lbl);
            }

            if (this.IsRequired)
            {
                this.cstmValidator = Utilities.CreateCustomValidator(this.ID, this.ValidationGroup, "ValidateCheckBoxList", "RequiredErrorMessage");
                this.cstmValidator.Attributes.Add("data-list-id", this.ClientID);
                this.Controls.Add(cstmValidator);
            }

            if (!this.PropertiesLoaded)
            {
                RepeatDirection = System.Web.UI.WebControls.RepeatDirection.Vertical;
                this.Width = Unit.Parse("100%");
                this.PropertiesLoaded = true;
            }
            // Apply security filtration on the drop down list based on the list content type.
            //if (this.ContentType.HasValue && SessionHelper.UserPermissionList != null)
            //{
            //    switch (this.ContentType.Value)
            //    {
            //        case ContentTypes.Country: this.Items.Cast<ListItem>().Where(item => !string.IsNullOrEmpty(item.Value) && !SessionHelper.UserPermissionList.Any(permission => permission.CountryID == int.Parse(item.Value))).ToList().ForEach(this.Items.Remove); break;
            //        case ContentTypes.Company: this.Items.Cast<ListItem>().Where(item => !string.IsNullOrEmpty(item.Value) && !SessionHelper.UserPermissionList.Any(permission => permission.CompanyID == int.Parse(item.Value))).ToList().ForEach(this.Items.Remove); break;
            //        case ContentTypes.Report: this.Items.Cast<ListItem>().Where(item => !string.IsNullOrEmpty(item.Value) && !SessionHelper.UserPermissionList.Any(permission => permission.PermissionID == int.Parse(item.Value))).ToList().ForEach(this.Items.Remove); break;
            //    }
            //}
            base.OnPreRender(e);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            string lbl = string.Empty, validator = string.Empty, checkall = string.Empty;
            StringBuilder builder = new StringBuilder();
            if (this.EnableCheckAll)
            {
                this.chkCheckAll.RenderControl(new HtmlTextWriter(new StringWriter(builder)));
                checkall = builder.ToString();
            }

            if (!string.IsNullOrEmpty(this.LabelResourceKey) || !string.IsNullOrEmpty(this.LabelText))
            {
                builder.Clear();
                this.lbl.RenderControl(new HtmlTextWriter(new StringWriter(builder)));
                lbl = builder.ToString();
            }

            if (IsRequired)
            {
                builder.Clear();
                this.cstmValidator.RenderControl(new HtmlTextWriter(new StringWriter(builder)));
                validator = builder.ToString();
            }


            writer.Write(string.Format("<div class='control-labels'>{0}{1}{2}{3}</div><div class='chck-list'>", checkall, lbl, this.IsRequired ? "<span class='required'>*</span>" : string.Empty, validator));

            base.Render(writer);

            writer.Write("</div>");
        }

        #endregion
    }
}
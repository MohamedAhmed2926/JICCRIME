using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIC.Base;
using JIC.Base.Resources;
using JIC.Crime.View;
using JIC.Crime.View.WebFormHelpers;
using System.Collections.Generic;

namespace JIC.Utilities.WebFormHelpers.JICControls
{
    [ToolboxData("<{0}:CGridView runat=server AllowPaging=true AllowSorting=true SelectMethod= ItemType= ></{0}:CGridView>")]
    public class CGridView : GridView
    {
        #region Variables

        CButton btnHiddenExportAsExcel;
        CDropDownList ddlPageSize;
        string ContainerWidth;

        #endregion

        #region Events

        /// <summary>
        /// Occurs when the "AddItem" button is clicked;
        /// </summary>
        public event EventHandler AddingEntity;

        /// <summary>
        /// Occurs when the "ExportAsExcel" button is clicked;
        /// </summary>
        public event EventHandler ExportingAsExcel;

        /// <summary>
        /// Occurs when the "Reload" button is clicked;
        /// </summary>
        public event EventHandler Reloading;

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
        /// Gets or sets a value indicating whether the add button is displayed in the grid view.
        /// The default is false.   AddUserButton
        /// </summary>
        public bool ShowAddButton
        {
            get { return (ViewState.Get<bool>("ShowAddButton", false)); }
            set { ViewState.Set<bool>("ShowAddButton", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the export as excel button is displayed in the grid view.
        /// The default is false.
        /// </summary>
        public bool ShowExportAsExcelButton
        {
            get { return (ViewState.Get<bool>("ShowExportAsExcelButton", false)); }
            set { ViewState.Set<bool>("ShowExportAsExcelButton", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Reload button is displayed in the grid view.
        /// The default is false.
        /// </summary>
        public bool ShowReloaCButton
        {
            get { return (ViewState.Get<bool>("ShowReloaCButton", false)); }
            set { ViewState.Set<bool>("ShowReloaCButton", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Filter button is displayed in the grid view.
        /// The default is false.
        /// </summary>
        public bool ShowFilterButton
        {
            get { return (ViewState.Get<bool>("ShowFilterButton", false)); }
            set { ViewState.Set<bool>("ShowFilterButton", value); }
        }

        /// <summary>
        /// Gets or sets the id of the panel which contains the filter controls.
        /// The default is string.Empty.
        /// </summary>
        public string FilterPanelID
        {
            get { return (ViewState.Get("FilterPanelID", string.Empty)); }
            set { ViewState.Set<string>("FilterPanelID", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the header is displayed in the grid view.
        /// Hiding the header will also hide the grid buttons.
        /// The default is true.
        /// </summary>
        public bool ShowGridHeader
        {
            get { return (ViewState.Get<bool>("ShowGridHeader", true)); }
            set { ViewState.Set<bool>("ShowGridHeader", value); }
        }

        /// <summary>
        /// Gets or sets the value of the resource key will be used to get the grid title.
        /// The default is string.Empty.
        /// </summary>
        public string GridTitleResourceKey
        {
            get { return (ViewState.Get("GridTitleResourceKey", string.Empty)); }
            set { ViewState.Set<string>("GridTitleResourceKey", value); }
        }

        /// <summary>
        /// Gets or sets the value of the text will be used to as the grid title.
        /// The default is string.Empty.
        /// </summary>
        public string GridTitleText
        {
            get { return (ViewState.Get("GridTitleText", string.Empty)); }
            set { ViewState.Set<string>("GridTitleText", value); }
        }

        /// <summary>
        /// Gets or sets the width of the grid view control.
        /// </summary>
        public override Unit Width
        {
            set { base.Width = Unit.Percentage(100); ContainerWidth = value.ToString(); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the page size drop down list is displayed in the grid view.
        /// This property won't work if ShowGridHeader is false.
        /// The default is true.
        /// </summary>
        public bool ShowPageSize
        {
            get { return (ViewState.Get<bool>("ShowPageSize", true)); }
            set { ViewState.Set<bool>("ShowPageSize", value); }
        }

        public int PageSizeFrom
        {
            get { return (ViewState.Get<int>("PageSizeFrom", 500)); }
            set { ViewState.Set<int>("PageSizeFrom", value); }
        }

        /// <summary>
        /// The page size maximum value in page size drop down list.
        /// The default is 2000.
        /// </summary>
        public int PageSizeTo
        {
            get { return (ViewState.Get<int>("PageSizeTo", 2000)); }
            set { ViewState.Set<int>("PageSizeTo", value); }
        }

        /// <summary>
        /// The page size which will be used to increment the "PageSizeFrom" value and fill the page size drop down list.
        /// The default is 500.
        /// </summary>
        public int PageSizeStep
        {
            get { return (ViewState.Get<int>("PageSizeStep", 500)); }
            set { ViewState.Set<int>("PageSizeStep", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the grid view should use the default css class or not.
        /// The default is true.
        /// </summary>
        public bool UseDefaultCSS
        {
            get { return (ViewState.Get<bool>("UseDefaultCSS", true)); }
            set { ViewState.Set<bool>("UseDefaultCSS", value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the control header should be visible while scrolling down the page or not.
        /// The default is false.
        /// </summary>
        public bool FreezeHeader
        {
            get { return (ViewState.Get<bool>("FreezeHeader", false)); }
            set { ViewState.Set<bool>("FreezeHeader", value); }
        }

        /// <summary>
        /// The width will be assigned to the scrollable area.
        /// The default is 2000.
        /// </summary>
        public int FreezeWidth
        {
            get { return (ViewState.Get<int>("FreezeWidth", 2000)); }
            set { ViewState.Set<int>("FreezeWidth", value); }
        }

        public bool AutoWidth
        {
            get { return (ViewState.Get<bool>("AutoWidth", false)); }
            set { ViewState.Set<bool>("AutoWidth", value); }

        }

        /// <summary>
        /// The height will be assigned to the scrollable area.
        /// The default is 300.
        /// </summary>
        public int FreezeHeight
        {
            get { return (ViewState.Get<int>("FreezeHeight", 300)); }
            set { ViewState.Set<int>("FreezeHeight", value); }
        }

        /// <summary>
        /// Contains list of the popups that the grid view will show in the tools area.
        /// </summary>
        public List<GridViewPopupProperties> Popups
        {
            get { return ViewState.Get<List<GridViewPopupProperties>>("Popups", new List<GridViewPopupProperties>()); }
            set { ViewState.Set<List<GridViewPopupProperties>>("Popups", value); }
        }

        /// <summary>
        /// Gets or sets the value of the resource key will be used to get the grid title.
        /// The default is string.Empty.
        /// </summary>
        public string ColumnsHeaderResourceKeys
        {
            get { return (ViewState.Get("ColumnsHeaderResourceKeys", string.Empty)); }
            set { ViewState.Set<string>("ColumnsHeaderResourceKeys", value); }
        }

        #endregion

        #region Methods

        public CGridView()
        {
            // override the default value for PageSize property .
            base.PageSize = 500;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            EmptyDataRowStyle.CssClass = "empty-data";
            this.EmptyDataText = Messages.NoDataFound;


            if (ShowExportAsExcelButton)
            {
                btnHiddenExportAsExcel = new CButton { ID = this.ClientID + "_btnHiddenExportAsExcel", CausesValidation = false, IconName = "file-excel-o" };
                btnHiddenExportAsExcel.Style.Add("display", "none");
                btnHiddenExportAsExcel.Click += (s, ea) =>
                {
                    if (ExportingAsExcel != null)
                        ExportingAsExcel(s, ea);
                };
                ScriptManager.GetCurrent(Page).RegisterPostBackControl(btnHiddenExportAsExcel);
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            #region Default Properties

            if (!this.PropertiesLoaded)
            {
                if (UseDefaultCSS)
                    CssClass = "table grid-view";

                if (string.IsNullOrEmpty(PagerStyle.CssClass))
                    PagerStyle.CssClass = "pagination";

                PagerStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;

                GridLines = System.Web.UI.WebControls.GridLines.None;

                PropertiesLoaded = true;
            }

            #endregion

            if (ShowExportAsExcelButton)
                Controls.Add(btnHiddenExportAsExcel);

            if (!string.IsNullOrEmpty(ColumnsHeaderResourceKeys))
            {
                string[] HeadersResourceKeys = ColumnsHeaderResourceKeys.Split(',');
                if (HeadersResourceKeys.Length != this.Columns.Count)
                    throw new ArgumentException("The ColumnsHeaderResourceKeys strings doesn't match the grid columns count");

                for (int i = 0; i < HeadersResourceKeys.Length; i++)
                {
                    this.Columns[i].HeaderText = Resources.ResourceManager.GetString(HeadersResourceKeys[i]);
                    this.Columns[i].HeaderStyle.CssClass = this.Columns[i].HeaderStyle.CssClass.Replace(" sorted-asc", "").Replace(" sorted-desc", "");
                    if (!string.IsNullOrEmpty(this.SortExpression) && this.Columns[i].SortExpression == this.SortExpression)
                        this.Columns[i].HeaderStyle.CssClass += this.SortDirection == System.Web.UI.WebControls.SortDirection.Ascending ? " sorted-asc" : " sorted-desc";
                }
            }

            base.OnPreRender(e);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (this.AllowPaging && this.ShowPageSize && !this.ShowGridHeader)
                throw new InvalidOperationException(string.Format("ShowPageSize for grid {0} requires ShowGridHeader to be true.", this.ID));

            System.Text.StringBuilder builder = new System.Text.StringBuilder();

            string btnAddOutput = string.Empty, btnReload = string.Empty, btnFilter = string.Empty, btnExcel = string.Empty, Tools = string.Empty, ddlPageSizeOutput = string.Empty, popupButtons = string.Empty;

            #region Add Button

            if (ShowAddButton)
            {
                builder.Clear();
                CButton btnAdd = new CButton { ClientIDMode = System.Web.UI.ClientIDMode.Static, ID = this.ClientID + "_btnAdd", Text = Resources.NewItem, IconName = "plus-square-o", OnClientClick = this.Page.ClientScript.GetPostBackEventReference(this, "New") + " ; return false;" };
                btnAdd.RenderControl(new HtmlTextWriter(new StringWriter(builder)));
                btnAddOutput = builder.ToString();
            }

            #endregion

            #region Excel Button

            if (ShowExportAsExcelButton)
            {
                builder.Clear();
                CButton btn = new CButton { ClientIDMode = System.Web.UI.ClientIDMode.Static, ID = this.ClientID + "_btnExcel", Text = Resources.ExportAsExcel, IconName = "file-excel-o" };
                btn.RenderControl(new HtmlTextWriter(new StringWriter(builder)));
                btnExcel = builder.ToString();
                string script = (";$(function(){ $(document.body).on('click','#btnExcel',function(){$('#btnExportAsExcel').click();return false;}); });").Replace("btnExportAsExcel", btn.ClientID).Replace("btnHiddenExportAsExcel", btnHiddenExportAsExcel.ClientID);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), this.ClientID + "_script", script, true);
            }

            #endregion

            #region Reload Button

            if (ShowReloaCButton)
            {
                builder.Clear();
                CButton btn = new CButton { ClientIDMode = System.Web.UI.ClientIDMode.Static, ID = this.ClientID + "_btnReload", Text = Resources.Reload, IconName = "refresh", OnClientClick = this.Page.ClientScript.GetPostBackEventReference(this, "Reload") + " ; return false;" };
                btn.RenderControl(new HtmlTextWriter(new StringWriter(builder)));
                btnReload = builder.ToString();
            }

            #endregion

            #region Filter Button

            if (ShowFilterButton)
            {
                builder.Clear();
                CButton btn = new CButton { ClientIDMode = System.Web.UI.ClientIDMode.Static, ID = this.ClientID + "_btnFilter", Text = Resources.Filter, IconName = "filter", OnClientClick = "return false;" };
                btn.Attributes.Add("data-toggle", "modal");
                btn.Attributes.Add("data-target", "#" + this.FilterPanelID + "-modal");
                btn.RenderControl(new HtmlTextWriter(new StringWriter(builder)));
                btnFilter = builder.ToString();
            }

            #endregion

            #region Popups

            if (Popups.Count > 0)
            {
                builder.Clear();
                for (int i = 0; i < Popups.Count; i++)
                {
                    CButton btn = new CButton { ClientIDMode = System.Web.UI.ClientIDMode.Static, ID = this.ClientID + "_popup" + i.ToString(), Text = Popups[i].ButtonText, IconName = Popups[i].ButtonIcon, OnClientClick = "return false;" };
                    btn.Attributes.Add("data-toggle", "modal");
                    btn.Attributes.Add("data-target", "#" + Popups[i].PopupID);
                    btn.RenderControl(new HtmlTextWriter(new StringWriter(builder)));
                }
                popupButtons = builder.ToString();
            }

            #endregion

            #region Page Size

            if (AllowPaging && ShowPageSize)
            {
                ddlPageSize = new CDropDownList { SelectedValueMode = DropDownListSelectedValueModes.ClearSelection, ID = this.ClientID + "_ddlPageSize", CausesValidation = false, AutoPostBack = true, ToolTip = Resources.ItemsCount };

                for (int i = PageSizeFrom; i <= PageSizeTo; i += PageSizeStep)
                {
                    ddlPageSize.Items.Add(new ListItem { Text = i.ToString(), Value = i.ToString(), Selected = i == PageSize });
                }
                if (ddlPageSize.Items.Count > 0)
                {
                    builder.Clear();

                    string postBackScript = this.Page.ClientScript.GetPostBackEventReference(this, "$$", false) + " ; return false;";
                    postBackScript = postBackScript.Replace("'$$'", "'PageSize$'+document.getElementById('##').value")
                                                   .Replace("##", ddlPageSize.ID);
                    ddlPageSize.Attributes.Add("onchange", postBackScript);
                    ddlPageSize.RenderControl(new HtmlTextWriter(new StringWriter(builder)));
                    ddlPageSizeOutput = builder.ToString();

                    // page count text
                    ddlPageSizeOutput += string.Format("<span class='rows-count'>{0} {1} {2}</span>", this.Rows.Count, Resources.From, this.SelectArguments.TotalRowCount.ToString());
                }
            }

            #endregion

            #region Freeze Header
            if (this.FreezeHeader)
            {
                string FreezeHeaderScript = @"$(document).ready(function () { $('#" + this.ClientID + "').Scrollable({ ScrollHeight: " + this.FreezeHeight + (AutoWidth ? ""  : ", Width: " + this.FreezeWidth) + " }); });";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), this.ClientID + "_freezeHeader", FreezeHeaderScript, true);
            }
            #endregion

            string title = !string.IsNullOrEmpty(this.GridTitleResourceKey) ? Resources.ResourceManager.GetString(this.GridTitleResourceKey) : this.GridTitleText;
      
            if (this.ShowGridHeader)
                writer.Write(string.Format("<div style='width: {0};' class='section'><h3 class='section-title'>{1}{2}</h3><div style='width : 99%;' class='grid-view-container'>", this.ContainerWidth ?? "99%", title, ddlPageSizeOutput));
            else
                writer.Write(string.Format("<div style='width: {0};' class='grid-view-container'>", this.ContainerWidth ?? "99%"));

            base.Render(writer);
            Tools = string.Format("<div class='grid-view-tools' style='width: {0}'>{1}{2}{3}{4}{5}</div>", this.FreezeHeader && !AutoWidth ? FreezeWidth + "px" : "100%", btnAddOutput, btnReload, btnFilter, btnExcel, popupButtons);
            writer.Write(Tools);

            if (this.ShowGridHeader)
                writer.Write("</div>");

            writer.Write("</div>");
        }

        protected override void RaisePostBackEvent(string eventArgument)
        {
            switch (eventArgument)
            {
                case "New":
                    if (AddingEntity != null)
                        AddingEntity(this, new EventArgs());
                    break;
                case "Excel":
                    if (ExportingAsExcel != null)
                        this.ExportingAsExcel(this, new EventArgs());
                    break;
                case "Reload":
                    this.PageIndex = 0;

                    if (Reloading != null)
                        this.Reloading(this, new EventArgs());
                    else
                    {
                        if (!string.IsNullOrEmpty(SelectMethod))
                            this.DataBind();
                        else
                            return;
                    }
                    break;
            }

            if (eventArgument.Contains("PageSize$"))
            {
                this.PageIndex = 0;
                this.PageSize = int.Parse(eventArgument.Replace("PageSize$", ""));
                if (Reloading != null && string.IsNullOrEmpty(SelectMethod))
                    this.Reloading(this, new EventArgs());
            }

            base.RaisePostBackEvent(eventArgument);
        }

        #endregion
    }

    /// <summary>
    /// Cotnains the required properties for gridview popup buttons.
    /// </summary>
    [Serializable]
    public class GridViewPopupProperties
    {
        /// <summary>
        /// The id of the popup container.
        /// </summary>
        public string PopupID { get; set; }

        /// <summary>
        /// The text of the popup button.
        /// </summary>
        public string ButtonText { get; set; }

        /// <summary>
        /// The icon of the popup button.
        /// </summary>
        public string ButtonIcon { get; set; }
    }
}

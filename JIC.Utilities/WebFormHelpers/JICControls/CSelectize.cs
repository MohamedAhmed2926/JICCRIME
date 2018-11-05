using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Web;
using System.Collections.Specialized;
using System.IO;
using JIC.Base;
using JIC.Crime.View;

namespace JIC.Utilities.WebFormHelpers.JICControls
{
    public class CSelectize : CDropDownList
    {
        #region Variables

        bool checkPostedValues = false;
        CHiddenField hfNewValueContainer;

        #endregion

        #region Properties

        bool ScriptRegistered
        {
            get { return ViewState.Get<bool>("ScriptRegistered"); }
            set { ViewState.Set<bool>("ScriptRegistered", value); }
        }

        bool ClearSelected
        {
            get { return ViewState.Get<bool>("ClearSelected", false); }
            set { ViewState.Set<bool>("ClearSelected", value); }
        }

        bool CSSRegistered
        {
            get { return ViewState.Get<bool>("CSSRegistered"); }
            set { ViewState.Set<bool>("CSSRegistered", value); }
        }

        public SelectizeModes Mode
        {
            get { return ViewState.Get<SelectizeModes>("SelectizeModes", SelectizeModes.Single); }
            set { ViewState.Set<SelectizeModes>("SelectizeModes", value); }
        }

        public SelectizeDataSourceModes DataSourceMode
        {
            get { return ViewState.Get<SelectizeDataSourceModes>("DataSourceMode", SelectizeDataSourceModes.Local); }
            set { ViewState.Set<SelectizeDataSourceModes>("DataSourceMode", value); }
        }

        /// <summary>
        /// ex : ID,Code,Name
        /// </summary>
        public string DataSearchFields
        {
            get { return ViewState.Get("DataSearchFields"); }
            set { ViewState.Set("DataSearchFields", value); }
        }

        string DataSearchFieldsArray
        {
            get
            {
                if (string.IsNullOrEmpty(DataSearchFields))
                    return DataTextField;
                return JsonConvert.SerializeObject(DataSearchFields.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
            }
        }

        public string Placeholder
        {
            get { return ViewState.Get("Placeholder"); }
            set { ViewState.Set("Placeholder", value); }
        }

        /// <summary>
        /// used to provide the selectize with a url , the data will be fetched form this url & stored to the local storage of the browser 
        /// so for any subsuquent page loads the data will be fetched from the local storage of the browser
        /// </summary>
        public string DataSourceCacheUrl
        {
            get { return ViewState.Get("DataSourceCacheUrl"); }
            set { ViewState.Set("DataSourceCacheUrl", value); }
        }

        /// <summary>
        /// used to provide the selectize with a url , the data will be fetched form this url if the searched data is not present in the local storge of the borwser
        /// if no url assigned to this prop it takes its default value from the DataSourceCacheUrl prop
        /// </summary>
        public string DataSourceRemoteUrl
        {
            get { return ViewState.Get("DataSourceRemoteUrl", DataSourceCacheUrl); }
            set { ViewState.Set("DataSourceRemoteUrl", value); }
        }

        public int NumberofDisplayedItems
        {
            get { return ViewState.Get<int>("NumberofDisplayedItems", 10); }
            set { ViewState.Set<int>("NumberofDisplayedItems", value); }
        }

        public List<ListItem> SelectedItems
        {
            get
            {
                if (!checkPostedValues)
                    SetSelectedItemsbyPostedData();
                return Items.GetSelectedItems();
            }
            set
            {
                foreach (var item in value)
                {
                    Items.FindByValue(item.Value).Selected = true;
                }
                ClearSelected = false;
            }
        }

        public List<string> SelectedValues
        {
            get { return SelectedItems.Select(item => item.Value).ToList(); }
            set
            {
                foreach (var item in value)
                {
                    Items.FindByValue(item).Selected = true;
                }
                ClearSelected = false;
            }
        }

        /// <summary>
        /// ex : %QUERY
        /// </summary>
        public string DataSourceRemoteSearchKeyword
        {
            get { return ViewState.Get("DataSourceRemoteSearchKeyword", "%QUERY"); }
            set { ViewState.Set("DataSourceRemoteSearchKeyword", value); }
        }

        /// <summary>
        /// to allow make item and choose it not exists in list 
        /// </summary>

        public bool AllowAddNewItem
        {
            get { return ViewState.Get<bool>("AllowAddNewItem"); }
            set { ViewState.Set<bool>("AllowAddNewItem", value); }
        }

        public string NewAddedValue
        {
            get { return hfNewValueContainer.Value; }
        }

        #endregion

        #region Events

        public event EventHandler MultipleSelectedIndexChanged;

        #endregion

        #region Methods

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (AllowAddNewItem)
                hfNewValueContainer = new CHiddenField { ID = this.ClientID + "_hfNewValueContainer" };
        }

        protected override void OnLoad(EventArgs e)
        {
            SetSelectedItemsbyPostedData();
            if (HttpContext.Current.Request.Form["__EVENTTARGET"] == this.UniqueID)
            {
                if (MultipleSelectedIndexChanged != null)
                    MultipleSelectedIndexChanged(this, new EventArgs());
            }

            if (AllowAddNewItem)
                this.Controls.Add(hfNewValueContainer);

            base.OnLoad(e);
        }

        protected override ControlCollection CreateControlCollection()
        {
            return new ControlCollection(this);
        }

        protected override void OnPreRender(EventArgs e)
        {
            ValidateControl();
            BuildDataAttributes();
            //reset the clear property to avoid clearing during the next postback
            this.ClearSelected = false;
            base.OnPreRender(e);
        }

        protected override void VerifyMultiSelect()
        {
            // work around to change dropdownlist behavoir when more than one list item is selected.
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (AllowAddNewItem)
                hfNewValueContainer.RenderControl(writer);
            base.Render(writer);
        }

        void BuildDataAttributes()
        {
            this.Attributes.Add("data-type", "selectize");
            this.Attributes.Add("data-value", DataValueField);
            this.Attributes.Add("data-text", DataTextField);
            this.Attributes.Add("data-search", DataSearchFieldsArray);
            this.Attributes.Add("data-prefetch", DataSourceCacheUrl);
            this.Attributes.Add("data-remote", DataSourceRemoteUrl);
            this.Attributes.Add("data-wildcard", DataSourceRemoteSearchKeyword);
            this.Attributes.Add("data-limit", NumberofDisplayedItems.ToString());
            this.Attributes.Add("data-clear", ClearSelected.ToString());
            if (ClearSelected)
                ClearSelected = false;
            this.Attributes.Add("data-mode", Mode == SelectizeModes.Tags ? "multi" : "single");
            this.Attributes.Add("data-create", AllowAddNewItem.ToString());
            this.Attributes.Add("placeholder", Placeholder);
            this.Attributes.Add("data-newValueContainer", !AllowAddNewItem ? "" : "#" + hfNewValueContainer.ClientID.ToString());
        }

        void ValidateControl()
        {
            if (DataSourceMode == SelectizeDataSourceModes.Remote && string.IsNullOrEmpty(DataSourceCacheUrl))
                throw new Exception("DataSourceCacheUrl prop must not be empty or null when DataSourceMode set to remote");

            if (string.IsNullOrEmpty(DataTextField))
                throw new Exception("DataTextField prop must not be empty or null");

            if (string.IsNullOrEmpty(DataValueField))
                throw new Exception("DataValueField prop must not be empty or null");
        }

        void SetSelectedItemsbyPostedData()
        {
            if (DataSourceMode != SelectizeDataSourceModes.Local || (DataSourceMode == SelectizeDataSourceModes.Local && !Page.IsPostBack))
                return;

            string[] values = new string[] { };
            Items.ClearSelectedItems();
            if (!checkPostedValues && HttpContext.Current.Request.Form.AllKeys.Contains(UniqueID))
                values = new NameValueCollection(HttpContext.Current.Request.Form).GetValues(UniqueID);

            for (int i = 0; i < values.Length; i++)
            {
                var item = Items.FindByValue(values[i].ToString());
                if (item != null)
                    item.Selected = true;
            }
            checkPostedValues = true;
        }

        public void ClearSelectedItems()
        {
            this.Items.ClearSelectedItems();
            this.ClearSelected = true;
        }
        #endregion
    }
}

using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
//using Microsoft.AspNet.Identity;
using System.Web.Mvc.Ajax;

namespace JIC.Utilities.MvcHelpers
{
    public static class CustomHtmlHelper
    {
        #region Messages
        public static MvcHtmlString SuccessMessage(this HtmlHelper htmlHelper, string Message)
        {
            return new MvcHtmlString(@"
                <script>
                $.notify({
	                // options
	                message: '" + Message + @"' 
                },{
	                // settings
	                type: 'success',
	                animate: {
		                enter: 'animated fadeInRight',
		                exit: 'animated fadeOutRight'
	                }
                });
                </script>");
        }
        public static MvcHtmlString ErrorMessage(this HtmlHelper htmlHelper, string Message)
        {
            return new MvcHtmlString(@"
                <script>
                $.notify({
	                // options
	                message: '" + Message + @"' 
                },{
	                // settings
	                type: 'danger',
	                animate: {
		                enter: 'animated fadeInRight',
		                exit: 'animated fadeOutRight'
	                }
                });
                </script>");
        }
        #endregion
        #region TextBox
        public static MvcHtmlString CTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, object htmlAttributes, bool IsDisabled = false)
        {
            IDictionary<string, object> attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            if (IsDisabled)
                attributes.Add("readonly", "readonly");
            attributes.Add("class", "form-control");

            var mainDiv = new TagBuilder("div");
            mainDiv.AddCssClass("form-group");

            mainDiv.InnerHtml += helper.CLabelFor(expression);
            mainDiv.InnerHtml += helper.TextBoxFor(expression, attributes);
            mainDiv.InnerHtml += helper.ValidationMessageFor(
                                        expression,
                                        "",
                                        new { @class = "text-danger" });

            return new MvcHtmlString(mainDiv.ToString(TagRenderMode.Normal));
        }
        public static MvcHtmlString CTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
        {
            return CTextBoxFor(helper, expression, new { });
        }
        public static MvcHtmlString CTextBoxFor<TModel>(
                                         this HtmlHelper<TModel> helper,
                                         Expression<Func<TModel, string>> expression)
        {
            return helper.CTextBoxFor(expression, null);
        }
        public static MvcHtmlString CTextBox(this HtmlHelper helper,string Name,string value = "",string ModelName = "")
        {

            string mainDiv = "";
            mainDiv += helper.TextBox(Name, value, new { @class = "form-control" });
            mainDiv += helper.ValidationMessage(String.IsNullOrEmpty(ModelName) ? Name : ModelName, new { @class = "text-danger" });


            return new MvcHtmlString(mainDiv);
        }
        public static MvcHtmlString CPasswordFor<TModel>(
                                         this HtmlHelper<TModel> helper,
                                         Expression<Func<TModel, string>> expression)
        {
            var mainDiv = new TagBuilder("div");
            mainDiv.AddCssClass("form-group");

            mainDiv.InnerHtml += helper.CLabelFor(expression);
            mainDiv.InnerHtml += helper.PasswordFor(expression, new { @class = "form-control" });
            mainDiv.InnerHtml += helper.ValidationMessageFor(
                                        expression,
                                        "",
                                        new { @class = "text-danger" });

            return new MvcHtmlString(mainDiv.ToString(TagRenderMode.Normal));
        }
        #endregion
        #region CheckBox
        public static MvcHtmlString CCheckBox<TModel>(
                                   this HtmlHelper<TModel> helper,
                                   Expression<Func<TModel, bool>> expression, string label, IDictionary<string, object> htmlAttributes)
        {
            var mainDiv = new TagBuilder("div");
            mainDiv.AddCssClass("checkbox");
            var labelDiv = new TagBuilder("label");

            labelDiv.InnerHtml += helper.CheckBoxFor(expression, htmlAttributes);
            labelDiv.InnerHtml += label;
            mainDiv.InnerHtml += labelDiv;

            return new MvcHtmlString(mainDiv.ToString(TagRenderMode.Normal));
        }
        public static MvcHtmlString CCheckBox<TModel>(
                                  this HtmlHelper<TModel> helper,
                                  Expression<Func<TModel, bool>> expression, string label)
        {
            var mainDiv = new TagBuilder("div");
            mainDiv.AddCssClass("checkbox");
            var labelDiv = new TagBuilder("label");

            labelDiv.InnerHtml += helper.CheckBoxFor(expression);
            labelDiv.InnerHtml += label;
            mainDiv.InnerHtml += labelDiv;

            return new MvcHtmlString(mainDiv.ToString(TagRenderMode.Normal));
        }
        public static MvcHtmlString CCheckBox<TModel>(
                                  this HtmlHelper<TModel> helper,
                                  Expression<Func<TModel, bool>> expression, string label, object htmlAttributes)
        {
            var mainDiv = new TagBuilder("div");
            mainDiv.AddCssClass("checkbox");
            var labelDiv = new TagBuilder("label");

            labelDiv.InnerHtml += helper.CheckBoxFor(expression,htmlAttributes);
            labelDiv.InnerHtml += label;
            mainDiv.InnerHtml += labelDiv;

            return new MvcHtmlString(mainDiv.ToString(TagRenderMode.Normal));
        }

        #endregion
        #region RadioButton
        public static MvcHtmlString CRadioButton<TModel>(
                                   this HtmlHelper<TModel> helper,
                                   Expression<Func<TModel, string>> expression, string label, object value, IDictionary<string, object> htmlAttributes)
        {
            var mainDiv = new TagBuilder("div");
            mainDiv.AddCssClass("radio");
            var labelDiv = new TagBuilder("label");

            labelDiv.InnerHtml += helper.RadioButtonFor(expression, value, htmlAttributes);
            labelDiv.InnerHtml += label;
            mainDiv.InnerHtml += labelDiv;

            return new MvcHtmlString(mainDiv.ToString(TagRenderMode.Normal));
        }
        public static MvcHtmlString CRadioButton<TModel>(
                                   this HtmlHelper<TModel> helper,
                                   Expression<Func<TModel, string>> expression, string label, object value, object htmlAttributes)
        {
            var mainDiv = new TagBuilder("div");
            mainDiv.AddCssClass("radio");
            var labelDiv = new TagBuilder("label");

            labelDiv.InnerHtml += helper.RadioButtonFor(expression, value, htmlAttributes);
            labelDiv.InnerHtml += label;
            mainDiv.InnerHtml += labelDiv;

            return new MvcHtmlString(mainDiv.ToString(TagRenderMode.Normal));
        }
        public static MvcHtmlString CRadioButton<TModel>(
                                   this HtmlHelper<TModel> helper,
                                   Expression<Func<TModel, string>> expression, string label, object value)
        {
            return CRadioButton(helper, expression, label, value, null);
        }
        #endregion
        public static MvcHtmlString CLabelFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression,bool isRequired = false)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            if (!isRequired && metadata.ContainerType != null)
            {
                isRequired = metadata.ContainerType.GetProperty(metadata.PropertyName)
                                .GetCustomAttributes(typeof(RequiredAttribute), true)
                                .Length == 1;
            }
            TagBuilder tag = new TagBuilder("label");
            tag.Attributes.Add("for",htmlHelper.IdFor(expression).ToString());
            
            if (isRequired)
                tag.Attributes.Add("class", "control-label");

            tag.SetInnerText(htmlHelper.DisplayNameFor(expression).ToString());
            var output = tag.ToString(TagRenderMode.Normal);
            if (isRequired)
            {
                var asteriskTag = new TagBuilder("span");
                asteriskTag.Attributes.Add("class", "required");
                asteriskTag.SetInnerText("*");
                output += asteriskTag.ToString(TagRenderMode.Normal);
            }
            return MvcHtmlString.Create(output);
        }

        #region DropDown
        public static MvcHtmlString CDropDownFor<TModel, TProperty>(
                                  this HtmlHelper<TModel> helper,
                                  Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> ListItem_, bool IncludeDefaultItem = false, bool IsDisabled = false, bool IsRequired = false)
        {
            return helper.CSelectize(expression, ListItem_, new CelectizeAtt { IncludeDefaultItem = IncludeDefaultItem,IsDisabled = IsDisabled,IsRequired = IsRequired });
        }
        public static MvcHtmlString CDropDownFor<TModel, TProperty>(
                                   this HtmlHelper<TModel> helper,
                                   Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> ListItem_, object htmlAttributes, bool IncludeDefaultItem = false, bool IsDisabled = false, bool IsRequired = false)
        {
            IDictionary<string, object> htmlAttributesDic = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            return helper.CSelectize(expression, ListItem_, new CelectizeAtt { IncludeDefaultItem = IncludeDefaultItem ,IsDisabled = IsDisabled,htmlAttribuites = htmlAttributesDic, IsRequired = IsRequired });
        }

        public static MvcHtmlString CSelectize<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> ListItem_, CelectizeAtt htmlAttributes)
        {
            if (htmlAttributes == null)
                htmlAttributes = new CelectizeAtt();
            var mainDiv = new TagBuilder("div");
            mainDiv.AddCssClass("form-group");
            htmlAttributes.Add("class", "form-control selectize");
            if (htmlAttributes.IsDisabled)
                htmlAttributes["class"] = (string)htmlAttributes["class"] + " disabled";
            mainDiv.GenerateId(helper.IdFor(expression).ToString() + "_div");
            string defaultOption = (htmlAttributes.IncludeDefaultItem ? JIC.Base.Resources.Resources.DefaultSelectItem : null);
            mainDiv.InnerHtml += helper.CLabelFor(expression,htmlAttributes.IsRequired);
            if(!String.IsNullOrEmpty(htmlAttributes.SelectedValue))
            {
                var ListItem = ListItem_.Where(item => item.Value == htmlAttributes.SelectedValue).FirstOrDefault();
                if (ListItem != null)
                    ListItem.Selected = true;
            }
            if(htmlAttributes.SelectedValues.Count > 0)
            {
                var ListItems = ListItem_.Where(item => htmlAttributes.SelectedValues.Contains(item.Value)).ToList();
                foreach (var ListItem in ListItems)
                {
                    ListItem.Selected = true;
                }
            }

            if (htmlAttributes.SelectizeMode == Base.SelectizeModes.Single)
                mainDiv.InnerHtml += helper.DropDownListFor(expression, ListItem_, defaultOption, htmlAttributes.GetDictionary());
            else
                mainDiv.InnerHtml += helper.ListBoxFor(expression, ListItem_, htmlAttributes.GetDictionary());

            mainDiv.InnerHtml += helper.ValidationMessageFor(
                                        expression,
                                        "",
                                        new { @class = "text-danger" });
            
            //string script = @"<script>OnDocReady(function(){{ $('.selectize').selectize();" + (htmlAttributes.IsDisabled ? "document.getElementById('{0}').selectize.lock();" : "") + @" }});</script>";
            string script = @"<script>
                            OnDocReady(function(){{ 
                                $('#{0}').selectize({{allowEmptyOption:true" +(htmlAttributes.SelectizeMode == Base.SelectizeModes.Tags ? ",plugins: ['remove_button']" : "") + @"}}); " + 
                                (htmlAttributes.IsDisabled ? "$('#{0}')[0].selectize.disable();$('#{0}').attr('disabled',false);" : "") + 
                                @" }});</script>";
            mainDiv.InnerHtml += String.Format(script, helper.IdFor(expression).ToString());

            return new MvcHtmlString(mainDiv.ToString(TagRenderMode.Normal));
        }
        #endregion
        public static string IdForDiv<TModel, TProperty>(this HtmlHelper<TModel> helper,
                                   Expression<Func<TModel, TProperty>> expression)
        {
            return (helper.IdFor(expression).ToString() + "_div");
        }

        public static CModel BeginModel(this HtmlHelper helper, string ID, string ModelTitle)
        {
            return new CModel(helper.ViewContext, ID, ModelTitle);
        }
        
        public static AjaxForm BeginAjaxForm(this HtmlHelper htmlHelper,string FormID, string url)
        {
            return new AjaxForm(htmlHelper, url,FormID);
        }


        public static MvcHtmlString CDatetimepickerFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            var mainDiv = new TagBuilder("div");
            mainDiv.AddCssClass("form-group");
            var DateDiv = new TagBuilder("div");
            DateDiv.AddCssClass("input-group date datetimepicker");

            mainDiv.InnerHtml += helper.LabelFor(expression, new { @class = "control-label" });
            //Date div part
            DateDiv.InnerHtml += helper.TextBoxFor(expression, new { @class = "form-control " });
            DateDiv.InnerHtml += "<span class='input-group-addon'><i class='glyphicon glyphicon-th'></i></span>";
            mainDiv.InnerHtml += DateDiv;

            mainDiv.InnerHtml += helper.ValidationMessageFor(expression,"",new { @class = "text-danger" });

            return new MvcHtmlString(mainDiv.ToString(TagRenderMode.Normal));
        }
        

        public static MvcHtmlString CDateTimePickerFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, DatePickerAtt datePickerAtt = null)
        {
            if (datePickerAtt == null)
                datePickerAtt = new DatePickerAtt();
            //< div class="input-group date">
            //  <input type = "text" class="form-control"><span class="input-group-addon"><i class="glyphicon glyphicon-th"></i></span>
            //</div>
            var mainDiv = new TagBuilder("div");
            mainDiv.AddCssClass("form-group");
            if(!datePickerAtt.Inline)
                mainDiv.InnerHtml += helper.CLabelFor(expression);
            mainDiv.GenerateId(helper.IdForDiv(expression).ToString());
            var value = ModelMetadata.FromLambdaExpression(expression, helper.ViewData).Model;
            var DateDiv = new TagBuilder("div");
            DateDiv.AddCssClass("input-group date dtpicker");
            if (datePickerAtt.Inline)
                DateDiv.InnerHtml += "<span class='input-group-addon requiredDate'>" + helper.DisplayNameFor(expression) +"</span>";
            IDictionary<string, object> dictionary = new Dictionary<string,object>();
            dictionary.Add("class", "form-control");
            if(datePickerAtt.IsDisabled)
                dictionary.Add("readonly", "readonly");

            DateDiv.InnerHtml += helper.TextBoxFor(expression, "{0:dd/MM/yyyy}", dictionary);
            DateDiv.InnerHtml += "<span class='input-group-addon'><i class='fa fa-calendar'></i></span>";
            mainDiv.InnerHtml += DateDiv;


            mainDiv.InnerHtml += helper.ValidationMessageFor(expression, "", new { @class = "text-danger" });

            var Script = String.Format(@"<script>
            OnDocReady(function () {{
                    $('#{0} .input-group.date').datepicker({{
                        format: ""{1}"",
                        weekStart: {2},
                        language: ""{3}"",
                        startDate: {4},
                        endDate: {5},
                        forceParse: false
                    }});

                    $('#{0} .input-group.date').datepicker().on('changeDate', function (e) {{
                        $('#{0}').trigger('Calendar:DateChanged', $('#{0} .input-group.date').datepicker('getUTCDate'));
                    }});
                    $('#{0} .input-group.date').datepicker().on('changeMonth', function (e) {{
                        var date = $('#{0} .input-group.date').datepicker('getUTCDate');
                        if(date)
                            $('#{0}').trigger('Calendar:MonthChanged', {{ month: date.getMonth() + 1, year: date.getFullYear() }});
                    }});
                    $('#{0}').on('Calendar:SetDate', function (event,date) {{
                        $('#{0} .input-group.date').datepicker('setUTCDate',date);
                    }});
                    $('#{0}').on('Calendar:ClearDate', function (event,date) {{
                        $('#{0} .input-group.date').datepicker('clearDates');
                    }});


                
            }});
            </script>", 
            helper.IdForDiv(expression).ToString(),
            datePickerAtt.Format,
            (int)datePickerAtt.WeekStart,
            datePickerAtt.Language,
            datePickerAtt.MinDate.HasValue ? String.Format("new Date({0},{1},{2})", datePickerAtt.MinDate.Value.Year, datePickerAtt.MinDate.Value.Month-1, datePickerAtt.MinDate.Value.Day) : "-Infinity",
            datePickerAtt.MaxDate.HasValue ? String.Format("new Date({0},{1},{2})", datePickerAtt.MaxDate.Value.Year, datePickerAtt.MaxDate.Value.Month-1, datePickerAtt.MaxDate.Value.Day) : "Infinity");


            mainDiv.InnerHtml+=Script;
            return new MvcHtmlString(mainDiv.ToString(TagRenderMode.Normal));
        }
        public static CUpdatePanel CUpdatePanel(this HtmlHelper helper, string url, string ID,Method Method = Method.Get)
        {
            return new CUpdatePanel(helper.ViewContext, ID, url,Method);
        }
        public static CMvcHtmlString MvcString(this HtmlHelper helper, string value)
        {
            return new CMvcHtmlString(value);
        }
        public static void RenderPartialWithPrefix(this HtmlHelper helper, string partialViewName, object model, string prefix)
        {
            ViewDataDictionary WDD = new ViewDataDictionary { TemplateInfo = new System.Web.Mvc.TemplateInfo { HtmlFieldPrefix = prefix } };

            foreach (string key in helper.ViewData.ModelState.Keys)
            {
                if (key.StartsWith(prefix + "."))
                {
                    foreach (ModelError err in helper.ViewData.ModelState[key].Errors)
                    {
                        if (!string.IsNullOrEmpty(err.ErrorMessage))
                            WDD.ModelState.AddModelError(key, err.ErrorMessage);
                        if (err.Exception != null)
                            WDD.ModelState.AddModelError(key, err.Exception);
                    }
                    WDD.ModelState.SetModelValue(key, helper.ViewData.ModelState[key].Value);
                }
            }

            helper.RenderPartial(partialViewName, model, WDD);
        }
        //public static vw_UserData CurrentUser(this IPrincipal User)
        //{
        //    return User.Identity.IsAuthenticated ? new JIC.Services.Services.Security_UsersService(Base.CaseType.Crime).FindUserByID(User.Identity.GetUserId<int>()) : null;

        //}
        public static void CRenderAction(this HtmlHelper helper,string ActionName,string ControllerName,object routeValues)
        {
            helper.ViewContext.TempData["CRenderActionModelState"] = helper.ViewData.ModelState;
            helper.RenderAction(ActionName, ControllerName, routeValues);
        }
    }
}

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIC.Base;
using JIC.Crime.View;

namespace JIC.Utilities.WebFormHelpers.JICControls
{
    public class CDateValidator : CCustomValidator
    {
        #region Properties

        /// <summary>
        /// Gets or sets the comparison operation to perform.
        /// </summary>
        public ValidationCompareOperator Operator
        {
            get { return ViewState.Get<ValidationCompareOperator>("Operator"); }
            set { ViewState.Set<ValidationCompareOperator>("Operator", value); }
        }

        /// <summary>
        /// Gets or sets the date picker control to validate.
        /// </summary>
        public string DatePickerToValidate
        {
            get { return ViewState.Get("DatePickerToValidate"); }
            set { ViewState.Set<string>("DatePickerToValidate", value); }
        }

        /// <summary>
        /// Gets or sets the date picker control to compare with the date picker control being validated.
        /// </summary>
        public string DatePickerToCompare
        {
            get { return ViewState.Get("DatePickerToCompare"); }
            set { ViewState.Set<string>("DatePickerToCompare", value); }
        }

        /// <summary>
        /// Gets or sets a constant date to compare with the date entered by the user in the date picker control being validated.
        /// </summary>
        public string DateValueToCompare
        {
            get { return ViewState.Get("DateValueToCompare"); }
            set { ViewState.Set<string>("DateValueToCompare", value); }
        }

        /// <summary>
        /// Gets or sets a value which indecates if the compare will be used against todays' date or not.
        /// Default is false.
        /// </summary>
        public bool CompareToToday
        {
            get { return ViewState.Get<bool>("CompareToToday", false); }
            set { ViewState.Set<bool>("CompareToToday", value); }
        }

        /// <summary>
        /// Gets the name of the client validation method.
        /// </summary>
        private string ClientValidationFunctionName
        {
            get { return "DDateValidator_" + this.ClientID; }
        }

        /// <summary>
        /// Gets or sets the value indecates if the page is doing pratial or full postback.
        /// </summary>
        private bool IsScriptRegister
        {
            get
            {
                // When doing partial postbacks, we need to register the scripts once.
                var scriptManager = ScriptManager.GetCurrent(this.Page);
                if (scriptManager != null && scriptManager.IsInAsyncPostBack)
                    return (ViewState.Get<bool>("IsScriptRegister", false));

                // When doing full postbacks, we register every time.
                return false;
            }
            set
            {
                ViewState.Set<bool>("IsScriptRegister", value);
            }
        }

        #endregion

        #region Methods

        protected override void OnPreRender(EventArgs e)
        {
            if (string.IsNullOrEmpty(this.DatePickerToValidate))
                throw new ArgumentNullException(string.Format("DatePickerToValidate property is not assigned to {0} DDateValidator.", this.ID));

            if (string.IsNullOrEmpty(this.DatePickerToCompare) && string.IsNullOrEmpty(this.DateValueToCompare) && !this.CompareToToday)
                throw new ArgumentNullException(string.Format("{0} DDateValidator must have 'DatePickerToCompare' property or 'DateValueToCompare' or 'CompareToToday' property.", this.ID));

            string ValidationScript = "";

            if (!IsScriptRegister)
            {
                ValidationScript = (@";function ClientValidationFunctionName(sender, args) {
                                var dateToValidate = Date.parseExact(DateToValidateExpression, 'DateFormat');
                                if(dateToValidate == null)
                                    args.IsValid = true;
                                else
                                    args.IsValid = dateToValidate Operator Date.parseExact(DateToCompareExpression, 'DateFormat');
                                };").Replace("ClientValidationFunctionName", this.ClientValidationFunctionName)
                                    .Replace("Operator", this.OperatorToUse)
                                    .Replace("DateFormat", SystemConfigurations.DateTime_ShortDateFormat)
                                    .Replace("DateToValidateExpression", this.DateToValidateExpression)
                                    .Replace("DateToCompareExpression", this.DateToCompareExpression);

                IsScriptRegister = true;
            }

            this.ClientValidationFunction = this.ClientValidationFunctionName;
            ScriptManager.RegisterStartupScript(this, this.GetType(), this.ClientID + "_script", ValidationScript, true);

            base.OnPreRender(e);
        }

        private string DateToValidateExpression
        {
            get
            {
                string id = this.GetControlRenderID(this.DatePickerToValidate);
                if (string.IsNullOrEmpty(id))
                    throw new ArgumentNullException(string.Format("Cannot find {0}", this.DatePickerToValidate));
                return string.Format("$('#{0}').val()", id);
            }
        }

        private string DateToCompareExpression
        {
            get
            {
                if (this.CompareToToday)
                    return string.Format("'{0}'", DateTime.Now.ToString(SystemConfigurations.DateTime_ShortDateFormat));

                else if (!string.IsNullOrEmpty(DateValueToCompare))
                {
                    DateTime output;
                    if (!DateTime.TryParseExact(this.DateValueToCompare.Trim(), SystemConfigurations.DateTime_ShortDateFormat, System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, out output))
                        throw new Exception(string.Format("Could not parse '{0}' as DateTime using format '{1}'", this.DateValueToCompare.Trim(), SystemConfigurations.DateTime_ShortDateFormat));

                    return string.Format("'{0}'", output.ToString(SystemConfigurations.DateTime_ShortDateFormat));
                }

                else
                {
                    string id = this.GetControlRenderID(this.DatePickerToCompare);
                    if (string.IsNullOrEmpty(id))
                        throw new ArgumentNullException(string.Format("Cannot find {0}", this.DatePickerToCompare));
                    return string.Format("$('#{0}').val()", id);
                }
            }
        }

        private string OperatorToUse
        {
            get
            {
                switch (this.Operator)
                {
                    case ValidationCompareOperator.GreaterThan: return ">";
                    case ValidationCompareOperator.GreaterThanEqual: return ">=";
                    case ValidationCompareOperator.LessThan: return "<";
                    case ValidationCompareOperator.LessThanEqual: return "<=";
                    case ValidationCompareOperator.Equal:
                    case ValidationCompareOperator.NotEqual:
                    case ValidationCompareOperator.DataTypeCheck:
                        throw new InvalidOperationException(string.Format("This Operator {0} couldn't be used in {1} DDateValidator control.", this.Operator.ToString(), this.ID));
                    default: return string.Empty;
                }
            }
        }

        #endregion
    }
}

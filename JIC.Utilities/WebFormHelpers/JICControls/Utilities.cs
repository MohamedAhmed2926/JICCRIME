using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIC.Base;
using JIC.Base.Resources;
using JIC.Utilities.Helpers;
using JIC.Utilities.WebFormHelpers.JICControls;

namespace JIC.Utilities.WebFormHelpers.JICControls
{
    internal class Utilities
    {
        internal static void AddPopoverAttributes(string ControlID, AttributeCollection Attributes, PopoverDiractions PopoverDiraction, PopoverTriggers PopoverTrigger, string PopoverTextResourceKey, string PopoverText = "")
        {
            if (string.IsNullOrEmpty(PopoverTextResourceKey) && string.IsNullOrEmpty(PopoverText))
                throw new ArgumentNullException(string.Format("PopoverTextResourceKey is missing for control {0}", ControlID));

            Attributes.Add("data-show-popover", "true");
            Attributes.Add("data-trigger", PopoverTrigger.ToString().ToLower());
            Attributes.Add("data-content", string.IsNullOrEmpty(PopoverText) ? Resources.ResourceManager.GetString(PopoverTextResourceKey) : PopoverText);

            ////switch the popover direction based on the language type
            //PopoverDiractions direction = PopoverDiraction;
            //if (LanguageHelper.CurrentLanguageIsRTL && (direction == PopoverDiractions.Left || direction == PopoverDiractions.Right))
            //    direction = PopoverDiraction == PopoverDiractions.Right ? PopoverDiractions.Left : PopoverDiractions.Right;

            Attributes.Add("data-placement", PopoverDiraction.ToString().ToLower());
        }

        internal static void AddPopoverAttributes(string ControlID, AttributeCollection Attributes, PopoverDiractions PopoverDiraction, PopoverTriggers PopoverTrigger, string PopoverTextResourceKey, params string[] parms)
        {
            if (string.IsNullOrEmpty(PopoverTextResourceKey))
                throw new ArgumentNullException(string.Format("PopoverTextResourceKey is missing for control {0}", ControlID));

            Attributes.Add("data-show-popover", "true");
            Attributes.Add("data-trigger", PopoverTrigger.ToString().ToLower());
            Attributes.Add("data-content", string.Format(Resources.ResourceManager.GetString(PopoverTextResourceKey), parms));

            ////switch the popover direction based on the language type
            //PopoverDiractions direction = PopoverDiraction;
            //if (LanguageHelper.CurrentLanguageIsRTL && (direction == PopoverDiractions.Left || direction == PopoverDiractions.Right))
            //    direction = PopoverDiraction == PopoverDiractions.Right ? PopoverDiractions.Left : PopoverDiractions.Right;

            Attributes.Add("data-placement", PopoverDiraction.ToString().ToLower());
        }

        internal static CRequiredFieldValidator CreateRequiredFieldValidator(string ControlID, string ValidationGroup)
        {
            return new CRequiredFieldValidator
            {
                ControlToValidate = ControlID,
                ID = "vldreq" + ControlID,
                ValidationGroup = ValidationGroup,
                Display = ValidatorDisplay.Dynamic
            };
        }

        internal static CRequiredFieldValidator CreateRequiredFieldValidator(string ControlID, string ValidationGroup, string ErrorMessage)
        {
            return new CRequiredFieldValidator
            {
                ControlToValidate = ControlID,
                ID = "vldreq" + ControlID,
                ValidationGroup = ValidationGroup,
                Display = ValidatorDisplay.Dynamic,
                ErrorMessage = ErrorMessage
            };
        }

        internal static CRangeValidator CreateRangeValidator(string ControlID, string ValidationGroup, ValidationDataType DataType, string MinimumValue, string MaximumValue)
        {
            return new CRangeValidator
            {
                ControlToValidate = ControlID,
                ID = "vldreg" + ControlID,
                ValidationGroup = ValidationGroup,
                Type = DataType,
                MinimumValue = MinimumValue,
                MaximumValue = MaximumValue
            };
        }

        internal static CCompareValidator CreateCompareValidator(string ControlID, string ValidationGroup, ValidationDataType DataType, string ValueToCompare, ValidationCompareOperator CompareOperator, string ErrorMessageResourceKey)
        {
            return new CCompareValidator
            {
                ControlToValidate = ControlID,
                ID = "vldcmpr" + ControlID,
                ValidationGroup = ValidationGroup,
                Type = DataType,
                Operator = CompareOperator,
                ValueToCompare = ValueToCompare,
                ErrorMessageResourceKey = ErrorMessageResourceKey,
            };
        }

        internal static CRegularExpressionValidator CreateRegularExpressionValidator(string ControlID, string ValidationGroup, string ValidationExpression, string ErrorMessageResourceKey)
        {
            return new CRegularExpressionValidator
            {
                ControlToValidate = ControlID,
                ID = "vldrng" + ControlID,
                ValidationGroup = ValidationGroup,
                ValidationExpression = ValidationExpression,
                ErrorMessageResourceKey = ErrorMessageResourceKey
            };
        }

        internal static CCustomValidator CreateCustomValidator(string ControlID, string ValidationGroup, string ClientValidationFunction, string ErrorMessageResourceKey)
        {
            return new CCustomValidator
            {
                ID = "vlcstm" + ControlID,
                ValidationGroup = ValidationGroup,
                ClientValidationFunction = ClientValidationFunction,
                ErrorMessageResourceKey = ErrorMessageResourceKey,
            };
        }

        internal static CCustomValidator CreateCustomValidator(string ControlID, string ControlToValidateID, string ValidationGroup, string ClientValidationFunction, string ErrorMessageResourceKey)
        {
            return new CCustomValidator
            {
                ControlToValidate = ControlToValidateID,
                ID = "vlcstm" + ControlID,
                ValidationGroup = ValidationGroup,
                ClientValidationFunction = ClientValidationFunction,
                ErrorMessageResourceKey = ErrorMessageResourceKey,
            };
        }

        internal static CLabel CreateLabel(string ResourceKey)
        {
            return new CLabel
            {
                TextResourceKey = ResourceKey
            };
        }

        internal static CCheckBox CreateCheckBox(string ResourceKey)
        {
            return new CCheckBox
            {
                LabelResourceKey = ResourceKey
            };
        }

        internal static CLabel CreateLabel(string ResourceKey, string LabelText)
        {
            return new CLabel
            {
                TextResourceKey = ResourceKey,
                Text = LabelText
            };
        }

        internal static CRadioButton CreateRadioButtonAddon(string ControlID, bool Checked, bool AutoPostBack, string GroupName)
        {
            return new CRadioButton
            {
                ID = ControlID + "_addon",
                Checked = Checked,
                AutoPostBack = AutoPostBack,
                GroupName = GroupName
            };
        }
    }
}

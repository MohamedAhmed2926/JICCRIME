using JIC.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIC.Utilities.MvcHelpers
{
    public abstract class AttributeBase : Dictionary<string,object>{
        private bool _isRequired = false;
        private bool _isDisabled = false;
        private bool _IsHidden = false;

        public bool IsRequired
        {
            get { return _isRequired; }
            set { _isRequired = value; }
        }

        public bool IsDisabled
        {
            get { return _isDisabled; }
            set
            {
                Remove("readonly");
                if (value)
                    Add("readonly", "readonly");
                _isDisabled = value;
            }
        }
        public bool IsHidden
        {
            get { return _IsHidden; }
            set
            {
                Remove("style");
                if (value)
                    Add("style", "display:none");
                _IsHidden = value;
            }
        }
        public new AttributeBase Add(string key, object value)
        {
            base.Add(key, value);
            return this;
        }
        public new AttributeBase Remove(string key)
        {
            base.Remove(key);
            return this;
        }

        public IDictionary<string, object> GetDictionary()
        {
            return this;
        }

        public IDictionary<string, object> htmlAttribuites
        {
            set
            {
                foreach (var item in value)
                {
                    this.Add(item.Key, item.Value);
                }
            }
        }

    }
    public class TextBoxAtt : AttributeBase
    {
        private TextBoxDataTypes _TextBoxDataTypes;

        public TextBoxDataTypes TextBoxDataTypes
        {
            get { return _TextBoxDataTypes; }
            set {
                _TextBoxDataTypes = value;
                Remove("type");
                switch (value)
                {
                    case TextBoxDataTypes.Int:
                    case TextBoxDataTypes.Double:
                        Add("type", "number");
                        break;
                    case TextBoxDataTypes.Email:
                        Add("type", "email");
                        break;
                    default:
                        Add("type", "text");
                        break;
                }
            }
        }
    }

    public class CelectizeAtt : AttributeBase
    {
        private SelectizeModes selectizeMode = SelectizeModes.Single;
        public bool IncludeDefaultItem { get; set; }
        public string SelectedValue { get; set; } = null;
        public List<string> SelectedValues { get; set; }
        public SelectizeModes SelectizeMode
        {
            get { return selectizeMode; }
            set
            {
                selectizeMode = value;
                Remove("multiple");
                if (value == SelectizeModes.Tags)
                    Add("multiple", "multiple");
            }
        }
        public CelectizeAtt()
        {
            SelectedValues = new List<string>();
        }

    }

    public class DatePickerAtt : AttributeBase
    {
        public string Format { get; set; } = "dd/mm/yyyy";
        public string Language { get; set; } = "ar";
        public DayOfWeek WeekStart { get; set; } = DayOfWeek.Saturday;
        public string DayOfWeekFormat { get; set; } = "daysMin";

        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; }

        public ShowMonthOnly monthOnly { set
            {
                MinDate = new DateTime(value.Year, value.Month, 1);
                MaxDate = new DateTime(value.Year, value.Month, DateTime.DaysInMonth(value.Year, value.Month));
            }
        }
        public bool Inline { get; set; } = false;
    }
    public class ShowMonthOnly
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public ShowMonthOnly(int month, int year)
        {
            Month = month;
            Year = year;
        }
        public ShowMonthOnly()
        {
        }
    }
}
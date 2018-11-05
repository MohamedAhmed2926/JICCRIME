using JIC.Services.ServicesInterfaces;
using JIC.Base.Views;
using JIC.Crime.View.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using JIC.Base;
using JIC.Base.Resources;

namespace JIC.Crime.View.Models
{
    public class PersonViewModel
    {
        public PersonViewModel(vw_PersonData _PersonData, ILookupService lookupService, IPersonService PersonService, Modes Mode = Modes.Add)
        {
            this.PersonData = _PersonData;
            Cities = lookupService.GetAllCitites();
            PoliceStations = new List<vw_KeyValue>();
            PoliceStations = lookupService.GetAllPoliceStations();
            Nationalities = lookupService.GetLookupsByCategory(Base.LookupsCategories.Nationalities);
            if (Mode == Modes.Update)
            {
                if (PersonService.NumberOfAttachedCases(PersonData.ID) <= 1)
                    IsLocked = true;
            }
            // Added By John 13/3/2018
            else if (PersonData.ID != 0 && PersonService.NumberOfAttachedCases(PersonData.ID) == 0)
                IsLocked = true;
            // Commented by John 13/3/2018
            //else if (PersonService.NumberOfAttachedCases(PersonData.ID) == 0)
            //    IsLocked = true;
            else
                IsLocked = false;
        }
        public vw_PersonData PersonData { get; set; }
        public List<vw_KeyValue> Cities { get; set; }
        public List<vw_KeyValue> PoliceStations { get; set; }
        public List<vw_KeyValue> Nationalities { get; set; }
        public bool IsLocked { get; internal set; }
    }
    public class vw_PersonData
    {
        [Display(Name = "Age", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int Age { get; set; }
        public long ID { get; set; } = 0;
        [Required(ErrorMessageResourceType = typeof(Base.Resources.Resources), ErrorMessageResourceName = "RequiredErrorMessage")]
        [Display(Name = "Name", ResourceType = typeof(JIC.Base.Resources.Resources))]
        [MaxLength(250, ErrorMessage = "إسم الشخص لا يمكن ان يتخطى 250 حرف")]
       
        [RegularExpression("^([\u0621-\u064A ]+[^ ]+$)", ErrorMessage = "الإسم غير صحيح")]
        public virtual string Name { get; set; }
        [Display(Name = "NationalId", ResourceType = typeof(JIC.Base.Resources.Resources))]
        //[StringLength(14, MinimumLength = 14, ErrorMessage = "الرقم القومى غير صحيح")]
        ////[RegularExpression("^[0-9]*", ErrorMessage = "الرقم القومى مكون من أرقام فقط")]
        //[Unique(UniqueTypes.NationalNo)]
        [RegularExpression(@"(2|3)[0-9][0-9][0-1][0-9][0-3][0-9](01|02|03|04|11|12|13|14|15|16|17|18|19|21|22|23|24|25|26|27|28|29|31|32|33|34|35|88)\d\d\d\d\d", ErrorMessageResourceType = typeof(Base.Resources.Resources), ErrorMessageResourceName = "NationalIdNotValid")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]

        public virtual string NatNo { get; set; }

        public bool NationalIDRequired { get; set; } = false;

        [Required(ErrorMessageResourceType = typeof(Base.Resources.Resources), ErrorMessageResourceName = "RequiredErrorMessage")]
        [Display(Name = "Nationality", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public virtual int? NationalityID { get; set; } = (int)Base.Nationality.Egyptian;
        [Display(Name = "PassportNo", ResourceType = typeof(JIC.Base.Resources.Resources))]
        [RegularExpression("^([a-zA-z0-9*-@#$!#&_/]+[^ ]+)", ErrorMessage = "رقم جواز السفر غير صحيح")]
        // [Unique(UniqueTypes.PassportNo)]
       
        public virtual string PassportNo { get; set; }
        [Display(Name = "Job", ResourceType = typeof(JIC.Base.Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]
        [RegularExpression("^([\u0621-\u064A ]+[^ ]+)", ErrorMessage = "يجب ان يكون هذا الحقل باللغة العربية")]
        public virtual string Job { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]

        [Display(Name = "City", ResourceType = typeof(Base.Resources.Resources))]
        public virtual int? address_CityID { get; set; }
        [Display(Name = "PoliceStation", ResourceType = typeof(Base.Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]

        public virtual int? address_PoliceStationID { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]

        [Display(Name = "Address", ResourceType = typeof(Base.Resources.Resources))]
        public virtual string address_address { get; set; }
        [Display(Name = "Birthday", ResourceType = typeof(JIC.Base.Resources.Resources))]
        [StrDateBeforeToday]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}",ApplyFormatInEditMode = true)]
        [RegularExpression(@"^(3[01]|[12][0-9]|0[1-9])[-/](1[0-2]|0[1-9])[-/][0-9]{4}$", ErrorMessage = "تاريخ الميلاد غير صحيح")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]

        public virtual string BirthDate { get; set; }
        public DateTime? BirthDateT { get; set; }

        [Display(Name = "Phone", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public virtual string PhoneNo { get; set; }

        public bool PhoneNoRequired { get; set; } = false;

        public DateTime? GetBirthDate()
        {
            DateTime output = DateTime.Now;
            CultureInfo provider = CultureInfo.InvariantCulture;
            if (DateTime.TryParseExact(BirthDate, JIC.Base.SystemConfigurations.DateTime_ShortDateFormat, provider, DateTimeStyles.None, out output))
            {
                return output;
            }
            return null;
            //return BirthDate;
        }
        public DateTime? CBirthDate
        {
            get { return GetBirthDate(); }
            set
            {
                BirthDate = (value.HasValue ? value.Value.ToString(Base.SystemConfigurations.DateTime_ShortDateFormat) : null);
                //BirthDate = value;
            }
        }

        internal static vw_PersonData Map(vw_CaseDefectsData caseParty)
        {
            return new vw_PersonData
            {
                address_address = caseParty.Address,
                address_CityID = caseParty.CityID,
                address_PoliceStationID = caseParty.PoliceStationID,
                //CBirthDate = caseParty.Birthdate,
                ID = caseParty.PersonID,
                Job = caseParty.JobName,
                Name = caseParty.Name,
                NatNo = caseParty.NationalID,
                PassportNo = caseParty.PassportNumber,
                PhoneNo = caseParty.PhoneNo,
                NationalityID = caseParty.NationalityType,
                CBirthDate=caseParty.Birthdate,
                

            };
        }
        internal static vw_PersonData Map(Base.Views.vw_PersonData caseParty)
        {
            return new vw_PersonData
            {
                address_address = caseParty.address.address,
                address_CityID = caseParty.address.CityID,
                address_PoliceStationID = caseParty.address.PoliceStationID,
                //CBirthDate = caseParty.BirthDate.Value.Date,
                
                ID = caseParty.ID,
                Job = caseParty.Job,
                Name = caseParty.Name,
                NatNo = caseParty.NatNo,
                PassportNo = caseParty.PassportNo,
                PhoneNo = caseParty.PhoneNo,
                BirthDate = (caseParty.BirthDate.HasValue) ? caseParty.BirthDate.Value.ToShortDateString() : "",
                Age = caseParty.Age

            };
        }
        private int CalculateAge(DateTime birthdate)
        {
            // Save today's date.
            var today = DateTime.Today;
            // Calculate the age.
            var age = today.Year - birthdate.Year;
            // Go back to the year the person was born in case of a leap year
            if (birthdate > today.AddYears(-age)) age--;
            return age;
        }

        internal Base.Views.vw_PersonData ToPersonData()
        {
            return new Base.Views.vw_PersonData
            {
                address = new vw_Address { address = address_address, CityID = address_CityID, PoliceStationID = address_PoliceStationID },
                BirthDate = CBirthDate,
                ID = ID,
                Job = Job,
                Name = Name,
                NationalityID = NationalityID,
                NatNo = NatNo,
                PassportNo = PassportNo,
                PhoneNo = PhoneNo,
                CleanFullName = Base.Utilities.RemoveSpaces(Base.Utilities.RemoveSpecialCharacters(Name)),
            };
        }
    }

}
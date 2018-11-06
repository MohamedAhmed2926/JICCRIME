using JIC.Base;
using JIC.Base.Views;
using JIC.Crime.View.Helpers;
using JIC.Crime.View.Models;
using JIC.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace JIC.Crime.View.Controllers
{
    [CAuthorize(SystemUserTypes.Secretary, SystemUserTypes.ElementaryCourtAdministrator, SystemUserTypes.Judge)]

    public class AttendanceController : ControllerBase
    {
        private IDefectsService DefectsService;
        private ISessionService SessionService;
        private ILookupService LookupService;
        private ITextPredectionsService TextPredectionsService;
        private ICrimeCaseService CaseService;
        private ICircuitMembersService CircuitMembersService;
        private IWitnessesService WitnessesService;

        public AttendanceController(IWitnessesService WitnessesService,IDefectsService DefectsService, ISessionService SessionService, ILookupService LookupService, ITextPredectionsService TextPredectionsService, ICrimeCaseService CaseService, ICircuitMembersService CircuitMembersService)
        {
            this.DefectsService = DefectsService;
            this.SessionService = SessionService;
            this.LookupService = LookupService;
            this.TextPredectionsService = TextPredectionsService;
            this.CaseService = CaseService;
            this.CircuitMembersService = CircuitMembersService;
            this.WitnessesService = WitnessesService;
        }

        private List<vw_KeyValue>  GetWitnessesAttendanceTypes()
        {

            return new List<vw_KeyValue>
            {
                 new vw_KeyValue(0 ,JIC.Base.Resources.Resources.NotAttended ),
                 new vw_KeyValue(1 ,JIC.Base.Resources.Resources.Attended)
            };
        }
        private MinutesOfSessionCreateViewModel PrepareViewModel(MinutesOfSessionViewModel MinutesOfSession = null)
        {
            if (MinutesOfSession == null)
            {
                MinutesOfSession = new MinutesOfSessionViewModel();
            }
            MinutesOfSessionCreateViewModel CreateViewObj = new MinutesOfSessionCreateViewModel();
          
           var caseWitnesses = WitnessesService.GetWitnessesByCaseID(MinutesOfSession.CaseID);
   
                var attendanctype = LookupService.GetLookupsByCategory(Base.LookupsCategories.PresenceStatuses);
            var defendents = DefectsService.GetDefectsByCaseID(MinutesOfSession.CaseID, MinutesOfSession.SessionID);

            var result = new MinutesOfSessionCreateViewModel()
            {
                CaseDefectsData = defendents
                      .Select(def => new CaseDefectsDataViewModel
                      {
                          ID = def.ID,
                          Address = def.Address,
                          Age = CalculateAge(def.Birthdate.Value),
                          Birthdate = def.Birthdate,
                          CaseID = def.CaseID,
                          DefectID = def.ID,
                          DefectType = def.DefectType,
                          IsCivilRightProsecutor = def.IsCivilRightProsecutor,
                          JobName = def.JobName,
                          Name = def.Name,
                          NationalID = def.NationalID,
                          NationalityType = def.NationalityType,
                          Nationality=def.Nationality,
                          Order = def.Order,
                          PassportNumber = def.PassportNumber,
                          PersonID = def.PersonID,
                          Presence = def.Presence,
                          Status = def.Status,
                          AtendanceType = attendanctype
                          .Select(attendance => new SelectListItem
                          {
                              Text = attendance.Name,
                              Value = attendance.ID.ToString(),
                              Selected = ((int)def.Presence == attendance.ID),

                          }).ToList(),
                      }).Where(e => e.DefectType == PartyTypes.Defendant).OrderBy(defect => defect.Order).ToList(),

                CaseVictims = defendents
                      .Select(def => new CaseDefectsDataViewModel
                      {
                          ID = def.ID,
                          Address = def.Address,
                          Age = CalculateAge(def.Birthdate.Value),
                          Birthdate = def.Birthdate,
                          CaseID = def.CaseID,
                          DefectID = def.ID,
                          DefectType = def.DefectType,
                          IsCivilRightProsecutor = def.IsCivilRightProsecutor,
                          JobName = def.JobName,
                          Name = def.Name,
                          NationalID = def.NationalID,
                          NationalityType = def.NationalityType,
                          Order = def.Order,
                          PassportNumber = def.PassportNumber,
                          PersonID = def.PersonID,
                          Presence = def.Presence,
                          Status = def.Status,
                          Nationality=def.Nationality,
                          AtendanceType = attendanctype
                          .Select(attendance => new SelectListItem
                          {
                              Text = attendance.Name,
                              Value = attendance.ID.ToString(),
                              Selected = ((int)def.Presence == attendance.ID),

                          }).ToList(),
                      }).Where(e => e.DefectType == PartyTypes.Victim).OrderBy(defect => defect.Order).ToList(),

              //  AutoCompleteText = GetTextPrediction(MinutesOfSession.CrimeTypeID),

                MinutesOfSession = MinutesOfSession,
                SessionData = SessionService.GetSessionData(MinutesOfSession.SessionID),
             //   CaseBasicData = CaseService.GetCaseBasicData(MinutesOfSession.CaseID),
            //    vw_CircuitsGrid = CircuitMembersService.GetCircuitMembersByCircuitID(MinutesOfSession.CircuitID),

            };
            result.MinutesOfSession.Text = SessionService.GetMinutesOfSession(MinutesOfSession.SessionID);

          //  result WitnessesAttendanceTypes = GetWitnessesAttendanceTypes();
            result.CaseWitnesses = caseWitnesses
                  .Select(W => new CaseDefectsDataViewModel
                  {
                      ID = W.ID,
                      Address = W.Address,
                      Age = CalculateAge(W.Birthdate.Value),
                      Birthdate = W.Birthdate,
                      CaseID = W.CaseID,
                      JobName = W.JobName,
                      Name = W.Name,
                      NationalID = W.NationalID,
                      NationalityType = W.NationalityType,
                      Nationality = W.Nationality,
                      Order = W.Order,
                      PassportNumber = W.PassportNumber,
                      PersonID = W.PersonID,
                      Presence = W.Presence,
                      Status = W.Status,
                      AtendanceType = GetWitnessesAttendanceTypes()
                      .Select(attendance => new SelectListItem
                      {
                          Text = attendance.Name,
                          Value = attendance.ID.ToString(),
                          Selected = ((int)W.Presence == 0),

                      }).ToList(),
                  }).ToList();
            return result;
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

        // GET: Attendance
        [HttpGet]
        public ActionResult Index(int id, int SessionID, int CircuitID/*, bool SaveAttendance = false, bool SavedBefore = false*/)
        {
            MinutesOfSessionViewModel MinutesOfSession = new MinutesOfSessionViewModel();
            //  MinutesOfSession.SaveAttendance = SaveAttendance;
            //  MinutesOfSession.SavedBefore = SavedBefore;
            MinutesOfSession.CaseID = id;
            MinutesOfSession.SessionID = SessionID;
            MinutesOfSession.CircuitID = CircuitID;
            MinutesOfSession.CrimeTypeID = CaseService.GetCaseBasicData(id).CrimeTypeID;
            MinutesOfSessionCreateViewModel CreateViewModel = PrepareViewModel(MinutesOfSession);
            foreach (var defent in CreateViewModel.CaseDefectsData)
            {
                string[] li = Regex.Split(defent.Address, "/");
                if (li[0] != null)
                    defent.Address = li[0];
            }
            foreach (var defent in CreateViewModel.CaseVictims)
            {
                string[] li = Regex.Split(defent.Address, "/");
                if (li[0] != null)
                    defent.Address = li[0];
            }

            foreach (var defent in CreateViewModel.CaseWitnesses )
            {
                string[] li = Regex.Split(defent.Address, "/");
                if (li[0] != null)
                    defent.Address = li[0];
            }
            CreateViewModel = DayNameChange(CreateViewModel);

            return CPartialView(CreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(List<AtendanceViewModel> attendance, int CaseID, int SessionID, int CircuitID, DateTime SessioDate)
        {
            //ViewBag.Saved = false;
            MinutesOfSessionViewModel MinutesOfSession = new MinutesOfSessionViewModel();
            MinutesOfSession.CaseID = CaseID;
            MinutesOfSession.SessionID = SessionID;
            MinutesOfSession.CircuitID = CircuitID;
            MinutesOfSession.CrimeTypeID = CaseService.GetCaseBasicData(CaseID).CrimeTypeID;
            MinutesOfSessionCreateViewModel CreateViewModel = PrepareViewModel(MinutesOfSession);
            CreateViewModel = DayNameChange(CreateViewModel);
            foreach (var defendent in attendance.Where(e => e.DefectType == PartyTypes.Defendant))
            {
                var obj = CreateViewModel.CaseDefectsData.Single(e => e.ID == defendent.DefectID);
                obj.Presence = defendent.Presence;
                obj.AtendanceType = obj.AtendanceType
                    .Select(attendanc => new SelectListItem
                    {
                        Text = attendanc.Text,
                        Value = attendanc.Value.ToString(),
                        Selected =(((int)obj.Presence).ToString()==attendanc.Value)

                    }).ToList();
            }
            foreach (var defendent in attendance.Where(e => e.DefectType == PartyTypes.Victim))
            {
                var obj = CreateViewModel.CaseVictims.Single(e => e.ID == defendent.DefectID);
                obj.Presence = defendent.Presence;
                obj.AtendanceType= obj.AtendanceType
                   .Select(attendanc => new SelectListItem
                   {
                       Text = attendanc.Text,
                       Value = attendanc.Value.ToString(),
                       Selected = (((int)obj.Presence).ToString() == attendanc.Value)

                   }).ToList();

            }
            List<vw_CaseDefectsData> DefectsList = new List<vw_CaseDefectsData>();

            List<vw_CaseDefectsData> Vw_listDefendent = CreateViewModel.CaseDefectsData
               .Select(def => new vw_CaseDefectsData
               {
                   ID = def.ID,
                   Presence = def.Presence,
                   DefectType = def.DefectType
                   ,
                   Status = def.Status
               }).ToList();
            List<vw_CaseDefectsData> Vw_listVictims = CreateViewModel.CaseVictims
               .Select(def => new vw_CaseDefectsData
               {
                   ID = def.ID,
                   Presence = def.Presence,
                   DefectType = def.DefectType
                   ,
                   Status = def.Status
               }).ToList();

            DefectsList.AddRange(Vw_listDefendent);
            DefectsList.AddRange(Vw_listVictims);
            List<string> Defect = new List<string>();
            string DefendantsPresenceFailed = "";

            if (!attendance.Any(e => e.Presence == 0))
            {
                var SaveDefects = DefectsService.UpdatePresenceOfDefects(DefectsList, SessionID, out Defect);
                if (Defect.Count() != 0)
                {
                    foreach (var name in Defect)
                        DefendantsPresenceFailed += " / "+ name +" ";
                }
                if (SaveDefects == Base.SaveDefectsStatus.Saved)
                {
                    //CreateViewModel = PrepareViewModel(MinutesOfSession);

                    ViewBag.Saved = true;
                    return CPartialView("_AttendanceForm", CreateViewModel).WithSuccessMessages("تمت العملية بنجاح");
                }
                else if (SaveDefects == Base.SaveDefectsStatus.DefendantsPresenceFailed)
                {
                    return CPartialView("_AttendanceForm", CreateViewModel).WithErrorMessages(  "تم اثبات حضور المتهم"+DefendantsPresenceFailed+"من قبل  "  );
                }
                else if (SaveDefects == Base.SaveDefectsStatus.SessionSentToJudge)
                {
                    return CPartialView("_AttendanceForm", CreateViewModel).WithErrorMessages("لا يمكن تعديل بيانات الحضور للخصم بالقضية بعد إرسال المحضر للقاضى للتصديق");
                }
                else if (SaveDefects == Base.SaveDefectsStatus.Saved_Before)
                {
                    CreateViewModel = PrepareViewModel(MinutesOfSession);
                    //    ViewBag.Saved = true;
                    return CPartialView("_AttendanceForm", CreateViewModel).WithErrorMessages("برجاء تغيير الدباجة");
                }
                else
                {
                    return CPartialView("_AttendanceForm", CreateViewModel).WithErrorMessages("لم تتم العملية ");
                }
            }
           

            return CPartialView("_AttendanceForm", CreateViewModel).WithErrorMessages("برجاء اختيار اثبات الحضور");
        }
     
        public MinutesOfSessionCreateViewModel DayNameChange(MinutesOfSessionCreateViewModel _AllViewModel)
        {
            switch (_AllViewModel.SessionData.SessionDate.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    _AllViewModel.SessionData.DayName = "السبت";
                    break;
                case DayOfWeek.Sunday:
                    _AllViewModel.SessionData.DayName = "الأحد";
                    break;
                case DayOfWeek.Monday:
                    _AllViewModel.SessionData.DayName = "الاثنين";
                    break;
                case DayOfWeek.Tuesday:
                    _AllViewModel.SessionData.DayName = "الثلاثاء";
                    break;
                case DayOfWeek.Wednesday:
                    _AllViewModel.SessionData.DayName = "الاربعاء";
                    break;
                case DayOfWeek.Thursday:
                    _AllViewModel.SessionData.DayName = "الخميس";
                    break;
                case DayOfWeek.Friday:
                    _AllViewModel.SessionData.DayName = "الجمعة";
                    break;

            }

            return _AllViewModel;
        }


    }
}
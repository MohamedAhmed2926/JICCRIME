using JIC.Base;
using JIC.Services.ServicesInterfaces;
using JIC.Crime.View.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Microsoft.AspNet.Identity.Owin;
using JIC.Base.Views;
using Microsoft.AspNet.Identity;
using JIC.Crime.View.Helpers;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace JIC.Crime.View.Controllers
{
    [HandleError]
    public class UserController : ControllerBase
    {
        private Services.UserService _userService;
        private Services.UserService UserService { get
            {
                if (_userService == null)
                    _userService = ServiceUserService();
                return _userService;
            }
        }
        private ILookupService lookupService;
        private IPersonService PersonService;
        private IUserService SecurityUserService;

        public UserController( ILookupService lookupService, IPersonService PersonService, IUserService SecurityUserService)
        {
            this.lookupService = lookupService;
            this.PersonService = PersonService;
            this.SecurityUserService = SecurityUserService;
        }

        private UserCreateViewModel GetUserViewModel(vw_UserDataModel UserData = null, UserPersonViewModel PersonData = null)
        {
            if (UserData == null)
                UserData = new vw_UserDataModel();
            if (PersonData == null)
                PersonData = new UserPersonViewModel();
            return new UserCreateViewModel
            {
                UserData = UserData,
                PersonData = new PersonViewModel(PersonData, lookupService, PersonService,PersonData.ID != 0 ? Modes.Update : Modes.Add),
                UserTypes = lookupService.GetUserTypes(),
                Courts = lookupService.GetCourts(),
                JudgeLevels = lookupService.GetLookupsByCategory(LookupsCategories.JudgLevel),
                Prosecutions = lookupService.GetProsecutions(UserData.CourtID.HasValue ? UserData.CourtID.Value : IsAuthenticatied ? CurrentUser.CourtID : null).Select(pros => new Base.Views.vw_KeyValue
                {
                    ID = pros.ID,
                    Name = pros.Name
                }).ToList(),
                
            };
        }

        private static void CleanUserData(vw_UserDataModel UserData)
        {
            switch (UserData.UserType.Value)
            {
                case (SystemUserTypes.ElementaryCourtAdministrator):
                case (SystemUserTypes.InitialCourtAdministrator):
                case (SystemUserTypes.InquiriesEmployee):
                case (SystemUserTypes.Secretary):
                    UserData.ProsecutionID = null;
                    UserData.UserJudgeLevel = null;
                    break;

                case (SystemUserTypes.Judge):
                case (SystemUserTypes.CourtHead):
                    UserData.ProsecutionID = null;
                    break;
                case SystemUserTypes.JICAdmin:
                    UserData.ProsecutionID = null;
                    UserData.CourtID = null;
                    UserData.UserJudgeLevel = null;
                    break;
                case SystemUserTypes.schedualEmployee:
                case SystemUserTypes.ImplementationEmployee:
                    UserData.UserJudgeLevel = null;
                    break;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            try
            {
               ViewBag.ReturnUrl = returnUrl;
            }
            catch (Exception ex)
            {
                return ErrorPage(ex);
            }
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
          
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            Base.SignInStatus result = await UserService.PasswordSignIn(model.UserName, model.Password, false);
             var User = await UserManager.FindByNameAsync(model.UserName);

            var user = SecurityUserService.FindUserByID(User.Id);


            switch (result)
            {
                case Base.SignInStatus.Success:
                    if (!UserService.CanAccess(model.UserName))
                    {

                        //  ModelState.AddModelError("", "لا يمكن تسجيل الدخول لأنه لم يتم تعيين هذا الحساب إلى دائرة حتى الآن");
                        //return JavaScript("$(document).trigger('User:CannotAccess');");
                        return RedirectTo(Url.Action("Login", new { returnUrl = "/" })).WithErrorMessages("لا يمكن تسجيل الدخول لأنه لم يتم تعيين هذا الحساب إلى دائرة حتى الآن");

                        //return View(model);
                    }

                    else
                    {
                        return RedirectToLocal(returnUrl);
                    }
                case Base.SignInStatus.LockedOut:
                    return View("Lockout");
                case Base.SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl,  model.RememberMe });
                case Base.SignInStatus.Blocked:
                    ModelState.AddModelError("",JIC.Base.Resources.Messages.NotAuthorizedAccess);
                    return View(model);
                case Base.SignInStatus.PasswordNeedChange:
                    if (!UserService.CanAccess(model.UserName))
                    {
                        //return JavaScript("$(document).trigger('User:CannotAccess');");

                        //ModelState.AddModelError("", "لا يمكن تسجيل الدخول لأنه لم يتم تعيين هذا الحساب إلى دائرة حتى الآن");
                        return RedirectTo(Url.Action("Login",new {returnUrl="/"})).WithErrorMessages("لا يمكن تسجيل الدخول لأنه لم يتم تعيين هذا الحساب إلى دائرة حتى الآن");
                    }
                    else
                    {
                        return RedirectToAction("ChangePassword");
                    }
                case Base.SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "إسم المستخدم أو كلمة المرور غير صحيحة لديك"+(5- user.AccessFailedCount)+" محاولات  ");
                    return View(model);
            }

             
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }


        #region Index
        // GET: User
        public ActionResult Index()
        {
            if (CurrentUser != null)
            { return View(); }
            else
            {
                return RedirectTo(Url.Action("login", "User", new { returnUrl = "/" })).WithErrorMessages("تم الخروج بشكل تلقائى لعدم التفاعل اكثر من 15 دقيقة");
            }

        }
        [HttpGet]
        public ActionResult UserGrid()
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                var Users = UserService.GetUsers(IsAuthenticatied ? CurrentUser.CourtID : null).Select(user => new UserViewModels
            {
                ID = user.ID,
                PhoneNo = user.PhoneNo,
                UserName = user.UserName,
                UserType = user.UserType,
                Locked = !user.Active
            }).OrderBy(user => user.ID);
            // Only grid string query values will be visible here.
            return PartialView("_UserGrid", Users);
            }
            else
            {

                ViewData["SessionEnded"] = true;
                return PartialView();
            }
        }
        #endregion
        #region CreateUser

        [CAuthorize(SystemUserTypes.JICAdmin)]
        // GET: User/Create
        public ActionResult Create()
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                UserCreateViewModel viewModel = GetUserViewModel();
            return PartialView(viewModel);
            }
            else
            {

                ViewData["SessionEnded"] = true;
                return PartialView();
            }
        }

        // POST: User/Create
        [CAuthorize(SystemUserTypes.JICAdmin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(string submit,vw_UserDataModel UserData, UserPersonViewModel PersonData)
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                UserCreateViewModel viewModel = null;
            try
            {
                using (var Transaction = DataContext.Database.BeginTransaction())
                {
                    if (!ModelState.IsValid)
                    {
                        KeyValuePair<String, ModelState> UserName = ModelState.FirstOrDefault(m => m.Key == "UserData.UserName");
                        if (UserName.Value.Errors.Count() != 0)
                        {
                            string errorUserName = UserName.Value.Errors.Select(e => e.ErrorMessage).FirstOrDefault().ToString();
                            if (errorUserName == "أسم المستخدم موجود من قبل")
                                return JavaScript("$(document).trigger('User:UsernameExist2')");
                        }
                        
                            KeyValuePair<String, ModelState> PhoneNo = ModelState.FirstOrDefault(m => m.Key == "UserData.PhoneNo");
                        if (PhoneNo.Value.Errors.Count() != 0)
                        {
                            string errorPhoneNo = PhoneNo.Value.Errors.Select(e => e.ErrorMessage).FirstOrDefault().ToString();
                            if (errorPhoneNo == "رقم التليفون المحمول موجود من قبل")
                                return JavaScript("$(document).trigger('User:PhoneNoExist2')");
                        }
                       // KeyValuePair<String, ModelState> PassportNo = ModelState.FirstOrDefault(m => m.Key == "PersonData.PersonData.PassportNo");
                       // string errorPassportNo = PassportNo.Value.Errors.Select(e => e.ErrorMessage).First().ToString();
                       //     return CPartialView(GetUserViewModel(UserData)).WithErrorMessages("إسم المستخدم موجود من قبل");
                        return PartialView(GetUserViewModel(UserData));
                    }
                    if (UserService.IsPassporeExist(PersonData.PassportNo, UserData.ID))
                        return JavaScript("$(document).trigger('User:PassportNoExist2')");

                    CleanUserData(UserData);
                    var vw_PersonData = GetPersonData(PersonData);

                    long PersonID = PersonData.ID;
                    if (vw_PersonData.ID == 0)
                        PersonService.AddPerson(vw_PersonData, out PersonID);
                    else
                        PersonService.EditPerson(vw_PersonData);
                    vw_PersonData.ID = PersonID;
                    var Result = await UserService.CreateUserAsync(vw_PersonData.ID, vw_PersonData.Name, UserData);
                    if (Result.Succeeded)
                    {
                        Transaction.Commit();
                        switch (submit)
                        {
                            case "حفظ و إضافة مستخدم جديد":
                                return JavaScript("$(document).trigger('User:ReopenModel')");

                          
                            case " حفظ و غلق":
                                return JavaScript("$(document).trigger('User:SaveSuccefull')");
                        }
                    }
                    else
                    {
                        this.AddErrors(Result);
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                DataContext.Database.CurrentTransaction.Rollback();
                return ErrorPage(ex);

            }

            catch (Exception ex)
            {
                DataContext.Database.CurrentTransaction.Rollback();
                return ErrorPage(ex);
            }
            if(viewModel == null)
                viewModel = GetUserViewModel(UserData, PersonData);
           
            return PartialView(viewModel);
            }
            else
            {

                ViewData["SessionEnded"] = true;
                return PartialView();
            }
        }

        #endregion
        #region Edit

        [CAuthorize(SystemUserTypes.JICAdmin)]
        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                var User = UserService.FindUserByID(id);
            vw_UserDataModel userModel = null;
            UserPersonViewModel personData = null;
            if (User != null)
            {
                userModel = GetUserModel(User);
                if (User.PersonID.HasValue)
                {
                    Base.Views.vw_PersonData person = PersonService.GetPerson(User.PersonID.Value);
                    personData = new UserPersonViewModel
                    {
                        CBirthDate = person.BirthDate,
                        Job = person.Job,
                        Name = person.Name,
                        NationalityID = person.NationalityID,
                        NatNo = person.NatNo,
                        PassportNo = person.PassportNo,
                        PhoneNo = User.PhoneNo,
                        address_address = person.address.address,
                        address_CityID = person.address.CityID,
                        address_PoliceStationID = person.address.PoliceStationID,
                        ID = person.ID,
                        BirthDateT=person.BirthDate,
                    };
                }

            }
            string[] li = Regex.Split(personData.address_address.ToString(), "/");
            if (li[0] != null)
                personData.address_address = li[0];
            if (li[1] != null)
                personData.address_CityID = int.Parse(li[1]);
            if (li[2] != null)
                personData.address_PoliceStationID = int.Parse(li[2]);
            personData.Age = CalculateAge(personData.BirthDateT.Value);

            return PartialView(GetUserViewModel(userModel, personData));
        }
            else
            {

                ViewData["SessionEnded"] = true;
                return PartialView();
    }
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

        // POST: User/Edit/5
        [CAuthorize(SystemUserTypes.JICAdmin)]
        [HttpPost]
        public ActionResult Edit(int id, [Bind(Prefix = "Edit.UserData")]vw_UserDataModel UserData, [Bind(Prefix = "Edit.PersonData")]UserPersonViewModel PersonData)
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                if (!ModelState.IsValid)
            {
                KeyValuePair<String, ModelState> birthday = ModelState.FirstOrDefault(m => m.Key == "Edit.UserData.UserName");
                string error = birthday.Value.Errors.Select(e => e.ErrorMessage).First().ToString();
                if (error == "هذا الحقل مطلوب."||error== "هذا الحقل يجب ان يحتوى على حروف وأرقام فقط")
                    return JavaScript("$(document).trigger('User:UserWrong3')");

                var Error = ModelState.Values;
                return CPartialView(GetUserViewModel(UserData, PersonData));
            }
            using (var Transaction = DataContext.Database.BeginTransaction())
            {
                var PersonResultStatus = PersonService.EditPerson(PersonData.ToPersonData());
                vw_UserData userData = UserData.ToVwUserData();
                userData.FullName = PersonData.Name;
                userData.PersonID = PersonData.ID;
                var UserResultStatus = UserService.UpdateUser(id, userData);
                if (UserResultStatus == UserStatus.AddSuccess && PersonResultStatus == PersonStatus.SuccefullSave)
                {
                    Transaction.Commit();
                    return JavaScript("$(document).trigger('User:UpdateSuccefull')");
                }
            }
            return CPartialView(GetUserViewModel(UserData, PersonData));

            }
            else
            {

                ViewData["SessionEnded"] = true;
                return PartialView();
            }
        }

        #endregion
        #region Delete

        // GET: User/Delete/5
        [CAuthorize(SystemUserTypes.JICAdmin)]
        public ActionResult Delete(int id)
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                var User = UserService.FindUserByID(id);
            vw_UserDataModel userData = GetUserModel(User);
            return PartialView(userData);
        }
            else
            {

                ViewData["SessionEnded"] = true;
                return PartialView();
    }
}

        // POST: User/Delete/5
        [CAuthorize(SystemUserTypes.JICAdmin)]
        [HttpPost]
        public ActionResult Delete(int id,FormCollection formCollection)
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                if (id != CurrentUser.ID)
            {
               DeleteUserStatus DUS= UserService.DeleteUser(id);
                if (DUS == DeleteUserStatus.Succeeded)
                {
                    UserService.DeleteUser(id);
                    ViewBag.OperationSuccess = true;
                    return PartialView(new vw_UserDataModel());
                }
                else if(DUS == DeleteUserStatus.IsSecretary )
                {
                    ModelState.AddModelError("", "لا يمكن حذف مستخدم مربوط بأى عملية على النظام");
                    return Delete(id);
                }
                else if (DUS == DeleteUserStatus.IsMember )
                {
                    ModelState.AddModelError("", "لا يمكن حذف مستخدم مربوط بأى عملية على النظام");
                    return Delete(id);
                }
                else if (DUS == DeleteUserStatus.Failed )
                {
                    ModelState.AddModelError("", "لم تتم العمليه ");
                    return Delete(id);
                }
            }
            ModelState.AddModelError("", "لا يمكن حذف المستخدم الحالى");
            return Delete(id);
            }
            else
            {

                ViewData["SessionEnded"] = true;
                return PartialView();
            }
        }
        #endregion
        #region ChangePassword
        [CAuthorize]
        [HttpGet]
        public ActionResult ChangePassword()
        {
            if (CurrentUser != null)
            {
                return View();
            }
            else
            {
                return RedirectTo(Url.Action("login", "User", new { returnUrl = "/" })).WithErrorMessages("تم الخروج بشكل تلقائى لعدم التفاعل اكثر من 15 دقيقة");
            }
        }

        [CAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(vw_UserChangePassword changePasswordModel)
        {
            if (CurrentUser != null)
            {
                if (ModelState.IsValid)
            {
                var Result = await UserService.UpdatePasswordAsync(CurrentUser, changePasswordModel.Password);
                if (Result.Succeeded)
                    return RedirectToAction("Index", "Home");
                AddErrors(Result);
            }

            return View(changePasswordModel);
            }

            else
            {
                return RedirectTo(Url.Action("login", "User", new { returnUrl = "/" })).WithErrorMessages("تم الخروج بشكل تلقائى لعدم التفاعل اكثر من 15 دقيقة");
            }
        }
        #endregion

        [CAuthorize(SystemUserTypes.JICAdmin)]
        public ActionResult Lock(int id)
        {
            var User = UserService.FindUserByID(id);
            vw_UserDataModel userData = GetUserModel(User);
            return PartialView(userData);
        }
        [CAuthorize(SystemUserTypes.JICAdmin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Lock(int id, FormCollection formCollection)
        {
          var result=  UserService.DeActivateUser(id);
          
            var User = UserService.FindUserByID(id);
            vw_UserDataModel userData = GetUserModel(User);

            if (result == Deactive.Deactive)
                ViewBag.OperationSuccess = true;

            else if (result == Deactive.CannotDeactive)

                return CPartialView(userData).WithSuccessMessages("المستخدم ضمن تشكيل حالي لدائرة معينة ولا يجوز تعطيله");
            else
            {
                return CPartialView(userData).WithSuccessMessages("لم تتم العملية بنجاح");

            }
            return PartialView(userData);
        }
        public ActionResult UnLock(int id)
        {
            var User = UserService.FindUserByID(id);
            vw_UserDataModel userData = GetUserModel(User);
            return PartialView(userData);
        }
        [CAuthorize(SystemUserTypes.JICAdmin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UnLock(int id, FormCollection formCollection)
        {
            UserService.ActivateUser(id);
            var User = UserService.FindUserByID(id);
            vw_UserDataModel userData = GetUserModel(User);
            ViewBag.OperationSuccess = true;
            var user = SecurityUserService.FindUserByID(id);
            user.AccessFailedCount=0;
            SecurityUserService.UpdateUser(user.ID, user);
            return PartialView(userData);
        }
        public ActionResult GetProsecution(int CourtID,int prosecutionID = 0,string Prefix = "")
        {
            if (string.IsNullOrEmpty(Prefix))
                Prefix = "UserData";
            else
                Prefix = Prefix + ".UserData";
            ViewData.TemplateInfo.HtmlFieldPrefix = Prefix;
            var Prosecutions = lookupService.GetProsecutions(CourtID);
            var ProsecutionsList = new SelectList(Prosecutions, "ID", "Name", prosecutionID);
            return CPartialView("_ddlProsecution", new ProsectuionModel { Prosecutions = ProsecutionsList,ProsecutionID = prosecutionID});
        }



        //
        // POST: /Account/LogOff
        [CAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        #region Helpers

        private static Base.Views.vw_PersonData GetPersonData(UserPersonViewModel PersonData)
        {
            return new Base.Views.vw_PersonData
            {
                ID = PersonData.ID,
                Job = PersonData.Job,
                BirthDate = PersonData.GetBirthDate(),
                Name = PersonData.Name,
                NationalityID = PersonData.NationalityID,
                NatNo = PersonData.NatNo,
                PassportNo = PersonData.PassportNo,
                address = (PersonData.address_address != null ? new Base.Views.vw_Address { address = PersonData.address_address, CityID = PersonData.address_CityID, PoliceStationID = PersonData.address_PoliceStationID } : null),
                CleanFullName = Base.Utilities.RemoveSpaces(Base.Utilities.RemoveSpecialCharacters(PersonData.Name)),
                
            };
        }


        private static vw_UserDataModel GetUserModel(vw_UserData User)
        {
            return new vw_UserDataModel
            {
                CourtID = User.CourtID,
                PhoneNo = User.PhoneNo,
                ProsecutionID = User.ProsecutionID,
                UserJudgeLevel = User.UserJudgeLevel,
                UserName = User.UserName,
                UserType = (SystemUserTypes)User.UserTypeID,
                ID = User.ID
            };
        }
        #endregion
    }
}

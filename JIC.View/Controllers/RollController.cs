using JIC.Base;
using JIC.Base.Resources;
using JIC.Services.ServicesInterfaces;
using JIC.Base.Views;
using JIC.Crime.View.Helpers;
using JIC.Crime.View.Models;
using JIC.Crime.View.TestInterfaces;
using JIC.Crime.View.TestService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JIC.Crime.View.Controllers
{
    [CAuthorize(SystemUserTypes.Secretary)]

    public class RollController : ControllerBase
    {
        private IUserService userService;
        private IRollService RollService;
        private ICircuitService CircuitService;
        private ICircuitMembersService CircuitMembersService;
        public RollController(IUserService userService, IRollService RollService, ICircuitService CircuitService, ICircuitMembersService CircuitMembersService)
        {

            this.userService = userService;
            this.RollService = RollService;
            this.CircuitService = CircuitService;
            this.CircuitMembersService = CircuitMembersService;
        }

        private RollOrderViewModel PrepareViewModel(List<CaseOrderViewModel> CaseOrder = null, RollOrderCaseViewModel Roll = null)
        {
            
            if (CaseOrder == null)
                CaseOrder = new List<CaseOrderViewModel>();
            if (Roll == null)
                Roll = new RollOrderCaseViewModel();
            var Secretaries = userService.GetUserSecritary(Roll.CircuitID);
            return new RollOrderViewModel
            {
                CaseOrder = CaseOrder,
                Roll = Roll,
                ListUnableOrder = RollService.GetUnApprovedMovmentCases(Roll.RollID)
                       .Select(roll => new RollOrderCaseViewModel
                       {

                           CaseID = roll.CaseID,
                           CaseStatus = roll.CaseStatus,
                           OverAllNumber = roll.OverAllNumber,
                           FirstLevelNumber = roll.FirstLevelNumber,
                           SecondLevelNumber = roll.SecondLevelNumber,
                           MainCrime = roll.MainCrime,

                       }).ToList(),
                ListRollToOrder = RollService.GetRollCasesForOrdering(Roll.CircuitID, Roll.RollID)
              
                 .Select(roll => new RollOrderCaseViewModel
                 {
                     Order = roll.Order,
                     CaseID = roll.CaseID,
                     CaseStatus = roll.CaseStatus,
                     OverAllNumber = roll.OverAllNumber,
                     FirstLevelNumber = roll.FirstLevelNumber,
                     SecondLevelNumber = roll.SecondLevelNumber,
                     MainCrime = roll.MainCrime,
             SecretaryID = roll.SecretaryID, 
                    
                     UserSecritary = Secretaries
                       .Select(r => new SelectListItem
                       {
                           Text = r.Name,
                           Value = r.ID.ToString(),
                           Selected = (roll.SecretaryID == r.ID),
                       }).ToList()
                 }).ToList(),
            };


        }
        private List<RollOrderCaseViewModel> PrepareOrder(List<RollOrderCaseViewModel> Order)
        {
            List<RollOrderCaseViewModel> OrderWhithZero = new List<RollOrderCaseViewModel>();
            OrderWhithZero.AddRange(Order.Where(roll => roll.Order != 0).OrderBy(roll => roll.Order));

            int count = 1;
            foreach (var roll in OrderWhithZero)
            {
                roll.Order = count;
                count++;
            };
            return OrderWhithZero;

        }
        private List<RollOrderCaseViewModel> PrepareUnableOrder(List<RollOrderCaseViewModel> Order)
        {
            int count = 1;
            foreach (var order in Order)
            {
                order.Order = count;
                count++;
            }
            return Order;
        }

        private List<RollOrderCaseViewModel> PrepareBeforeOrder(List<RollOrderCaseViewModel> Order)
        {
            List<RollOrderCaseViewModel> OrderWhithZero = new List<RollOrderCaseViewModel>();
            OrderWhithZero.AddRange(Order.Where(roll => roll.Order == 0).OrderBy(roll => roll.Order));
            int count = 1;
            foreach (var roll in OrderWhithZero)
            {
                roll.Order = count;
                count++;
            };
            return OrderWhithZero;
        }
        // GET: Roll
        public ActionResult Index()
        {
            if (CurrentUser != null)
            {
                List<vw_KeyValue> Circuits = CircuitService.GetCircuitsBySecretairyID(CurrentUser.ID, null);
                RollOrderViewModel _AllViewModel = new RollOrderViewModel();
                _AllViewModel.Circuits = Circuits;

                RollOrderCaseViewModel Roll = new RollOrderCaseViewModel();
                Roll.CurentUserID = CurrentUser.ID;
                Roll.CourtID = CurrentUser.CourtID;
                _AllViewModel.Roll = Roll;

                return View(_AllViewModel);
            }
            else
            {
                return RedirectTo(Url.Action("login", "User", new { returnUrl = "/" })).WithErrorMessages("تم الخروج بشكل تلقائى لعدم التفاعل اكثر من 15 دقيقة");
            }
        }
        public ActionResult RollsReadyToOrder(int CircuitID)
        {
            List<vw_KeyValueDate> Rolls = RollService.GetCircuitRollDates(CircuitID, CurrentUser.ID).ToList();
          
            foreach (var st in Rolls)
            {
                st.StDate = st.Date.ToShortDateString();
            }
            return Json(Rolls, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]

        public ActionResult Create(int RollID, int CircuitID)
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                RollOrderCaseViewModel Roll = new RollOrderCaseViewModel()
                {
                    CircuitID = CircuitID,
                    RollID = RollID,
                    CourtID = CurrentUser.CourtID,
                    CurentUserID = CurrentUser.ID,
                };
                RollOrderViewModel _AllViewModel = PrepareViewModel(null, Roll);
                _AllViewModel.ListRollToOrder = PrepareOrder(_AllViewModel.ListRollToOrder);
                _AllViewModel.ListUnableOrder = PrepareUnableOrder(_AllViewModel.ListUnableOrder);
                _AllViewModel.ListRollBeforeOrder = PrepareBeforeOrder(_AllViewModel.ListRollToOrder);
                return PartialView(_AllViewModel);
            }
            else
            {
                ViewData["SessionEnded"] = true;
                return PartialView();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(List<CaseOrderViewModel> CaseOrder, int RollID, int CircuitID, bool saveBeforeOrder)
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                RollOrderCaseViewModel Roll = new RollOrderCaseViewModel()
                {
                    CircuitID = CircuitID,
                    RollID = RollID,
                    CourtID = CurrentUser.CourtID,
                    CurentUserID = CurrentUser.ID,
                };
                RollOrderViewModel _AllViewModel = PrepareViewModel(CaseOrder, Roll);
                _AllViewModel.ListRollToOrder = PrepareOrder(_AllViewModel.ListRollToOrder);
                _AllViewModel.ListUnableOrder = PrepareUnableOrder(_AllViewModel.ListUnableOrder);
                _AllViewModel.ListRollBeforeOrder = PrepareBeforeOrder(_AllViewModel.ListRollToOrder);

                try
                {
                    if (ModelState.IsValid)
                    {
                        List<vw_CaseOrder> vw_order = new List<vw_CaseOrder>();

                        if (saveBeforeOrder == false)
                        {
                            vw_order = CaseOrder
                     .Select(order => new vw_CaseOrder
                     {
                         CaseID = order.CaseID,
                         Order = order.Order,
                         SecritaryID = order.SecritaryID
                     }).ToList();
                        }
                        else if (saveBeforeOrder == true)
                        {
                            int count = _AllViewModel.ListRollToOrder.Count();
                            count++;
                            vw_order = CaseOrder
                          .Select(order => new vw_CaseOrder
                          {
                              CaseID = order.CaseID,
                              Order = count,
                              SecritaryID = order.SecritaryID
                          }).ToList();
                            foreach (var order in vw_order)
                            {
                                order.Order = count;
                                count++;
                            }
                        }

                        SaveRollOrderStatus saveRollOrderStatus = RollService.SaveRollOrder(Roll.RollID, vw_order);
                        if (saveBeforeOrder == false)
                        {
                            if (saveRollOrderStatus == Base.SaveRollOrderStatus.SuccessFull)
                            {
                                return CPartialView("_RollForm", _AllViewModel).WithSuccessMessages(JIC.Base.Resources.Messages.SuccessOrder);

                            }
                        }
                        else if (saveBeforeOrder == true)
                        {
                            if (saveRollOrderStatus == Base.SaveRollOrderStatus.SuccessFull)
                            {
                                return CPartialView("_RollForm", _AllViewModel).WithSuccessMessages("تم النقل لجدول الترتيب");
                                //  ViewBag.sccessMessage = true;
                            }
                        }

                        else if (saveRollOrderStatus == Base.SaveRollOrderStatus.RollOpened)
                        {
                            //  return RedirectJS(Url.Action("Index")).WithSuccessMessages(JIC.Base.Resources.Messages.RollOpened);
                            return CPartialView("_RollForm", _AllViewModel).WithSuccessMessages(JIC.Base.Resources.Messages.RollOpened);


                        }

                        else if (saveRollOrderStatus == Base.SaveRollOrderStatus.Failed)
                        {
                            return CPartialView("_RollForm", _AllViewModel).WithErrorMessages(JIC.Base.Resources.Messages.OperationNotCompleted);

                            //  ViewBag.sccessMessage = false;
                        }

                    }

                    return CPartialView("_RollForm", _AllViewModel).WithErrorMessages(JIC.Base.Resources.Messages.OperationNotCompleted);


                }
                catch (Exception ex)
                {
                    return ErrorPage(ex);
                }
            }
            else
            {
                ViewData["SessionEnded"] = true;
                return PartialView();
            }
        }

        public ActionResult BeforeOrder(int RollID, int CircuitID)
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                RollOrderCaseViewModel Roll = new RollOrderCaseViewModel()
                {
                    CircuitID = CircuitID,
                    RollID = RollID,
                    CourtID = CurrentUser.CourtID,
                    CurentUserID = CurrentUser.ID,
                };
                RollOrderViewModel _AllViewModel = PrepareViewModel(null, Roll);
                _AllViewModel.ListRollBeforeOrder = PrepareBeforeOrder(_AllViewModel.ListRollToOrder);

                return PartialView(_AllViewModel);
            }
            else
            {
                ViewData["SessionEnded"] = true;
                return PartialView();
            }
            }
        //[HttpGet]
        // public ActionResult IndexGrid(int RollID, int CircuitID)
        // {
        //     RollOrderCaseViewModel Roll = new RollOrderCaseViewModel()
        //     {
        //         CircuitID =CircuitID,
        //          RollID =RollID,
        //      };
        //     RollOrderViewModel _AllViewModel = PrepareViewModel(null, Roll);
        //     _AllViewModel.ListRollBeforeOrder = PrepareBeforeOrder(_AllViewModel.ListRollToOrder);

        //     // Only grid string query values will be visible here.
        //     return PartialView("_IndexGrid", _AllViewModel);
        // }
        public ActionResult AfterOrder(int RollID, int CircuitID)
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                RollOrderCaseViewModel Roll = new RollOrderCaseViewModel()
            {
                CircuitID = CircuitID,
                RollID = RollID,
                CourtID = CurrentUser.CourtID,
                CurentUserID = CurrentUser.ID,
            };
            RollOrderViewModel _AllViewModel = PrepareViewModel(null, Roll);
            _AllViewModel.ListRollToOrder = PrepareOrder(_AllViewModel.ListRollToOrder);
            return PartialView(_AllViewModel);
        }
          else
            {
                ViewData["SessionEnded"] = true;
                return PartialView();
    }
}


        public ActionResult CircuitMember(int CircuitID)
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                List<vw_CircuitsJudges> vw_CircuitsGrid = CircuitMembersService.GetCircuitMembersByCircuitID(CircuitID);
            RollOrderCaseViewModel members = new RollOrderCaseViewModel();
            members.JudgeCount = vw_CircuitsGrid.Count();
            foreach (var member in vw_CircuitsGrid)
            {
                if (member.JudgePodiumType == (int)JudgePodiumType.HeadJudge)
                    members.HeadJudge = member.JudgeName;
                if (member.JudgePodiumType == (int)JudgePodiumType.LeftJudge)
                    members.FirstJudge = member.JudgeName;
                if (member.JudgePodiumType == (int)JudgePodiumType.LeftLeftJudge)
                    members.ThirdJudge = member.JudgeName;
                if (member.JudgePodiumType == (int)JudgePodiumType.LeftLeftLeftJudge)
                    members.FourthJudge = member.JudgeName;
                if (member.JudgePodiumType == (int)JudgePodiumType.LeftLeftLeftLeftJudge)
                    members.FifthJudge = member.JudgeName;
                if (member.JudgePodiumType == (int)JudgePodiumType.LeftLeftLeftLeftLeftJudge)
                    members.SixthJudge = member.JudgeName;
                if (member.JudgePodiumType == (int)JudgePodiumType.OptionalJudge)
                    members.alternativeJudge = member.JudgeName;
                if (member.JudgePodiumType == (int)JudgePodiumType.RightJudge)
                    members.SecondJudge = member.JudgeName;
            }
            return PartialView(members);
            }
            else
            {
                ViewData["SessionEnded"] = true;
                return PartialView();
            }
        }
        
         
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JIC.Base;
using JIC.Services.ServicesInterfaces;
using JIC.Base.Views;
using JIC.Crime.View.Helpers;
using JIC.Crime.View.Models;

namespace JIC.Crime.View.Controllers
{
    [CAuthorize(SystemUserTypes.schedualEmployee, SystemUserTypes.ElementaryCourtAdministrator)]

    public class OrderOfAssignmentController : ControllerBase
    {
        private IOrderOfAssignment Order;
        public OrderOfAssignmentController(IOrderOfAssignment Order)
        {
            this.Order = Order;
        }
        [HttpGet]
        public ActionResult Create(int id)
        {
            OrderOfAssignmentViewModels OrderOfAssignment = new OrderOfAssignmentViewModels();

            vw_OrderOfAssignment _OrderOfAssignment = Order.GetOrderByID(id);
            if (_OrderOfAssignment!=null) {
               OrderOfAssignment = new OrderOfAssignmentViewModels()
                {
                    CaseID = id,
                    Description = _OrderOfAssignment.Description,

                };
            }
            else
            {
                OrderOfAssignment.CaseID = id;
                
            }
            
             
            return View(OrderOfAssignment);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderOfAssignmentViewModels OrderOfAssignment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    vw_OrderOfAssignment _OrderOfAssignment = new vw_OrderOfAssignment()
                    {
                        CaseID = OrderOfAssignment.CaseID,
                        Description = OrderOfAssignment.Description,
                      

                    };
                    if (Order.AddOrder(_OrderOfAssignment) == AddOrder.AddSuccefull)
                    {
                        return RedirectTo(Url.Action("Create", OrderOfAssignment.CaseID)).WithSuccessMessages(JIC.Base.Resources.Messages.OperationCompletedSuccessfully);
                    }
                    else
                    {
                        return CPartialView(OrderOfAssignment).WithErrorMessages(JIC.Base.Resources.Messages.OperationNotCompleted);

                    }


                }

                return View(OrderOfAssignment);
            }
            catch (Exception ex)
            {
                return ErrorPage(ex);
            }

        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                vw_OrderOfAssignment _OrderOfAssignment = Order.GetOrderByID(id);
                OrderOfAssignmentViewModels OrderOfAssignment = new OrderOfAssignmentViewModels()
                {
                    CaseID = id,
                    Description = _OrderOfAssignment.Description,
                    
                };

                return View(OrderOfAssignment);
            }
            catch (Exception ex)
            {
                return ErrorPage(ex);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrderOfAssignmentViewModels _OrderOfAssignment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    vw_OrderOfAssignment OrderOfAssignment = new vw_OrderOfAssignment()
                    {
                        CaseID = _OrderOfAssignment.CaseID,
                        Description = _OrderOfAssignment.Description,
                        
                    };
                    var EditOrderResult = Order.EditOrder(OrderOfAssignment);
                    switch (EditOrderResult)
                    {
                        case EditAssignment.NoUpdateOverAllNumberExist:
                            return CPartialView(_OrderOfAssignment).WithErrorMessages(JIC.Base.Resources.Messages.NoUpdateOverAllNumberExist);
                        case EditAssignment.NoUpdateThereIsASession:
                            return CPartialView(_OrderOfAssignment).WithErrorMessages(JIC.Base.Resources.Messages.NoUpdateThereIsASession);

                        case EditAssignment.EditSuccefull:
                            return RedirectTo(Url.Action("Edit", new { id = _OrderOfAssignment.CaseID })).WithSuccessMessages(JIC.Base.Resources.Messages.OperationCompletedSuccessfully);

                        case EditAssignment.FailedToEdit:
                            return CPartialView(_OrderOfAssignment).WithErrorMessages(JIC.Base.Resources.Messages.OperationNotCompleted);
                    }
                    
                }
                return View(_OrderOfAssignment);
            }
            catch (Exception ex)
            {
                return ErrorPage(ex);
            }
        }
      

    }
}
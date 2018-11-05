using JIC.Base;
using JIC.Services.ServicesInterfaces;
using JIC.Base.views;
using JIC.Base.Views;
using JIC.Crime.View.Helpers;
using JIC.Crime.View.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JIC.Crime.View.Controllers
{
    [CAuthorize(SystemUserTypes.Secretary)]
    public class TextPredectionsController : ControllerBase
    {
        private ITextPredectionsService TextPredectionsService;
        private ILookupService LookupService;
        private ICircuitService CircuitService;

        public TextPredectionsController(ITextPredectionsService TextPredectionsService, ILookupService LookupService, ICircuitService CircuitService)
        {
            this.TextPredectionsService = TextPredectionsService;
            this.LookupService = LookupService;
            this.CircuitService = CircuitService;
        }
        private TextPredectionsModels PrepareViewModel(TextPredectionsViewModels TextPredections = null)
        {

            if (TextPredections == null)
                TextPredections = new TextPredectionsViewModels();
            return new TextPredectionsModels
            {
                Circuits = CircuitService.GetCircuitsBySecretairyID(CurrentUser.ID)
                ,TextPredections = TextPredections
            };

        }

        // GET: Prosecutor
        public ActionResult Index()
        {
            if (CurrentUser != null)
            {
              
                try
                {
                    List<vw_KeyValue> list = CircuitService.GetCircuitsBySecretairyID(CurrentUser.ID);
                    //ViewBag.RedirectPage = null;
                    //if (TempData["SuccessMessage"] != null)
                    //    ShowMessage(MessageTypes.Success, (string)TempData["SuccessMessage"]);
                    //if (TempData["[FailedMessage"] != null)
                    //    ShowMessage(MessageTypes.Error, (string)TempData["[FailedMessage"]);
                    List<TextPredectionsViewModels> TextPredections = TextPredectionsService.GetTextPredections(list)
                        .Select(Text => new TextPredectionsViewModels
                        {
                            TextID = Text.TextID,
                            CircuitName = Text.CircuitName,
                            CircuitID = Text.CircuitID,
                            TextPredectionsDescription = Text.TextPredectionsDescription,
                            TextTitle = Text.TextTitle,
                        }).ToList();
                    return View(TextPredections);
                }
                catch (Exception ex)
                {
                    return ErrorPage(ex);
                }
            }
            else
            {
                return RedirectTo(Url.Action("login", "User", new { returnUrl = "/" })).WithErrorMessages("تم الخروج بشكل تلقائى لعدم التفاعل اكثر من 15 دقيقة");
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                try
                {
                    TextPredectionsModels TextCreate = PrepareViewModel();
                    return PartialView(TextCreate);
                }
                catch (Exception ex)
                {
                    return ErrorPage(ex);
                }
            }
            else
            {
                ViewData["SessionEnded"] = false;
                return PartialView();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TextPredectionsViewModels TextPredections)
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                try
            {
                if (ModelState.IsValid)
                {
                    vw_TextPredections vw_TextPredections = new vw_TextPredections
                    {
                        CircuitID = TextPredections.CircuitID,
                        TextTitle = TextPredections.TextTitle,
                        TextPredectionsDescription = TextPredections.TextPredectionsDescription,
                        CircuitName = TextPredections.CircuitName
                    };
                    if (TextPredectionsService.AddTextPredections(vw_TextPredections) == AddTextStatus.AddSuccefull)
                    {
                        return RedirectJS(Url.Action("Index")).WithSuccessMessages(JIC.Base.Resources.Messages.OperationCompletedSuccessfully);

                        //  TempData["SuccessMessage"] = JIC.Base.Resources.Messages.sucessAdd;

                    }
                    if (TextPredectionsService.AddTextPredections(vw_TextPredections) == AddTextStatus.SameTitle)
                    {
                        return CPartialView(PrepareViewModel(TextPredections)).WithErrorMessages(JIC.Base.Resources.Messages.SameName);

                   }
                    else
                    {
                        return CPartialView(PrepareViewModel(TextPredections)).WithErrorMessages(JIC.Base.Resources.Messages.OperationNotCompleted);

                     
                    }

                }

                return PartialView(PrepareViewModel(TextPredections));
            }
            catch (Exception ex)
            {
                return ErrorPage(ex);
            }
        }
            else
            {
                ViewData["SessionEnded"] = false;
                return PartialView();
    }
}
 
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                try
            {
                vw_TextPredections Text = TextPredectionsService.GetTextByID(id);
                TextPredectionsViewModels TextPredection = new TextPredectionsViewModels
                {
                    TextID = Text.TextID,
                    CircuitName = Text.CircuitName,
                    TextTitle = Text.TextTitle,
                    TextPredectionsDescription = Text.TextPredectionsDescription,
                    CircuitID = Text.CircuitID
                };
                return PartialView(PrepareViewModel(TextPredection));
            }
            catch (Exception ex)
            {
                return ErrorPage(ex);
            }
            }
            else
            {
                ViewData["SessionEnded"] = false;
                return PartialView();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(TextPredectionsViewModels TextPredections)
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                if (ModelState.IsValid)
            {
                vw_TextPredections Text = new vw_TextPredections
                {
                    CircuitID = TextPredections.CircuitID,
                    TextTitle = TextPredections.TextTitle,
                    TextPredectionsDescription = TextPredections.TextPredectionsDescription,
                    CircuitName = TextPredections.CircuitName,
                    TextID = TextPredections.TextID,
                };

                if (TextPredectionsService.EditText(Text)== EditTextStatus.EditSuccefull)
                {
                    return RedirectJS(Url.Action("Index")).WithSuccessMessages(JIC.Base.Resources.Messages.OperationCompletedSuccessfully);
                    // TempData["SuccessMessage"] = JIC.Base.Resources.Messages.EditSucess;

                }
                if (TextPredectionsService.EditText(Text)==EditTextStatus.SameTitle)
                {
                    return CPartialView(PrepareViewModel(TextPredections)).WithErrorMessages(JIC.Base.Resources.Messages.SameName);

                }
                else
                {
                    return CPartialView(PrepareViewModel(TextPredections)).WithErrorMessages(JIC.Base.Resources.Messages.OperationNotCompleted);

                    // return RedirectJS(Url.Action("Index")).WithSuccessMessages(JIC.Base.Resources.Messages.EditVaild);

                    // TempData["[FailedMessage"] = JIC.Base.Resources.Messages.EditVaild;
                    //return JavaScript("window.location = '" + Url.Action("Index") + "'");
                }
            }

            return PartialView(PrepareViewModel(TextPredections));

            }
            else
            {
                ViewData["SessionEnded"] = false;
                return PartialView();
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                try
            {
                vw_TextPredections TextPredections = TextPredectionsService.GetTextByID(id);
                TextPredectionsViewModels TextModels = new TextPredectionsViewModels
                {
                    TextID = TextPredections.TextID,
                    TextTitle = TextPredections.TextTitle,
                    TextPredectionsDescription = TextPredections.TextPredectionsDescription,
                   CircuitID  = TextPredections.CircuitID,
                    CircuitName = TextPredections.CircuitName,

                    // UserID = CurrentUser.ID,
                };

                return PartialView(TextModels);

            }
            catch (Exception ex)
            {
                return ErrorPage(ex);
            }
            }
            else
            {
                ViewData["SessionEnded"] = false;
                return PartialView();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Delete(TextPredectionsViewModels TextModels)
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                try
            {
                if (TextPredectionsService.DeleteTextByID(TextModels.TextID))
                {
                    // TempData["SuccessMessage"] = JIC.Base.Resources.Messages.DeleteSucess;
                    return RedirectJS(Url.Action("Index")).WithSuccessMessages(JIC.Base.Resources.Messages.OperationCompletedSuccessfully);

                }
                else
                {
                    return CPartialView(TextModels).WithErrorMessages(JIC.Base.Resources.Messages.OperationNotCompleted);

                    // TempData["[FailedMessage"] = JIC.Base.Resources.Messages.DeleteFaild;
                    //  return RedirectJS(Url.Action("Index")).WithSuccessMessages(JIC.Base.Resources.Messages.DeleteFaild);

                }
                //ViewBag.RedirectPage = Url.Action("Index");

               // return View();

            }
            catch (Exception ex)
            {
                return ErrorPage(ex);
            }
            }
            else
            {
                ViewData["SessionEnded"] = false;
                return PartialView();
            }
        }

    }
}
using GameStore.Entities;
using GameStore.Managers;
using GameStore.ViewModels;
using GameStore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Controllers
{
    public class PegiController : Controller
    {
        public PegiManager PegiManager { get; private set; }
        
        public PegiController()
        {
            GameStoreDbContext context = new GameStoreDbContext();
            PegiManager = new PegiManager(context);
        }

        public PegiController(PegiManager pegiManager)
        {
            PegiManager = pegiManager;
        }

        public ActionResult Index()
        {
            var model = new PegiIndexViewModel
            {
                Content = PegiManager.PegiContent.ToList().ToViewModel(),
                Rates = PegiManager.PegiRates.ToList().ToListViewModel()
            };
            return View(model);
        }

        #region Pegi Content

        [HttpGet]
        public ActionResult CreateContent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateContent(PegiContentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var pegi = new PegiContent
                {
                    Name = model.Name,
                    Description = model.Description,
                    IconLink = model.IconLink
                };
                PegiManager.CreateContent(pegi);
                PegiManager.Save();

                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult UpdateContent(Guid? id)
        {
            if (id.HasValue)
            {
                var entity = PegiManager.FindContentById(id.Value);
                return View("UpdateContent", entity.ToViewModel());
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdateContent(PegiContentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var pegi = new PegiContent
                {
                    Id = model.Id,
                    Name = model.Name, 
                    IconLink = model.IconLink, 
                    Description = model.Description
                };
                PegiManager.UpdateContent(pegi);
                PegiManager.Save();

                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult DeleteContent(Guid? id)
        {
            if (id.HasValue)
            {
                PegiManager.DeleteContent(id.Value);
                PegiManager.Save();
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region Pegi Rating

        [HttpGet]
        public ActionResult CreateRating()
        {
            return View(new PegiRatingViewModel()
            {
                Content = PegiManager.PegiContent.ToList().ToSelectableList()
            });
        }

        [HttpPost]
        public ActionResult CreateRating(PegiRatingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var content = new List<PegiContent>();
                foreach (var ctnt in model.Content)
                {
                    if (ctnt.Selected)
                    {
                        content.Add(PegiManager.FindContentById(new Guid(ctnt.Value)));
                    }
                }
                var pegi = new PegiRating
                {
                    Name = model.Name,
                    IconLink = model.IconLink,
                    Content = content
                };
                PegiManager.CreateRating(pegi);
                PegiManager.Save();

                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult UpdateRating(Guid? id)
        {
            if (id.HasValue)
            {
                var entity = PegiManager.FindRatingById(id.Value);
                var model = new PegiRatingViewModel
                {
                     Id = entity.Id, 
                     IconLink = entity.IconLink, 
                     Name = entity.Name, 
                     Content = PegiManager.PegiContent.ToSelectableList(entity.Content.ToList())
                };
                return View("UpdateRating", model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdateRating(PegiRatingViewModel model)
        {
            if (ModelState.IsValid)
            {                
                var pegi = new PegiRating
                {
                    Id = model.Id,
                    Name = model.Name,
                    IconLink = model.IconLink
                };
                
                PegiRating pr = PegiManager.FindRatingById(model.Id);

                foreach (var ctnt in model.Content)
                {
                    Guid guid = new Guid(ctnt.Value);
                    var temp = PegiManager.FindContentById(new Guid(ctnt.Value));

                    if (ctnt.Selected)
                    {
                        if (!pr.Content.Any(c => c.Id == guid))
                        {
                            pr.Content.Add(temp);
                            temp.Rates.Add(pr);
                        }
                    }
                    else
                    {
                        if(pr.Content.Any(c => c.Id == guid))
                        {
                            pr.Content.Remove(temp);
                            temp.Rates.Remove(pr);
                        }
                    }
                }

                PegiManager.UpdateRating(pr);
                PegiManager.Save();

                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult DeleteRating(Guid? id)
        {
            if (id.HasValue)
            {
                PegiManager.DeleteRating(id.Value);
                PegiManager.Save();
            }
            return RedirectToAction("Index");
        }

        #endregion
    }
}
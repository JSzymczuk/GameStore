using GameStore.Entities;
using GameStore.Helpers;
using GameStore.Managers;
using GameStore.ViewModels;
using ImageResizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Controllers
{
    public class ArticleController : Controller
    {
        public ArticleManager ArticleManager { get; private set; }

        public ArticleController()
        {
            GameStoreDbContext context = new GameStoreDbContext();
            ArticleManager = new ArticleManager(context);
        }

        [HttpGet]
        public ActionResult Index()
        {
            var articles = new List<ArticleViewModel>();
            foreach (var article in ArticleManager.Articles.OrderByDescending(a => a.Added).ToList())
            {
                articles.Add(new ArticleViewModel 
                { 
                    AuthorId = article.AuthorId, 
                    AuthorName = article.Author.UserName,
                    Content = article.Content, 
                    DateString = article.Added.ToDisplayableDate(),
                    Id = article.Id, 
                    Image = article.Image, 
                    ImageThumb = article.ImageThumb, 
                    ShortInfo = article.ShortInfo,
                    Title = article.Title 
                });
            }
            return View();
        }

        [HttpGet]
        public PartialViewResult Top4Articles()
        {
            var articles = new List<ArticleViewModel>();
            foreach (var article in ArticleManager.Articles.OrderByDescending(a => a.Added).ToList())
            {
                articles.Add(new ArticleViewModel
                {
                    AuthorId = article.AuthorId,
                    AuthorName = article.Author.UserName,
                    DateString = article.Added.ToDisplayableDate(),
                    Id = article.Id,
                    Image = article.Image,
                    ImageThumb = article.ImageThumb,
                    ShortInfo = article.ShortInfo,
                    Title = article.Title
                });
            }
            return PartialView("_Top4Articles", articles);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ArticleCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                SaveImage(model);
                ArticleManager.Create(new Article
                {
                    Added = DateTime.Now,
                    AuthorId = AccountHelper.GetLoggedUserId(),
                    Content = model.Content,
                    Image = model.ImageUrl,
                    ImageThumb = model.ImageThumbUrl,
                    ShortInfo = model.ShortInfo,
                    Title = model.Title
                });
                ArticleManager.Save();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {
            var article = ArticleManager.FindById(id);
            if(article != null)
            {
                return View(new ArticleViewModel
                {
                    AuthorId = article.AuthorId,
                    AuthorName = article.Author.UserName,
                    Content = article.Content,
                    DateString = article.Added.ToDisplayableDate(),
                    Id = article.Id,
                    Image = article.Image,
                    ImageThumb = article.ImageThumb,
                    ShortInfo = article.ShortInfo,
                    Title = article.Title
                });
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Update(Guid? id)
        {
            if (id.HasValue)
            {
                var entity = ArticleManager.FindById(id.Value);
                var model = new ArticleCreateViewModel
                {
                    Title = entity.Title,
                    ShortInfo = entity.ShortInfo,
                    ImageUrl = entity.Image,
                    DateAdded = entity.Added,
                    Content = entity.Content,
                    AuthorId = entity.AuthorId,
                    Id = entity.Id
                };
                return View("Update", model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Update(ArticleCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Image != null)
                {
                    SaveImage(model);
                }

                ArticleManager.Update(new Article
                {
                    Added = DateTime.Now,
                    AuthorId = AccountHelper.GetLoggedUserId(),
                    Content = model.Content,
                    Image = model.ImageUrl,
                    ImageThumb = model.ImageThumbUrl,
                    ShortInfo = model.ShortInfo,
                    Title = model.Title,
                    Id = model.Id
                });
                
                ArticleManager.Save();

                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            ArticleManager.Delete(id);
            ArticleManager.Save();
            return RedirectToAction("Index");
        }

        [NonAction]
        private bool SaveImage(ArticleCreateViewModel model)
        {
            var file = model.Image;

            if (file == null)
            {
                return false;
            }

            string fileName = Guid.NewGuid().ToString();
            string path = Server.MapPath("~/Images/Articles/");

            if (!(UploadImage(file, fileName, path, "format=jpg")
            && UploadImage(file, fileName, path + "Thumbs\\", "width=156&format=jpg")))
            {
                return false;
            }

            model.ImageUrl = path + fileName + ".jpg";
            model.ImageThumbUrl = path + "Thumbs\\" + fileName + ".jpg";

            return true;
        }

        [NonAction]
        private bool UploadImage(HttpPostedFileBase file, string fileName, string path, string options)
        {
            try
            {
                file.InputStream.Seek(0, SeekOrigin.Begin);
                ImageBuilder.Current.Build(new ImageJob(
                    file.InputStream, path + fileName, new Instructions(options), false, true));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}
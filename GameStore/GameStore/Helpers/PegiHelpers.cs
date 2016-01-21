using GameStore.Entities;
using GameStore.Managers;
using GameStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Helpers
{
    public static class PegiHelper
    {
        public static SelectList GetPegiSelectList(this PegiManager manager)
        {
            return new SelectList(manager.PegiRates, "Id", "Name");
        }

        public static SelectList GetPegiSelectList(this PegiManager manager, Guid selectedId)
        {
            return new SelectList(manager.PegiRates, "Id", "Name", manager.PegiRates.FirstOrDefault(item => item.Id == selectedId));
        }

        public static PegiContentViewModel ToViewModel(this PegiContent pegi)
        {
            return new PegiContentViewModel
            {
                 Id = pegi.Id, 
                 IconLink = pegi.IconLink, 
                 Name = pegi.Name, 
                 Description = pegi.Description, 
                 CanBeDeleted = pegi.Rates.Count == 0
            };
        }

        public static PegiRatingListItem ToListItem(this PegiRating pegi)
        {
            var content = new List<PegiContentViewModel>();
            foreach (var c in pegi.Content)
            {
                content.Add(c.ToViewModel());
            }
            return new PegiRatingListItem
            {
                 Id = pegi.Id, 
                 IconLink = pegi.IconLink, 
                 Name = pegi.Name, 
                 Content = content, 
                 CanBeDeleted = pegi.Products.Count == 0
            };
        }

        public static List<PegiContentViewModel> ToViewModel(this IEnumerable<PegiContent> content)
        {
            var result = new List<PegiContentViewModel>();
            foreach (var item in content)
            {
                result.Add(item.ToViewModel());
            }
            return result;
        }

        public static List<PegiRatingListItem> ToListViewModel(this IEnumerable<PegiRating> rates)
        {
            var result = new List<PegiRatingListItem>();
            foreach (var item in rates)
            {
                result.Add(item.ToListItem());
            }
            return result;
        }

        public static List<SelectListItem> ToSelectableList(this IEnumerable<PegiContent> content)
        {
            return content.ToSelectableList(new List<PegiContent>());
        }        

        public static List<SelectListItem> ToSelectableList(this IEnumerable<PegiContent> content, IEnumerable<PegiContent> selected)
        {
            var result = new List<SelectListItem>();
            foreach (var ctnt in content)
            {
                result.Add(new SelectListItem
                {
                    Selected = selected.Any(s => s.Id == ctnt.Id),
                    Value = ctnt.Id.ToString(),
                    Text = ctnt.Name
                });
            }
            return result;
        }
    }
}
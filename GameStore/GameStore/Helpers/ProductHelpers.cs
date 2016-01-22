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
    public static class ProductHelpers
    {
        public static SelectList GetCategoriesSelectList(this ProductManager manager)
        {
            var collection = manager.ProductCategories.ToList().Where(c => c.Children.Count == 0);
            return new SelectList(collection, "Id", "Name");
        }

        public static SelectList GetGenreSelectList(this ProductManager manager)
        {
            return new SelectList(manager.Genres, "Id", "Name");
        }

        public static SelectList GetCategoriesSelectList(this ProductManager manager, Guid selectedId)
        {
            var collection = manager.ProductCategories.ToList().Where(c => c.Children.Count == 0);
            return new SelectList(collection, "Id", "Name", collection.FirstOrDefault(item => item.Id == selectedId));
        }

        public static SelectList GetGenreSelectList(this ProductManager manager, Guid selectedId)
        {
            return new SelectList(manager.Genres, "Id", "Name", manager.Genres.FirstOrDefault(item => item.Id == selectedId));
        }

        public static ProductListItem ToListItem(this Product product)
        {
            string description = product.Description != null ? 
                product.Description.Length < 250 ? 
                product.Description : product.Description.Substring(0, 250) : string.Empty;
            int gr = (int)((product.BasePrice - (int)product.BasePrice) * 100);

            var model = new ProductListItem
            {
                Id = product.Id,
                Name = product.Name,
                Publisher = product.Publisher,
                Description = description,
                CoverList = product.CoverList,
                ReleaseDate = product.ReleaseDate,
                Language = product.Language,
                Rating = product.Rating, 
                BasePriceZl = (int)product.BasePrice,
                BasePriceGr = gr < 10 ? gr < 1 ? "00" : "0" + gr.ToString() : gr.ToString(),
                IsDiscounted = product.DiscountPrice.HasValue,
                Price = product.DiscountPrice.HasValue ? product.DiscountPrice.Value : product.BasePrice,
                Available = product.Remaining > 0,
                GenreName = product.Genre.Name,
                GenreId = product.GenreId,
                PlatformName = product.Platform.Name,
                PlatformId = product.PlatformId,
                CommentsCount = product.Comments.Count,
                VotesCount = product.Rates.Count, 
                ReleaseDateString = product.ReleaseDate.ToDisplayableDate()
            };

            if (model.IsDiscounted)
            {
                model.DiscountPercentage = product.DiscountPrice.HasValue ?
                    (int)(100 - 100 * product.DiscountPrice.Value / product.BasePrice) : 0;
                int dgr = (int)((product.DiscountPrice - (int)product.DiscountPrice) * 100);
                model.DiscountPriceZl = (int)product.DiscountPrice;
                model.DiscountPriceGr = dgr < 10 ? dgr < 1 ? "00" : "0" + dgr.ToString() : dgr.ToString();
            }

            return model;
        }

        public static ProductViewModel ToViewModel(this Product product)
        {
            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Publisher = product.Publisher,
                Description = product.Description,
                CoverLarge = null,
                Url = product.CoverLarge,
                ReleaseDate = product.ReleaseDate,
                Language = product.Language,
                Rating = product.Rating,
                BasePrice = product.BasePrice,
                DiscountPrice = product.DiscountPrice,
                GenreId = product.GenreId,
                PlatformId = product.PlatformId,
                CollectorEdition = product.CollectorEdition, 
                CoverList = product.CoverList,
                CoverThumb = product.CoverThumb, 
                PegiRatingId = product.PegiRatingId, 
                Remaining = product.Remaining, 
                ReqMinimal = product.ReqMinimal, 
                ReqRecommended = product.ReqRecommended
            };
        }

        public static Product ToProduct(this ProductViewModel model, string url = null)
        {
            return new Product
            {
                Id = model.Id,
                Name = model.Name,
                Publisher = model.Publisher,
                Description = model.Description,
                CoverLarge = url,
                ReleaseDate = model.ReleaseDate,
                Language = model.Language,
                Rating = model.Rating,
                BasePrice = model.BasePrice,
                DiscountPrice = model.DiscountPrice,
                GenreId = model.GenreId,
                PlatformId = model.PlatformId,
                CollectorEdition = model.CollectorEdition,
                CoverList = model.CoverList,
                CoverThumb = model.CoverThumb,
                PegiRatingId = model.PegiRatingId,
                Remaining = model.Remaining,
                ReqMinimal = model.ReqMinimal,
                ReqRecommended = model.ReqRecommended, 
                IsDeleted = false
            };
        }
        
        public static IOrderedEnumerable<ProductListItem> SortBy(this IEnumerable<ProductListItem> items, ProductSortType sortType)
        {
            switch (sortType)
            {
                case ProductSortType.NameDescending:
                    return items.OrderByDescending(i => i.Name);
                case ProductSortType.PriceAscending:
                    return items.OrderBy(i => i.Price);
                case ProductSortType.PriceDescending:
                    return items.OrderByDescending(i => i.Price);
                case ProductSortType.RatingAscending:
                    return items.OrderBy(i => i.Rating);
                case ProductSortType.RatingDescending:
                    return items.OrderByDescending(i => i.Rating);
                case ProductSortType.ReleaseAscending:
                    return items.OrderBy(i => i.ReleaseDate);
                case ProductSortType.ReleaseDescending:
                    return items.OrderByDescending(i => i.ReleaseDate);
                default:
                    return items.OrderBy(i => i.Name);
            }
        }
    }
}
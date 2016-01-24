using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GameStore.Entities
{
    public class AppUser : IdentityUser
    {
        public Boolean NewsletterEnrolled { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Rating> Rates { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}
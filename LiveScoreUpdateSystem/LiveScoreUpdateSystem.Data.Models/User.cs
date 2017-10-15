using LiveScoreUpdateSystem.Data.Models.Contracts;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LiveScoreUpdateSystem.Data.Models
{
    public class User : IdentityUser, IDeletable, IAuditable, IDataModel
    {
        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        Guid IDataModel.Id { get; }

        [NotMapped]
        public string TestUsername
        {
            get
            {
                return base.UserName;
            }
            set
            {
                base.UserName = this.UserName;
            }
        }

        public ICollection<Team> Subscriptions { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}

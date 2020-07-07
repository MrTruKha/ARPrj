using ARPrj.DataAccess.Model;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ARPrj.DataAccess
{
    public class ARPrjContext : IdentityDbContext
    {
        public ARPrjContext() : base("ARPrjContextDb")
        {

        }

        public DbSet<UserCommon> Users { get; set; }

        public static ARPrjContext Create()
        {
            return new ARPrjContext();
        }
    }
}

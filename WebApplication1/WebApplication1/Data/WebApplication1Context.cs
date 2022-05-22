using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1;

namespace WebApplication1.Data
{
    public class WebApplication1Context : DbContext
    {
        public WebApplication1Context (DbContextOptions<WebApplication1Context> options)
            : base(options)
        {
        }

        public DbSet<WebApplication1.Contact>? Contact { get; set; }

        public DbSet<WebApplication1.User> User { get; set; }

        public DbSet<WebApplication1.Chat> Chat { get; set; }

        public DbSet<WebApplication1.Message> Message { get; set; }
    }
}

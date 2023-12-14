using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServerApiBlog.Models;

namespace ServerApiBlog.Data
{
    public class ServerApiBlogContext : DbContext
    {
        public ServerApiBlogContext (DbContextOptions<ServerApiBlogContext> options)
            : base(options)
        {
        }

        public DbSet<ServerApiBlog.Models.Member> Member { get; set; } = default!;
    }
}

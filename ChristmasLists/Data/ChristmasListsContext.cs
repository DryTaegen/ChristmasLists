using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ChristmasLists.Models;

namespace ChristmasLists.Data
{
    public class ChristmasListsContext : DbContext
    {
        public ChristmasListsContext (DbContextOptions<ChristmasListsContext> options)
            : base(options)
        {
        }

        public DbSet<ChristmasLists.Models.Lists> Lists { get; set; } = default!;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Filmy.Models;

namespace Filmy.Data
{
    public class FilmyContext : DbContext
    {
        public FilmyContext (DbContextOptions<FilmyContext> options)
            : base(options)
        {
        }

        public DbSet<Filmy.Models.Film> Film { get; set; } = default!;

        public DbSet<Filmy.Models.Aktor> Aktor { get; set; } = default!;

        public DbSet<Filmy.Models.Ocena> Ocena { get; set; } = default!;

        public DbSet<Filmy.Models.Uzytkownik> Uzytkownik { get; set; } = default!;
    }
}

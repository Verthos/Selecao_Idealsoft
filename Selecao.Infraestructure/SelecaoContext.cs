using Microsoft.EntityFrameworkCore;
using Selecao.Core.Entities;


namespace Selecao.Infraestructure
{
    public class SelecaoContext : DbContext
    {
        public SelecaoContext(DbContextOptions<SelecaoContext> options) : base(options){    }
        public DbSet<Person> People { get; set; }

    }
}

using Selecao.Core.Entities;
using Selecao.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selecao.Infraestructure.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        private readonly SelecaoContext _db;
        public PersonRepository(SelecaoContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Person person)
        {
            _db.People.Update(person);
        }


    }
}

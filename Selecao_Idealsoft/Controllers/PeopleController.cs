using Microsoft.AspNetCore.Mvc;
using Selecao.Application.Services.PersonValidator;
using Selecao.Core.Entities;
using Selecao.Core.Interfaces.Repositories;


namespace Selecao_Idealsoft_Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPersonRepository _db;
        private readonly IPersonPropertiesValidator _validator;

        public PeopleController(IPersonRepository db, IPersonPropertiesValidator validator)
        {
            _validator = validator;
            _db = db;
        }

        // GET: api/people
        [HttpGet("people")]
        public ActionResult<IEnumerable<Person>> GetPeople()
        {
            try
            {
                IEnumerable<Person> people = _db.GetAll();
                return Ok(people);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // GET: api/person
        [HttpGet("person/{id}")]
        public ActionResult<Person> GetPerson(int id)
        {
            try
            {
                Person person = _db.GetFirstOrDefault(p => p.Id == id);
                if(person == null)
                {
                    return NotFound("Id nao encontrado");
                }
                return Ok(person);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro: {e.Message}");
            }
        }
        // POST: api/person
        [HttpPost("person")]
        public ActionResult<Person> CreatePerson(Person person)
        {
            if(!_validator.PersonIsValid(person.Name, person.LastName, person.PhoneNumber))
            {
                return BadRequest("Dados invalidos");
            }
            try
            {
                _db.Add(person);
                _db.Save();

                return Created("Usuario adicionado", new {Message = $"Usuario {person.Name} adicionado" });
            }
            catch(Exception e)
            {
                return BadRequest($"Erro: {e.Message}");
            }
        }
        // PUT: api/person/2
        [HttpPut("person/{id}")]
        public ActionResult<Person> UpdatePerson(Person person)
        {
            Person userToupdate = _db.GetFirstOrDefault(p => (p.Id == person.Id));

            if (userToupdate == null)
            {
                return NotFound("Id nao encontrado");
            };
            if (!_validator.PersonIsValid(person.Name, person.LastName, person.PhoneNumber))
            {
                return BadRequest("Dados invalidos");
            };

            _db.Update(person);
            _db.Save();
            return Ok($"Usuario {person.Name} editado");
        }
        // DELETE: api/person/2
        [HttpDelete("person/{id}")]
        public ActionResult<Person> DeletePerson(int id)
        {
            Person userToRemove = _db.GetFirstOrDefault(p => (p.Id == id));
            if(userToRemove == null)
            {
                return NotFound("Id nao encontrado");
            };
            try
            {
                _db.Remove(userToRemove);
                _db.Save();

                return Ok($"Usuario id: {id} deletado");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }


}

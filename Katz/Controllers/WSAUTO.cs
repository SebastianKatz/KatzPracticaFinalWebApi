using Katz.Data;
using Katz.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Katz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WSAUTO : ControllerBase
    {
        private readonly AutoDbContext context;
        public WSAUTO(AutoDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Auto> Get()
        {
            return context.Autos.ToList();
        }

        [HttpGet("GetByModelo/{modelo}")]
        public IEnumerable<Auto> GetByModelo(string modelo)
        {
            var autos = (from m in context.Autos where m.Modelo == modelo select m).ToList();

            return autos;
        }

        [HttpPost]
        public ActionResult Post(Auto auto)
        {
            context.Autos.Add(auto);
            context.SaveChanges();
            return Ok();
        }

        // OPCIONALES

        [HttpGet("{Id}")]
        public ActionResult<Auto> Get(int Id)
        {
            return context.Autos.Find(Id);
        }

        [HttpPut("{id}")]
        public ActionResult actionResult(int Id, Auto auto)
        {
            if (Id != auto.Id)
            {
                return BadRequest();
            }
            context.Entry(auto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Auto> Delete(int Id)
        {
            Auto auto = context.Autos.Find(Id);
            if (auto == null)
            {
                return NotFound();
            }
            context.Autos.Remove(auto);
            context.SaveChanges();
            return auto;
        }

        [HttpGet("marca/{marca}/modelo/{modelo}")]
        public IEnumerable<Auto> GetByMarcaModelo(string marca, string modelo)
        {
            var autos = (from a in context.Autos
                         where a.Marca == marca & a.Modelo == modelo
                         select a).ToList();
            return autos;
        }

        [HttpGet("GetByColor/{color}")]
        public IEnumerable<Auto> GetByColor(string color)
        {
            var autos = (from m in context.Autos where m.Color == color select m).ToList();

            return autos;
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using ZooPlanet.Models;

namespace ZooPlanet.Repositories
{
	

        public class EspeciesRepository : Repository<Especies>
        {

            public EspeciesRepository(animalesContext ctx) : base(ctx)
            {

            }


            public override IEnumerable<Especies> GetAll()
            {
                return base.GetAll().OrderBy(x => x.Especie);
            }

            public IEnumerable<Especies> GetEspeciesByClase(string Id)
            {
                return Context.Especies
                    .Include(x => x.IdClaseNavigation)
                    .Where(x => x.IdClaseNavigation.Nombre == Id)
                    .OrderBy(x => x.Especie);
            }

            public Especies GetById(int Id)
            {
                return Context.Especies.Include(x => x.IdClaseNavigation).FirstOrDefault(x => x.Id == Id);
            }

            public override bool Validate(Especies entidad)
            {
                if (string.IsNullOrWhiteSpace(entidad.Especie))
                {
                    throw new Exception("Escriba el nombre de la especie.");
                }

                if (Context.Especies.Any(x => x.Especie == entidad.Especie && x.Id != entidad.Id))
                {
                    throw new Exception("Ya existe el nombre de la especie.");
                }
                return true;
            }

        }
    }

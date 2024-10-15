using Microsoft.EntityFrameworkCore;
using Projekat_Microservice.Data;
using Projekat_Microservice.Models;

namespace Projekat_Microservice.Services
{
    public class ProjekatService
    {
  
            private readonly DatabaseContext db;

            public ProjekatService(DatabaseContext ctx )
            {
                db = ctx;
            }

            public IEnumerable<Projekat> GetAll()
            {
                return db.Projekat.ToList();
            }

            public Projekat GetById(Guid id)
            {
                return db.Projekat.Find(id);
            }

            public Projekat GetByNaziv(string naziv)
            {
                return db.Projekat.FirstOrDefault(projekat => projekat.NazivProjekta.Equals(naziv));

            }

            public void Save(Projekat projekat)
            {
                db.Projekat.Add(projekat);
                db.SaveChanges();
            }

            public void Update(Projekat projekat)
            {
            db.Entry(projekat).State = EntityState.Modified;
                db.SaveChanges();

            }

            public void Delete(Projekat projekat)
            {
                db.Projekat.Remove(projekat);
                db.SaveChanges();
            }

        public List<Projekat> GetByTimId(Guid id)
        {
            var projekti = db.Projekat.Where(projekat => projekat.TimID.Equals(id)).ToList();
            return projekti;

        }

    }
}


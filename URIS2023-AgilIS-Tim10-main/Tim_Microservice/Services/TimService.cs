using Microsoft.EntityFrameworkCore;
using Tim_Microservice.Data;
using Tim_Microservice.Models;

namespace Tim_Microservice.Services
{
    public class TimService
    {
        private readonly DatabaseContext db;

        public TimService(DatabaseContext ctx)
        {
            db = ctx;
        }

        public IEnumerable<Tim> GetAll()
        {
            return db.Tim.ToList();
        }

        public Tim GetById(Guid id)
        {
            return db.Tim.Find(id); 
        }

        public Tim GetByNaziv(string naziv)
        {
            return db.Tim.FirstOrDefault(tim => tim.NazivTima.Equals(naziv));

        } 

        public void Save (Tim tim)
        {
            db.Tim.Add(tim);
            db.SaveChanges(); 
        }

        public void Update (Tim tim)
        {
            db.Entry(tim).State = EntityState.Modified;
            db.SaveChanges();

        }

        public void Delete (Tim tim)
        {
            db.Tim.Remove(tim);
            db.SaveChanges();       
        }

        public List<ClanTima> GetClanoviTimId(Guid id)
        {
            return db.ClanTima.AsNoTracking().Include(b => b.Tim).Where(e => e.TimId == id).ToList();
        }

        public List<Tim> GetTimoviUsername(string username)
        {
            List<ClanTima> clanovi = db.ClanTima.AsNoTracking().Include(b => b.Tim).Where(e => e.userName == username).ToList();
            List<Tim> timovi = new List<Tim>();
            foreach(ClanTima clan in clanovi)
            {
                timovi.Add(clan.Tim); 
            }
            return timovi;
        }
    }
}

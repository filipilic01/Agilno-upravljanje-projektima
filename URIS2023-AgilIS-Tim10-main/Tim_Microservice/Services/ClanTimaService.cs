using Microsoft.EntityFrameworkCore;
using Tim_Microservice.Data;
using Tim_Microservice.Models;

namespace Tim_Microservice.Services
{
    public class ClanTimaService
    {
        private readonly DatabaseContext db;

        public ClanTimaService(DatabaseContext ctx)
        {
            db = ctx;
        }


        public IEnumerable<ClanTima> GetAll()
        {
            return db.ClanTima.AsNoTracking().Include(b => b.Tim).ToList();
        }

        public ClanTima GetById(Guid id)
        {
            return db.ClanTima.AsNoTracking().Include(b => b.Tim).FirstOrDefault(e => e.ClanTimaId == id);
        }


        public void Save(ClanTima clanTima)
        {
            db.ClanTima.Add(clanTima);
            db.SaveChanges();
        }

        public void Update(ClanTima clanTima)
        {
            db.Entry(clanTima).State = EntityState.Modified;
            db.SaveChanges();

        }

        public void Delete(ClanTima clanTima)
        {
            db.ClanTima.Remove(clanTima);
            db.SaveChanges();
        }
    }

}

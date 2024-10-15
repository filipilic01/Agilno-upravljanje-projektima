namespace Backlog_MicroService.Models.Backlog
{
    public class BacklogDto
    {
        /// <summary>
        /// Id backloga
        /// </summary>
        public Guid IdBacklog { get; set; }
        public string Opis { get; set; }
        public string Naslov { get; set; }

       /* public Guid? ProjekatId { get; set; }
        public Guid? KorisnikId { get; set; }
        public string OpisZadatak {  get; set; }
        public string VremeTrajanja { get; set; }
        public string AngazovaniRadnici { get; set; }
        public DateTime Pocetak {  get; set; }
        public DateTime Kraj { get; set; }
        public string Cilj { get; set; }*/
    }
}

namespace e_Voting.Models.Domain
{
    public class BureauVote
    {
        public int Id { get; set; }     
        public string NomBureauVote { get; set; }
        public int NombreInscrits { get; set; }
        public ICollection<Citoyen> Citoyens { get; set; }
    }
}

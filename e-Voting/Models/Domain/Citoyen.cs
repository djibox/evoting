namespace e_Voting.Models.Domain
{
    public class Citoyen
    {
        public int Id { get; set; }
        public string Prénom { get; set; }    
        public string Nom { get; set; }
        public DateTime DateNaissance { get; set; }
        public string Adresse { get; set; }
        public int BureauVoteId { get; set; }
        public BureauVote BureauVote { get; set; }
    }
}

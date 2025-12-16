using System;

namespace Chess_DB.Models

{
    public class Inscription
    {
        public int Id { get; set; }

        public Joueur Joueur { get; set; }

        public Competition Competition { get; set; }

        public DateTime DateInscription { get; set; } = DateTime.Now;

        public Inscription(int id, Joueur joueur, Competition competition)
        {
            Id = id;
            Joueur = joueur;
            Competition = competition;
            DateInscription = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Joueur} inscrit à {Competition}";
        }
    }
}

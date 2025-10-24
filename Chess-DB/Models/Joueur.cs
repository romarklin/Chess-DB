namespace Chess_DB.Models
{
    public class Joueur
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Classement ELO du joueur
        public double ClassementElo { get; set; } = 1200;

        // Statistiques optionnelles
        public int PartiesJouees { get; set; }
        public int Victoires { get; set; }
        public int Defaites { get; set; }
        public int Nuls { get; set; }

        // Constructeur par défaut
        public Joueur() { }

        // Constructeur pratique pour initialiser rapidement un joueur
        public Joueur(int id, string nom, string prenom, string email, double classementElo = 1200)
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            Email = email;
            ClassementElo = classementElo;
        }

        // Méthode utilitaire pour calculer le taux de victoire
        public double TauxDeVictoire()
        {
            if (PartiesJouees == 0) return 0;
            return (double)Victoires / PartiesJouees * 100;
        }

        // Redéfinition de ToString() utile pour le debug ou les listes
        public override string ToString()
        {
            return $"{Prenom} {Nom} (ELO: {ClassementElo})";
        }
    }
}
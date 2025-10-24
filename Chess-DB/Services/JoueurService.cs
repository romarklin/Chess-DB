using Chess_DB.Models;
using System.Collections.ObjectModel;

namespace Chess_DB.Services
{
    public class JoueurService
    {
        public ObservableCollection<Joueur> Joueurs { get; } = new();

        public void Ajouter(Joueur joueur)
        {
            Joueurs.Add(joueur);
        }

        public void Supprimer(Joueur joueur)
        {
            Joueurs.Remove(joueur);
        }
    }
}
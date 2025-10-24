using System.Collections.ObjectModel;
using Chess_DB.Models;
using Chess_DB.Services;

namespace Chess_DB.ViewModels

{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly JoueurService _joueurService;

        public ObservableCollection<Joueur> Joueurs => _joueurService.Joueurs;

        public MainWindowViewModel()
        { 
            _joueurService = new JoueurService();

            // Exemple de données temporaires
            _joueurService.Ajouter(new Joueur { Nom = "Dupont", Prenom = "Luc", ClassementElo = 1500 });
            _joueurService.Ajouter(new Joueur { Nom = "Martin", Prenom = "Alice", ClassementElo = 1350 });
        }
    }
}
using Chess_DB.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace Chess_DB.Services
{
    public class JoueurService
    {
        private const string FilePath = "joueurs.json";
        public ObservableCollection<Joueur> Joueurs { get; } = new();

        public JoueurService()
        {
            // Charger les joueurs depuis le fichier JSON au démarrage
            Charger();
        }
        public void Sauvegarder()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(FilePath, JsonSerializer.Serialize(Joueurs, options));
        }
        public void Charger()
        {
            if (!File.Exists(FilePath))
                return;

            var json = File.ReadAllText(FilePath);
            var joueurs = JsonSerializer.Deserialize<ObservableCollection<Joueur>>(json);

            if (joueurs != null)
            {
                Joueurs.Clear();
                foreach (var j in joueurs)
                {
                    Joueurs.Add(j);
                    j.PropertyChanged += (s, e) => Sauvegarder(); // abonner après chargement
                }
            }
        }
        public void Ajouter(Joueur joueur)
        {
            Joueurs.Add(joueur);
            joueur.PropertyChanged += (s, e) => Sauvegarder();
            Sauvegarder();
        }

        public void Supprimer(Joueur joueur)
        {
            Joueurs.Remove(joueur);
            Sauvegarder();
        }
    }
}
using System;
using System.Collections.Generic; // Pour List<T>

namespace Chess_DB.Models

{
    public class Competition
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }

        // Liste des parties de la compétition
        public List<Partie> Parties { get; set; } = new List<Partie>();

        // Liste des inscriptions
        public List<Inscription> Inscriptions { get; set; } = new List<Inscription>();

        public Competition() { }

        public Competition(int id, string nom, DateTime debut, DateTime fin)
        {
            Id = id;
            Nom = nom;
            DateDebut = debut;
            DateFin = fin;
        }

        public override string ToString()
        {
            return $"{Nom} ({DateDebut:dd/MM/yyyy} - {DateFin:dd/MM/yyyy})";
        }
    }
}

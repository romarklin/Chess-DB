# Chess-DB

## Introduction

Notre projet Chess DB consistait a réaliser un logiciel de gestion des matchs d’échecs pour Google, cette application sera mise sur un site web appelé « GoogleChess-DB », utilisable par les gestionnaires des parties sur leurs portables ou ordinateurs en parallèle d’une compétition d’échecs.

## Fonctionnalité supplémentaire

Notre fonctionnalité supplémentaire est une visualisation des statistiques individuelles de chaque joueur, avec le nombre de victoires, de défaites, de jeux nuls et leur taux de victoires en pourcents, on peut aussi y trouver le nombre de parties jouées.
L’interface nous présente également le classment ELO du joueur.

## Diagramme de classes :

![Diagramme de classes](/Chess-DB/Diagrammes/diagramme_classe.png)

## Diagramme de séquences :

![Diagramme de séquences](/Chess-DB/Diagrammes/diagramme_sequence_encodage.png)

## Diagramme d’activité :

![Diagramme d'activité](/Chess-DB/Diagrammes/diagramme_activite.png)

## Qualités d’adaptabilité du projet à une autre fédération

Notre projet est adaptable à d’autres fédérations parce que, à part pour le calcul du score ELO et l’enregistrement des coups, les principales fonctionnalitées, comme l’enregistrement des joueurs, l’inscription aux parties et leur historique sont assez générales et peuvent être utilisées pour d’autres jeux ou fédérations.

## Description de deux principes SOLID utilisés dans le projet

L'architecture du projet Chess-DB a été conçue en suivant les bonnes pratiques de développement orienté objet, notamment par l'application de deux principes majeurs du paradigme SOLID : le Principe de Responsabilité Unique (SRP) et le Principe de Substitution de Liskov (LSP).

**1. Principe de Responsabilité Unique (_Single Responsibility Principle - SRP_)**

Ce principe stipule qu'une classe ne doit avoir qu'une seule raison de changer, ce qui implique qu'elle ne doit prendre en charge qu'une seule responsabilité fonctionnelle. Cette séparation des préoccupations est clairement établie dans le projet à travers plusieurs couches :

- Gestion des données : Les services `JoueurService` et `CompetitionService` sont exclusivement dédiés à la gestion du cycle de vie des données (chargement, sauvegarde, ajout, suppression) pour leurs entités respectives. Ils sont totalement découplés de la logique d'affichage.

- Logique métier pure : La classe `EloCalculator` isole la complexité mathématique liée au calcul et à l'ajustement du classement ELO. Elle n'interagit pas avec l'interface utilisateur ni avec le stockage des fichiers, se concentrant uniquement sur l'application des règles métier.

- Logique de présentation : Les `ViewModels`, tels que `PagePrincipaleViewModel` ou `EncoderResultatViewModel`, se chargent uniquement de la préparation des données pour la vue et de la gestion des interactions utilisateur, sans empiéter sur la logique métier ou l'accès aux données.

**2. Principe de Substitution de Liskov (_Liskov Substitution Principle - LSP_)**

Le principe de substitution de Liskov établit que les objets d'une classe de base doivent pouvoir être remplacés par des objets de ses classes dérivées sans altérer la cohérence ou le fonctionnement du programme. Ce principe est le fondement du système de navigation de l'application :

- Abstraction commune : Le projet définit une classe parente `ViewModelBase` dont héritent tous les contrôleurs de page spécifiques (`PagePrincipaleViewModel`, `InscriptionsViewModel`, `HistoriqueViewModel`, etc.).

- Polymorphisme en action : Dans le `MainWindowViewModel`, la propriété responsable de l'affichage courant, `_pageActuelle`, est typée comme `ViewModelBase`.

- Interchangeabilité : Lors de l'exécution, cette propriété reçoit indifféremment des instances de n'importe quel `ViewModel` enfant. Le moteur de l'application traite ces objets de manière transparente via la classe de base, garantissant que chaque page spécifique peut se substituer à une autre sans nécessiter de modification du code de gestion de la fenêtre principale.

## Conclusion du projet

Pour conclure, le projet Chess-DB a permis de développer une solution logicielle complète et robuste répondant aux besoins de gestion de compétitions d'échecs pour Google. Au-delà des fonctionnalités essentielles de gestion des matchs et des inscriptions, l'intégration du module de statistiques individuelles offre aux utilisateurs une vision analytique pertinente des performances des joueurs.

D'un point de vue technique, l'architecture de l'application a été pensée pour durer. L'application rigoureuse des principes SOLID, notamment la Responsabilité Unique et la Substitution de Liskov, a permis de créer un code modulaire, maintenable et évolutif. Cette rigueur architecturale justifie pleinement la capacité d'adaptation du logiciel : en isolant les règles spécifiques aux échecs (comme le calcul ELO) du reste de la gestion administrative, nous avons posé les bases d'un outil polyvalent capable de s'ouvrir à d'autres fédérations ou types de jeux.

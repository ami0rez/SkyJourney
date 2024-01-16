# SkyJourney - Système de réservation de vols

![Icône FlightProfitOptimizer](https://i.imgur.com/QeSyl3Jm.png)

## Statut Actuel de l'Implémentation

Le backend du système de réservation de vols SkyJourney est actuellement en cours de développement et partiellement complet. L'accent a été mis sur l'établissement de la logique centrale du backend dans les limites du temps disponible.

Fonctionnalités Implémentées :
- **Entity Framework Core** : Utilisé pour l'ORM et les configurations d'API fluente.
- **Modèles de Domaine** : Conçus pour représenter les entités commerciales.
- **Couche de Services** : Les services de base pour les opérations de vol ont été développés.
- **AutoMapper** : Intégré pour le mappage d'objets entre les couches.
- **EfcoreNamingConventions** : Appliqué pour assurer un schéma de base de données cohérent et intuitif.
- **Bibliothèque Bogus** : Utilisée pour peupler la base de données avec des données de test réalistes.

## Améliorations Backend en Attente

Le backend nécessite un développement supplémentaire pour inclure :
- **Achèvement de l'API** : Création et finalisation de divers points de terminaison d'API RESTful.
- **Gestion des Erreurs Avancée** : Amélioration du suivi et de la gestion des erreurs pour renforcer la tolérance aux fautes.
- **Implémentations de Sécurité** : Les meilleures pratiques pour l'authentification et l'autorisation doivent être intégrées.
- **Ajustement des Performances** : Optimisation des interactions avec la base de données et des réponses de l'API pour une meilleure expérience utilisateur.

## Développement Frontend

Le développement du frontend, qui sera réalisé avec React en conjonction avec ASP.NET Core 7, est en attente et fournira :
- **Interface Utilisateur Interactive** : Une interface engageante pour la recherche et la réservation de vols.
- **Intégration Backend** : Une connexion sans faille avec les services backend.
- **Design Réactif** : Adaptabilité à diverses tailles d'écran et dispositifs.

## Feuille de Route des Tests

Des tests complets sont essentiels et sont planifiés comme suit :
- **Tests Unitaires** : Pour les composants individuels afin de garantir leur bon fonctionnement en isolation.
- **Tests d'Intégration** : Pour vérifier que les différentes parties de l'application fonctionnent bien ensemble.

## Technologies Utilisées

Les technologies clés employées dans le backend jusqu'à présent comprennent :
- **Entity Framework Core** : Pour l'accès et la gestion des données.
- **AutoMapper** : Pour le mappage efficace d'objet à objet.
- **Bogus** : Pour créer des données factices pour les tests de l'application.

## Conclusion

L'infrastructure backend de SkyJourney est en cours de développement, nécessitant un temps supplémentaire pour l'achèvement et le raffinement

# Déploiement d'une application Vue, .NET et PostgreSQL avec Docker Compose  

Ce projet met en place une architecture complète comprenant un **frontend en Vue.js**, un **backend en .NET** et une **base de données PostgreSQL**, le tout orchestré via **Docker Compose** sur un réseau commun.  

Des captures d'écran illustrant le fonctionnement du frontend communiquant avec l'API pour récupérer une liste de produits sont disponibles dans le dossier **"screenshots"**.  

## Construction des conteneurs  

Avant de lancer l'application, il est nécessaire de construire les images Docker :  

docker-compose build

## Démarrage des services  

Une fois les images prêtes, les conteneurs peuvent être démarrés en arrière-plan avec la commande :  

docker-compose up -d

Une fois lancé, chaque service est accessible aux adresses suivantes :  
- **Frontend** : `http://localhost:8081`  
- **Backend** : `http://localhost:5000`  
- **Base de données** : `localhost:5432` (accès interne uniquement)  

## Personnalisation des variables d'environnement  

Le backend utilise une **chaîne de connexion** définie dans `docker-compose.yml`, où sont également déclarées les variables liées à PostgreSQL.  

Si besoin, vous pouvez modifier ces paramètres directement dans le fichier **docker-compose.yml**.  

De plus, les ports du frontend et du backend peuvent être ajustés en modifiant leur **Dockerfile** respectif et en adaptant les correspondances dans `docker-compose.yml`.  


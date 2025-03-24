import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {

  username: string = '';  // Nom de l'utilisateur (par défaut vide)
  stats = { ouvert: 0, enattente: 0, resolu: 0, highPriority: 0 }; // Initialisation des stats
  incidents: any[] = []; // Tableau pour stocker les incidents

  // Listes des statuts et priorités
  statusList: any[] = [
    { id: 1, name: 'Ouvert' },
    { id: 2, name: 'En attente' },
    { id: 3, name: 'Résolu' }
  ];

  priorityList: any[] = [
    { id: 1, name: 'Low' },
    { id: 3, name: 'Medium' },
    { id: 4, name: 'High' }
  ];

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit(): void {
    this.http.get<any>('http://localhost:5074/api/Index/index')
      .subscribe(
        (response) => {
          console.log('Réponse du serveur:', response); // Vérifier la structure des données

          // Vérifier si les données existent avant de les assigner
          if (response) {
            this.username = response.username || 'Utilisateur';
            this.stats = response.stats || { ouvert: 0, enattente: 0, resolu: 0, highPriority: 0 };
            this.incidents = response.incidents || [];
          }
        },
        (error) => {
          console.error('Erreur de connexion', error);
        }
      );
  }

  // Méthode pour récupérer le nom du statut
  getStatusName(status: string): string {
    return status || 'Unknown';
  }

  // Méthode pour récupérer le nom de la priorité
  getPriorityName(priorityID: number): string {
    const priority = this.priorityList.find(p => p.id === priorityID);
    return priority ? priority.name : 'Unknown';
  }
  // Méthode pour récupérer le nom du type d'incident
getIncidentTypeName(typeID: number): string {
  const typeList = [
    { id: 1, name: 'Network' },
    { id: 2, name: 'Hardware' },
    { id: 3, name: 'Software' }
  ];
  const type = typeList.find(t => t.id === typeID);
  return type ? type.name : 'Unknown';
}


}

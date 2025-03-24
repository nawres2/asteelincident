import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-ajouter-incident',
  templateUrl: './ajouter-incident.component.html',
  styleUrls: ['./ajouter-incident.component.css']
})
export class AjouterIncidentComponent {
  incident = {
    title: '',
    description: '',
    incidentTypeID: 1, // ID par défaut
    priorityID: 1, // Moyenne priorité par défaut
    status: 'Open', // Statut par défaut
    createdBy: 1, // ID de l'utilisateur courant, à adapter selon votre logique
    createdAt: new Date().toISOString(), // Date actuelle
    updatedAt: new Date().toISOString() // Date actuelle
  };

  incidentTypes: any[] = []; // Liste des types d'incidents
  priorities: any[] = []; // Liste des priorités

  // Flag pour afficher les messages de succès ou d'erreur
  isLoading = false;
  message: string = '';

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit(): void {
    this.fetchIncidentTypes();
    this.fetchPriorities();
  }

  // Récupérer les types d'incidents
  fetchIncidentTypes(): void {
    this.http.get<any[]>('http://localhost:5074/api/Incidents/GetTypeIncident')
      .subscribe((data) => {
        console.log('Types d\'incidents récupérés:', data);
        this.incidentTypes = data;
      }, (error) => {
        console.error('Erreur lors de la récupération des types d\'incidents:', error);
        this.message = 'Erreur lors de la récupération des types d\'incidents.';
      });
  }

  // Récupérer les priorités
  fetchPriorities(): void {
    this.http.get<any[]>('http://localhost:5074/api/Incidents/GetPriorite')
      .subscribe((data) => {
        console.log('Priorités récupérées:', data);
        this.priorities = data;
      }, (error) => {
        console.error('Erreur lors de la récupération des priorités:', error);
        this.message = 'Erreur lors de la récupération des priorités.';
      });
  }

  // Ajouter un incident via l'API
  addIncident() {
    this.isLoading = true; // On active le chargement
    this.message = ''; // Réinitialisation du message

    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    this.http.post('https://localhost:7209/api/Incidents/AddIncident', this.incident, { headers })
      .subscribe(
        (response) => {
          console.log('Incident ajouté avec succès !', response);
          this.isLoading = false;
          this.message = 'Incident ajouté avec succès!';
          // Rediriger l'utilisateur vers la liste des incidents ou autre page
          setTimeout(() => this.router.navigate(['/index']), 2000); // Redirection après 2 secondes
        },
        (error) => {
          console.error('Erreur lors de l\'ajout de l\'incident', error);
          this.isLoading = false;
          this.message = 'Erreur lors de l\'ajout de l\'incident.';
        }
      );
  }
}

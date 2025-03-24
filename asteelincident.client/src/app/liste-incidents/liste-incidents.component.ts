import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-liste-incidents',
  templateUrl: './liste-incidents.component.html',
  styleUrl: './liste-incidents.component.css'
})
export class ListeIncidentsComponent {

incidents: any[] = [];  // Tous les incidents
  filteredIncidents: any[] = []; // Incidents après filtrage

  searchText: string = ''; // Texte de recherche
  selectedType: string = ''; // Type sélectionné
  selectedPriority: string = ''; // Priorité sélectionnée
  selectedStatus: string = ''; // Statut sélectionné

  // Options des filtres (on les remplit après l'appel API)
  incidentTypes: string[] = [];
  priorities: string[] = [];
  statuses: string[] = [];

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getAllIncidents();
  }

  // Récupérer tous les incidents
  getAllIncidents(): void {
    this.http.get<any[]>('http://localhost:5074/api/Incidents/all')
      .subscribe(
        (data) => {
          this.incidents = data;
          this.filteredIncidents = [...data]; // ✅ Cloner les données d'origine

          // Extraire les valeurs uniques pour les filtres
          this.incidentTypes = [...new Set(data.map(incident => String(incident.incidentType)))];
          this.priorities = [...new Set(data.map(incident => String(incident.priority)))];
          this.statuses = [...new Set(data.map(incident => String(incident.status)))];
        },
        (error) => {
          console.error('Erreur lors de la récupération des incidents:', error);
        }
      );
  }

  // Appliquer les filtres
  filterIncidents(): void {
    this.filteredIncidents = this.incidents.filter(incident => {
      return (
        (this.searchText === '' || incident.title.toLowerCase().includes(this.searchText.toLowerCase())) &&
        (this.selectedType === '' || String(incident.incidentType) === this.selectedType) &&  // ✅ Assurer la conversion en string
        (this.selectedPriority === '' || String(incident.priority) === this.selectedPriority) &&
        (this.selectedStatus === '' || String(incident.status) === this.selectedStatus)
      );
    });
  }
}

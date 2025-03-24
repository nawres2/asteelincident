import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-liste-incident',
  templateUrl: './liste-incident.component.html',
  styleUrls: ['./liste-incident.component.css']
})
export class ListeIncidentComponent implements OnInit {

  incidents: any[] = []; // Liste des incidents

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getAllIncidents();
  }

  // Récupérer tous les incidents
  getAllIncidents(): void {
    this.http.get<any[]>('http://localhost:5074/api/Incidents/all')
      .subscribe(
        (data) => {
          console.log("Incidents reçus:", data);
          this.incidents = data;
        },
        (error) => {
          console.error('Erreur lors de la récupération des incidents:', error);
        }
      );
  }
}

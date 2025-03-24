import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-details-incidents',
  templateUrl: './details-incidents.component.html',
  styleUrls: ['./details-incidents.component.css']
})
export class DetailsIncidentsComponent implements OnInit {
  id!: number;
  incident: any = null; // ✅ Ajout de la variable incident
  incidents: any[] = [];

  constructor(private route: ActivatedRoute, private http: HttpClient) { }

  ngOnInit(): void {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
    console.log('ID de l’incident:', this.id);
    this.getAllIncidents();
  }
  getAllIncidents(): void {
    this.http.get<any[]>('http://localhost:5074/api/Incidents/all')
      .subscribe(
        (data) => {
          console.log('Données reçues:', data); // ✅ Vérifier les incidents récupérés
          this.incidents = data;
          this.incident = this.incidents.find(i => i.incidentID === this.id);
          console.log('Incident trouvé:', this.incident); // ✅ Vérifier l’incident sélectionné
        },
        (error) => {
          console.error('Erreur lors de la récupération des incidents:', error);
        }
      );
  }

  getIncidentTypeName(type: string): string {
    console.log('Valeur brute de typeID:', type); // ✅ Vérifier la valeur reçue

    const typeList = [
      { id: 1, name: 'Network' },
      { id: 2, name: 'Hardware' },
      { id: 3, name: 'Software' }
    ];

    // ✅ Chercher par NOM au lieu d'ID
    const typeMatch = typeList.find(t => t.name.toLowerCase() === type.toLowerCase());


    console.log('Nom de l\'utilisateur qui a créé l\'incident:', this.incident?.CreatedBy);

    console.log('Type trouvé:', typeMatch);

    return typeMatch ? typeMatch.name : 'Unknown';
  }



}

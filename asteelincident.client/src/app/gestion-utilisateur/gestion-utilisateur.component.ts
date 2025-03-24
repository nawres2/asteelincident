import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-gestion-utilisateur',
  templateUrl: './gestion-utilisateur.component.html',
  styleUrl: './gestion-utilisateur.component.css'
})
export class GestionUtilisateurComponent {
  users: any[] = []; // Tableau pour stocker la liste des utilisateurs

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getUsers(); // Appel de la méthode pour récupérer les utilisateurs
  }

  getUsers(): void {
    this.http.get<any[]>('http://localhost:5074/api/Users') // Assure-toi que l'URL est correcte
      .subscribe(
        (data) => {
          console.log('Liste des utilisateurs:', data);
          this.users = data;
        },
        (error) => {
          console.error('Erreur lors de la récupération des utilisateurs:', error);
        }
      );

  }
}

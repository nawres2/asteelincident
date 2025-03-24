import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http'; // Import de HttpClient
import { Observable } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})



export class LoginComponent {

  // Définir les propriétés utilisées dans le template
  username: string = '';
  password: string = '';

  constructor(private http: HttpClient, private router: Router) { } // Injection de HttpClient


  // Définir la méthode onLogin
  onLogin() {
    // Construire la requête pour l'API
    const loginRequest = { username: this.username, password: this.password };

    // Effectuer la requête HTTP POST directement depuis le composant
    this.http.post('http://localhost:5074/api/Login/login', loginRequest)
      .subscribe(
        (response: any) => {
          console.log('Utilisateur connecté', response);
          // Traitement après une connexion réussie, par exemple, redirection
          this.router.navigate(['/index']);  // Exemple de redirection

        },
        (error: any) => {
          console.error('Erreur de connexion', error);
        }
      );
  }
}

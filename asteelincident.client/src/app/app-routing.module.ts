import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { IndexComponent } from './index/index.component';  // Assurez-vous d'avoir un composant Home

import { LoginComponent } from './login/login.component';
import {AjouterIncidentComponent} from './ajouter-incident/ajouter-incident.component';
import { ListeIncidentsComponent } from './liste-incidents/liste-incidents.component';
import { DetailsIncidentsComponent } from './details-incidents/details-incidents.component';
import { RaportComponent } from './raport/raport.component';
import { GestionUtilisateurComponent } from './gestion-utilisateur/gestion-utilisateur.component';

const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'index', component: IndexComponent },
  { path: 'ajouter-incident', component: AjouterIncidentComponent },
  { path: 'liste-incident', component: ListeIncidentsComponent },
  { path: 'details-incidents/:id', component: DetailsIncidentsComponent },
  { path: 'raport', component: RaportComponent },
  { path: 'gestion-utilisateur', component: GestionUtilisateurComponent }, 




  { path: '', redirectTo: '/login', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

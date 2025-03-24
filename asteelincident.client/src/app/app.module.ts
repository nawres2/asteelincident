import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { NgChartsModule } from 'ng2-charts';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { FormsModule } from '@angular/forms';
import { IndexComponent } from './index/index.component';
import { NavbarComponent } from './navbar/navbar.component';
import { AjouterIncidentComponent } from './ajouter-incident/ajouter-incident.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { DetailsIncidentsComponent } from './details-incidents/details-incidents.component';
import { ListeIncidentComponent } from './liste-incident/liste-incident.component';
import { ListeIncidentsComponent } from './liste-incidents/liste-incidents.component';

import { CommonModule } from '@angular/common';
import { GestionUtilisateurComponent } from './gestion-utilisateur/gestion-utilisateur.component';
import { RaportComponent } from './raport/raport.component';

@NgModule({
  declarations: [
    AppComponent,

    LoginComponent,
    IndexComponent,
    NavbarComponent,
    AjouterIncidentComponent,
    SidebarComponent,
    DetailsIncidentsComponent,
    ListeIncidentComponent,
    ListeIncidentsComponent,
    GestionUtilisateurComponent,
    RaportComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule, FormsModule, CommonModule, NgChartsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Chart, ChartConfiguration, ChartData, ChartOptions, ChartTypeRegistry, registerables } from 'chart.js';
import ChartDataLabels from 'chartjs-plugin-datalabels';

@Component({
  selector: 'app-raport',
  templateUrl: './raport.component.html',
  styleUrl: './raport.component.css'
})
export class RaportComponent implements OnInit {
  totalIncidents: number = 0;
  pieChartType: keyof ChartTypeRegistry = 'pie';  // Correction du type

  pieChartData: ChartData<'pie'> = {
    labels: [],
    datasets: [{ data: [], backgroundColor: ['#3498db', '#e74c3c', '#f39c12', '#2ecc71'] }]
  };

  pieChartOptions: ChartOptions = {
    responsive: true,
    maintainAspectRatio: false,
    plugins: {
      legend: {
        position: 'bottom',
        labels: {
          padding: 20,
          usePointStyle: true,
          pointStyle: 'circle',
          font: {
            size: 12
          }
        }
      },
      datalabels: {
        color: '#fff',
        font: {
          weight: 'bold'
        },
        formatter: function (value, context) {
          return context.chart.data.labels?.[context.dataIndex];
        }
      }
    }
  };

  lineChartType: keyof ChartTypeRegistry = 'line';  // Correction du type
  lineChartData: ChartData<'line'> = {
    labels: [],
    datasets: [
      {
        data: [],
        label: 'Nouveaux',
        borderColor: '#3498db',
        backgroundColor: 'rgba(52, 152, 219, 0.1)',
        tension: 0.4,
        fill: false
      },
      {
        data: [],
        label: 'En cours',
        borderColor: '#f39c12',
        backgroundColor: 'rgba(243, 156, 18, 0.1)',
        tension: 0.4,
        fill: false
      },
      {
        data: [],
        label: 'Résolus',
        borderColor: '#2ecc71',
        backgroundColor: 'rgba(46, 204, 113, 0.1)',
        tension: 0.4,
        fill: false
      }
    ]
  };

  lineChartOptions: ChartOptions = {  // Correction du type
    responsive: true,
    maintainAspectRatio: false,
    scales: {
      x: {
        grid: {
          display: false
        }
      },
      y: {
        beginAtZero: true
      }
    },
    plugins: {
      legend: {
        position: 'bottom',
        labels: {
          usePointStyle: true,
          padding: 20
        }
      },
      tooltip: {
        mode: 'index',
        intersect: false
      }
    }
  };

  // Données pour l'exportation
  incidentsParType: any[] = [];
  evolutionData: any[] = [];

  constructor(private http: HttpClient) {
    Chart.register(...registerables, ChartDataLabels);
  }

  ngOnInit(): void {
    this.loadData();
    this.loadLineChartData();
  }

  loadData(): void {
    this.http.get<any>('http://localhost:5074/api/Incidents/stats').subscribe(data => {
      console.log("Données reçues:", data);
      this.totalIncidents = data.totalIncidents;
      this.incidentsParType = data.incidentsParType || [];  // Sauvegarder pour l'exportation

      if (!data || !data.incidentsParType) {
        console.error("Données manquantes!");
        return;
      }

      // Extraire les noms et les nombres
      const typeNames = data.incidentsParType.map((i: any) => i.type.typeName);
      const counts = data.incidentsParType.map((i: any) => i.nombre);

      // Calculer les pourcentages
      const total = counts.reduce((sum: number, count: number) => sum + count, 0);
      const percentages = counts.map((count: number) => Math.round((count / total) * 100));

      // Créer les libellés avec pourcentages
      const labelsWithPercentages = typeNames.map((name: string, index: number) =>
        `${name} (${percentages[index]}%)`);

      // Créer un nouvel objet pour forcer la détection de changement
      this.pieChartData = {
        labels: labelsWithPercentages,
        datasets: [{
          data: counts,
          backgroundColor: ['#3498db', '#e74c3c', '#f39c12', '#2ecc71']
        }]
      };
    }, error => {
      console.error("Erreur lors de la récupération des données:", error);
    });
  }

  loadLineChartData(): void {
    this.http.get<any[]>('http://localhost:5074/api/Incidents/evolution').subscribe(
      (data) => {
        console.log("Données d'évolution reçues:", data);
        this.evolutionData = data || [];  // Sauvegarder pour l'exportation

        if (!data || !data.length) {
          console.error("Données d'évolution manquantes ou vides!");
          return;
        }

        // Trier les données par date
        const sortedData = data.sort((a: any, b: any) =>
          new Date(a.date).getTime() - new Date(b.date).getTime()
        );

        // Extraire les dates pour les étiquettes
        const labels = sortedData.map((item: any) => {
          const date = new Date(item.date);
          return `${date.getDate().toString().padStart(2, '0')}/${(date.getMonth() + 1).toString().padStart(2, '0')}`;
        });

        // Extraire les données pour chaque statut
        const nouveauxData = sortedData.map((item: any) => item.nouveaux || 0);
        const enCoursData = sortedData.map((item: any) => item.enCours || 0);
        const resolusData = sortedData.map((item: any) => item.resolus || 0);

        // Mettre à jour les données du graphique
        this.lineChartData = {
          labels,
          datasets: [
            {
              data: nouveauxData,
              label: 'Nouveaux',
              borderColor: '#3498db',
              backgroundColor: 'rgba(52, 152, 219, 0.1)',
              tension: 0.4,
              fill: false
            },
            {
              data: enCoursData,
              label: 'En cours',
              borderColor: '#f39c12',
              backgroundColor: 'rgba(243, 156, 18, 0.1)',
              tension: 0.4,
              fill: false
            },
            {
              data: resolusData,
              label: 'Résolus',
              borderColor: '#2ecc71',
              backgroundColor: 'rgba(46, 204, 113, 0.1)',
              tension: 0.4,
              fill: false
            }
          ]
        };
      },
      (error) => {
        console.error("Erreur lors de la récupération des données d'évolution:", error);
      }
    );
  }

  // Méthode 1: Exportation côté client
  exportToCsv(): void {
    // Vérifier si des données sont disponibles
    if ((!this.incidentsParType || this.incidentsParType.length === 0) &&
      (!this.evolutionData || this.evolutionData.length === 0)) {
      alert('Aucune donnée à exporter');
      return;
    }

    // Créer le contenu CSV
    let csvContent = "Rapport d'incidents\n\n";

    // Ajouter la section Total
    csvContent += `Total d'incidents;${this.totalIncidents}\n\n`;

    // Ajouter la section Incidents par type
    csvContent += "Incidents par type\n";
    csvContent += "Type;Nombre;Pourcentage\n";

    let totalIncidentsByType = 0;
    this.incidentsParType.forEach(item => {
      totalIncidentsByType += item.nombre;
    });

    this.incidentsParType.forEach(item => {
      const percentage = ((item.nombre / totalIncidentsByType) * 100).toFixed(1);
      csvContent += `${item.type.typeName};${item.nombre};${percentage}%\n`;
    });

    csvContent += "\n";

    // Ajouter la section Évolution du statut des incidents
    csvContent += "Évolution du statut des incidents\n";
    csvContent += "Date;Nouveaux;En cours;Résolus\n";

    this.evolutionData.sort((a, b) => new Date(a.date).getTime() - new Date(b.date).getTime())
      .forEach(item => {
        const date = new Date(item.date);
        const formattedDate = `${date.getDate().toString().padStart(2, '0')}/${(date.getMonth() + 1).toString().padStart(2, '0')}/${date.getFullYear()}`;
        csvContent += `${formattedDate};${item.nouveaux || 0};${item.enCours || 0};${item.resolus || 0}\n`;
      });

    // Encodage pour gérer les caractères spéciaux
    const encodedUri = 'data:text/csv;charset=utf-8,' + encodeURIComponent(csvContent);

    // Créer un lien de téléchargement et cliquer dessus automatiquement
    const link = document.createElement('a');
    link.setAttribute('href', encodedUri);
    link.setAttribute('download', `rapport_incidents_${new Date().toISOString().split('T')[0]}.csv`);
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
  }

  // Méthode 2: Utiliser le backend pour l'exportation
  exportToCsvFromBackend(): void {
    // Appeler l'API backend qui génère le CSV
    this.http.get('http://localhost:5074/api/Incidents/export-csv', {
      responseType: 'blob'
    }).subscribe(
      (data: Blob) => {
        // Créer un objet URL pour le blob
        const url = window.URL.createObjectURL(data);

        // Créer un élément a pour le téléchargement
        const link = document.createElement('a');
        link.href = url;
        link.download = `rapport_incidents_${new Date().toISOString().split('T')[0]}.csv`;

        // Simuler un clic sur le lien pour démarrer le téléchargement
        document.body.appendChild(link);
        link.click();

        // Nettoyer
        document.body.removeChild(link);
        window.URL.revokeObjectURL(url);
      },
      (error) => {
        console.error('Erreur lors de l\'exportation CSV:', error);
        alert('Erreur lors de l\'exportation du rapport. Veuillez réessayer.');
      }
    );
  }
}

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AjouterIncidentComponent } from './ajouter-incident.component';

describe('AjouterIncidentComponent', () => {
  let component: AjouterIncidentComponent;
  let fixture: ComponentFixture<AjouterIncidentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AjouterIncidentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AjouterIncidentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

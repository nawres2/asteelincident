import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListeIncidentComponent } from './liste-incident.component';

describe('ListeIncidentComponent', () => {
  let component: ListeIncidentComponent;
  let fixture: ComponentFixture<ListeIncidentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ListeIncidentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ListeIncidentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

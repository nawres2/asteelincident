import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListeIncidentsComponent } from './liste-incidents.component';

describe('ListeIncidentsComponent', () => {
  let component: ListeIncidentsComponent;
  let fixture: ComponentFixture<ListeIncidentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ListeIncidentsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ListeIncidentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

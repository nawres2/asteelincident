import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetailsIncidentsComponent } from './details-incidents.component';

describe('DetailsIncidentsComponent', () => {
  let component: DetailsIncidentsComponent;
  let fixture: ComponentFixture<DetailsIncidentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DetailsIncidentsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DetailsIncidentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

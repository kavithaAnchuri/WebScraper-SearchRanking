import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchRankingComponent } from './search-ranking.component';

describe('SearchRankingComponent', () => {
  let component: SearchRankingComponent;
  let fixture: ComponentFixture<SearchRankingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SearchRankingComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SearchRankingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

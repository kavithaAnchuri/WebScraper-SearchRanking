import { TestBed } from '@angular/core/testing';

import { SearchRankingService } from './search-ranking.service';

describe('SearchRankingService', () => {
  let service: SearchRankingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SearchRankingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

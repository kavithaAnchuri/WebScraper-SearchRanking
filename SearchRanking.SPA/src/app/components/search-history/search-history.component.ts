import { Component, OnInit, OnDestroy } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { SearchRankingService } from '../../services/search-ranking.service';
import { SearchHistory } from '../../models/searchHistory';
import { SharedService } from '../../services/shared.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-search-history',
  templateUrl: './search-history.component.html',
  styleUrl: './search-history.component.css'
})
export class SearchHistoryComponent implements OnInit {
  searchHistory: SearchHistory[] = [];
  private refreshSubscription: Subscription | undefined;;

  constructor(
    private searchService: SearchRankingService, 
    private toastr: ToastrService,
    private sharedService: SharedService
  ) { }

  ngOnInit(): void {
    this.loadSearchHistory();

    // Subscribe to refresh notifications
    this.refreshSubscription = this.sharedService.refreshHistory$.subscribe(() => {
      this.loadSearchHistory();
    });
  }

  ngOnDestroy(): void {
    // Clean up the subscription when the component is destroyed
    if (this.refreshSubscription) {
      this.refreshSubscription.unsubscribe();
    }
  }

  loadSearchHistory(): void {
    this.searchService.getSearchHistory().subscribe({
      next: (data: SearchHistory[]) => {
        this.searchHistory = data;
      },
      error: (err) => {
        console.error('Error loading search history:', err);
      }
    });
  }

}
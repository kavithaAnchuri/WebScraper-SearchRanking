import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { NgForm } from "@angular/forms";
import { SearchRankingService } from '../../services/search-ranking.service';
import { SearchResult } from '../../models/searchResult';
import { SharedService } from '../../services/shared.service';

@Component({
  selector: 'app-search-ranking',
  templateUrl: './search-ranking.component.html',
  styleUrl: './search-ranking.component.css'
})
export class SearchRankingComponent{
  rankings: string | null = null; 
  showMessage: boolean = false;
  isLoading: boolean = false;
  
  constructor(
    private service: SearchRankingService, 
    private toastr: ToastrService,
    private sharedService: SharedService
  ) { }

  onSubmit(form: NgForm) {
    if (form.valid) {
      this.isLoading = true;
      const { keywords, url } = form.value;
      
      this.service.newSearch(keywords, url).subscribe({
        next: (response: string) => {
          console.log('res- ' + response);
          this.rankings = response; 
          this.isLoading = false;  
          this.showMessage = true; 
        
          // Hide the message after 3 seconds
          setTimeout(() => {
            this.showMessage = false;
          }, 3000);
          //Notify to refresh search history
          this.sharedService.triggerRefresh();
        },
        error: (err) => {
          this.isLoading = false;
        }
      });
    } else {
      console.warn('Form is invalid');
    }
  }

}

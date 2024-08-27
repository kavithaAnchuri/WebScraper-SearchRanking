import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
  private refreshSearchHistory = new Subject<void>();

  refreshHistory$ = this.refreshSearchHistory.asObservable();

  triggerRefresh(): void {
    this.refreshSearchHistory.next();
  }
}

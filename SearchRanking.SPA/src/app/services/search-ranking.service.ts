import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { SearchRequest } from '../models/searchRequest';
import { SearchResult } from '../models/searchResult';
import { SearchHistory } from '../models/searchHistory';
import { Observable, catchError, of} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SearchRankingService {
  baseUrl: string = environment.apiBaseUrl;

  constructor(private http: HttpClient) { }

  newSearch(Keyword: string, Url: string):Observable<string> {
    const payload: SearchRequest = {
      Keyword,
      Url
    };

    return this.http.post<string>(`${this.baseUrl}/Search`, payload);
  }

  // Fetch search history from the server
  getSearchHistory(): Observable<SearchHistory[]> {
    return this.http.get<SearchHistory[]>(`${this.baseUrl}/Search/history`).pipe(
      catchError(this.handleError<SearchHistory[]>('getSearchHistory', []))
    );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(`${operation} failed: ${error.message}`);
      return of(result as T);
    };
  }
}

import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { provideHttpClient } from '@angular/common/http'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { SearchRankingComponent } from '../app/components/search-ranking/search-ranking.component';
import { SearchHistoryComponent } from '../app/components/search-history/search-history.component';

@NgModule({
  declarations: [
    AppComponent,
    SearchRankingComponent,
    SearchHistoryComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    BrowserAnimationsModule, 
    ToastrModule.forRoot(),
  ],
  providers: [
    provideHttpClient()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

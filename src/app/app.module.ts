import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { ChordsStorageComponent } from './chords-storage/chords-storage.component';
import { HttpClientModule } from '@angular/common/http';
import { ChordsStorageDetailComponent } from './chords-storage/chords-storage-detail.component';
import { AddChordSheetComponent } from './add-chord-sheet/add-chord-sheet.component';
import { HomeComponent } from './home/home.component';
import { BlogsComponent } from './blogs/blogs.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    ChordsStorageComponent,
    ChordsStorageDetailComponent,
    AddChordSheetComponent,
    HomeComponent,
    BlogsComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

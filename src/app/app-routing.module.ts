import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddChordSheetComponent } from './add-chord-sheet/add-chord-sheet.component';
import { AppComponent } from './app.component';
import { BlogsComponent } from './blogs/blogs.component';
import { ChordsStorageDetailComponent } from './chords-storage/chords-storage-detail.component';
import { ChordsStorageComponent } from './chords-storage/chords-storage.component';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  {
    path: '',
    component: AppComponent
  },
  {
    path: 'home',
    component: HomeComponent
  },
  {
    path: 'chords',
    component: ChordsStorageComponent
  },
  {
    path: 'chords/edit/:id',
    component: ChordsStorageDetailComponent
  },
  {
    path: 'chords/add',
    component: AddChordSheetComponent
  },
  {
    path: 'blogs',
    component: BlogsComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

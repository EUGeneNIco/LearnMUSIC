import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { AuthGuard } from './auth.guard';
import { ChordsStorageDetailComponent } from './chords-storage/chords-storage-detail.component';
import { ChordsStorageComponent } from './chords-storage/chords-storage.component';
import { FeedbackComponent } from './feedback/feedback.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';

const routes: Routes = [
  {
    path: '', canActivate: [AuthGuard],component: HomeComponent, data: { module: 'Home'}
  },
  {
    path: 'chords', canActivate: [AuthGuard],component: ChordsStorageComponent, data: { module: 'SongSheet'}
  },
  {
    path: 'chords/:id', canActivate: [AuthGuard],component: ChordsStorageDetailComponent, data: { module: 'SongSheet'}
  },
  {
    path: 'chords/detail/:id', canActivate: [AuthGuard],component: ChordsStorageDetailComponent, data: { module: 'SongSheet'}
  },
  {
    path: 'feedback', canActivate: [AuthGuard],component: FeedbackComponent, data: { module: 'Feedback'}
  },
  {
    path: 'register',component: RegisterComponent,
  },
  {
    path: 'login', component: LoginComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { ChordsStorageComponent } from './chords-storage/chords-storage.component';
import { HttpClientModule } from '@angular/common/http';
import { ChordsStorageDetailComponent } from './chords-storage/chords-storage-detail.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { ToastrModule } from 'ngx-toastr';
import { Globals } from './globals';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RegisterComponent } from './register/register.component';
import { FeedbackComponent } from './feedback/feedback.component';
import { AdminComponent } from './admin/admin.component';
import { ProfileComponent } from './profile/profile.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    ChordsStorageComponent,
    ChordsStorageDetailComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    FeedbackComponent,
    AdminComponent,
    ProfileComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    ToastrModule.forRoot({
      positionClass: 'toast-top-right',
    }),
    BrowserAnimationsModule,
  ],
  providers: [Globals,],
  bootstrap: [AppComponent]
})
export class AppModule { }

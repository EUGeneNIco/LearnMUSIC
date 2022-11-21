import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../service/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  loggedIn: boolean = false;

  constructor(
    private router: Router,
    private authService: AuthService,
  ) { }

  ngOnInit(): void {
    this.checkIfLoggedIn();
  }

  goToFeedBackPage(){
    this.router.navigateByUrl('feedback');
  }

  checkIfLoggedIn(){
    this.authService.authToken != null ? this.loggedIn = true : this.loggedIn = false;
  }

  goToLoginPage(){
    this.router.navigateByUrl('login');
  }

  goToRegisterPage(){
    this.router.navigateByUrl('register');
  }



  

}

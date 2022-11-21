import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../service/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  loggedOut: boolean = false;

  constructor(
    private toastr: ToastrService,
    private authService: AuthService,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.checkIfSignedIn();
  }

  signOut() {
    this.authService.destroyAuthToken();

    this.router.navigateByUrl('login');
    this.loggedOut = true;
    this.toastr.success("Logged out.");
  }

  checkIfSignedIn(){
    this.authService.authToken != null ? this.loggedOut = true : this.loggedOut = false;
  }

}

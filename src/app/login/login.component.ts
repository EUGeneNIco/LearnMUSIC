import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../service/auth.service';
import { ApiCallStatusCodes } from '../_enums/enums';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm!: FormGroup;

  get userName() { return this.loginForm.get("userName"); }
  get password() { return this.loginForm.get("password"); }

  constructor(
    private router: Router,
    private authService: AuthService,
    private fb: FormBuilder,
    private toastr: ToastrService,
  ) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(){
    this.loginForm = this.fb.group({
      userName: ['', Validators.required],
      password: ['', Validators.required]
    })
  }

  login(){
    if(this.loginForm.valid){
      const record = this.loginForm.getRawValue();
      console.log(record);
      
      this.authService.login({
        username: record.userName,
        password: record.password,
      }).subscribe({
        next:(data: any) => {
					this.authService.authToken = JSON.parse(data).token;

          this.router.navigateByUrl('');
        },
        error: (e) => {

					if (e.status == 0) {
						// this.msgs.push({ severity: 'warn', detail: 'Server is not available. Please try again later.' });
            this.toastr.error("Server is not available. Please try again later")
					}
					else if (e.status == ApiCallStatusCodes.UNAUTHORIZED) {
						// this.msgs.push({ severity: 'error', detail: err.error });
            // console.log(e);
            this.toastr.error(e.error)
					}
					else {
						// this.msgs.push({ severity: 'warn', detail: err.error });
            // console.log(e);
            this.toastr.error(e.error)
					}
        }
      })
    }
    else{
      this.toastr.warning("Enter username and password.")
    }
  }

}

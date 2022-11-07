import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ValidationErrors } from '@angular/forms';

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
    private fb: FormBuilder,
  ) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(){
    this.loginForm = this.fb.group({
      userName: [''],
      password: ['']
    })
  }

  login(){
    if(this.loginForm.valid){
      const record = this.loginForm.getRawValue();
      console.log(record);
    }
    else{
    }
  }

}

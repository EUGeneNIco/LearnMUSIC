import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../service/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm!: FormGroup

  get userName() { return this.registerForm.get('userName'); };
  get passWord() { return this.registerForm.get('passWord'); };
  get firstName() { return this.registerForm.get('firstName'); };
  get lastName() { return this.registerForm.get('lastName'); };
  get email() { return this.registerForm.get('email'); };

  constructor(
    private router: Router,
    private toastr: ToastrService,
    private auth: AuthService,
    private fb: FormBuilder,
  ) { }

  ngOnInit(): void {
    this.initializeForm();
  }


  initializeForm(){
    if(!this.registerForm){
      this.registerForm = this.fb.group({
        userName: ['', Validators.required],
        passWord: ['', Validators.required],
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        email: ['', Validators.required],
      })
    }
  }

  register(){
    if(this.registerForm.valid){
      const record = this.registerForm.getRawValue();
      this.auth.register(record).subscribe({
        next: (data: any) =>{
          this.toastr.success("Registration is successfull");
  
          this.router.navigateByUrl('login');
        },
        error: (e) => {
          this.toastr.error(e.error);
        }
      })
    }
    else{
      this.toastr.warning("Some fields are not finished.")
    }
    
  }

  backToLogin(){
    this.router.navigateByUrl('login');
  }

}

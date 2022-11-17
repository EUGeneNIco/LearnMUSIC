import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../service/auth.service';
import { FeedbackService } from '../service/feedback.service';
import { NotificationMessages } from '../_enums/notification-messages';

@Component({
  selector: 'app-feedback',
  templateUrl: './feedback.component.html',
  styleUrls: ['./feedback.component.css']
})
export class FeedbackComponent implements OnInit {

  formModel!: FormGroup
  constructor(
    public router: Router,
    private authService: AuthService,
    private feedBackService: FeedbackService,
    private toastr: ToastrService,
    private fb: FormBuilder,
  ) { }

  
  get subject() { return this.formModel.get('subject'); }
  get content() { return this.formModel.get('content'); }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(){
    if(!this.formModel){
      this.formModel = this.fb.group({
        subject: ['', Validators.required],
        content: ['', Validators.required]
      })
    }
  }

  backToHomePage(){
    this.router.navigateByUrl('home');
  }

  submit(){
    const record = this.formModel.getRawValue();

    if(this.formModel.valid){
      this.feedBackService.sendFeedback({
        subject: record.subject,
        content: record.content,
        userId: this.authService.userID,
      }).subscribe({
        next: (data: any) => {
          this.toastr.success(NotificationMessages.SaveSuccessful.Message);
          setTimeout(() => this.backToHomePage(), 500);
        },
        error: (e) => {
          this.toastr.error(e.error);
        }
      })
    }
    else{
      this.toastr.warning("Some fields are not finished.");
    }
  }

}

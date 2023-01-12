import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { FeedbackService } from '../service/feedback.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

  feedbacks: any[] = [];
  seeFeedbacks: boolean = false;

  constructor(
    private feedbackService: FeedbackService,
    private toastr: ToastrService,
  ) { }

  ngOnInit(): void {
    this.feedbackService.getAllFeedback().subscribe({
      next: (data: any) => {
        console.log("Feedbacks are: ", data);
        data.forEach((fb:any) => {
          fb.createdOn = fb.createdOn.slice(0, fb.createdOn.indexOf('T'))
        });

        this.feedbacks = data;
      },
      error: (e) => {
        this.toastr.error(e.error);
      }
    })
  }

}

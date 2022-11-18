import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { of } from 'rxjs';
import { AuthService } from '../service/auth.service';
import { CodeListValuesService } from '../service/code-list-values.service';
import { SongSheetService } from '../service/song-sheet.service';
import { ConfirmationMessages } from '../_enums/confirmation-messages';
import { NotificationMessages } from '../_enums/notification-messages';

@Component({
  selector: 'app-chords-storage-detail',
  templateUrl: './chords-storage-detail.component.html',
  styleUrls: ['./chords-storage-detail.component.css']
})
export class ChordsStorageDetailComponent implements OnInit {

  addForm!: FormGroup;
  
  sheets: any[] = [];
  editMode: boolean = false;
  viewMode: boolean = false;
  isViewMode: boolean = false;
  addMode: boolean = false;
  songSheetId: any;
  formMode: any;

  genresOptions: any[] = [];

  get songTitle() { return this.addForm.get('songTitle'); }
  get singer() { return this.addForm.get('singer'); }
  get keySignature() { return this.addForm.get('keySignature'); }
  get contents() { return this.addForm.get('contents'); }

  constructor(
    private codeListValueService: CodeListValuesService,
    private authService: AuthService,
    private toastr: ToastrService,
    public songSheetService: SongSheetService,
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.initializeForm();
    this.initializeAll();
    this.checkViewMode();
    this.getIdFromRoute();

    if (this.songSheetId > 0) {
      this.loadRecordData();

      // Enable/Disable Form
      this.isViewMode ? this.addForm.disable() : this.addForm.enable();

      this.formMode = this.isViewMode ? 'View' : 'Edit';
      console.log("{0} Mode" , this.formMode)
      console.log("Add Mode", this.addForm)
    }
    else {
        this.formMode = 'Add';
        this.addMode = true;
        console.log("Add Mode", this.addForm)
    }

    this.getGenres();
  }

  checkViewMode() {
    this.isViewMode = !this.router.url.includes("detail");
  }

  loadRecordData(){
    this.songSheetService.getSheetById(this.songSheetId).subscribe({
      next: (data: any) => {
        // console.log(data);
        this.addForm.patchValue(data);
      },
      error: (e) => {
        this.toastr.error(e.error);
      }
    })
  }

  getGenres(){
    this.codeListValueService.getGenres().subscribe({
      next: (data: any) => {
        console.log(data);
        this.genresOptions = data;
      },
      error: (e) => {
        this.toastr.error(e.error);
      }
    })
  }

  initializeForm() {
    if(!this.addForm){
      this.addForm = this.fb.group({
        songTitle: ['', Validators.required],
        singer: ['', Validators.required],
        keySignature: ['', Validators.required],
        contents: ['', Validators.required]
    })
  }
};

  initializeAll(){

  }

  getIdFromRoute(){
    this.route.paramMap.subscribe({
      next: (params) => {
        const id = params.get('id');
        this.songSheetId = id;
      },
      error: (e) => {
        this.toastr.error(e.error);
      }
    })
  }

  edit(){
    this.addForm.enable();
    this.isViewMode = false;
  }

  save(){
    var record = this.addForm.getRawValue();
    console.log(record);
    if(this.addForm.valid === true){

      // console.log("Valid!: ", this.addForm.valid)
      // if(this.songSheetId > 0){
      //   // console.log(record);
      //   this.songSheetService.updateSheet({
      //     songTitle: record.songTitle,
      //     singer: record.singer,
      //     keySignature: record.keySignature,
      //     contents: record.contents,
      //     id: this.songSheetId,
      //   }).subscribe({
      //     next:(data: any) => {
      //       // console.log(data);
      //       this.toastr.success(NotificationMessages.SaveSuccessful.Message)
      //       setTimeout(() => this.backToListPage(), 500);
      //       ;
      //     },
      //     error:(e) => {
      //       this.toastr.error(e.error);
      //       // console.log(e);
      //     }
      //   })
      // }
      // else{
      //   this.songSheetService.addSongSheet({
      //     userId: this.authService.userID,
      //     songTitle: record.songTitle,
      //     singer: record.singer,
      //     keySignature: record.keySignature,
      //     contents: record.contents,
      //   }
      //     ).subscribe({
      //     next: (data: any) => {
      //       // console.log(data);
      //       this.toastr.success(NotificationMessages.SaveSuccessful.Message);
      //       setTimeout(() => this.backToListPage(), 500);
      //     },
      //     error: (e) => {
      //       this.toastr.error(e.error);
      //     }
      //   })
      // }
    }
    else {
      this.toastr.warning("Some fields are not finished.");
    }
  }

  deleteSongSheet(){
    this.songSheetService.delete(this.songSheetId).subscribe({
      next: (data: any) => {
        // console.log(data);
        this.toastr.success(NotificationMessages.DeleteSuccessful.Message)
        setTimeout(() => this.backToListPage(), 500);
      },
      error: (e) => {
        this.toastr.error(e.error);
      }
    })
  }

  backToListPage(){
    this.router.navigateByUrl('chords');
  }
}

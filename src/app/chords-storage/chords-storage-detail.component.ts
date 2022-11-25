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
import { SelectItem } from '../_helpers/selectItem';

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

  keysOptions: SelectItem[] = [];
  genreOptions: SelectItem[] = [];

  get songTitle() { return this.addForm.get('songTitle'); }
  get singer() { return this.addForm.get('singer'); }
  get keySignature() { return this.addForm.get('keySignature'); }
  get contents() { return this.addForm.get('contents'); }
  get genre() { return this.addForm.get('genre'); }

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
    this.checkViewMode();
    this.getIdFromRoute();
    
    this.getKeys();
    this.getGenres();

    this.checkWhatMode()
  }

  checkWhatMode(){
    if (this.songSheetId > 0) {
      this.loadRecordData();

      // Enable/Disable Form
      this.isViewMode ? this.addForm.disable() : this.addForm.enable();

      this.formMode = this.isViewMode ? 'View' : 'Edit';
      // console.log("Mode" , this.formMode)
      // console.log("View Mode" , this.isViewMode)
      // console.log("Add Mode", this.addMode)
    }
    else {
        this.formMode = 'Add';
        this.addMode = true;
        // console.log("Add Mode", this.addForm)
    }
  }

  checkViewMode() {
    this.isViewMode = !this.router.url.includes("detail");
  }

  loadRecordData(){
    this.songSheetService.getSheetById(this.songSheetId).subscribe({
      next: (data: any) => {
        // console.log("Data: ", data);
        this.addForm.patchValue(data);

        let genre = this.genreOptions.find(x => { return x.value === data.genreId } );
        this.genre?.setValue(genre?.value);

        let key = this.keysOptions.find(x => { return x.value === data.keySignatureId } );
        this.keySignature?.setValue(key?.value);
      },
      error: (e) => {
        this.toastr.error(e.error);
      }
    })
  }

  getKeys(){
    this.codeListValueService.getKeys().subscribe({
      next: (data: any) => {
        // console.log("Keys: ",data);
        this.keysOptions = data.map((key: any) => {
          return { label: key.name, value: key.id }
        })
        
        // console.log("Key Options: ",this.keysOptions);
      },
      error: (e) => {
        this.toastr.error(e.error);
      }
    })
  }

  getGenres(){
    this.codeListValueService.getGenres().subscribe({
      next: (data: any) => {
        // console.log("Genres: ",data);
        this.genreOptions = data.map((genre: any) => {
          return { label: genre.name, value: genre.id };
        })
        // console.log("Genre Options: ",this.genreOptions);
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
        contents: ['', Validators.required],
        genre: ['', Validators.required],
    })
  }
};

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
      if(this.songSheetId > 0){
        // console.log(record);
        this.songSheetService.updateSheet({
          songTitle: record.songTitle,
          singer: record.singer,
          keySignatureId: record.keySignature.toString(),
          genreId: record.genre.toString(),
          contents: record.contents,
          id: this.songSheetId,
        }).subscribe({
          next:(data: any) => {
            // console.log(data);
            this.toastr.success(NotificationMessages.SaveSuccessful.Message)
            setTimeout(() => this.backToListPage(), 500);
            ;
          },
          error:(e) => {
            this.toastr.error(e.error);
            // console.log(e);
          }
        })
      }
      else{
        this.songSheetService.addSongSheet({
          userId: this.authService.userID,
          songTitle: record.songTitle,
          singer: record.singer,
          keySignatureId: record.keySignature,
          genreId: record.genre,
          contents: record.contents,
        }
          ).subscribe({
          next: (data: any) => {
            // console.log(data);
            this.toastr.success(NotificationMessages.SaveSuccessful.Message);
            setTimeout(() => this.backToListPage(), 500);
          },
          error: (e) => {
            this.toastr.error(e.error);
          }
        })
      }
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

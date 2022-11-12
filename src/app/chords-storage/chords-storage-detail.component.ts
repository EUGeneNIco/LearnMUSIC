import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { of } from 'rxjs';
import { SongSheetService } from '../service/song-sheet.service';

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
  songSheetId: any;

  get songTitle() { return this.addForm.get('songTitle'); }
  get singer() { return this.addForm.get('singer'); }
  get keySignature() { return this.addForm.get('keySignature'); }
  get contents() { return this.addForm.get('contents'); }

  constructor(
    public songSheetService: SongSheetService,
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.initializeForm();
    this.initializeAll();

    this.route.paramMap.subscribe({
      next: (params) => {
        const id = params.get('id');
        this.songSheetId = id;

        if(id){
          this.songSheetService.getSheetById(id).subscribe({
            next: (data: any) => {
              console.log(data);
              this.songTitle?.setValue(data.songTitle);
              this.singer?.setValue(data.singer);
              this.keySignature?.setValue(data.keySignature);
              this.contents?.setValue(data.contents);
            },
            error: (e) => {
              console.log(e);
            }
          })
        }
      },
      error: (e) => {
        console.log(e);
      }
    })
  }

  initializeForm() {
    if(!this.addForm){
      this.addForm = this.fb.group({
        songTitle: [''],
        singer: [''],
        keySignature: [''],
        contents: ['']
    })
  }
};

  initializeAll(){
    this.onViewMode();
  }

  onViewMode(){
    this.addForm.disable();
    this.editMode = false;
  }

  onEditMode(){
    this.addForm.enable();
    this.editMode = true;
  }

  deleteCard(songSheetId: any){
  }

  addCard(){
  }

  edit(){
    this.onEditMode();
  }

  getCardDetail(songSheetId: any){
  }

  updateSheet(){
    var record = this.addForm.getRawValue();
    console.log(record);
    this.songSheetService.updateSheet({
      songTitle: record.songTitle,
      singer: record.singer,
      keySignature: record.keySignature,
      contents: record.contents,
      id: this.songSheetId,
    }).subscribe({
      next:(data: any) => {
        // console.log(data);
        this.backToListPage();
      },
      error:(e) => {
        console.log(e);
      }
    })
  }

  deleteSongSheet(){
    this.songSheetService.delete(this.songSheetId).subscribe({
      next: (data: any) => {
        // console.log(data);
        this.backToListPage();
      },
      error: (e) => {
        console.log(e);
      }
    })
  }

  cancelEdit(){
    console.log("Cancelling...");

    this.backToListPage();
    
  }

  backToListPage(){
    this.router.navigateByUrl('chords');
  }
}

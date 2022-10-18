import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { SongSheetService } from '../service/song-sheet.service';

@Component({
  selector: 'app-add-chord-sheet',
  templateUrl: './add-chord-sheet.component.html',
  styleUrls: ['./add-chord-sheet.component.css']
})
export class AddChordSheetComponent implements OnInit {

  addForm!: FormGroup;

  
  get songTitle() { return this.addForm.get('songTitle'); }
  get singer() { return this.addForm.get('singer'); }
  get keySignature() { return this.addForm.get('keySignature'); }
  get contents() { return this.addForm.get('contents'); }

  constructor(
    private fb: FormBuilder,
    public songSheetService: SongSheetService,
    public router: Router
  ) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    if(!this.addForm){
      this.addForm = this.fb.group({
        songTitle: ['',],
        singer: [''],
        keySignature: [''],
        contents: ['']
    })
  }
};

  addChordSheet(){
    var record = this.addForm.getRawValue();
    this.songSheetService.addSongSheet(record).subscribe({
      next: (data: any) => {
        console.log(data);
        this.backToListPage();
      },
      error: (e) => {
        console.log(e);
      }
    })
  }

  backToListPage(){
    this.router.navigateByUrl('chords');
  }

  cancel(){
    this.backToListPage();
  }

}

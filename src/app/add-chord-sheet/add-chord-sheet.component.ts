import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
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
    private toastr: ToastrService,
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
        songTitle: ['', Validators.required],
        singer: ['', Validators.required],
        keySignature: ['', Validators.required],
        contents: ['', Validators.required]
    })
  }
  };

  addChordSheet(){
    var record = this.addForm.getRawValue();
    this.songSheetService.addSongSheet(record).subscribe({
      next: (data: any) => {
        // console.log(data);
        this.toastr.success("Song sheet added.");
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

  cancel(){
    this.backToListPage();
  }
}

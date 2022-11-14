
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SongSheetService } from '../service/song-sheet.service';

@Component({
  selector: 'app-chords-storage',
  templateUrl: './chords-storage.component.html',
  styleUrls: ['./chords-storage.component.css']
})
export class ChordsStorageComponent implements OnInit {

  title = 'LearnMUSIC';
  
  needMorePage: boolean = false;
  hasPrevPage: boolean = false;
  sheets: any[] = [];
  allSheets: any[] = [];
  sheetsOnPage: any[] = [];
  editMode: boolean = false;
  viewMode: boolean = false;
  id: number = 0;
  videos: any[] = [];

  //Pagin
  pageLimit: any = 6;
  lastSheetNo: any = 0;
  sheetsLeft: any = 0;
  lastLastSheetNo: any = 0;

  constructor(
    private songSheetService: SongSheetService, 
    private fb: FormBuilder,
    public router: Router,
    private route: ActivatedRoute,)
    { }
  
  ngOnInit(): void {
    this.getAllCards();

    this.videos = [ 
      { src: "https://www.youtube.com/embed/6cuoH5M8U9Y"},
      { src: "https://www.youtube.com/embed/oXfNViGuojw"},
    ]
  }

  getAllCards(){
    this.songSheetService.getAllMembers().subscribe({
      next: (data: any) => {
        //console.log(data);
        if(data.length > 7){
          this.needMorePage = true;
          this.allSheets = data;
          
          // console.log("Need more page?? " + this.needMorePage);
          // console.log("New sheets: " +  this.sheets.length);
          // console.log("All sheets: " + this.allSheets.length);
          // this.showNextPage();
          this.showFirstPage();
        }
        else{
          this.sheets = data;
        }
      },
      error: (e) => {
        console.log(e);
      }
    })
  }

  showFirstPage(){
    for(let index = 0; index < 6; index++){
      this.sheets.push(this.allSheets[index]);
    }
    this.lastSheetNo = 6;
    this.sheetsLeft = this.allSheets.length - this.lastSheetNo;
    this.showStatus();
  }

  showNextPage(){
    while(this.sheets.length > 0){
      this.sheets.pop();
    }

    this.hasPrevPage = true;

      if(this.sheetsLeft >= 6){

        for(let index = this.lastSheetNo; index < (this.lastSheetNo + 6); index++){
          this.sheets.push(this.allSheets[index]);
        }
    
        this.lastLastSheetNo = this.lastSheetNo;
        this.lastSheetNo = this.lastSheetNo + 6;
        this.sheetsLeft = this.allSheets.length - this.lastSheetNo;

        this.showStatus();

        if(this.sheetsLeft === 0){
          this.needMorePage = false;
        }

      }
      else if(this.sheetsLeft < 6){
        for(let index = this.lastSheetNo; index < this.allSheets.length; index++){
          this.sheets.push(this.allSheets[index]);
        }
        
        this.needMorePage = false;
        this.lastLastSheetNo = this.lastSheetNo;
        this.lastSheetNo = this.lastSheetNo + this.sheetsLeft;
        this.sheetsLeft = this.allSheets.length - this.lastSheetNo;
        this.showStatus();
      }
  }

  showPrevPage(){
    while(this.sheets.length > 0){
      this.sheets.pop();
    }
    let firstSheet = this.lastSheetNo - 5;

    if(firstSheet > 0 && this.sheetsLeft != 0){
      for(let index = firstSheet - 7; index < (firstSheet - 1); index++){
        this.sheets.push(this.allSheets[index]);
      }
      
      this.lastLastSheetNo = this.lastSheetNo;
      this.lastSheetNo = firstSheet - 1;
      this.sheetsLeft = this.allSheets.length - this.lastSheetNo;

      this.showStatus();

      if(this.lastSheetNo === 6){
        this.hasPrevPage = false;
      }
    }
    else if(this.sheetsLeft === 0){
      // console.log(this.lastLastSheetNo);

      for(let index = this.lastLastSheetNo - 6; index < this.lastLastSheetNo; index++){
        this.sheets.push(this.allSheets[index]);
      }

      this.sheetsLeft = this.lastSheetNo - this.lastLastSheetNo;
      this.lastSheetNo = this.lastLastSheetNo;
      this.needMorePage = true;

      this.showStatus();
    }
  }

  showStatus(){
    // console.log("Sheets left:" + this.sheetsLeft);
    // console.log("Last Sheet No:" + this.lastSheetNo);
  }

  viewSongSheetDetails(sheetId: any){
    console.log(sheetId);
    console.log("View card details... " + sheetId);
    this.router.navigateByUrl('chords/edit/' + sheetId);
    // this.getCardDetail(cardId);
    // this.viewMode = true;
    // this.addForm.disable();
    // this.id = cardId;
  }

  addSongSheet(){
    this.router.navigateByUrl('/chords/add');
  }
}

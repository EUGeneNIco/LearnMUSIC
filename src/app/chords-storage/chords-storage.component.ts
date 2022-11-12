
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
  lastRowNo: number = 0;
  sheetsLeft: number = 0;
  videos: any[] = [];

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
          console.log("All sheets: " + this.allSheets.length);
          this.showNextPage();
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

  showNextPage(){
    if(this.needMorePage){
        //Erase all this.sheetsOnPage
        while(this.sheets.length > 0){
          this.sheets.pop();
        }

        if(this.lastRowNo === 0){
          for (let index = 0; index < 7; index++) {
            this.sheets.push(this.allSheets[index]);
          }
          console.clear();

          this.lastRowNo = 6;
          console.log("LastRowNO is : " +this.lastRowNo);

          this.sheetsLeft = (this.allSheets.length - 1) - this.lastRowNo;
          console.log("There is " + this.sheetsLeft + " sheets left!");
        }
        else if(this.lastRowNo > 0 && this.sheetsLeft > 0 && this.sheetsLeft <= 7){
          console.log("Stog paging!");
          for (let index = this.lastRowNo + 1; index < this.allSheets.length; index++) {
            this.sheets.push(this.allSheets[index]);
          }
          console.clear();

          this.needMorePage = false;
          this.hasPrevPage = true;
          
          this.lastRowNo = this.allSheets.length - 1;

          console.log("LastRowNO is : " + this.lastRowNo);

          // this.sheetsLeft = (this.allSheets.length - 1) - this.lastRowNo;
          console.log("!There is " + this.sheetsLeft + " sheets left!");
        }
        else if(this.lastRowNo > 0 && this.sheetsLeft > 7){
          for (let index = this.lastRowNo + 1; index < this.lastRowNo + 8; index++) {
            this.sheets.push(this.allSheets[index]);
          }
          console.clear();

          this.lastRowNo += 7;
          console.log("LastRowNO is : " +this.lastRowNo);

          this.sheetsLeft = (this.allSheets.length - 1) - this.lastRowNo;
          console.log("HEY!There is " + this.sheetsLeft + " sheets left!");
          
          this.hasPrevPage = true;

          var stopPaging = this.sheetsLeft === 0 ? true : false;
          if(stopPaging){
            this.needMorePage = false;
            console.log("No more next page");
          }
        }

        
      }
  }

  showPrevPage(){
    if(this.hasPrevPage){
      while(this.sheets.length > 0){
        this.sheets.pop();
      }

      if(this.lastRowNo === 6){
        for(let index = 0 ; index < 7 ; index++){
          this.sheets.push(this.allSheets[index]);
        }
        console.clear();
        
        console.log("No more prev page.")
        console.log("1lastRowNo:, " +  this.lastRowNo);

        this.hasPrevPage = false;
        this.needMorePage = true;

        
      }
      else if(this.lastRowNo > 6){
        for(let index = this.lastRowNo - 13 ; index < this.lastRowNo - 6; index++){
          this.sheets.push(this.allSheets[index]);
        }
        console.clear();

        this.lastRowNo -= 7;
        console.log("2lastRowNo:, " +  this.lastRowNo);

        if(this.lastRowNo === 6){
          this.hasPrevPage = false;
          this.needMorePage = true;

          this.sheetsLeft = (this.allSheets.length - 1) - this.lastRowNo;
          console.log("There is " + this.sheetsLeft + " sheets left!");
        }
      }
      else if(this.lastRowNo > 6 && this.sheetsLeft <= 7){
        var end = this.lastRowNo - this.sheetsLeft;
        var start = end - 6;
        for(let index = start; index < end + 1; index++){
          this.sheets.push(this.allSheets[index]);
        }
        console.clear();

        this.lastRowNo = end - 7;
        console.log("ENd " + end);
        console.log("3lastRowNo:, " +  this.lastRowNo);
      }
    }
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

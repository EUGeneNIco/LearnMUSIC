
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../service/auth.service';
import { SongSheetService } from '../service/song-sheet.service';
import { NotificationMessages } from '../_enums/notification-messages';

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
  editMode: boolean = false;
  viewMode: boolean = false;
  videos: any[] = [];

  //Pagin
  pageLimit: any = 6;
  lastSheetNo: any = 0;
  sheetsLeft: any = 0;
  lastLastSheetNo: any = 0;

  constructor(
    private auth: AuthService,
    private toastr: ToastrService,
    private songSheetService: SongSheetService, 
    private fb: FormBuilder,
    public router: Router,
    private route: ActivatedRoute,)
    { }
  
  ngOnInit(): void {
    this.reloadData();
  }

  reloadData(){
    this.pageLimit = 6;
    this.lastSheetNo = 0;
    this.lastLastSheetNo = 0;
    this.sheetsLeft = 0;
    this.needMorePage = false;
    this.hasPrevPage = false;
    this.sheets = [];
    this.allSheets = [];
    this.editMode = false;
    this.viewMode = false;
    this.getAllCards();
  }

  getAllCards(){
    this.songSheetService.getAllSheets(this.auth.userID).subscribe({
      next: (data: any) => {
        //console.log(data);
        if(data.length > 6){
          this.needMorePage = true;
          this.allSheets = data;
          this.showFirstPage();
        }
        else{
          this.sheets = data;
        }
      },
      error: (e) => {
        this.toastr.error(e.error);
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
      for(let index = this.lastLastSheetNo - 6; index < this.lastLastSheetNo; index++){
        this.sheets.push(this.allSheets[index]);
      }

      this.sheetsLeft = this.lastSheetNo - this.lastLastSheetNo;
      this.lastSheetNo = this.lastLastSheetNo;
      this.needMorePage = true;

      this.showStatus();
      if(this.lastSheetNo === 6){
        this.hasPrevPage = false;
      }
    }
  }

  showStatus(){
    console.log("Sheets left:" + this.sheetsLeft);
    console.log("Last Sheet No:" + this.lastSheetNo);
  }

  viewSongSheetDetails(sheetId: any){
    this.router.navigateByUrl('chords/' + sheetId);
  }

  addSongSheet(){
    this.router.navigateByUrl('/chords/detail');
  }

  deleteSongSheet(sheetId: any){
    console.log(sheetId);

    this.songSheetService.delete(sheetId).subscribe({
      next: (data: any) => {
        // console.log(data);
        this.toastr.success(NotificationMessages.DeleteSuccessful.Message)
        setTimeout(() => this.reloadData(), 500);
      },
      error: (e) => {
        this.toastr.error(e.error);
      }
    })
  }

  editSongSheet(sheetId: any){
    this.router.navigateByUrl('chords/detail/' + sheetId);

  }
}

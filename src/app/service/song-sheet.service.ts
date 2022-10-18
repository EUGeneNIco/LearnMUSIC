import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SongSheetService {

  baseApiUrl: string = environment.baseApiURL
  constructor(private http: HttpClient) { }
  
  getAllMembers() {
    return this.http.get(this.baseApiUrl + '/api/SongSheets/getAll');
  }

  getSheetById(id: any) {
    return this.http.get(this.baseApiUrl + '/api/SongSheets/' + id);
  }

  updateSheet(record:any){
    return this.http.post(this.baseApiUrl + '/api/SongSheets/update', record);
  }

  addSongSheet(record:any){
    return this.http.post(this.baseApiUrl + '/api/SongSheets', record);
  }

  delete(id: any){
    return this.http.get(this.baseApiUrl + '/api/SongSheets/delete/' + id);
  }



}

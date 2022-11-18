import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class CodeListValuesService extends BaseService {

  constructor(public override http: HttpClient) {
    super(http)
   }

  getGenres(){
    return this.http.get(this.API_URL + '/api/CodeListValues/getGenres');
  }
}

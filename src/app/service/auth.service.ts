import { HttpClient } from "@angular/common/http";
import { Injectable } from '@angular/core';

import { Globals } from '../globals';

import { BaseService } from './base.service';

@Injectable({
    providedIn: 'root'
})
export class AuthService extends BaseService {

    get accessibleModulesOfUser(): string[] {
        const payLoad = JSON.parse(window.atob(this.authToken.split('.')[1]));
        const accessibleModules = payLoad.AccessibleModules.split(",");

        return accessibleModules;
    }

    get authToken(): any {
        return localStorage.getItem(this.globals.TOKEN_NAME);
    }
    set authToken(val: string) {
        localStorage.setItem(this.globals.TOKEN_NAME, val);
    }

    get userID(): number {
        const payLoad = JSON.parse(window.atob(this.authToken.split('.')[1]));
        const userId = payLoad.UserGuid;

        return +userId;
    }

    get userName(): string {
        const payLoad = JSON.parse(window.atob(this.authToken.split('.')[1]));

        return payLoad.unique_name;
    }

    constructor(
      private globals: Globals,
      public override http: HttpClient) {
      super(http);
  }

    destroyAuthToken() {
        localStorage.removeItem(this.globals.TOKEN_NAME);
    }

    getAccessibleModulesOfUser(id: number) {
        return this.http.get(this.API_URL + '/api/Auth/getAccessibleModulesOfUser/' + id);
    }

    login(formData: any) {
        return this.http.post(this.API_URL + '/api/Auth/login', formData, { responseType: 'text' });
    }

    // userHasAccessToModule(accessibleModules: string[], moduleToCheck: string): boolean {
    //     const modules = accessibleModules.map(module => module.toUpperCase());

    //     return modules.includes(moduleToCheck.toUpperCase());
    // }

    userHasAccessToModule(moduleToCheck: string): boolean {
        const modules = this.accessibleModulesOfUser.map(module => module.toUpperCase());

        return modules.includes(moduleToCheck.toUpperCase());
    }

    // validateToken() {
    //     return this.http.get(this.API_URL + '/Auth/validateToken');
    // }
}
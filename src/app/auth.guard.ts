import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { AuthService } from './service/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(
    private toastr:ToastrService,
    private authService: AuthService,
    private router: Router) {
  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    
      if(this.authService.authToken != null){
        
        const module = route.data['module'];
        const hasAccess = this.authService.userHasAccessToModule(module);

        if(!hasAccess){
          this.toastr.error("You have no access!")
        }
        return hasAccess;
      }
      else{
        console.log("Navigated by Auth Guard");
        this.router.navigateByUrl('login');
        return false;
      }
  }
  
}

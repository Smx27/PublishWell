import { Component, OnInit, ViewChild, viewChild } from '@angular/core';
import { AuthService } from '../../../_services/auth/auth.service';
import { Router } from '@angular/router';
@Component({
  selector: 'jsp-modal-login',
  templateUrl: './modal-login.component.html',
  styleUrl: './modal-login.component.scss'
})
export class ModalLoginComponent implements OnInit {
  user:any = {};
  // authService = Inject(AuthService);
  /**
   *
   */
  constructor(public authService:AuthService, private router: Router ) {}
  @ViewChild('loginModalClose') loginModalClose: any;
  ngOnInit(): void {
  }
  login()
  {
    console.log(this.user);
    this.authService.login(this.user).subscribe({
      next: () => {
        this.router.navigateByUrl('user/profile');
        this.loginModalClose.nativeElement.click();
      }
    })
  }

  logout()
  {
    this.authService.logout();
    this.router.navigateByUrl('/')
  }
}

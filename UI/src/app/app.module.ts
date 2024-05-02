import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { errorInterceptor } from './helpers/_interceptor/error.interceptor';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FooterComponent } from './pages/home/footer/footer.component';
import { NavComponent } from './pages/home/nav/nav.component';
import { ModalLoginComponent } from './pages/home/modal-login/modal-login.component';
import { ModalSignupComponent } from './pages/home/modal-signup/modal-signup.component';
import { LoginComponent } from './pages/auth/login/login.component';
import { SignupComponent } from './pages/auth/signup/signup.component';
import { ForgotPasswordComponent } from './pages/auth/forgot-password/forgot-password.component';
import { ServerErrorComponent } from './pages/errors/server-error/server-error.component';
import { HomeComponent } from './pages/home/home/home.component';
import { ProfileViewComponent } from './pages/users/profile/profile-view/profile-view.component';
import { FormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HTTP_INTERCEPTORS, provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    FooterComponent,
    NavComponent,
    ModalLoginComponent,
    ModalSignupComponent,
    LoginComponent,
    SignupComponent,
    ForgotPasswordComponent,
    ServerErrorComponent,
    HomeComponent,
    ProfileViewComponent,
  ],
  imports: [
    FormsModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot()
  ],
  providers: [
    provideClientHydration(),
    provideHttpClient(
      withFetch(),
      withInterceptors([errorInterceptor])
    ),
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

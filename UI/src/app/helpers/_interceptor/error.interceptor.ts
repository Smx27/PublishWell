import { HttpErrorResponse, HttpHandlerFn, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { inject } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (
  req: HttpRequest<unknown>,
  next: HttpHandlerFn,
  ) => {

  const toster = inject(ToastrService);
  const router = inject(Router);
  return next(req).pipe(
    catchError((error:HttpErrorResponse): Observable<any> => {
      console.log(error);
      if(error)
      {
        switch(error.status){
          case 400:

          case 500:
            const navigationExtrs:NavigationExtras = {state: {error: error.error}};
            router.navigateByUrl('/server-error',navigationExtrs);
            break;

          case 404:
            router.navigateByUrl('/not-found');
            break;
          case 401:
            toster.error('Unauthorised',error.status.toString())
            break;
          default:
            toster.error('Something unexpedted happen');
            console.error(error.error);
            break;
        }
      }
      return throwError(() => error);
    })
  );
};

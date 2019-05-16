import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AuthenticationService } from '../auth-services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;
  errorMsg: string;
  loginButtonText: string;

  constructor( private formBuilder: FormBuilder,
               private router: Router,
               private authenticationService: AuthenticationService ) {}

  ngOnInit() {
    console.log("--Login component Init")
    if(this.isLoggedIn()) this.router.navigate(['/dashboard']);

    this.loginForm = this.formBuilder.group({});
  }

  onSubmit() {
    this.submitted = true;
    this.authenticationService.login();
  }

  isLoggedIn() : Boolean {
    return this.authenticationService.isLoggedIn();
  }

}

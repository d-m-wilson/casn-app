import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { AuthenticationService } from '../auth-services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;
  errorMsg: string;

  constructor( private formBuilder: FormBuilder,
               private route: ActivatedRoute,
               private router: Router,
               private authenticationService: AuthenticationService ) {}

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
        // username: ['', Validators.required],
        // password: ['', Validators.required]
    });

    // reset login status
    //this.authenticationService.logout();

    var buttonText = this.isLoggedIn() ? 'Log Out' : 'Log In';
    document.getElementById('loginSubmitButton').innerText = buttonText;

    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  // convenience getter for easy access to form fields
  get f() { return this.loginForm.controls; }

  onSubmit() {
    this.submitted = true;
    if (this.isLoggedIn()) {
      this.authenticationService.logout();
    } else {
      this.authenticationService.login();
    }
  }

  isLoggedIn() : Boolean {
    return this.authenticationService.isLoggedIn();
  }

}

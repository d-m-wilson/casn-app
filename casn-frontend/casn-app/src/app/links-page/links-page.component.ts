import { Component, OnInit } from '@angular/core';
import { DefaultApiService } from '../api/api/defaultApi.service';

@Component({
  selector: 'app-links-page',
  templateUrl: './links-page.component.html',
  styleUrls: ['./links-page.component.scss']
})
export class LinksPageComponent implements OnInit {

  links = [
    {
      href: "",
      title: "Covid-19 Drive Waiver"
    },
    {
      href: "",
      title: "Reimbursement and Donation Form"
    },
    {
      href: "",
      title: "Liability Waiver"
    },
    {
      href: "",
      title: "Mandatory Reporting Incident Form"
    },
    {
      href: "",
      title: "Suicide Incident Reporting Protocol"
    },
    {
      href: "",
      title: "Hotline - 123 456 789"
    },
  ]

  constructor( private defaultService: DefaultApiService ) { }

  ngOnInit(): void {
    this.getLinks();
  }

  /*********************************************************************
                            Service Calls
  **********************************************************************/
  getLinks(): void {
    this.defaultService.getLinks().subscribe(l => {
      console.log("Links", l);
      // this.appointmentTypes = a.map(i => {
      //   return { value: i.id, displayValue: i.title };
      // })
    })
  }

}

import { Component, OnInit } from '@angular/core';

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

  constructor() { }

  ngOnInit(): void {
  }

}

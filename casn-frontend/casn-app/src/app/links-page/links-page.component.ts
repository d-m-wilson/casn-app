import { Component, OnInit } from '@angular/core';
import { DefaultApiService } from '../api/api/defaultApi.service';

export interface Link {
  title?: string;
  target?: string;
  url?: string;
  useTelLinkStyle: boolean;
}

@Component({
  selector: 'app-links-page',
  templateUrl: './links-page.component.html',
  styleUrls: ['./links-page.component.scss']
})
export class LinksPageComponent implements OnInit {

  links: Link[] = [];

  constructor( private defaultService: DefaultApiService ) { }

  ngOnInit(): void {
    this.getLinks();
  }

  /*********************************************************************
                            Service Calls
  **********************************************************************/
  getLinks(): void {
    this.defaultService.getLinks().subscribe(linksDTO => {
      this.links = linksDTO.map(l => {
        return {
          title: l.title,
          target: l.target,
          url: l.url,
          useTelLinkStyle: l.url && l.url.startsWith('tel'),
        };
      });
    });
  }

}

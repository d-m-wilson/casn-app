import { Component, OnInit } from '@angular/core';
import { DefaultApiService } from '../api/api/defaultApi.service';

export interface Link {
  title?: string;
  target?: string;
  url?: string;
  linkStyle: LinkStyle;
}

enum LinkStyle {
  Http,
  Tel,
  Sms,
  Mailto
}

@Component({
  selector: 'app-links-page',
  templateUrl: './links-page.component.html',
  styleUrls: ['./links-page.component.scss']
})
export class LinksPageComponent implements OnInit {

  links: Link[] = [];
  // NOTE: Declared here simply so the template can reference it
  linkStyleType = LinkStyle;

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
          linkStyle: this.getLinkStyle(l.url)
        };
      });
    });
  }

  getLinkStyle(url: string): LinkStyle {
    if(url.startsWith('http')) return LinkStyle.Http;
    if(url.startsWith('tel')) return LinkStyle.Tel;
    if(url.startsWith('sms')) return LinkStyle.Sms;
    if(url.startsWith('mailto')) return LinkStyle.Mailto;

    return LinkStyle.Http;
  }
}

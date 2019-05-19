import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-leaderboard',
  templateUrl: './leaderboard.component.html',
  styleUrls: ['./leaderboard.component.scss']
})
export class LeaderboardComponent implements OnInit {
  monthLeaders: any[] = [];
  allTimeLeaders: any[] = [];

  constructor() {
    this.monthLeaders = [
      { name: "Jamie S.", points: 3012 },
      { name: "Dolores M.", points: 2985 },
      { name: "Leslie S.", points:  2830 },
      { name: "Lewis A.", points: 2702 },
      { name: "Maggie L.", points: 2015 },
      { name: "Michelle N.", points: 1897 },
      { name: "Noel F.", points:  1700 },
      { name: "Akela D.", points: 1698 },
      { name: "Justin B.", points: 1696 },
      { name: "Julia A.", points: 1692 },
    ]
  }

  ngOnInit() {
  }

}

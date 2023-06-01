import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-titleShared',
  templateUrl: './titleShared.component.html',
  styleUrls: ['./titleShared.component.css']
})
export class TitleSharedComponent implements OnInit {

  @Input() title: string = "";

  constructor() {}

  ngOnInit(): void {

  }

}

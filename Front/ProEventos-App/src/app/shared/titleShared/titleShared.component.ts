import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-titleShared',
  templateUrl: './titleShared.component.html',
  styleUrls: ['./titleShared.component.css']
})
export class TitleSharedComponent implements OnInit {

  @Input() title: string = "";
  @Input() subtitle: string = "";
  @Input() iconClass: string = "";
  @Input() button: boolean = false;
  @Input() route: string = "";

  constructor() {}

  ngOnInit(): void {
  }

}

import {Component, Input, OnInit} from '@angular/core';
import {Document} from "../../classes/Document";
import {Comment} from "../../classes/Comment";

@Component({
  selector: 'app-document',
  templateUrl: './document.component.html',
  styleUrls: ['./document.component.css']
})
export class DocumentComponent implements OnInit {
  @Input() document: Document;

  types: string[] = [
    'Usually',
    'Discussion'
  ];
  comments: Comment[];

  constructor() { }

  ngOnInit() {
  }

}

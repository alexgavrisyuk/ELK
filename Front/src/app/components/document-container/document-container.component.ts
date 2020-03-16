import { Component, OnInit } from '@angular/core';
import {Document} from "../../classes/Document";
import {Comment} from "../../classes/Comment";

@Component({
  selector: 'app-document-container',
  templateUrl: './document-container.component.html',
  styleUrls: ['./document-container.component.css']
})
export class DocumentContainerComponent implements OnInit {
  documents: Document[] = [
    { name: 'Doc', description: 'desc', typeId: 0, photo: '' },
    { name: 'Doc2', description: 'desc2', typeId: 1, photo: '' },
  ];
  comments: Comment[] = [{ description: 'desc', content: 'content' }];

  constructor() { }

  ngOnInit() {
  }

  addComment() {
  }
}

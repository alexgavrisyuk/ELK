import {Component, Input} from '@angular/core';
import {Comment} from "../../classes/Comment";

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css']
})
export class CommentComponent {
  @Input() comment: Comment;

  constructor() { }

}

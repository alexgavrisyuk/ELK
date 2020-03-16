import {Directive, ElementRef, HostListener} from '@angular/core';

@Directive({
  selector: '[appMiddleinvisible]'
})
export class MiddleinvisibleDirective {

  @Input() highlightColor: string;
  elementref: ElementRef;

  constructor(el: ElementRef) {
    this.elementref = el;

    this.elementref.nativeElement.style.opacity = 0.5;
  }

  @HostListener('mouseenter') onMouseEnter() {
    this.middleInvisible(0.8);
  }

  @HostListener('mouseleave') onMouseLeave() {
    this.middleInvisible(0.5);
  }

  middleInvisible(value) {
    this.elementref.nativeElement.style.opacity = value;
  }
}

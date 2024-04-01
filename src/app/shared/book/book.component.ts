import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-book',
  template: `
    <div class="book">
      <img [src]="book.coverArt" />
      <div class="title">{{book.title}}</div>
      <div class="author">{{ book.authors ? book.authors[0]?.name : book.authorName }}</div>
    </div>
  `,
  styleUrl: './book.component.scss'
})
export class BookComponent {
  @Input() book: any;
}

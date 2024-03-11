import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-book',
  template: `
    <div class="book">
      <div>
        <img [src]="book.coverArt">
      </div>
      <div>{{book.title}}</div>
      <div>{{ book.authors[0].name }}</div>
    </div>
  `,
  styleUrl: './book.component.scss'
})
export class BookComponent {
  @Input() book: any;
}

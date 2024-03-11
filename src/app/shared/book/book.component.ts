import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-book',
  template: `
    <div>
      {{ book | json }}
    </div>
  `,
  styleUrl: './book.component.scss'
})
export class BookComponent {
  @Input() book: any;
}

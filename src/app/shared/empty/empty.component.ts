import { Component, Input } from '@angular/core';

@Component ({
  selector: 'app-empty',
  template: `
  <div>{{message}}</div>
  `,
  styleUrl: './empty.component.scss'
})
export class EmptyComponent {
  @Input() message!: string;
}

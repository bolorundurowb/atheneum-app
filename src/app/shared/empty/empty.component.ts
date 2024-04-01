import { Component, Input } from '@angular/core';

@Component ({
  selector: 'app-empty',
  template: `
  <div class="empty">
    <ion-icon name="file-tray-outline" size="large"></ion-icon>
    <div class="message">{{message}}</div>
  </div>
  `,
  styleUrl: './empty.component.scss'
})
export class EmptyComponent {
  @Input() message!: string;
}

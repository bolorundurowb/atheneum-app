import { Component } from '@angular/core';
import { BookService, NotificationService } from '../services';

export interface ManualBookPayload {

}

@Component({
  selector: 'app-tabs',
  templateUrl: 'tabs.page.html',
  styleUrls: [ 'tabs.page.scss' ]
})
export class TabsPage {
  public actionSheetButtons = [
    {
      text: 'scan barcode',
      data: {
        action: 'scan-isbn',
      },
    },
    {
      text: 'enter isbn',
      data: {
        action: 'manual-isbn',
      },
    },
    {
      text: 'manually enter',
      data: {
        action: 'manual-entry',
      },
    },
    {
      text: 'close',
      role: 'cancel',
      data: {
        action: 'cancel',
      },
    },
  ];

  isAddingBook: boolean = false;
  manualPayload: ManualBookPayload = {};

  constructor(private bookService: BookService, private notificationService: NotificationService) {
  }
}

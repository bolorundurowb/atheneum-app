import { Component } from '@angular/core';

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

  constructor() {
  }
}

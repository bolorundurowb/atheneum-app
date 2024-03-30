import { Component } from '@angular/core';
import { BookService, NotificationService } from '../services';

export interface ManualBookPayload {
  title?: string;
  authors?: string;
  summary?: string;
  isbn?: string;
  publishYear?: number;
  publisher?: string;
  pageCount?: number;
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
      handler: () => {
        this.isManualBookModalVisible = true;
      }
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
  isManualBookModalVisible = false;
  manualPayload: ManualBookPayload = {};

  constructor(private bookService: BookService, private notificationService: NotificationService) {
  }

  async addManual(modalRef: any) {

  }

  async canDismiss(data?: any, role?: string) {
    return role === undefined;
  }
}

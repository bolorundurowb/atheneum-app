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

export interface ManualIsbnPayload {
  isbn?: string;
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

  isManualIsbnModalVisible = false;
  isbnPayload: ManualIsbnPayload = {};

  constructor(private bookService: BookService, private notificationService: NotificationService) {
  }

  async addManual() {
    this.isAddingBook = true;

    try {
      await this.bookService.createManually(this.manualPayload);
      await this.notificationService.success('Book successfully added to library');

      this.manualPayload = {};
      this.isManualBookModalVisible = false;
    } catch (e) {
      await this.notificationService.error(e as string);
    } finally {
      this.isAddingBook = false;
    }
  }

  async addByIsbn() {
    this.isAddingBook = true;

    try {
      await this.bookService.createByIsbn(this.isbnPayload);
      await this.notificationService.success('Book successfully added to library');

      this.isbnPayload = {};
      this.isManualIsbnModalVisible = false;
    } catch (e) {
      await this.notificationService.error(e as string);
    } finally {
      this.isAddingBook = false;
    }
  }

  async canDismiss(data?: any, role?: string) {
    return role === undefined;
  }
}

import { Component, OnInit } from '@angular/core';
import { BookService, NotificationService } from '../../services';
import { InfiniteScrollCustomEvent } from '@ionic/angular';

@Component({
  selector: 'app-library',
  templateUrl: 'library.page.html',
  styleUrls: [ 'library.page.scss' ]
})
export class LibraryPage implements OnInit {
  isLoading = false;
  books: any[] = [];

  constructor(private bookService: BookService, private notificationService: NotificationService) {
  }

  async ngOnInit() {
    this.isLoading = true;

    try {
      this.books = await this.bookService.getAll();
    } catch (e) {
      await this.notificationService.error(e as string);
    } finally {
      this.isLoading = false;
    }
  }

  async loadBookData() {

  }

  handlePullRefresh(event: any) {
    console.log('Pull down refresh', event);

    setTimeout(() => {
      event.target.complete();
    }, 3000);
  }

  handleScrollEnd(event: any) {
    console.log('Scroll to end', event);

    setTimeout(() => {
      (event as InfiniteScrollCustomEvent).target.complete();
    }, 3000);
  }
}

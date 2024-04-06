import { Component, OnInit } from '@angular/core';
import { BookService, NotificationService } from '../../services';
import { InfiniteScrollCustomEvent, NavController } from '@ionic/angular';
import { Router } from '@angular/router';

@Component({
  selector: 'app-library',
  templateUrl: 'library.page.html',
  styleUrls: [ 'library.page.scss' ]
})
export class LibraryPage implements OnInit {
  isLoading = false;
  books: any[] = [];

  search?: string;
  currentPage = 1;
  limit = 50;

  constructor(private bookService: BookService, private notificationService: NotificationService,
              private navCtrl: NavController) {
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

  async goToBookDetails(book: any) {
    await this.navCtrl.navigateForward('/details/book', { queryParams: { book: JSON.stringify(book) } });
  }

  async handlePullRefresh(event: any) {
    this.currentPage = 1;
    const skip = this.getSkip();

    this.books = await this.bookService.getAll(skip, this.limit, this.search);
    event.target.complete();
  }

  async handleScrollEnd(event: any) {
    console.log('Scroll to end', event);

    setTimeout(() => {
      (event as InfiniteScrollCustomEvent).target.complete();
    }, 3000);
  }

  getSkip() {
    return (this.currentPage - 1) * this.limit;
  }
}

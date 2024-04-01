import { Component, OnInit } from '@angular/core';
import {
  AuthorService,
  AuthService,
  BookService,
  NotificationService,
  PublisherService,
  StatisticService
} from '../../services';
import { NavController } from '@ionic/angular';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: [ 'home.page.scss' ]
})
export class HomePage implements OnInit {
  isLoading = false;
  currentUser: any = {};
  stats: any = {
    books: 0,
    authors: 0,
    publishers: 0,
    wishListItems: 0
  };
  recentBooks: any[] = [];
  topAuthors: any[] = [];
  topPublishers: any[] = [];

  constructor(private bookService: BookService, private authorService: AuthorService, private publisherService: PublisherService,
              private authService: AuthService, private notificationService: NotificationService, private navCtrl: NavController,
              private statService: StatisticService) {
  }

  async ngOnInit() {
    this.isLoading = true;

    try {
      this.stats = await this.statService.get();
      this.currentUser = this.authService.getUser();
      this.topAuthors = await this.authorService.getTop();
      this.recentBooks = await this.bookService.getRecent();
      this.topPublishers = await this.publisherService.getTop();
    } catch (e) {
      await this.notificationService.error(e as string);
    } finally {
      this.isLoading = false;
    }
  }

  async goToBookDetails(book: any) {
    await this.navCtrl.navigateForward('/details/book', { queryParams: { book: JSON.stringify(book) } });
  }
}

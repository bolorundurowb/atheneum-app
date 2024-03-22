import { Component, OnInit } from '@angular/core';
import { AuthorService, AuthService, BookService, NotificationService, PublisherService } from '../../services';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: [ 'home.page.scss' ]
})
export class HomePage implements OnInit {
  isLoading = false;
  currentUser: any = {};
  recentBooks: any[] = [];
  topAuthors: any[] = [];
  topPublishers: any[] = [];

  constructor(private bookService: BookService, private authorService: AuthorService, private publisherService: PublisherService,
              private authService: AuthService, private notificationService: NotificationService) {
  }

  async ngOnInit() {
    this.isLoading = true;

    try {
      this.currentUser = this.authService.getUser();
      this.recentBooks = await this.bookService.getRecent();
      this.topAuthors = await this.authorService.getTop();
      this.topPublishers = await this.publisherService.getTop();
    } catch (e) {
      await this.notificationService.error(e as string);
    } finally {
      this.isLoading = false;
    }
  }
}

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NavController } from '@ionic/angular';
import { BookService, NotificationService } from '../../services';
import { convertToHttps } from '../../utils';

@Component({
  selector: 'app-book-details',
  templateUrl: 'book.page.html',
  styleUrl: 'book.page.scss'
})
export class BookPage implements OnInit {
  book: any;
  removeButtons = [
    {
      text: 'Cancel',
      role: 'cancel',
      handler: () => {
      }
    },
    {
      text: 'Proceed',
      role: 'confirm',
      handler: async () => {
        try {
          await this.bookService.removeBook(this.book.id);
          await this.notificationService.success('Book successfully removed');
          await this.goBack();
        } catch (e) {
          await this.notificationService.error(e as string);
        }
      }
    }
  ];

  protected readonly convertToHttps = convertToHttps;

  constructor(private route: ActivatedRoute, private navCtrl: NavController,
              private notificationService: NotificationService, private bookService: BookService) {
  }

  ngOnInit() {
    this.book = JSON.parse((this.route.snapshot.queryParams as any).book);
  }

  async goBack() {
    await this.navCtrl.pop();
  }
}

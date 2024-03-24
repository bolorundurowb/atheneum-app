import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NavController } from '@ionic/angular';

@Component({
  selector: 'app-book-details',
  templateUrl: 'book.page.html',
  styleUrl: 'book.page.scss'
})
export class BookPage implements OnInit {
  book: any;

  constructor(private route: ActivatedRoute, private navCtrl: NavController) {
  }

  ngOnInit() {
    this.book = JSON.parse((this.route.snapshot.queryParams as any).book);
  }

  async goBack() {
    await this.navCtrl.pop();
  }
}

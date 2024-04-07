import { Component, OnInit } from '@angular/core';
import { NotificationService, WishlistService } from '../../services';

interface AddToWishlistPayload {
  bookTitle?: string;
  bookAuthor?: string;
  bookIsbn?: string;
}

@Component({
  selector: 'app-wishlist',
  templateUrl: 'wishlist.page.html',
  styleUrls: [ 'wishlist.page.scss' ]
})
export class WishlistPage implements OnInit {
  isLoading = false;
  wishlist: any[] = [];

  isAdding: boolean = false;
  addPayload: AddToWishlistPayload = {};

  constructor(private wishlistService: WishlistService, private notificationService: NotificationService) {
  }

  async ngOnInit() {
    this.isLoading = true;

    try {
      await this.loadData();
    } finally {
      this.isLoading = false;
    }
  }

  async addToWishlist(modalRef: any) {
    this.isAdding = true;

    try {
      const book = await this.wishlistService.add(this.addPayload);
      await this.notificationService.success('Book added to wishlist');

      this.wishlist.unshift(book);
      this.addPayload = {};

      modalRef.dismiss();
    } catch (e) {
      await this.notificationService.error(e as string);
    } finally {
      this.isAdding = false;
    }
  }

  async handlePullRefresh(event: any) {
    await this.loadData();
    event.target.complete();
  }

  async canDismiss(data?: any, role?: string) {
    return role === undefined;
  }

  async loadData() {
    try {
      this.wishlist = await this.wishlistService.getAll();
    } catch (e) {
      await this.notificationService.error(e as string);
    }
  }
}

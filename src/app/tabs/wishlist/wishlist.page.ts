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
      this.wishlist = await this.wishlistService.getAll();
    } catch (e) {
      await this.notificationService.error(e as string);
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

  handlePullRefresh(event: any) {
    console.log('Pull down refresh', event);

    setTimeout(() => {
      event.target.complete();
    }, 3000);
  }

  async canDismiss(data?: any, role?: string) {
    console.log(role);
    return role === undefined;
  }
}

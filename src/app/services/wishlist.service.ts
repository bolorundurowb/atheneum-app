import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { asPromise } from '../utils';

@Injectable({providedIn: 'root'})
export class WishlistService {
  private readonly baseUrl;

  constructor(private http: HttpClient) {
    this.baseUrl = `${environment.baseApiUrl}/v1/wish-list`;
  }

  getAll(): Promise<any[]> {
    return asPromise<any[]>(this.http.get<any>(`${this.baseUrl}`));
  }
}

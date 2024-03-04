import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { asPromise } from '../utils';

@Injectable({ providedIn: 'root' })
export class BookService {
  private readonly baseUrl;

  constructor(private http: HttpClient) {
    this.baseUrl = `${environment.baseApiUrl}/v1/books`;
  }

  getAll(skip = 0, limit = 50, search = '', publisherId = '', authorId = ''): Promise<any[]> {
    return asPromise<any[]>(this.http.get<any>(`${this.baseUrl}?skip=${skip}&limit=${limit}&search=${search}&publisherId=${publisherId}&authorId=${authorId}`));
  }

  getRecent(): Promise<any[]> {
    return asPromise<any[]>(this.http.get<any>(`${this.baseUrl}/recent`));
  }

  createByIsbn(payload: any): Promise<any> {
    return asPromise(this.http.post<any>(`${this.baseUrl}/isbn `, payload));
  }
}

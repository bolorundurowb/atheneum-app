import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { asPromise } from '../utils';

@Injectable({ providedIn: 'root' })
export class AccountService {
  private readonly baseUrl;

  constructor(private http: HttpClient) {
    this.baseUrl = `${environment.baseApiUrl}/v1/accounts`;
  }

  delete(): Promise<any> {
    return asPromise(this.http.delete<any>(`${this.baseUrl}/delete`));
  }
}

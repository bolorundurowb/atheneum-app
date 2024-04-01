import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { asPromise } from '../utils';

@Injectable({ providedIn: 'root' })
export class StatisticService {
  private readonly baseUrl;

  constructor(private http: HttpClient) {
    this.baseUrl = `${environment.baseApiUrl}/v1/statistics`;
  }

  get(): Promise<any> {
    return asPromise(this.http.get<any>(this.baseUrl));
  }
}

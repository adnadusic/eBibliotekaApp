import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ReportsService {
  private readonly http = inject(HttpClient);

  private readonly apiUrl =
    `${environment.apiUrl}/api/reports`;

  getBookReviewsPdf(
    bookId: number,
    dateFrom: string,
    dateTo: string
  ): Observable<Blob> {
    const params = new HttpParams()
      .set('bookId', bookId.toString())
      .set('dateFrom', dateFrom)
      .set('dateTo', dateTo);

    return this.http.get(
      `${this.apiUrl}/book-reviews`,
      {
        params,
        responseType: 'blob',
      }
    );
  }

  getAuditTrailPdf(
    userId: number,
    dateFrom: string,
    dateTo: string
  ): Observable<Blob> {
    const params = new HttpParams()
      .set('userId', userId.toString())
      .set('dateFrom', dateFrom)
      .set('dateTo', dateTo);

    return this.http.get(
      `${this.apiUrl}/audit-trail`,
      {
        params,
        responseType: 'blob',
      }
    );
  }
}
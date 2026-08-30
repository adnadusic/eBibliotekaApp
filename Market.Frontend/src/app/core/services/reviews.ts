import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, map } from 'rxjs';

import { environment } from '../../../environments/environment';
import { PageResult } from './page-result';

export interface ReviewItem {
  id: number;
  userId: number;
  userName: string;
  rating: number;
  title: string;
  comment: string;
  date: string;
  helpfulCount: number;
  unhelpfulCount: number;
}

export interface CreateReviewRequest {
  bookId: number;
  rating: number;
  title: string;
  comment: string;
}

export interface CreateReviewResponse {
  id: number;
  bookId: number;
  userId: number;
  rating: number;
  title: string;
  comment: string;
  date: string;
}

export interface ReactToReviewRequest {
  reviewId: number;
  reactionType: 1 | 2;
}

@Injectable({
  providedIn: 'root',
})
export class ReviewsService {
  private readonly http = inject(HttpClient);

  private readonly apiUrl =
    `${environment.apiUrl}/api/reviews`;

  getByBook(bookId: number): Observable<ReviewItem[]> {
    return this.getByBookPage(
      bookId,
      1,
      100
    ).pipe(
      map((result) => result.items)
    );
  }

  getByBookPage(
    bookId: number,
    page = 1,
    pageSize = 10
  ): Observable<PageResult<ReviewItem>> {
    const params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());

    return this.http.get<PageResult<ReviewItem>>(
      `${this.apiUrl}/book/${bookId}`,
      { params }
    );
  }

  create(
    request: CreateReviewRequest
  ): Observable<CreateReviewResponse> {
    return this.http.post<CreateReviewResponse>(
      this.apiUrl,
      request
    );
  }

  react(request: ReactToReviewRequest): Observable<void> {
    return this.http.post<void>(
      `${this.apiUrl}/react`,
      request
    );
  }
}
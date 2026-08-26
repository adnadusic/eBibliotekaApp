import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

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

@Injectable({
  providedIn: 'root',
})
export class ReviewsService {
  private readonly http = inject(HttpClient);

  private readonly apiUrl = 'https://localhost:7260/api/reviews';

  getByBook(bookId: number): Observable<ReviewItem[]> {
    return this.http.get<ReviewItem[]>(
      `${this.apiUrl}/book/${bookId}`
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
}
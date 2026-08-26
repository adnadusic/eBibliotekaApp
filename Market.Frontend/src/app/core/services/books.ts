import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface BookListItem {
  id: number;
  title: string;
  isbn: string;
  publicationYear: number | null;
  pageCount: number | null;
  languageId: number;
  publisherId: number | null;
  availableCopies: number | null;
  averageRating: number | null;
}

export interface PagedBooksResult {
  total: number;
  items: BookListItem[];
}

export interface BookDetails {
  id: number;
  title: string;
  isbn: string;
  publicationYear: number | null;
  pageCount: number | null;
  languageId: number;
  publisherId: number | null;
  description: string | null;
  coverImage: string | null;
  totalCopies: number | null;
  availableCopies: number | null;
  averageRating: number | null;
  ratingCount: number | null;
  viewCount: number | null;
  addedAt: string | null;
  authorIds: number[];
  genreIds: number[];
}

export interface BookUpsertRequest {
  title: string;
  isbn: string;
  publicationYear: number | null;
  pageCount: number | null;
  languageId: number;
  publisherId: number | null;
  description: string | null;
  coverImage: string | null;
  authorIds: number[];
  genreIds: number[];
}

export interface CreateBookResponse {
  id: number;
  title: string;
  isbn: string;
}

export interface UpdateBookRequest extends BookUpsertRequest {
  id: number;
}

export interface UpdateBookResponse {
  id: number;
  title: string;
  isbn: string;
}

export interface BookFilters {
  page: number;
  pageSize: number;
  title?: string;
  isbn?: string;
  authorId?: number | null;
  genreId?: number | null;
  languageId?: number | null;
  sortBy?: 'title' | 'isbn' | 'publicationYear' | 'pageCount';
  sortDirection?: 'asc' | 'desc';
}

@Injectable({
  providedIn: 'root',
})
export class BooksService {
  private readonly http = inject(HttpClient);

  private readonly apiUrl = 'https://localhost:7260/api/books';

  getPaged(filters: BookFilters): Observable<PagedBooksResult> {
    let params = new HttpParams()
      .set('page', filters.page)
      .set('pageSize', filters.pageSize);

    if (filters.title?.trim()) {
      params = params.set('title', filters.title.trim());
    }

    if (filters.isbn?.trim()) {
      params = params.set('isbn', filters.isbn.trim());
    }

    if (filters.authorId) {
      params = params.set('authorId', filters.authorId);
    }

    if (filters.genreId) {
      params = params.set('genreId', filters.genreId);
    }

    if (filters.languageId) {
      params = params.set('languageId', filters.languageId);
    }

    if (filters.sortBy) {
      params = params.set('sortBy', filters.sortBy);
    }

    if (filters.sortDirection) {
      params = params.set('sortDirection', filters.sortDirection);
    }

    return this.http.get<PagedBooksResult>(this.apiUrl, { params });
  }

  getById(id: number): Observable<BookDetails> {
    return this.http.get<BookDetails>(`${this.apiUrl}/${id}`);
  }

  create(request: BookUpsertRequest): Observable<CreateBookResponse> {
    return this.http.post<CreateBookResponse>(this.apiUrl, request);
  }

  update(request: UpdateBookRequest): Observable<UpdateBookResponse> {
    return this.http.put<UpdateBookResponse>(this.apiUrl, request);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
import { CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';

import {
  BookListItem,
  BooksService,
} from '../../../core/services/books';

@Component({
  selector: 'app-book-list',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './book-list.html',
  styleUrl: './book-list.scss',
})
export class BookList implements OnInit {
  private readonly booksService = inject(BooksService);
  private readonly fb = inject(FormBuilder);
  private readonly router = inject(Router);

  books: BookListItem[] = [];

  total = 0;
  page = 1;
  pageSize = 10;

  loading = false;
  errorMessage = '';

  readonly filterForm = this.fb.group({
    title: [''],
    isbn: [''],
    authorId: [null as number | null],
    genreId: [null as number | null],
    languageId: [null as number | null],
  });

  get totalPages(): number {
    return Math.max(1, Math.ceil(this.total / this.pageSize));
  }

  ngOnInit(): void {
    this.loadBooks();
  }

  loadBooks(): void {
    this.loading = true;
    this.errorMessage = '';

    const filters = this.filterForm.getRawValue();

    this.booksService
      .getPaged({
        page: this.page,
        pageSize: this.pageSize,
        title: filters.title ?? '',
        isbn: filters.isbn ?? '',
        authorId: filters.authorId,
        genreId: filters.genreId,
        languageId: filters.languageId,
      })
      .subscribe({
        next: (result) => {
          this.books = result.items;
          this.total = result.total;
          this.loading = false;
        },
        error: (error) => {
          console.error(error);
          this.errorMessage = 'Failed to load books.';
          this.loading = false;
        },
      });
  }

  applyFilters(): void {
    this.page = 1;
    this.loadBooks();
  }

  resetFilters(): void {
    this.filterForm.reset({
      title: '',
      isbn: '',
      authorId: null,
      genreId: null,
      languageId: null,
    });

    this.page = 1;
    this.loadBooks();
  }

  createBook(): void {
    this.router.navigate(['/books/new']);
  }

  editBook(id: number): void {
    this.router.navigate(['/books', id, 'edit']);
  }

  deleteBook(book: BookListItem): void {
    const confirmed = window.confirm(
      `Da li sigurno želiš obrisati knjigu "${book.title}"?`
    );

    if (!confirmed) {
      return;
    }

    this.booksService.delete(book.id).subscribe({
      next: () => this.loadBooks(),
      error: (error) => {
        console.error(error);

        if (error?.status === 401) {
          this.errorMessage = 'Potrebna je prijava korisnika za brisanje knjige.';
          return;
        }

        this.errorMessage = 'Brisanje knjige nije uspjelo.';
      },
    });
  }

  previousPage(): void {
    if (this.page <= 1) {
      return;
    }

    this.page--;
    this.loadBooks();
  }

  nextPage(): void {
    if (this.page >= this.totalPages) {
      return;
    }

    this.page++;
    this.loadBooks();
  }
}
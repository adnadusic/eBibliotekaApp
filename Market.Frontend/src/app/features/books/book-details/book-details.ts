import { CommonModule } from '@angular/common';
import {
  ChangeDetectorRef,
  Component,
  OnInit,
  inject,
} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import {
  BookDetails as BookDetailsModel,
  BooksService,
} from '../../../core/services/books';

@Component({
  selector: 'app-book-details',
  imports: [CommonModule],
  templateUrl: './book-details.html',
  styleUrl: './book-details.scss',
})
export class BookDetailsPage implements OnInit {
  private readonly booksService = inject(BooksService);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly cdr = inject(ChangeDetectorRef);

  book: BookDetailsModel | null = null;
  loading = false;
  errorMessage = '';

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    if (id <= 0) {
      this.errorMessage = 'Neispravan ID knjige.';
      return;
    }

    this.loadBook(id);
  }

  loadBook(id: number): void {
    this.loading = true;
    this.errorMessage = '';

    this.booksService.getById(id).subscribe({
      next: (book) => {
        this.book = book;
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: (error) => {
        console.error(error);

        if (error?.status === 404) {
          this.errorMessage = 'Knjiga nije pronađena.';
        } else {
          this.errorMessage = 'Učitavanje detalja knjige nije uspjelo.';
        }

        this.loading = false;
        this.cdr.detectChanges();
      },
    });
  }

  editBook(): void {
    if (!this.book) {
      return;
    }

    this.router.navigate(['/books', this.book.id, 'edit']);
  }

  backToList(): void {
    this.router.navigate(['/books']);
  }
}
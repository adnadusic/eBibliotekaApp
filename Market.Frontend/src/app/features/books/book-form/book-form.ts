import { CommonModule } from '@angular/common';
import {
  ChangeDetectorRef,
  Component,
  OnInit,
  inject,
} from '@angular/core';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { BooksService } from '../../../core/services/books';

@Component({
  selector: 'app-book-form',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './book-form.html',
  styleUrl: './book-form.scss',
})
export class BookForm implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly booksService = inject(BooksService);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly cdr = inject(ChangeDetectorRef);

  bookId: number | null = null;
  isEditMode = false;
  loading = false;
  saving = false;
  errorMessage = '';

  readonly form = this.fb.group({
    title: ['', [Validators.required]],
    isbn: ['', [Validators.required, Validators.maxLength(20)]],
    publicationYear: [
      null as number | null,
      [Validators.min(1)],
    ],
    pageCount: [
      null as number | null,
      [Validators.min(1)],
    ],
    languageId: [
      1,
      [Validators.required, Validators.min(1)],
    ],
    publisherId: [
      null as number | null,
      [Validators.min(1)],
    ],
    description: [''],
    coverImage: [''],

    // For now IDs are entered comma-separated, e.g. 1,2,3.
    authorIds: [''],
    genreIds: [''],
  });

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    if (id > 0) {
      this.bookId = id;
      this.isEditMode = true;
      this.loadBook(id);
    }
  }

  loadBook(id: number): void {
    this.loading = true;
    this.errorMessage = '';

    this.booksService.getById(id).subscribe({
      next: (book) => {
        this.form.patchValue({
          title: book.title,
          isbn: book.isbn,
          publicationYear: book.publicationYear,
          pageCount: book.pageCount,
          languageId: book.languageId,
          publisherId: book.publisherId,
          description: book.description ?? '',
          coverImage: book.coverImage ?? '',
          authorIds: book.authorIds.join(','),
          genreIds: book.genreIds.join(','),
        });

        this.loading = false;
        this.cdr.detectChanges();
      },
      error: (error) => {
        console.error(error);
        this.errorMessage = 'Nije moguće učitati knjigu.';
        this.loading = false;
        this.cdr.detectChanges();
      },
    });
  }

  save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.saving = true;
    this.errorMessage = '';

    const value = this.form.getRawValue();

    const request = {
      title: value.title?.trim() ?? '',
      isbn: value.isbn?.trim() ?? '',
      publicationYear: value.publicationYear,
      pageCount: value.pageCount,
      languageId: Number(value.languageId),
      publisherId: value.publisherId,
      description: value.description?.trim() || null,
      coverImage: value.coverImage?.trim() || null,
      authorIds: this.parseIds(value.authorIds ?? ''),
      genreIds: this.parseIds(value.genreIds ?? ''),
    };

    if (this.isEditMode && this.bookId !== null) {
      this.booksService
        .update({
          id: this.bookId,
          ...request,
        })
        .subscribe({
          next: () => {
            this.router.navigate(['/books']);
          },
          error: (error) => {
            console.error(error);
            this.errorMessage = this.getErrorMessage(error);
            this.saving = false;
            this.cdr.detectChanges();
          },
        });

      return;
    }

    this.booksService.create(request).subscribe({
      next: () => {
        this.router.navigate(['/books']);
      },
      error: (error) => {
        console.error(error);
        this.errorMessage = this.getErrorMessage(error);
        this.saving = false;
        this.cdr.detectChanges();
      },
    });
  }

  cancel(): void {
    this.router.navigate(['/books']);
  }

  private parseIds(value: string): number[] {
    if (!value.trim()) {
      return [];
    }

    return value
      .split(',')
      .map((id) => Number(id.trim()))
      .filter((id) => Number.isInteger(id) && id > 0);
  }

  private getErrorMessage(error: any): string {
    if (error?.status === 409) {
      return 'Knjiga sa istim ISBN-om već postoji.';
    }

    if (error?.status === 400) {
      return 'Provjeri unesene podatke.';
    }

    if (error?.status === 401) {
      return 'Potrebna je prijava korisnika.';
    }

    if (error?.status === 404) {
      return 'Traženi podatak nije pronađen.';
    }

    return 'Došlo je do greške prilikom spremanja knjige.';
  }
}
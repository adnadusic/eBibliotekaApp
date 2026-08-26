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

import {
  BookDetails as BookDetailsModel,
  BooksService,
} from '../../../core/services/books';
import {
  ReviewItem,
  ReviewsService,
} from '../../../core/services/reviews';

@Component({
  selector: 'app-book-details',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './book-details.html',
  styleUrl: './book-details.scss',
})
export class BookDetailsPage implements OnInit {
  private readonly booksService = inject(BooksService);
  private readonly reviewsService = inject(ReviewsService);
  private readonly fb = inject(FormBuilder);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly cdr = inject(ChangeDetectorRef);

  book: BookDetailsModel | null = null;
  reviews: ReviewItem[] = [];

  loading = false;
  reviewsLoading = false;
  submittingReview = false;

  errorMessage = '';
  reviewErrorMessage = '';
  reviewSuccessMessage = '';

  readonly reviewForm = this.fb.group({
    rating: [
      5,
      [
        Validators.required,
        Validators.min(1),
        Validators.max(5),
      ],
    ],
    title: [
      '',
      [
        Validators.required,
        Validators.maxLength(200),
      ],
    ],
    comment: [
      '',
      [
        Validators.required,
        Validators.maxLength(2000),
      ],
    ],
  });

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    if (id <= 0) {
      this.errorMessage = 'Neispravan ID knjige.';
      return;
    }

    this.loadBook(id);
    this.loadReviews(id);
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
          this.errorMessage =
            'Učitavanje detalja knjige nije uspjelo.';
        }

        this.loading = false;
        this.cdr.detectChanges();
      },
    });
  }

  loadReviews(bookId: number): void {
    this.reviewsLoading = true;
    this.reviewErrorMessage = '';

    this.reviewsService.getByBook(bookId).subscribe({
      next: (reviews) => {
        this.reviews = reviews;
        this.reviewsLoading = false;
        this.cdr.detectChanges();
      },
      error: (error) => {
        console.error(error);

        this.reviewErrorMessage =
          'Učitavanje recenzija nije uspjelo.';

        this.reviewsLoading = false;
        this.cdr.detectChanges();
      },
    });
  }

  submitReview(): void {
    if (!this.book) {
      return;
    }

    if (this.reviewForm.invalid) {
      this.reviewForm.markAllAsTouched();
      return;
    }

    const values = this.reviewForm.getRawValue();

    this.submittingReview = true;
    this.reviewErrorMessage = '';
    this.reviewSuccessMessage = '';

    this.reviewsService
      .create({
        bookId: this.book.id,
        rating: values.rating ?? 5,
        title: values.title?.trim() ?? '',
        comment: values.comment?.trim() ?? '',
      })
      .subscribe({
        next: () => {
          const bookId = this.book!.id;

          this.reviewForm.reset({
            rating: 5,
            title: '',
            comment: '',
          });

          this.reviewSuccessMessage =
            'Recenzija je uspješno dodana.';

          this.submittingReview = false;

          this.loadReviews(bookId);
          this.loadBook(bookId);

          this.cdr.detectChanges();
        },
        error: (error) => {
          console.error(error);

          if (error?.status === 401) {
            this.reviewErrorMessage =
              'Morate biti prijavljeni da biste ostavili recenziju.';
          } else if (error?.status === 409) {
            this.reviewErrorMessage =
              'Već ste ostavili recenziju za ovu knjigu.';
          } else {
            this.reviewErrorMessage =
              'Dodavanje recenzije nije uspjelo.';
          }

          this.submittingReview = false;
          this.cdr.detectChanges();
        },
      });
  }

  reactToReview(
    reviewId: number,
    reactionType: 1 | 2
  ): void {
    if (!this.book) {
      return;
    }

    const bookId = this.book.id;

    this.reviewErrorMessage = '';

    this.reviewsService
      .react({
        reviewId,
        reactionType,
      })
      .subscribe({
        next: () => {
          this.loadReviews(bookId);
        },
        error: (error) => {
          console.error(error);

          if (error?.status === 401) {
            this.reviewErrorMessage =
              'Morate biti prijavljeni da biste reagovali na recenziju.';
          } else {
            this.reviewErrorMessage =
              'Reakcija na recenziju nije uspjela.';
          }

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
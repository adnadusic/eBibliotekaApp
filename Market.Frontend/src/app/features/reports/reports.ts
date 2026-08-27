import {
  ChangeDetectorRef,
  Component,
} from '@angular/core';

import {
  ReportsService,
} from '../../core/services/reports';

@Component({
  selector: 'app-reports',
  imports: [],
  templateUrl: './reports.html',
  styleUrl: './reports.scss',
})
export class ReportsPage {
  bookId = 1;
  bookDateFrom = '';
  bookDateTo = '';

  auditUserId = 1;
  auditDateFrom = '';
  auditDateTo = '';

  bookReportLoading = false;
  auditReportLoading = false;

  errorMessage = '';

  constructor(
    private readonly reportsService: ReportsService,
    private readonly cdr: ChangeDetectorRef
  ) {}

  onBookIdChange(event: Event): void {
    this.bookId = Number(
      (event.target as HTMLInputElement).value
    );
  }

  onBookDateFromChange(event: Event): void {
    this.bookDateFrom =
      (event.target as HTMLInputElement).value;
  }

  onBookDateToChange(event: Event): void {
    this.bookDateTo =
      (event.target as HTMLInputElement).value;
  }

  onAuditUserIdChange(event: Event): void {
    this.auditUserId = Number(
      (event.target as HTMLInputElement).value
    );
  }

  onAuditDateFromChange(event: Event): void {
    this.auditDateFrom =
      (event.target as HTMLInputElement).value;
  }

  onAuditDateToChange(event: Event): void {
    this.auditDateTo =
      (event.target as HTMLInputElement).value;
  }

  generateBookReviewsPdf(): void {
    this.errorMessage = '';

    if (
      this.bookId <= 0 ||
      !this.bookDateFrom ||
      !this.bookDateTo
    ) {
      this.errorMessage =
        'Unesite Book ID i period za izvještaj.';

      return;
    }

    this.bookReportLoading = true;

    this.reportsService
      .getBookReviewsPdf(
        this.bookId,
        this.bookDateFrom,
        this.bookDateTo
      )
      .subscribe({
        next: (pdf) => {
          this.downloadPdf(
            pdf,
            `book-reviews-${this.bookId}-${this.bookDateFrom}-${this.bookDateTo}.pdf`
          );

          this.bookReportLoading = false;
          this.cdr.detectChanges();
        },
        error: (error) => {
          console.error(error);

          this.errorMessage =
            'Generisanje PDF izvještaja recenzija nije uspjelo.';

          this.bookReportLoading = false;
          this.cdr.detectChanges();
        },
      });
  }

  generateAuditTrailPdf(): void {
    this.errorMessage = '';

    if (
      this.auditUserId <= 0 ||
      !this.auditDateFrom ||
      !this.auditDateTo
    ) {
      this.errorMessage =
        'Unesite User ID i period za izvještaj.';

      return;
    }

    this.auditReportLoading = true;

    this.reportsService
      .getAuditTrailPdf(
        this.auditUserId,
        this.auditDateFrom,
        this.auditDateTo
      )
      .subscribe({
        next: (pdf) => {
          this.downloadPdf(
            pdf,
            `audit-trail-${this.auditUserId}-${this.auditDateFrom}-${this.auditDateTo}.pdf`
          );

          this.auditReportLoading = false;
          this.cdr.detectChanges();
        },
        error: (error) => {
          console.error(error);

          this.errorMessage =
            'Generisanje Audit Trail PDF izvještaja nije uspjelo.';

          this.auditReportLoading = false;
          this.cdr.detectChanges();
        },
      });
  }

  private downloadPdf(
    pdf: Blob,
    fileName: string
  ): void {
    const url = window.URL.createObjectURL(pdf);

    const link = document.createElement('a');

    link.href = url;
    link.download = fileName;

    document.body.appendChild(link);
    link.click();
    link.remove();

    window.URL.revokeObjectURL(url);
  }
}
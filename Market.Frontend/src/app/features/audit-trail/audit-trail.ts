import { DatePipe } from '@angular/common';
import {
  ChangeDetectorRef,
  Component,
  OnInit,
} from '@angular/core';

import {
  AuditLogItem,
  AuditTrailService,
} from '../../core/services/audit-trail';

@Component({
  selector: 'app-audit-trail',
  imports: [DatePipe],
  templateUrl: './audit-trail.html',
  styleUrl: './audit-trail.scss',
})
export class AuditTrailPage implements OnInit {
  auditLogs: AuditLogItem[] = [];

  loading = false;
  errorMessage = '';

  selectedEntity = '';
  selectedAction = '';

  page = 1;
  readonly pageSize = 10;
  total = 0;

  constructor(
    private readonly auditTrailService: AuditTrailService,
    private readonly cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.loadAuditLogs();
  }

  loadAuditLogs(): void {
    this.loading = true;
    this.errorMessage = '';

    const entityName =
      this.selectedEntity || undefined;

    const action =
      this.selectedAction || undefined;

    this.auditTrailService
      .getAuditLogsPage(
        entityName,
        action,
        this.page,
        this.pageSize
      )
      .subscribe({
        next: (result) => {
          this.auditLogs = result.items;
          this.total = result.total;
          this.loading = false;
          this.cdr.detectChanges();
        },
        error: (error) => {
          console.error(error);

          this.errorMessage =
            'Failed to load Audit Trail entries.';

          this.loading = false;
          this.cdr.detectChanges();
        },
      });
  }

  onEntityFilterChange(event: Event): void {
    this.selectedEntity =
      (event.target as HTMLSelectElement).value;

    this.page = 1;
    this.loadAuditLogs();
  }

  onActionFilterChange(event: Event): void {
    this.selectedAction =
      (event.target as HTMLSelectElement).value;

    this.page = 1;
    this.loadAuditLogs();
  }

  get totalPages(): number {
    return Math.max(
      1,
      Math.ceil(this.total / this.pageSize)
    );
  }

  get hasPreviousPage(): boolean {
    return this.page > 1;
  }

  get hasNextPage(): boolean {
    return this.page < this.totalPages;
  }

  previousPage(): void {
    if (!this.hasPreviousPage) {
      return;
    }

    this.page--;
    this.loadAuditLogs();
  }

  nextPage(): void {
    if (!this.hasNextPage) {
      return;
    }

    this.page++;
    this.loadAuditLogs();
  }

  formatValues(values: string | null): string {
    if (!values) {
      return '-';
    }

    try {
      const parsed = JSON.parse(values);

      return JSON.stringify(
        parsed,
        null,
        2
      );
    } catch {
      return values;
    }
  }
}
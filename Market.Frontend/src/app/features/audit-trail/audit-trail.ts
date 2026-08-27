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
  imports: [],
  templateUrl: './audit-trail.html',
  styleUrl: './audit-trail.scss',
})
export class AuditTrailPage implements OnInit {
  auditLogs: AuditLogItem[] = [];

  loading = false;
  errorMessage = '';

  selectedEntity = '';
  selectedAction = '';

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
      .getAuditLogs(entityName, action)
      .subscribe({
        next: (logs) => {
          this.auditLogs = logs;
          this.loading = false;
          this.cdr.detectChanges();
        },
        error: (error) => {
          console.error(error);

          this.errorMessage =
            'Učitavanje Audit Trail zapisa nije uspjelo.';

          this.loading = false;
          this.cdr.detectChanges();
        },
      });
  }

  onEntityFilterChange(event: Event): void {
    this.selectedEntity =
      (event.target as HTMLSelectElement).value;

    this.loadAuditLogs();
  }

  onActionFilterChange(event: Event): void {
    this.selectedAction =
      (event.target as HTMLSelectElement).value;

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
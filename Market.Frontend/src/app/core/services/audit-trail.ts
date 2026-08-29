import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';

export interface AuditLogItem {
  id: number;
  userId: number | null;
  userEmail: string | null;
  entityName: string;
  entityId: string | null;
  action: string;
  oldValues: string | null;
  newValues: string | null;
  changedAtUtc: string;
}

@Injectable({
  providedIn: 'root',
})
export class AuditTrailService {
  private readonly http = inject(HttpClient);

  private readonly apiUrl =
    `${environment.apiUrl}/api/audit-trail`;

  getAuditLogs(
    entityName?: string,
    action?: string
  ): Observable<AuditLogItem[]> {
    let params = new HttpParams();

    if (entityName) {
      params = params.set(
        'entityName',
        entityName
      );
    }

    if (action) {
      params = params.set(
        'action',
        action
      );
    }

    return this.http.get<AuditLogItem[]>(
      this.apiUrl,
      { params }
    );
  }
}
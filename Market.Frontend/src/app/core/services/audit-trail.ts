import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, map } from 'rxjs';

import { environment } from '../../../environments/environment';
import { PageResult } from './page-result';

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
    return this.getAuditLogsPage(
      entityName,
      action,
      1,
      100
    ).pipe(
      map((result) => result.items)
    );
  }

  getAuditLogsPage(
    entityName?: string,
    action?: string,
    page = 1,
    pageSize = 10
  ): Observable<PageResult<AuditLogItem>> {
    let params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());

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

    return this.http.get<PageResult<AuditLogItem>>(
      this.apiUrl,
      { params }
    );
  }
}
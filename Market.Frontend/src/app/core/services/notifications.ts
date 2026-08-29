import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';

export interface NotificationItem {
  id: number;
  type: number;
  title: string;
  message: string;
  sentAt: string;
  isRead: boolean;
  readAt: string | null;
  relatedId: number | null;
  relatedType: string | null;
}

export interface SetNotificationReadStatusRequest {
  notificationId: number;
  isRead: boolean;
}

export interface NotificationSettingItem {
  type: number;
  isPriority: boolean;
}

@Injectable({
  providedIn: 'root',
})
export class NotificationsService {
  private readonly http = inject(HttpClient);

  private readonly apiUrl =
    `${environment.apiUrl}/api/notifications`;

  getMyNotifications(
    type?: number,
    isRead?: boolean
  ): Observable<NotificationItem[]> {
    let params = new HttpParams();

    if (type !== undefined) {
      params = params.set('type', type.toString());
    }

    if (isRead !== undefined) {
      params = params.set('isRead', isRead.toString());
    }

    return this.http.get<NotificationItem[]>(
      this.apiUrl,
      { params }
    );
  }

  getMyNotificationSettings(): Observable<NotificationSettingItem[]> {
    return this.http.get<NotificationSettingItem[]>(
      `${this.apiUrl}/settings`
    );
  }

  setPriority(
    type: number,
    isPriority: boolean
  ): Observable<void> {
    return this.http.put<void>(
      `${this.apiUrl}/priority`,
      {
        type,
        isPriority,
      }
    );
  }

  setReadStatus(
    request: SetNotificationReadStatusRequest
  ): Observable<void> {
    return this.http.put<void>(
      `${this.apiUrl}/read-status`,
      request
    );
  }
}
import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

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

@Injectable({
  providedIn: 'root',
})
export class NotificationsService {
  private readonly http = inject(HttpClient);

  private readonly apiUrl =
    'https://localhost:7260/api/notifications';

  getMyNotifications(): Observable<NotificationItem[]> {
    return this.http.get<NotificationItem[]>(
      this.apiUrl
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
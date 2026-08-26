import {
  ChangeDetectorRef,
  Component,
  OnInit,
} from '@angular/core';

import {
  NotificationItem,
  NotificationsService,
} from '../../core/services/notifications';

@Component({
  selector: 'app-notifications',
  imports: [],
  templateUrl: './notifications.html',
  styleUrl: './notifications.scss',
})
export class NotificationsPage implements OnInit {
  notifications: NotificationItem[] = [];

  loading = false;
  errorMessage = '';

  constructor(
    private readonly notificationsService: NotificationsService,
    private readonly cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.loadNotifications();
  }

  get unreadCount(): number {
    return this.notifications.filter(
      (notification) => !notification.isRead
    ).length;
  }

  loadNotifications(): void {
    this.loading = true;
    this.errorMessage = '';

    this.notificationsService
      .getMyNotifications()
      .subscribe({
        next: (notifications) => {
          this.notifications = notifications;
          this.loading = false;
          this.cdr.detectChanges();
        },
        error: (error) => {
          console.error(error);

          this.errorMessage =
            'Učitavanje notifikacija nije uspjelo.';

          this.loading = false;
          this.cdr.detectChanges();
        },
      });
  }

  toggleReadStatus(notification: NotificationItem): void {
    const newStatus = !notification.isRead;

    this.errorMessage = '';

    this.notificationsService
      .setReadStatus({
        notificationId: notification.id,
        isRead: newStatus,
      })
      .subscribe({
        next: () => {
          notification.isRead = newStatus;
          notification.readAt = newStatus
            ? new Date().toISOString()
            : null;

          this.cdr.detectChanges();
        },
        error: (error) => {
          console.error(error);

          this.errorMessage =
            'Promjena statusa notifikacije nije uspjela.';

          this.cdr.detectChanges();
        },
      });
  }
}
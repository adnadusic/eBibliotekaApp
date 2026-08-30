import {
  ChangeDetectorRef,
  Component,
  OnInit,
} from '@angular/core';

import {
  NotificationItem,
  NotificationSettingItem,
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
  notificationSettings: NotificationSettingItem[] = [];

  loading = false;
  settingsLoading = false;
  errorMessage = '';

  selectedType?: number;
  selectedReadStatus: 'all' | 'read' | 'unread' = 'all';

  constructor(
    private readonly notificationsService: NotificationsService,
    private readonly cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.loadNotificationSettings();
    this.loadNotifications();
  }

  get unreadCount(): number {
    return this.notifications.filter(
      (notification) => !notification.isRead
    ).length;
  }

  get sortedNotifications(): NotificationItem[] {
    return [...this.notifications].sort((a, b) => {
      const aPriority = this.isPriority(a.type);
      const bPriority = this.isPriority(b.type);

      if (aPriority === bPriority) {
        return 0;
      }

      return aPriority ? -1 : 1;
    });
  }

  loadNotifications(): void {
    this.loading = true;
    this.errorMessage = '';

    let isRead: boolean | undefined;

    if (this.selectedReadStatus === 'read') {
      isRead = true;
    } else if (this.selectedReadStatus === 'unread') {
      isRead = false;
    }

    this.notificationsService
      .getMyNotifications(
        this.selectedType,
        isRead
      )
      .subscribe({
        next: (notifications) => {
          this.notifications = notifications;
          this.loading = false;
          this.cdr.detectChanges();
        },
        error: (error) => {
          console.error(error);

          this.errorMessage =
            'Failed to load notifications.';

          this.loading = false;
          this.cdr.detectChanges();
        },
      });
  }

  loadNotificationSettings(): void {
    this.settingsLoading = true;

    this.notificationsService
      .getMyNotificationSettings()
      .subscribe({
        next: (settings) => {
          this.notificationSettings = settings;
          this.settingsLoading = false;
          this.cdr.detectChanges();
        },
        error: (error) => {
          console.error(error);

          this.errorMessage =
            'Failed to load notification settings.';

          this.settingsLoading = false;
          this.cdr.detectChanges();
        },
      });
  }

  isPriority(type: number): boolean {
    return (
      this.notificationSettings.find(
        (setting) => setting.type === type
      )?.isPriority ?? false
    );
  }

  togglePriority(type: number): void {
    const newValue = !this.isPriority(type);

    this.errorMessage = '';

    this.notificationsService
      .setPriority(type, newValue)
      .subscribe({
        next: () => {
          const existingSetting =
            this.notificationSettings.find(
              (setting) => setting.type === type
            );

          if (existingSetting) {
            existingSetting.isPriority = newValue;
          } else {
            this.notificationSettings.push({
              type,
              isPriority: newValue,
            });
          }

          this.cdr.detectChanges();
        },
        error: (error) => {
          console.error(error);

          this.errorMessage =
            'Failed to update the priority setting.';

          this.cdr.detectChanges();
        },
      });
  }

  onTypeFilterChange(event: Event): void {
    const value =
      (event.target as HTMLSelectElement).value;

    this.selectedType =
      value === ''
        ? undefined
        : Number(value);

    this.loadNotifications();
  }

  onReadFilterChange(event: Event): void {
    const value =
      (event.target as HTMLSelectElement).value;

    if (
      value === 'read' ||
      value === 'unread'
    ) {
      this.selectedReadStatus = value;
    } else {
      this.selectedReadStatus = 'all';
    }

    this.loadNotifications();
  }

  toggleReadStatus(
    notification: NotificationItem
  ): void {
    const newStatus = !notification.isRead;

    this.errorMessage = '';

    this.notificationsService
      .setReadStatus({
        notificationId: notification.id,
        isRead: newStatus,
      })
      .subscribe({
        next: () => {
          this.loadNotifications();
        },
        error: (error) => {
          console.error(error);

          this.errorMessage =
            'Failed to update the notification status.';

          this.cdr.detectChanges();
        },
      });
  }
}
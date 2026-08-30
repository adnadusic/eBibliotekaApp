import {
  ChangeDetectorRef,
  Component,
  OnInit,
} from '@angular/core';

import {
  NotificationSettingItem,
  NotificationsService,
} from '../../core/services/notifications';

@Component({
  selector: 'app-settings',
  imports: [],
  templateUrl: './settings.html',
  styleUrl: './settings.scss',
})
export class SettingsPage implements OnInit {
  notificationSettings: NotificationSettingItem[] = [];

  readonly notificationTypes = [
    {
      type: 1,
      name: 'Reservations',
      description: 'Notifications related to book reservations.',
    },
    {
      type: 2,
      name: 'Loans',
      description: 'Notifications related to active loans.',
    },
    {
      type: 3,
      name: 'Penalties',
      description: 'Notifications related to penalties and obligations.',
    },
    {
      type: 4,
      name: 'General',
      description: 'General system notifications.',
    },
  ];

  loading = false;
  savingType?: number;
  errorMessage = '';
  successMessage = '';

  constructor(
    private readonly notificationsService: NotificationsService,
    private readonly cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.loadSettings();
  }

  loadSettings(): void {
    this.loading = true;
    this.errorMessage = '';

    this.notificationsService
      .getMyNotificationSettings()
      .subscribe({
        next: (settings) => {
          this.notificationSettings = settings;
          this.loading = false;
          this.cdr.detectChanges();
        },
        error: (error) => {
          console.error(error);

          this.errorMessage =
            'Failed to load settings.';

          this.loading = false;
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

    this.savingType = type;
    this.errorMessage = '';
    this.successMessage = '';

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

          this.successMessage =
            'Settings saved successfully.';

          this.savingType = undefined;
          this.cdr.detectChanges();
        },
        error: (error) => {
          console.error(error);

          this.errorMessage =
            'Failed to save the setting.';

          this.savingType = undefined;
          this.cdr.detectChanges();
        },
      });
  }
}
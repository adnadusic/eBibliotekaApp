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
      name: 'Rezervacije',
      description: 'Obavijesti vezane za rezervacije knjiga.',
    },
    {
      type: 2,
      name: 'Posudbe',
      description: 'Obavijesti vezane za aktivne posudbe.',
    },
    {
      type: 3,
      name: 'Kazne',
      description: 'Obavijesti vezane za kazne i obaveze.',
    },
    {
      type: 4,
      name: 'Opšte',
      description: 'Opšte sistemske obavijesti.',
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
            'Učitavanje postavki nije uspjelo.';

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
            'Postavke su uspješno sačuvane.';

          this.savingType = undefined;
          this.cdr.detectChanges();
        },
        error: (error) => {
          console.error(error);

          this.errorMessage =
            'Čuvanje postavke nije uspjelo.';

          this.savingType = undefined;
          this.cdr.detectChanges();
        },
      });
  }
}
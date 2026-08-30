import { DecimalPipe } from '@angular/common';
import {
  ChangeDetectorRef,
  Component,
  OnInit,
  inject,
} from '@angular/core';
import { RouterLink } from '@angular/router';
import { forkJoin } from 'rxjs';

import { BooksService } from '../../core/services/books';
import { NotificationsService } from '../../core/services/notifications';

@Component({
  selector: 'app-dashboard',
  imports: [RouterLink, DecimalPipe],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss',
})
export class DashboardPage implements OnInit {
  private readonly booksService = inject(BooksService);
  private readonly notificationsService = inject(NotificationsService);
  private readonly cdr = inject(ChangeDetectorRef);

  totalBooks = 0;
  unreadNotifications = 0;
  priorityNotificationTypes = 0;

  loading = false;
  errorMessage = '';

  ngOnInit(): void {
    this.loadDashboard();
  }

  loadDashboard(): void {
    this.loading = true;
    this.errorMessage = '';

    forkJoin({
      books: this.booksService.getPaged({
        page: 1,
        pageSize: 1,
      }),
      unreadNotifications:
        this.notificationsService.getMyNotifications(
          undefined,
          false
        ),
      notificationSettings:
        this.notificationsService.getMyNotificationSettings(),
    }).subscribe({
      next: (result) => {
        this.totalBooks = result.books.total;
        this.unreadNotifications =
          result.unreadNotifications.length;
        this.priorityNotificationTypes =
          result.notificationSettings.filter(
            (setting) => setting.isPriority
          ).length;

        this.loading = false;
        this.cdr.detectChanges();
      },
      error: (error) => {
        console.error(error);

        this.errorMessage =
          'Failed to load dashboard data.';

        this.loading = false;
        this.cdr.detectChanges();
      },
    });
  }
}
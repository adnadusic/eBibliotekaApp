import { Routes } from '@angular/router';

import { authGuard } from './core/guards/auth.guard';
import { Login } from './features/auth/login/login';
import { AuditTrailPage } from './features/audit-trail/audit-trail';
import { BookDetailsPage } from './features/books/book-details/book-details';
import { BookForm } from './features/books/book-form/book-form';
import { BookList } from './features/books/book-list/book-list';
import { DashboardPage } from './features/dashboard/dashboard';
import { Landing } from './features/landing/landing';
import { NotificationsPage } from './features/notifications/notifications';
import { ReportsPage } from './features/reports/reports';

export const routes: Routes = [
  {
    path: '',
    component: Landing,
  },
  {
    path: 'login',
    component: Login,
  },
  {
    path: 'books',
    component: BookList,
  },
  {
    path: 'books/new',
    component: BookForm,
    canActivate: [authGuard],
  },
  {
    path: 'books/:id/edit',
    component: BookForm,
    canActivate: [authGuard],
  },
  {
    path: 'books/:id',
    component: BookDetailsPage,
  },
  {
    path: 'dashboard',
    component: DashboardPage,
    canActivate: [authGuard],
  },
  {
    path: 'settings',
    loadComponent: () =>
      import('./features/settings/settings').then(
        (m) => m.SettingsPage
      ),
    canActivate: [authGuard],
  },
  {
    path: 'notifications',
    component: NotificationsPage,
    canActivate: [authGuard],
  },
  {
    path: 'audit-trail',
    component: AuditTrailPage,
    canActivate: [authGuard],
  },
  {
    path: 'reports',
    component: ReportsPage,
    canActivate: [authGuard],
  },
  {
    path: '**',
    redirectTo: '',
  },
];
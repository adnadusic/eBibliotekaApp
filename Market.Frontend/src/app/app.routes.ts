import { Routes } from '@angular/router';

import { authGuard } from './core/guards/auth.guard';
import { Login } from './features/auth/login/login';
import { BookList } from './features/books/book-list/book-list';
import { BookForm } from './features/books/book-form/book-form';
import { BookDetailsPage } from './features/books/book-details/book-details';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'books',
    pathMatch: 'full',
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
    path: '**',
    redirectTo: 'books',
  },
];
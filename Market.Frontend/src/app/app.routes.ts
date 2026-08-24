import { Routes } from '@angular/router';

import { authGuard } from './core/guards/auth.guard';
import { Login } from './features/auth/login/login';
import { BookDetailsPage } from './features/books/book-details/book-details';
import { BookForm } from './features/books/book-form/book-form';
import { BookList } from './features/books/book-list/book-list';
import { Landing } from './features/landing/landing';

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
    path: '**',
    redirectTo: '',
  },
];
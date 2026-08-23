import { Routes } from '@angular/router';

import { BookList } from './features/books/book-list/book-list';
import { BookForm } from './features/books/book-form/book-form';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'books',
    pathMatch: 'full',
  },
  {
    path: 'books',
    component: BookList,
  },
  {
    path: 'books/new',
    component: BookForm,
  },
  {
    path: 'books/:id/edit',
    component: BookForm,
  },
  {
    path: '**',
    redirectTo: 'books',
  },
];
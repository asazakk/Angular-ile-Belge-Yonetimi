import { Routes } from '@angular/router';
import { Login } from './pages/login/login';
import { Dashboard } from './pages/dashboard/dashboard';
import { CreateDocument } from './pages/documents/create-document';
import { Profile } from './pages/profile/profile';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: Login },
  { path: 'dashboard', component: Dashboard },
  { path: 'documents/create', component: CreateDocument },
  { path: 'profile', component: Profile },
  { path: '**', redirectTo: '/login' }
];

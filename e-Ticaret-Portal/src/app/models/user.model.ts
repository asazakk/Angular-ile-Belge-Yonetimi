export interface User {
  id: number;
  username: string;
  email: string;
  firstName: string;
  lastName: string;
  department?: string;
  position?: string;
  isActive: boolean;
  isDeleted: boolean;
  lastLoginDate?: Date;
  createdAt: Date;
  updatedAt: Date;
}

export interface UserCreateDto {
  username: string;
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  department?: string;
  position?: string;
}

export interface UserUpdateDto {
  email: string;
  firstName: string;
  lastName: string;
  department?: string;
  position?: string;
  isActive: boolean;
}

export interface LoginRequest {
  username: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  expiration: string;
  user: User;
}

export interface ChangePasswordDto {
  currentPassword: string;
  newPassword: string;
}

import { writable } from 'svelte/store';
import { authService } from '../api/auth';
import type { Employee, LoginRequest } from '../types';

interface AuthState {
	isAuthenticated: boolean;
	user: Employee | null;
	token: string | null;
	error: string | null;
}

const initialState: AuthState = {
	isAuthenticated: false,
	user: null,
	token: null,
	error: null
};

function createAuthStore() {
	const { subscribe, set, update } = writable<AuthState>(initialState);

	return {
		subscribe,
		initializeFromStorage: () => {
			if (typeof window === 'undefined') return;
			const token = localStorage.getItem('token');
			const userJson = localStorage.getItem('user');
			if (token && userJson) {
				try {
					const user = JSON.parse(userJson);
					set({ isAuthenticated: true, user, token, error: null });
				} catch (error) {
					console.error('Failed to parse user from localStorage:', error);
					localStorage.removeItem('token');
					localStorage.removeItem('user');
				}
			}
		},
		login: async (credentials: LoginRequest) => {
			update(state => ({ ...state, error: null }));
			try {
				const response = await authService.login(credentials);
				const { token, employee } = response;
				
				// Store in localStorage
				if (typeof window !== 'undefined') {
					localStorage.setItem('token', token);
					localStorage.setItem('user', JSON.stringify(employee));
				}
				
				set({
					isAuthenticated: true,
					user: employee,
					token,
					error: null
				});
			} catch (error: any) {
				const errorMessage = error.response?.data?.message || error.message || 'Login failed';
				update(state => ({ ...state, error: errorMessage }));
				throw error;
			}
		},
		logout: async () => {
			try {
				await authService.logout();
			} catch (error) {
				console.error('Logout error:', error);
			} finally {
				// Clear storage and state regardless of API call success
				if (typeof window !== 'undefined') {
					localStorage.removeItem('token');
					localStorage.removeItem('user');
				}
				set(initialState);
			}
		},
		clearError: () => {
			update(state => ({ ...state, error: null }));
		}
	};
}

export const authStore = createAuthStore();

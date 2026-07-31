import { writable } from 'svelte/store';

interface User {
  employeeId: number;
  name: string;
  role: string;
  warehouseId?: number;
}

interface AuthState {
  token: string | null;
  user: User | null;
  isAuthenticated: boolean;
}

const initialState: AuthState = {
  token: null,
  user: null,
  isAuthenticated: false,
};

function createAuthStore() {
  const { subscribe, set, update } = writable<AuthState>(initialState);

  return {
    subscribe,
    login: (token: string, user: User) => {
      if (typeof document !== 'undefined') {
        document.cookie = `token=${token}; path=/; max-age=86400; SameSite=Lax`;
        document.cookie = `user=${JSON.stringify(user)}; path=/; max-age=86400; SameSite=Lax`;
      }
      set({ token, user, isAuthenticated: true });
    },
    logout: () => {
      if (typeof document !== 'undefined') {
        document.cookie = 'token=; path=/; max-age=0';
        document.cookie = 'user=; path=/; max-age=0';
      }
      set({ token: null, user: null, isAuthenticated: false });
    },
    checkAuth: () => {
      if (typeof document === 'undefined') {
        set({ token: null, user: null, isAuthenticated: false });
        return;
      }
      const tokenMatch = document.cookie.match(/token=([^;]+)/);
      const userMatch = document.cookie.match(/user=([^;]+)/);
      const token = tokenMatch ? tokenMatch[1] : null;
      const user = userMatch ? JSON.parse(decodeURIComponent(userMatch[1])) : null;
      set({ token, user, isAuthenticated: !!token });
    },
  };
}

export const auth = createAuthStore();

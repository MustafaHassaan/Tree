import { redirect } from '@sveltejs/kit';
import type { Handle } from '@sveltejs/kit';

export const handle: Handle = async ({ event, resolve }) => {
  const token = event.cookies.get('token');

  // Protect dashboard routes
  if (event.url.pathname.startsWith('/dashboard')) {
    if (!token) {
      throw redirect(302, '/');
    }
  }

  // Redirect to dashboard if already logged in
  if (event.url.pathname === '/' && token) {
    throw redirect(302, '/dashboard');
  }

  const response = await resolve(event);
  return response;
};

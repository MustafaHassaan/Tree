import axios from 'axios';

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5157/api';

export const apiClient = axios.create({
	baseURL: API_BASE_URL,
	headers: {
		'Content-Type': 'application/json'
	}
});

// Request interceptor to add JWT token
apiClient.interceptors.request.use(
	(config) => {
		if (typeof window !== 'undefined') {
			const token = localStorage.getItem('token');
			if (token) {
				config.headers.Authorization = `Bearer ${token}`;
			}
		}
		return config;
	},
	(error) => {
		return Promise.reject(error);
	}
);

// Response interceptor for global error handling
apiClient.interceptors.response.use(
	(response) => response,
	(error) => {
		if (error.response) {
			// Server responded with error status
			switch (error.response.status) {
				case 401:
					// Unauthorized - clear token and redirect to login
					if (typeof window !== 'undefined') {
						localStorage.removeItem('token');
						window.location.href = '/';
					}
					break;
				case 403:
					console.error('Access forbidden:', error.response.data);
					break;
				case 404:
					console.error('Resource not found:', error.response.data);
					break;
				case 500:
					console.error('Server error:', error.response.data);
					break;
				default:
					console.error('API error:', error.response.data);
			}
		} else if (error.request) {
			// Request made but no response
			console.error('Network error:', error.message);
		} else {
			// Error in request setup
			console.error('Request error:', error.message);
		}
		return Promise.reject(error);
	}
);

export default apiClient;

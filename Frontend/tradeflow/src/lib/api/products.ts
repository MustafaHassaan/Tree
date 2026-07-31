import { apiClient } from './client';
import type { Product, Category } from '../types';

export const productsService = {
	async getAll(): Promise<Product[]> {
		const response = await apiClient.get<Product[]>('/products');
		return response.data;
	},

	async getById(id: number): Promise<Product> {
		const response = await apiClient.get<Product>(`/products/${id}`);
		return response.data;
	},

	async create(product: Omit<Product, 'id' | 'category'>): Promise<number> {
		const response = await apiClient.post<number>('/products', product);
		return response.data;
	},

	async update(id: number, product: Partial<Product>): Promise<boolean> {
		const response = await apiClient.put<boolean>(`/products/${id}`, { ...product, id });
		return response.data;
	},

	async delete(id: number): Promise<boolean> {
		const response = await apiClient.delete<boolean>(`/products/${id}`);
		return response.data;
	}
};

export const categoriesService = {
	async getAll(): Promise<Category[]> {
		const response = await apiClient.get<Category[]>('/categories');
		return response.data;
	},

	async getById(id: number): Promise<Category> {
		const response = await apiClient.get<Category>(`/categories/${id}`);
		return response.data;
	},

	async create(category: Omit<Category, 'id'>): Promise<number> {
		const response = await apiClient.post<number>('/categories', category);
		return response.data;
	},

	async update(id: number, category: Partial<Category>): Promise<boolean> {
		const response = await apiClient.put<boolean>(`/categories/${id}`, { ...category, id });
		return response.data;
	},

	async delete(id: number): Promise<boolean> {
		const response = await apiClient.delete<boolean>(`/categories/${id}`);
		return response.data;
	}
};

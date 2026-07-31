import { apiClient } from './client';
import type { Customer } from '../types';

export const customersService = {
	async getAll(): Promise<Customer[]> {
		const response = await apiClient.get<Customer[]>('/customers');
		return response.data;
	},

	async getById(id: number): Promise<Customer> {
		const response = await apiClient.get<Customer>(`/customers/${id}`);
		return response.data;
	},

	async create(customer: Omit<Customer, 'id'>): Promise<number> {
		const response = await apiClient.post<number>('/customers', customer);
		return response.data;
	},

	async update(id: number, customer: Partial<Customer>): Promise<boolean> {
		const response = await apiClient.put<boolean>(`/customers/${id}`, { ...customer, id });
		return response.data;
	},

	async delete(id: number): Promise<boolean> {
		const response = await apiClient.delete<boolean>(`/customers/${id}`);
		return response.data;
	}
};

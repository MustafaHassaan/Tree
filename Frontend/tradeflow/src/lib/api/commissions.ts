import { apiClient } from './client';
import type { Commission } from '../types';

export const commissionsService = {
	async getAll(): Promise<Commission[]> {
		const response = await apiClient.get<Commission[]>('/commissions');
		return response.data;
	},

	async getById(id: number): Promise<Commission> {
		const response = await apiClient.get<Commission>(`/commissions/${id}`);
		return response.data;
	},

	async create(commission: Omit<Commission, 'id' | 'employee'>): Promise<number> {
		const response = await apiClient.post<number>('/commissions', commission);
		return response.data;
	},

	async update(id: number, commission: Partial<Commission>): Promise<boolean> {
		const response = await apiClient.put<boolean>(`/commissions/${id}`, { ...commission, id });
		return response.data;
	},

	async delete(id: number): Promise<boolean> {
		const response = await apiClient.delete<boolean>(`/commissions/${id}`);
		return response.data;
	}
};

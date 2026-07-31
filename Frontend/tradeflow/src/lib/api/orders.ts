import { apiClient } from './client';
import type { Order, CreateOrderCommand } from '../types';

export const ordersService = {
	async getAll(): Promise<Order[]> {
		const response = await apiClient.get<Order[]>('/orders');
		return response.data;
	},

	async getById(id: number): Promise<Order> {
		const response = await apiClient.get<Order>(`/orders/${id}`);
		return response.data;
	},

	async createOrder(command: CreateOrderCommand): Promise<number> {
		const response = await apiClient.post<number>('/orders', command);
		return response.data;
	},

	async updateStatus(id: number, status: number): Promise<boolean> {
		const response = await apiClient.put<boolean>(`/orders/${id}/status`, { status });
		return response.data;
	},

	async getBySalesRep(salesRepId: number): Promise<Order[]> {
		const response = await apiClient.get<Order[]>(`/orders/sales-rep/${salesRepId}`);
		return response.data;
	}
};

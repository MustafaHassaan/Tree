import api from './api';

export interface Warehouse {
  id: number;
  name: string;
  location: string;
}

export interface StockItem {
  productId: number;
  productName: string;
  barcode: string;
  price: number;
  quantity: number;
  categoryName: string;
}

export const warehousesService = {
  getAll: async (): Promise<Warehouse[]> => {
    const response = await api.get('/warehouses');
    return response.data;
  },

  getById: async (id: number): Promise<Warehouse> => {
    const response = await api.get(`/warehouses/${id}`);
    return response.data;
  },

  getProductsByWarehouse: async (warehouseId: number): Promise<StockItem[]> => {
    const response = await api.get(`/warehouses/${warehouseId}/products`);
    return response.data;
  },

  create: async (data: { name: string; location: string }): Promise<Warehouse> => {
    const response = await api.post('/warehouses', data);
    return response.data;
  },

  update: async (id: number, data: { name: string; location: string }): Promise<Warehouse> => {
    const response = await api.put(`/warehouses/${id}`, { id, ...data });
    return response.data;
  },

  delete: async (id: number): Promise<void> => {
    await api.delete(`/warehouses/${id}`);
  },

  assignEmployee: async (employeeId: number, warehouseId: number): Promise<void> => {
    await api.post('/warehouses/assign-employee', { employeeId, warehouseId });
  }
};

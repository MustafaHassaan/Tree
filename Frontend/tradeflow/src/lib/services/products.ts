import api from './api';

export interface Product {
  id: number;
  name: string;
  barcode: string;
  price: number;
  cost: number;
  categoryId: number;
  isDeleted: boolean;
}

export const productsService = {
  getAll: async (): Promise<Product[]> => {
    const response = await api.get('/products');
    return response.data;
  },

  getById: async (id: number): Promise<Product> => {
    const response = await api.get(`/products/${id}`);
    return response.data;
  },

  create: async (data: { name: string; barcode: string; price: number; cost: number; categoryId: number }): Promise<Product> => {
    const response = await api.post('/products', data);
    return response.data;
  },

  update: async (id: number, data: { id: number; name: string; barcode: string; price: number; cost: number; categoryId: number }): Promise<Product> => {
    const response = await api.put(`/products/${id}`, data);
    return response.data;
  },

  softDelete: async (id: number): Promise<void> => {
    await api.patch(`/products/${id}/soft-delete`);
  },

  delete: async (id: number): Promise<void> => {
    await api.delete(`/products/${id}`);
  }
};

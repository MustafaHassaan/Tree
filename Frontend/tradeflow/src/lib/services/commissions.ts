import api from './api';

export interface Commission {
  id: number;
  targetAmount: number;
  percentage: number;
  notes: string;
}

export const commissionsService = {
  getAll: async (): Promise<Commission[]> => {
    const response = await api.get('/commissions');
    return response.data;
  },

  getById: async (id: number): Promise<Commission> => {
    const response = await api.get(`/commissions/${id}`);
    return response.data;
  },

  create: async (data: { targetAmount: number; percentage: number; notes: string }): Promise<Commission> => {
    const response = await api.post('/commissions', data);
    return response.data;
  },

  update: async (id: number, data: { targetAmount: number; percentage: number; notes: string }): Promise<Commission> => {
    const response = await api.put(`/commissions/${id}`, data);
    return response.data;
  },

  delete: async (id: number): Promise<void> => {
    await api.delete(`/commissions/${id}`);
  }
};

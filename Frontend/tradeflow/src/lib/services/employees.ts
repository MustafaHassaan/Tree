import api from './api';

export interface Employee {
  id: number;
  name: string;
  role: number;
  phone: string;
  target?: number;
  commissionId?: number;
}

export interface PerformanceData {
  salesRepId: number;
  salesRepName: string;
  targetAmount: number;
  commissionPercentage: number;
  totalSales: number;
  achievementPercentage: number;
  earnedCommission: number;
  totalOrders: number;
}

export const employeesService = {
  getAll: async (): Promise<Employee[]> => {
    const response = await api.get('/employees');
    return response.data;
  },

  getById: async (id: number): Promise<Employee> => {
    const response = await api.get(`/employees/${id}`);
    return response.data;
  },

  getPerformance: async (salesRepId: number): Promise<PerformanceData> => {
    const response = await api.get(`/employees/${salesRepId}/performance`);
    return response.data;
  },

  create: async (data: { name: string; role: number; phone: string; password: string; commissionId?: number }): Promise<Employee> => {
    const response = await api.post('/employees', data);
    return response.data;
  },

  update: async (id: number, data: { name: string; role: number; phone: string; password?: string; commissionId?: number }): Promise<Employee> => {
    const response = await api.put(`/employees/${id}`, data);
    return response.data;
  },

  delete: async (id: number): Promise<void> => {
    await api.delete(`/employees/${id}`);
  }
};

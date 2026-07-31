import api from './api';

export interface DailySales {
  day: string;
  total: number;
  actual: number;
}

export interface WarehouseStatus {
  id: number;
  name: string;
  capacityPercentage: number;
  color: string;
}

export interface TopProduct {
  id: number;
  name: string;
  sku: string;
  sold: number;
  revenue: number;
  trend: string;
  trendClass: string;
}

export interface SalesRepPerformance {
  id: number;
  name: string;
  position: string;
  sales: number;
  goal: string;
  rank: number;
  active: boolean;
}

export interface DashboardStats {
  totalSales: number;
  monthlyRevenue: number;
  activeCustomers: number;
  lowStockAlerts: number;
  dailySales: DailySales[];
  warehouseStatus: WarehouseStatus[];
  topProducts: TopProduct[];
  salesRepsPerformance: SalesRepPerformance[];
}

export const dashboardService = {
  getStats: async (): Promise<DashboardStats> => {
    const response = await api.get('/dashboard');
    return response.data;
  }
};
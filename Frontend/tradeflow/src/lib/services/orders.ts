import api from './api';

export interface Order {
  id: number;
  customerId: number;
  salesRepId: number;
  status: string;
  totalAmount: number;
  createdAt: string;
  updatedAt: string;
}

export interface OrderDetailDto {
  id: number;
  customerId: number;
  salesRepId: number;
  status: string;
  totalAmount: number;
  createdAt: string;
  updatedAt: string;
  items: OrderItem[];
}

export interface OrderItem {
  id: number;
  productId: number;
  quantity: number;
  price: number;
}

export interface OrderDto {
  id: number;
  customerId: number;
  salesRepId: number;
  status: string;
  totalAmount: number;
  createdAt: string;
}

const ordersService = {
  async getAll(): Promise<Order[]> {
    const response = await api.get('/orders');
    return response.data;
  },

  async getById(id: number): Promise<OrderDetailDto> {
    const response = await api.get(`/orders/${id}`);
    return response.data;
  },

  async getBySalesRep(salesRepId: number): Promise<OrderDto[]> {
    const response = await api.get(`/orders/sales-rep/${salesRepId}`);
    return response.data;
  },

  async create(order: { customerId: number; salesRepId: number; warehouseId: number; items: { productId: number; quantity: number }[] }): Promise<number> {
    const response = await api.post('/orders', order);
    return response.data;
  },

  async updateStatus(orderId: number, status: string): Promise<boolean> {
    const response = await api.put(`/orders/${orderId}/status`, { orderId, status });
    return response.data;
  }
};

export default ordersService;

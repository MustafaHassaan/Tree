export interface Customer {
  id: number;
  name: string;
  type: number | string; // 0: Restaurant, 1: Hotel, 2: Shop (or string for enriched data)
  address: string;
  phone: string;
  location?: string;
  status?: string;
  lastOrder?: string;
  totalSpend?: number;
  icon?: string;
  iconColor?: string;
  accountType?: string;
  groupId?: string;
  contact?: any;
  addressDetail?: any;
  stats?: any;
  recentOrders?: any[];
}
export interface Warehouse {
  id: number;
  name: string;
  code: string;
  region: string;
  utilization: number;
  totalSkus: number;
  status: string;
  statusColor: 'green' | 'blue' | 'error' | string;
  lastAudit: string;
  icon: string;
  iconColor: 'primary' | 'secondary' | 'error' | string;
}

export interface RestockItem {
  id: number;
  sku: string;
  name: string;
  currentStock: number;
  image: string;
  isCritical: boolean;
}
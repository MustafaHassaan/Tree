export interface SalesRep {
  id: number;
  name: string;
  initials: string;
  role: string;
  avatar: string;
  targetPercent: number;
  actualSales: number;
  commission: number;
  performance: string;
  performanceColor: 'primary' | 'error' | string;
}

export interface Activity {
  id: number;
  rep: string;
  initials: string;
  color: 'primary' | 'secondary' | 'tertiary' | string;
  action: string;
  client: string;
  amount: number;
  status: 'COMPLETED' | 'PENDING' | 'SCHEDULED' | 'IN PROGRESS' | string;
  statusColor: 'green' | 'blue' | 'gray' | 'orange' | string;
  time: string;
}
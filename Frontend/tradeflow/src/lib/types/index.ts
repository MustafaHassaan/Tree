// Enums
export enum EmployeeRole {
	Admin = 'Admin',
	SalesRepresentative = 'SalesRepresentative',
	WarehouseManager = 'WarehouseManager'
}

export enum CustomerType {
	Hotel = 'Hotel',
	Restaurant = 'Restaurant',
	Shop = 'Shop'
}

export enum OrderStatus {
	Pending = 0,
	Approved = 1,
	Completed = 2,
	Cancelled = 3
}

// Interfaces
export interface Product {
	id: number;
	barcode: string;
	name: string;
	categoryId: number;
	category?: Category;
	price: number;
	cost: number;
	isDeleted: boolean;
}

export interface Category {
	id: number;
	name: string;
}

export interface Order {
	id: number;
	customerId: number;
	customer?: Customer;
	salesRepId: number;
	salesRep?: Employee;
	warehouseId: number;
	warehouse?: Warehouse;
	totalAmount: number;
	status: OrderStatus;
	createdAt: string;
	orderDetails?: OrderDetail[];
}

export interface OrderDetail {
	id: number;
	orderId: number;
	productId: number;
	product?: Product;
	quantity: number;
	unitPrice: number;
	totalPrice: number;
}

export interface Customer {
	id: number;
	name: string;
	type: CustomerType;
	address: string;
	phone: string;
	email?: string;
}

export interface Employee {
	id: number;
	name: string;
	phone: string;
	email?: string;
	role: EmployeeRole;
	commission?: Commission;
	salesOrders?: Order[];
	employeeWarehouses?: EmployeeWarehouse[];
}

export interface Commission {
	id: number;
	employeeId: number;
	percentage: number;
	targetAmount: number;
	employee?: Employee;
}

export interface Warehouse {
	id: number;
	name: string;
	location: string;
}

export interface EmployeeWarehouse {
	id: number;
	employeeId: number;
	warehouseId: number;
	employee?: Employee;
	warehouse?: Warehouse;
}

export interface SalesRepPerformanceDto {
	salesRepId: number;
	totalSales: number;
	totalOrders: number;
	achievementPercentage: number;
	earnedCommission: number;
	targetAmount?: number;
	commissionPercentage?: number;
}

// DTOs for API requests/responses
export interface LoginRequest {
	phone: string;
	password: string;
}

export interface LoginResponse {
	token: string;
	employee: Employee;
}

export interface CreateOrderCommand {
	customerId: number;
	salesRepId: number;
	warehouseId: number;
	orderDetails: CreateOrderDetailCommand[];
}

export interface CreateOrderDetailCommand {
	productId: number;
	quantity: number;
}

import { writable, derived } from 'svelte/store';
import type { Product } from '../types';

interface CartItem {
	product: Product;
	quantity: number;
	warehouseId: number;
	availableStock: number;
}

interface CartState {
	items: CartItem[];
	selectedWarehouseId: number | null;
	selectedCustomerId: number | null;
}

const initialState: CartState = {
	items: [],
	selectedWarehouseId: null,
	selectedCustomerId: null
};

function createCartStore() {
	const { subscribe, set, update } = writable<CartState>(initialState);

	return {
		subscribe,
		addItem: (product: Product, warehouseId: number, availableStock: number) => {
			update(state => {
				const existingItem = state.items.find(
					item => item.product.id === product.id && item.warehouseId === warehouseId
				);

				if (existingItem) {
					// Check stock limit
					if (existingItem.quantity < availableStock) {
						return {
							...state,
							items: state.items.map(item =>
								item.product.id === product.id && item.warehouseId === warehouseId
									? { ...item, quantity: item.quantity + 1 }
									: item
							)
						};
					}
					return state; // Stock limit reached
				} else {
					// Add new item
					if (availableStock > 0) {
						return {
							...state,
							items: [
								...state.items,
								{ product, quantity: 1, warehouseId, availableStock }
							]
						};
					}
					return state; // No stock available
				}
			});
		},
		removeItem: (productId: number, warehouseId: number) => {
			update(state => ({
				...state,
				items: state.items.filter(
					item => !(item.product.id === productId && item.warehouseId === warehouseId)
				)
			}));
		},
		updateQuantity: (productId: number, warehouseId: number, quantity: number) => {
			update(state => {
				const item = state.items.find(
					item => item.product.id === productId && item.warehouseId === warehouseId
				);

				if (item) {
					if (quantity <= 0) {
						// Remove item if quantity is 0 or negative
						return {
							...state,
							items: state.items.filter(
								item => !(item.product.id === productId && item.warehouseId === warehouseId)
							)
						};
					} else if (quantity <= item.availableStock) {
						// Update quantity within stock limit
						return {
							...state,
							items: state.items.map(item =>
								item.product.id === productId && item.warehouseId === warehouseId
									? { ...item, quantity }
									: item
							)
						};
					}
				}
				return state; // Invalid quantity or item not found
			});
		},
		clearCart: () => {
			set(initialState);
		},
		setWarehouse: (warehouseId: number) => {
			update(state => ({
				...state,
				selectedWarehouseId: warehouseId,
				// Clear items when warehouse changes
				items: []
			}));
		},
		setCustomer: (customerId: number) => {
			update(state => ({
				...state,
				selectedCustomerId: customerId
			}));
		}
	};
}

export const cartStore = createCartStore();

// Derived store for total amount
export const cartTotal = derived(cartStore, $cartStore => {
	return $cartStore.items.reduce(
		(total, item) => total + item.product.price * item.quantity,
		0
	);
});

// Derived store for total item count
export const cartItemCount = derived(cartStore, $cartStore => {
	return $cartStore.items.reduce((count, item) => count + item.quantity, 0);
});

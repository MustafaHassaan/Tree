<!-- src/routes/dashboard/orders/+page.svelte -->
<script lang="ts">
  import { onMount } from 'svelte';
  import OrderHeader from '../../../lib/components/ui/dashboard/orders/OrderHeader.svelte';
  import CustomerSelector from '../../../lib/components/ui/dashboard/orders/CustomerSelector.svelte';
  import WarehouseSelector from '../../../lib/components/ui/dashboard/orders/WarehouseSelector.svelte';
  import ProductCatalog from '../../../lib/components/ui/dashboard/orders/ProductCatalog.svelte';
  import OrderSummary from '../../../lib/components/ui/dashboard/orders/OrderSummary.svelte';
  import RecentOrdersTable from '../../../lib/components/ui/dashboard/orders/RecentOrdersTable.svelte';
  import { customersService } from '../../../lib/services/customers';
  import { warehousesService, type StockItem } from '../../../lib/services/warehouses';
  import ordersService from '../../../lib/services/orders';
  import { auth } from '../../../lib/stores/auth';
  import { EmployeeRole } from '../../../lib/types';

  let selectedCustomer = $state(1);
  let selectedWarehouse = $state(1);
  let searchQuery = $state('');
  let cartItems = $state<{ productId: number; name: string; price: number; quantity: number; availableStock: number }[]>([]);
  let isLoading = $state(true);
  let isLoadingProducts = $state(false);

  let customers: any[] = $state([]);
  let warehouses: any[] = $state([]);
  let stockItems: StockItem[] = $state([]);
  let recentOrders: any[] = $state([]);
  let currentUser = $state<any>(null);

  // Transform customers API data to component format
  let enrichedCustomers = $derived(
    customers.map(c => ({
      id: c.id,
      name: c.name,
      type: c.type === 0 ? 'Restaurant' : c.type === 1 ? 'Hotel' : 'Shop',
      customerId: `WH-${c.id}`,
      icon: c.type === 0 ? 'restaurant' : c.type === 1 ? 'apartment' : 'storefront'
    }))
  );

  // Transform warehouses API data to component format
  let enrichedWarehouses = $derived(
    warehouses.map(w => ({
      id: w.id,
      name: w.name,
      location: w.location
    }))
  );

  // Transform stock items to product catalog format
  let enrichedProducts = $derived(
    stockItems.map(s => ({
      id: s.productId,
      name: s.productName,
      sku: s.barcode,
      category: s.categoryName,
      price: s.price,
      image: '',
      inStock: s.quantity > 0,
      stockQuantity: s.quantity
    }))
  );

  // Transform recent orders for display (sorted descending by date)
  let enrichedRecentOrders = $derived(
    recentOrders
      .map((o: any) => ({
        id: o.orderId,
        customer: o.customerName || 'Unknown',
        date: new Date(o.createdAt).toLocaleDateString('en-US', { month: 'short', day: 'numeric', year: 'numeric' }),
        amount: o.totalAmount,
        status: o.status === 'Completed' ? 'Delivered' : o.status === 'Pending' ? 'Processing' : 'Pending Pay',
        icon: 'storefront'
      }))
      .sort((a, b) => new Date(b.date).getTime() - new Date(a.date).getTime())
  );

  onMount(async () => {
    try {
      // Check auth from cookies
      auth.checkAuth();

      // Get current user immediately after initialization
      auth.subscribe((state) => {
        currentUser = state.user;
      });

      const [customersData, warehousesData] = await Promise.all([
        customersService.getAll(),
        warehousesService.getAll()
      ]);
      customers = customersData;
      warehouses = warehousesData;

      // Load products from first warehouse
      if (warehousesData.length > 0) {
        selectedWarehouse = warehousesData[0].id;
        await loadWarehouseProducts(selectedWarehouse);
      }

      // Load all orders
      ordersService.getAll()
        .then(orders => {
          recentOrders = orders;
        })
        .catch(err => console.error('Failed to load orders:', err));
    } catch (err) {
      console.error('Failed to load orders page data:', err);
    } finally {
      isLoading = false;
    }
  });

  async function loadWarehouseProducts(warehouseId: number) {
    try {
      isLoadingProducts = true;
      stockItems = await warehousesService.getProductsByWarehouse(warehouseId);
    } catch (err) {
      console.error('Failed to load warehouse products:', err);
    } finally {
      isLoadingProducts = false;
    }
  }

  async function handleWarehouseChange(warehouseId: number) {
    await loadWarehouseProducts(warehouseId);
  }

  function updateQuantity(productId: number, delta: number) {
    const item = cartItems.find(i => i.productId === productId);
    if (item) {
      item.quantity = Math.max(1, item.quantity + delta);
    }
  }

  function addToCart(productId: number) {
    const product = enrichedProducts.find(p => p.id === productId);
    if (product && product.inStock) {
      const existing = cartItems.find(i => i.productId === productId);
      if (existing) {
        existing.quantity = Math.min(existing.quantity + 1, existing.availableStock);
      } else {
        cartItems = [...cartItems, { productId: product.id, name: product.name, price: product.price, quantity: 1, availableStock: product.stockQuantity }];
      }
    }
  }

  async function handleConfirmOrder() {
    if (cartItems.length === 0) {
      return;
    }

    try {
      if (!currentUser || !currentUser.employeeId) {
        return;
      }

      const subtotal = cartItems.reduce((sum, item) => sum + (item.price * item.quantity), 0);
      const tax = subtotal * 0.08;
      const totalAmount = subtotal + tax - 15.00;

      const orderData = {
        customerId: selectedCustomer,
        salesRepId: currentUser.employeeId,
        warehouseId: selectedWarehouse,
        items: cartItems.map(item => ({
          productId: item.productId,
          quantity: item.quantity
        }))
      };

      const orderId = await ordersService.create(orderData);

      // Clear cart
      cartItems = [];

      // Refresh recent orders
      const orders = await ordersService.getAll();
      recentOrders = orders;
    } catch (error: any) {
      console.error('Failed to create order:', error);
    }
  }
</script>

{#if isLoading}
  <div class="flex items-center justify-center py-20">
    <div class="animate-spin rounded-full h-12 w-12 border-b-2 border-primary"></div>
  </div>
{:else}
  <OrderHeader />

  <div class="grid grid-cols-1 lg:grid-cols-12 gap-lg lg:gap-xl mb-lg lg:mb-xl">
    <div class="col-span-1 lg:col-span-8 flex flex-col gap-lg">
      <WarehouseSelector warehouses={enrichedWarehouses} bind:selectedWarehouse onChange={handleWarehouseChange} />
      {#if isLoadingProducts}
        <div class="flex items-center justify-center py-10">
          <div class="animate-spin rounded-full h-8 w-8 border-b-2 border-primary"></div>
        </div>
      {:else}
        <ProductCatalog products={enrichedProducts} bind:searchQuery onAddToCart={addToCart} />
      {/if}
      <CustomerSelector customers={enrichedCustomers} bind:selectedCustomer />
    </div>

    <div class="col-span-1 lg:col-span-4">
      <OrderSummary bind:cartItems onUpdateQuantity={updateQuantity} onConfirm={handleConfirmOrder} />
    </div>
  </div>

  <RecentOrdersTable orders={enrichedRecentOrders} />
{/if}
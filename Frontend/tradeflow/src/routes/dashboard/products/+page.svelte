<script lang="ts">
  import { onMount } from 'svelte';
  import ProductHeader from '../../../lib/components/ui/dashboard/products/ProductHeader.svelte';
  import ProductFilters from '../../../lib/components/ui/dashboard/products/ProductFilters.svelte';
  import ProductTable from '../../../lib/components/ui/dashboard/products/ProductTable.svelte';
  import AddCategoryModal from '../../../lib/components/ui/dashboard/products/AddCategoryModal.svelte';
  import CategoriesListModal from '../../../lib/components/ui/dashboard/products/CategoriesListModal.svelte';
  import AddProductModal from '../../../lib/components/ui/dashboard/products/AddProductModal.svelte';
  import { productsService } from '../../../lib/services/products';
  import { categoriesService } from '../../../lib/services/categories';

  let selectedCategory = $state('All Categories');
  let currentPage = $state(1);
  let rowsPerPage = $state(15);
  let isLoading = $state(true);
  let error = $state('');
  let isAddModalOpen = $state(false);
  let isListModalOpen = $state(false);
  let isProductModalOpen = $state(false);
  let categoryToEdit = $state<any>(null);
  let productToEdit = $state<any>(null);

  let allCategories = $state<any[]>([]);

  let categories = $derived(['All Categories', ...allCategories.map(c => c.name)]);

  let products = $state<any[]>([]);
  let filteredProducts = $state<any[]>([]);

  $effect(() => {
    let result = [...products];

    if (selectedCategory !== 'All Categories') {
      result = result.filter(p => p.category === selectedCategory);
    }

    filteredProducts = result;
  });

  onMount(async () => {
    try {
      const [productsData, categoriesData] = await Promise.all([
        productsService.getAll(),
        categoriesService.getAll()
      ]);

      allCategories = categoriesData;

      // Sort products by ID descending (newest first)
      const sortedProducts = productsData.sort((a, b) => b.id - a.id);

      products = sortedProducts.map((p, index) => ({
        id: p.id,
        name: p.name,
        sku: `SKU-${p.id}`,
        barcode: p.barcode,
        category: categoriesData.find(c => c.id === p.categoryId)?.name || 'Unknown',
        categoryId: p.categoryId,
        cost: p.cost,
        quantity: Math.floor(Math.random() * 2000),
        price: p.price,
        status: Math.random() > 0.7 ? 'Low Stock' : Math.random() > 0.9 ? 'Out of Stock' : 'In Stock',
        image: 'https://lh3.googleusercontent.com/aida-public/AB6AXuA_WfH2WkqRxSSSizJk_wsjSSEGYb1s9Z9PhUqioOIt2dA5dDc3dvS-0rp2dL9YD_ic_4fIjHkxwah9qnGwptyOKTLUE7w8HVCb4qQQVZaGrUNRdL41zjUZ49jCkZf-XTsS17Wg5vf9K5gx5yZVpB_Oo0L94ha8z_hhI6GhGnig-vEJ6eYUf3sLBmVKwtfrHhEG-gbyt4PfpADuMtjt6D4F4ibrrvNv1tjpOqpgminQC2MMabveH0_uekoUXYd21X_3RdayUb13HR0',
        checked: false
      }));
    } catch (err: any) {
      error = 'Failed to load products data';
      console.error(err);
    } finally {
      isLoading = false;
    }
  });

  async function handleCategoryAdded() {
    // Reload categories
    try {
      allCategories = await categoriesService.getAll();
    } catch (err) {
      console.error('Failed to reload categories:', err);
    }
  }

  function handleEditCategory(category: any) {
    categoryToEdit = category;
    isListModalOpen = false;
    isAddModalOpen = true;
  }

  function handleAddCategory() {
    categoryToEdit = null;
    isAddModalOpen = true;
  }

  function handleViewCategories() {
    isListModalOpen = true;
  }

  function handleAddProduct() {
    productToEdit = null;
    isProductModalOpen = true;
  }

  function handleEditProduct(product: any) {
    productToEdit = product;
    isProductModalOpen = true;
  }

  async function handleProductAdded() {
    // Reload products
    try {
      const productsData = await productsService.getAll();

      // Sort products by ID descending (newest first)
      const sortedProducts = productsData.sort((a, b) => b.id - a.id);

      products = sortedProducts.map((p, index) => ({
        id: p.id,
        name: p.name,
        sku: `SKU-${p.id}`,
        barcode: p.barcode,
        category: allCategories.find(c => c.id === p.categoryId)?.name || 'Unknown',
        categoryId: p.categoryId,
        cost: p.cost,
        quantity: Math.floor(Math.random() * 2000),
        price: p.price,
        status: Math.random() > 0.7 ? 'Low Stock' : Math.random() > 0.9 ? 'Out of Stock' : 'In Stock',
        image: 'https://lh3.googleusercontent.com/aida-public/AB6AXuA_WfH2WkqRxSSSizJk_wsjSSEGYb1s9Z9PhUqioOIt2dA5dDc3dvS-0rp2dL9YD_ic_4fIjHkxwah9qnGwptyOKTLUE7w8HVCb4qQQVZaGrUNRdL41zjUZ49jCkZf-XTsS17Wg5vf9K5gx5yZVpB_Oo0L94ha8z_hhI6GhGnig-vEJ6eYUf3sLBmVKwtfrHhEG-gbyt4PfpADuMtjt6D4F4ibrrvNv1tjpOqpgminQC2MMabveH0_uekoUXYd21X_3RdayUb13HR0',
        checked: false
      }));
    } catch (err) {
      console.error('Failed to reload products:', err);
    }
  }
</script>

{#if isLoading}
  <div class="flex items-center justify-center py-20">
    <div class="animate-spin rounded-full h-12 w-12 border-b-2 border-primary"></div>
  </div>
{:else if error}
  <div class="bg-error-container text-on-error-container p-4 rounded-lg">
    {error}
  </div>
{:else}
  <ProductHeader
    onAddCategory={handleAddCategory}
    onViewCategories={handleViewCategories}
    onAddProduct={handleAddProduct}
  />

  <ProductFilters
    bind:selectedCategory
    {categories}
  />

  <ProductTable
    products={filteredProducts}
    bind:currentPage
    bind:rowsPerPage
    onEditProduct={handleEditProduct}
  />

  <AddCategoryModal
    bind:isOpen={isAddModalOpen}
    onClose={() => isAddModalOpen = false}
    onCategoryAdded={handleCategoryAdded}
    categoryToEdit={categoryToEdit}
  />

  <CategoriesListModal
    bind:isOpen={isListModalOpen}
    onClose={() => isListModalOpen = false}
    onEditCategory={handleEditCategory}
  />

  <AddProductModal
    bind:isOpen={isProductModalOpen}
    onClose={() => isProductModalOpen = false}
    onProductAdded={handleProductAdded}
    productToEdit={productToEdit}
  />
{/if}
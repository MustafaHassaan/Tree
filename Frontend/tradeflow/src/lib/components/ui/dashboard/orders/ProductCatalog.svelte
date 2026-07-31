<!-- src/routes/dashboard/orders/components/ProductCatalog.svelte -->
<script lang="ts">
  type Product = {
    id: number;
    name: string;
    sku: string;
    category: string;
    price: number;
    image: string;
    inStock: boolean;
  };

  let { 
    products = [], 
    searchQuery = $bindable(), 
    onAddToCart 
  }: { 
    products: Product[], 
    searchQuery: string, 
    onAddToCart: (id: number) => void 
  } = $props();
</script>

<section class="bg-surface-container-lowest border border-outline-variant rounded-xl shadow-sm flex flex-col overflow-hidden h-[500px]">
  <div class="p-lg border-b border-outline-variant bg-surface-container-low/30 flex-shrink-0">
    <div class="flex flex-col sm:flex-row justify-between items-start sm:items-center gap-sm">
      <h3 class="font-title-lg text-title-lg flex items-center gap-2">
        <span class="material-symbols-outlined text-primary">category</span>
        Product Catalog
      </h3>
    </div>
  </div>
  <div class="flex-1 overflow-y-auto custom-scrollbar p-lg">
    <div class="grid grid-cols-1 sm:grid-cols-2 gap-md">
      {#each products as product (product.id)}
        <div class="flex gap-md p-md border border-outline-variant rounded-xl hover:shadow-md transition-shadow group relative bg-surface-container-lowest {product.inStock ? '' : 'opacity-60'}">
          <div class="w-20 sm:w-24 h-20 sm:h-24 rounded-lg bg-surface-container-high overflow-hidden border border-outline-variant/30 flex-shrink-0 {product.inStock ? '' : 'grayscale'}">
            <img class="w-full h-full object-cover" src={product.image} alt={product.name} />
          </div>
          <div class="flex-1 flex flex-col justify-between">
            <div>
              <span class="font-label-sm text-label-sm {product.inStock ? 'text-on-surface-variant bg-surface-container-high' : 'text-error bg-error-container/20'} px-1.5 rounded uppercase">{product.inStock ? product.category : 'Out of Stock'}</span>
              <h4 class="font-body-lg text-body-lg font-bold mt-1">{product.name}</h4>
              <p class="font-label-md text-label-md text-on-surface-variant">SKU: {product.sku}</p>
            </div>
            <div class="flex justify-between items-end">
              <span class="{product.inStock ? 'text-primary' : 'text-on-surface-variant'} font-bold font-title-lg">${product.price.toFixed(2)} <small class="text-[10px] text-on-surface-variant">/ unit</small></span>
              {#if product.inStock}
                <button 
                  class="p-1.5 bg-primary-container text-on-primary-container rounded-lg hover:bg-primary hover:text-on-primary transition-all"
                  onclick={() => onAddToCart(product.id)}
                >
                  <span class="material-symbols-outlined">add_shopping_cart</span>
                </button>
              {:else}
                <button class="p-1.5 bg-surface-container-high text-outline rounded-lg cursor-not-allowed" disabled>
                  <span class="material-symbols-outlined">block</span>
                </button>
              {/if}
            </div>
          </div>
        </div>
      {/each}
    </div>
  </div>
</section>
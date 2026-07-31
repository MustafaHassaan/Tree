<!-- src/routes/dashboard/orders/components/OrderSummary.svelte -->
<script lang="ts">
  type CartItem = {
    productId: number;
    name: string;
    price: number;
    quantity: number;
  };

  let {
    cartItems = $bindable(),
    discount = 15.00,
    onUpdateQuantity,
    onConfirm
  }: {
    cartItems: CartItem[],
    discount?: number,
    onUpdateQuantity: (productId: number, delta: number) => void,
    onConfirm: () => void
  } = $props();

  let subtotal = $derived(cartItems.reduce((sum, item) => sum + (item.price * item.quantity), 0));
  let tax = $derived(subtotal * 0.08);
  let total = $derived(subtotal + tax - discount);
</script>

<section class="bg-surface-container-lowest border border-outline-variant rounded-xl shadow-lg flex flex-col h-full overflow-hidden">
  <div class="p-lg bg-primary-container text-on-primary-container">
    <h3 class="font-title-lg text-title-lg flex items-center gap-2">
      <span class="material-symbols-outlined">shopping_basket</span>
      Order Summary
    </h3>
    <p class="font-label-md text-label-md opacity-80">Reference: ORD-2024-8832</p>
  </div>
  
  <div class="flex-1 overflow-y-auto custom-scrollbar p-lg space-y-md">
    {#each cartItems as item (item.productId)}
      <div class="flex gap-md py-md border-b border-outline-variant/50">
        <div class="flex-1">
          <h5 class="font-body-md text-body-md font-bold">{item.name}</h5>
          <p class="font-label-sm text-label-sm text-on-surface-variant">Unit: ${item.price.toFixed(2)}</p>
        </div>
        <div class="flex items-center gap-2 sm:gap-3">
          <div class="flex items-center border border-outline-variant rounded-lg overflow-hidden h-9">
            <button class="px-2 hover:bg-surface-container text-on-surface-variant" onclick={() => onUpdateQuantity(item.productId, -1)}>
              <span class="material-symbols-outlined text-[16px]">remove</span>
            </button>
            <input class="w-10 text-center border-none focus:ring-0 text-label-md bg-transparent" type="text" value={item.quantity} readonly />
            <button class="px-2 hover:bg-surface-container text-on-surface-variant" onclick={() => onUpdateQuantity(item.productId, 1)}>
              <span class="material-symbols-outlined text-[16px]">add</span>
            </button>
          </div>
          <span class="font-body-md text-body-md font-bold w-16 sm:w-20 text-right">${(item.price * item.quantity).toFixed(2)}</span>
        </div>
      </div>
    {/each}
    {#if cartItems.length === 0}
      <div class="py-lg text-center opacity-20 border-2 border-dashed border-outline-variant rounded-xl">
        <span class="material-symbols-outlined text-[48px]">playlist_add</span>
        <p class="text-label-md">Add more items to this order</p>
      </div>
    {/if}
  </div>

  <div class="p-lg bg-surface-container-low/50 border-t border-outline-variant space-y-sm">
    <div class="flex justify-between text-body-md">
      <span class="text-on-surface-variant">Subtotal</span>
      <span class="font-medium">${subtotal.toFixed(2)}</span>
    </div>
    <div class="flex justify-between text-body-md">
      <span class="text-on-surface-variant">Estimated Tax (8%)</span>
      <span class="font-medium">${tax.toFixed(2)}</span>
    </div>
    <div class="flex justify-between text-body-md">
      <span class="text-on-surface-variant">Wholesale Discount</span>
      <span class="text-tertiary-container font-medium">-${discount.toFixed(2)}</span>
    </div>
    <div class="pt-sm border-t border-outline-variant flex justify-between items-center">
      <span class="font-title-lg text-title-lg">Total Amount</span>
      <span class="font-headline-lg text-headline-lg text-primary">${total.toFixed(2)}</span>
    </div>
  </div>

  <div class="p-lg">
    <button
      onclick={onConfirm}
      disabled={cartItems.length === 0}
      class="w-full bg-primary text-on-primary py-4 rounded-xl font-bold text-lg hover:bg-primary/90 shadow-lg active:scale-[0.98] transition-all flex items-center justify-center gap-2 disabled:opacity-50 disabled:cursor-not-allowed"
      type="button"
    >
      <span class="material-symbols-outlined">send_and_archive</span>
      Confirm Order
    </button>
  </div>
</section>
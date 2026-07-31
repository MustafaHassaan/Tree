<script lang="ts">
  // Revenue by Segment Donut Chart
</script>

<div class="col-span-1 lg:col-span-4 bg-white border border-outline-variant rounded-xl p-lg shadow-sm hover:shadow-md transition-shadow">
  <h3 class="font-title-lg text-title-lg text-on-surface mb-xs">Revenue by Segment</h3>
  <p class="font-label-md text-label-md text-on-surface-variant mb-lg">Breakdown of total volume</p>
  <div class="flex justify-center mb-lg relative">
    <svg class="w-48 h-48 transform -rotate-90">
      <circle cx="96" cy="96" fill="transparent" r="70" stroke="#eff6ff" stroke-width="24"></circle>
      <circle cx="96" cy="96" fill="transparent" r="70" stroke="#2563EB" stroke-dasharray="440" stroke-dashoffset="120" stroke-width="24"></circle>
      <circle cx="96" cy="96" fill="transparent" r="70" stroke="#515f74" stroke-dasharray="440" stroke-dashoffset="350" stroke-width="24"></circle>
      <circle cx="96" cy="96" fill="transparent" r="70" stroke="#bc4800" stroke-dasharray="440" stroke-dashoffset="410" stroke-width="24"></circle>
    </svg>
    <div class="absolute inset-0 flex flex-col items-center justify-center">
      <span class="font-headline-md text-headline-md font-black">$4.2M</span>
      <span class="font-label-sm text-label-sm text-on-surface-variant">Total Vol.</span>
    </div>
  </div>
  <div class="space-y-sm">
    <div class="flex justify-between items-center text-body-md">
      <div class="flex items-center gap-sm">
        <span class="w-2 h-2 rounded-full bg-primary"></span>
        <span>Electronics</span>
      </div>
      <span class="font-bold">64%</span>
    </div>
    <div class="flex justify-between items-center text-body-md">
      <div class="flex items-center gap-sm">
        <span class="w-2 h-2 rounded-full bg-secondary"></span>
        <span>Home Appliances</span>
      </div>
      <span class="font-bold">22%</span>
    </div>
    <div class="flex justify-between items-center text-body-md">
      <div class="flex items-center gap-sm">
        <span class="w-2 h-2 rounded-full bg-tertiary-container"></span>
        <span>Accessories</span>
      </div>
      <span class="font-bold">14%</span>
    </div>
  </div>
</div>
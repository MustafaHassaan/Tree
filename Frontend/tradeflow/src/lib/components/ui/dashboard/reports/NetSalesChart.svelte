<script lang="ts">
  // Net Sales Revenue Line Chart Component
</script>

<div class="col-span-1 lg:col-span-8 bg-white border border-outline-variant rounded-xl p-lg shadow-sm hover:shadow-md transition-shadow">
  <div class="flex justify-between items-start mb-xl">
    <div>
      <h3 class="font-title-lg text-title-lg text-on-surface">Net Sales Revenue</h3>
      <p class="font-label-md text-label-md text-on-surface-variant">Daily performance compared to previous month</p>
    </div>
    <div class="flex items-center gap-md">
      <div class="flex items-center gap-sm">
        <span class="w-3 h-3 bg-primary rounded-full"></span>
        <span class="text-label-sm font-medium">Current</span>
      </div>
      <div class="flex items-center gap-sm">
        <span class="w-3 h-3 bg-secondary-fixed-dim rounded-full"></span>
        <span class="text-label-sm font-medium">Previous</span>
      </div>
    </div>
  </div>
  <div class="h-64 flex items-end gap-2 px-md">
    <svg class="w-full h-full" preserveAspectRatio="none" viewBox="0 0 1000 250">
      <path d="M0,200 Q100,180 200,210 T400,150 T600,100 T800,120 T1000,80" fill="none" stroke="#2563EB" stroke-linecap="round" stroke-width="3"></path>
      <path d="M0,220 Q100,230 200,200 T400,190 T600,180 T800,160 T1000,150" fill="none" stroke="#cbd5e1" stroke-dasharray="6,4" stroke-width="2"></path>
      <!-- Grid lines -->
      <line stroke="#f1f5f9" stroke-width="1" x1="0" x2="1000" y1="50" y2="50"></line>
      <line stroke="#f1f5f9" stroke-width="1" x1="0" x2="1000" y1="100" y2="100"></line>
      <line stroke="#f1f5f9" stroke-width="1" x1="0" x2="1000" y1="150" y2="150"></line>
      <line stroke="#f1f5f9" stroke-width="1" x1="0" x2="1000" y1="200" y2="200"></line>
    </svg>
  </div>
  <div class="flex justify-between mt-md px-md text-label-sm text-on-surface-variant font-medium">
    <span>Oct 01</span>
    <span>Oct 07</span>
    <span>Oct 14</span>
    <span>Oct 21</span>
    <span>Oct 28</span>
    <span>Oct 31</span>
  </div>
</div>
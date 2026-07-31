import adapter from '@sveltejs/adapter-vercel'; // أو adapter-static / adapter-auto حسب بيئة التشغيل
import { vitePreprocess } from '@sveltejs/vite-plugin-svelte';

/** @type {import('@sveltejs/kit').Config} */
const config = {
	preprocess: vitePreprocess(),
	compilerOptions: {
		// تفعيل الـ Runes للمشروع وتجاهل node_modules
		runes: ({ filename }) =>
			filename.split(/[/\\]/).includes('node_modules') ? undefined : true
	},
	kit: {
		adapter: adapter()
	}
};

export default config;
import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

export default defineConfig({
  plugins: [react()],
  server: {
    // Must match the SPA redirect URI registered in Entra (http://localhost:5173/redirect.html).
    port: 5173,
    strictPort: true,
  },
  build: {
    rollupOptions: {
      input: {
        main: "index.html",
        redirect: "redirect.html",
      },
    },
  },
});

import { createRouter, createWebHistory } from "vue-router";
import GameJoinView from "@/components/views/GameJoinView.vue";
import ModeratorJoinView from "@/components/views/ModeratorJoinView.vue";
import PrivacyView from "@/components/views/PrivacyView.vue";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    { path: '/', component: GameJoinView },
    { path: '/moderator', component: ModeratorJoinView },
    { path: '/privacy', component: PrivacyView }
  ],
})

export default router;

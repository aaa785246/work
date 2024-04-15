<script setup lang="ts">
import "@/assets/article.css";
import { ref, onMounted } from "vue";
import { getArticleList, getMessageList } from "@/js/api";
import articleContent from "./articleContent.vue";
import messageContent from "./messageContent.vue";

// onMounted(async () => {

// });
</script>

<template>
  <RouterLink to="/">
    <img
      src="@/img/back.png"
      className="ar-back"
      title="back"
      alt="this is back"
    />
  </RouterLink>
  <!-- 文章內容 -->
  <articleContent :articleNumber="3" />
  <!-- 留言內容 -->
  <messageContent />
</template>

<!-- <style scoped></style> -->

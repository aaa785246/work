<script setup lang="ts">
import "@/assets/article.css";
import {  ref,onMounted } from "vue";
import articleContent from "@/components/articleContent.vue";
import messageContent from "@/components/messageContent.vue";



</script>

<template>
  <RouterLink to="/shareExp">
    <img
      src="@/img/back.png"
      className="ar-back"
      title="back"
      alt="this is back"
    />
  </RouterLink>
  <!-- 文章內容 -->
  <articleContent />
  <!-- 留言內容 -->
  <messageContent />
</template>

<!-- <style scoped></style> -->

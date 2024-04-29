<script setup lang="ts">
import "@/assets/article.css";
import bookmarkImg from "@/img/bookmark.png";
import bookmarkImgOn from "@/img/bookmarkon.png";
import { ref, onMounted, computed } from "vue";
import { useArticleStore } from '@/stores/article'

const store = useArticleStore();
const title =ref("");
const userName = ref("")
const content = ref("")
const likeCount = ref("");

onMounted(async () => {
 title.value = store.article_title
 userName.value = store.userName
 content.value = store.article_content
 likeCount.value = store.like_count
 if(store.article_title == null){
  
 }
});


// 開啟書籤換圖片
const bookMarkImgSrc = ref(`${bookmarkImg}`);
// 開啟書籤調整class
const bookMarkClass = ref("ar-bookmark");
//開啟書籤調整分享按鈕
const share = ref("ar-share");
//開啟關閉開關
const myMarkToggle = ref(false);
const toggleMark = () => {
  myMarkToggle.value = !myMarkToggle.value;
  bookMarkImgSrc.value = myMarkToggle.value ? bookmarkImgOn : bookmarkImg;
  bookMarkClass.value = myMarkToggle.value ? "ar-bookmarkOn" : "ar-bookmark";
  share.value = myMarkToggle.value ? "ar-shareOn" : "ar-share";
};
</script>

<template>
  <div>
    <div class="article-title">
      {{ title }}
    </div>
    <div class="article-user">{{ userName }}</div>
    <div class="article-content">{{ content }}</div>

    <div class="ar-heartAndMsg-container">
      <img src="@/img/heart.png" alt="heart" class="ar-heart" />
      <div class="ar-heartCount">{{ likeCount }}</div>
      <img
        :src="bookMarkImgSrc"
        alt="bookmark"
        :class="bookMarkClass"
        @click="toggleMark"
      />
      <img src="@/img/share.png" alt="share" :class="share" />
    </div>
    <div class="ar-allMsg">所有留言</div>@/stores/article
  </div>
</template>

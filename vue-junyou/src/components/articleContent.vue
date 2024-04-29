<script setup lang="ts">
import "@/assets/article.css";
import bookmarkImg from "@/img/bookmark.png";
import bookmarkImgOn from "@/img/bookmarkon.png";
import { ref, onMounted, computed } from "vue";
import { useArticleStore } from '@/stores/article'
const store = useArticleStore();


onMounted(async () => {

 if(store.article_title == ""){
 console.log("抓不到pinia")
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
      {{ store.article_title }}
    </div>
    <div class="article-user">{{ store.userName }}</div>
    <div class="article-content">{{ store.article_content}}</div>

    <div class="ar-heartAndMsg-container">
      <img src="@/img/heart.png" alt="heart" class="ar-heart" />
      <div class="ar-heartCount">{{ store.like_count }}</div>
      <img
        :src="bookMarkImgSrc"
        alt="bookmark"
        :class="bookMarkClass"
        @click="toggleMark"
      />
      <img src="@/img/share.png" alt="share" :class="share" />
    </div>
    <div class="ar-allMsg">所有留言</div>
  </div>
</template>

<script setup lang="ts">
import "@/assets/article.css";
import { ref, watch } from "vue";


//標題、發文者、內容
const title = ref("前端工程師面試阿阿阿阿阿阿阿");
const user = ref("羅志祥");
const content = ref(
  "至於為什麼要思考面試呢？其實是有更深層的原因，面試的發生，到底需要如何做到，不面試的發生，又會如何產生。你真的了解面試嗎？奧斯特洛夫斯基曾經說過，共同的事業，共同的鬥爭，可以使人們產生忍受一切的力量。　這似乎非常的有道理，對吧？我們一般認為，抓住了問題的關鍵，其他一切則會迎刃而解。就我個人來說，面試對我的意義，不能不說非常重大。"
);
//留言者、留言內容
const messageName = ref("小火龍");
const msg = ref(
  "最近去了很多家軟工面試，這家是我目前面試下來最有趣的，一開始一樣是正常的自我介紹與一些面試官的問題，在自我介紹與一些簡單的問題後"
);
//愛心數
const heartCount = ref(84);
// 開啟書籤換圖片
const bookMarkImgSrc = ref(`${import.meta.env.BASE_URL}src/img/bookmark.png`);
// 開啟書籤調整class
const bookMarkClass = ref("ar-bookmark");
//開啟書籤調整分享按鈕
const share = ref("ar-share");
//開啟關閉開關
const myMarkToggle = ref(false);
const toggleMark = () => {
  myMarkToggle.value = !myMarkToggle.value;
  bookMarkImgSrc.value = myMarkToggle.value
    ? `${import.meta.env.BASE_URL}src/img/bookmarkOn.png`
    : `${import.meta.env.BASE_URL}src/img/bookmark.png`;
  bookMarkClass.value = myMarkToggle.value ? "ar-bookmarkOn" : "ar-bookmark";
  share.value = myMarkToggle.value ? "ar-shareOn" : "ar-share";
};
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
  <div class="article-title">{{ title }}</div>
  <div class="article-user">{{ user }}</div>
  <div class="article-content">{{ content }}</div>

  <div class="ar-heartAndMsg-container">
    <img src="@/img/heart.png" alt="heart" class="ar-heart" />
    <div class="ar-heartCount">{{ heartCount }}</div>
    <img
      :src="bookMarkImgSrc"
      alt="bookmark"
      :class="bookMarkClass"
      @click="toggleMark"
    />
    <img src="@/img/share.png" alt="share" :class="share" />
  </div>
  <div class="ar-allMsg">所有留言</div>
  <!-- 到時候用for迴圈跑 第一個框的 class="ar-articleBox"與第二個框之後class="ar-articleBox2"
  margin不同需要額外做處理-->
  <div class="ar-articleBox">
    <div class="ar-messageName">{{ messageName }}</div>
    <div class="ar-content">{{ msg }}</div>
  </div>
  <div class="ar-articleBox2">
    <div class="ar-messageName">{{ messageName }}</div>
    <div class="ar-content">{{ msg }}</div>
  </div>
  <div class="ar-articleBox2">
    <div class="ar-messageName">{{ messageName }}</div>
    <div class="ar-content">{{ msg }}</div>
  </div>
</template>

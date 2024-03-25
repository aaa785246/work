<script setup lang="ts">
import "@/assets/article.css";
import { ref, watch } from "vue";

var title = "測試文章";
var user = "羅志祥";
var content =
  "至於為什麼要思考面試呢？其實是有更深層的原因，面試的發生，到底需要如何做到，不面試的發生，又會如何產生。你真的了解面試嗎？奧斯特洛夫斯基曾經說過，共同的事業，共同的鬥爭，可以使人們產生忍受一切的力量。　這似乎非常的有道理，對吧？我們一般認為，抓住了問題的關鍵，其他一切則會迎刃而解。就我個人來說，面試對我的意義，不能不說非常重大。";
var heartCount = 84;
var bookImgSrc="@/img/heart.png";
const myMarkToggle = ref(false);
const toggleMark = () => {
    myMarkToggle.value !=  myMarkToggle.value
    bookImgSrc = myMarkToggle ? "@/img/bookmarkOn.png" : "@/img/bookmark.png";
}

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
    <img src="@/img/bookmark.png" alt="bookmark" class="ar-bookmark" :class= "myMarkToggle? 'bookMarkOn':'bookMarkOff'"  @click="toggleMark"/>
    <img src="@/img/share.png" alt="share" class="ar-share" />
  </div>
</template>

<script setup lang="ts">
import axios from "axios";
import "@/assets/article.css";
import { ref, onMounted } from "vue";
import articleContent from "@/components/articleContent.vue";
import messageContent from "@/components/messageContent.vue";
import bookmarkImg from "@/img/bookmark.png";
import bookmarkImgOn from "@/img/bookmarkon.png";
import { getCookie } from "@/js/cookie";

const email = ref("");
const userName = ref("");
const article_title = ref("");
const article_content = ref("");
const like_count = ref("");
const message_count = ref("");
const article_number = ref(getCookie("article_number"));

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
const article = async () => {
  const api = `http://192.168.1.200:8000/searcharticlenumber`;
  await axios
    .post(api, {
      articleNumber: parseInt(article_number.value),
    })
    .then((response) => {
      email.value = response.data.email
      userName.value = response.data.username
      article_title.value = response.data.article_title
      article_content.value = response.data.article_content
      like_count.value = response.data.like_count
      message_count.value = response.data.message_count
      console.log(response.data)
    });
};
onMounted(()=>{
  article()
}) 
const test = () =>{
  // console.log(article_number.value)
  console.log(email.value)
}
</script>

<template>
  <RouterLink to="/shareExp">
    <img src="@/img/back.png" className="ar-back" title="back" alt="this is back" />
  </RouterLink>
  <!-- 文章內容 -->
  <div>
    <div class="article-title">
      {{ article_title }}
    </div>
    <div class="article-user">{{ userName }}</div>
    <div class="article-content">{{ article_content }}</div>

    <div class="ar-heartAndMsg-container">
      <img src="@/img/heart.png" alt="heart" class="ar-heart" />
      <div class="ar-heartCount">{{ like_count }}</div>
      <img :src="bookMarkImgSrc" alt="bookmark" :class="bookMarkClass" @click="toggleMark" />
      <img src="@/img/share.png" alt="share" :class="share" />
    </div>
    <div class="ar-allMsg">所有留言</div>
    <button @click="test">測試</button>
  </div>
  <!-- 留言內容 -->
  <messageContent />
</template>

<!-- <style scoped></style> -->

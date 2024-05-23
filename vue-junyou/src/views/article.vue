<script setup lang="ts">
import axios from "axios";
import "@/assets/article.css";
import { ref, onMounted } from "vue";
import messageContent from "@/components/messageContent.vue";
import bookmarkImg from "@/img/bookmark.png";
import bookmarkImgOn from "@/img/bookmarkon.png";
import { getCookie } from "@/js/cookie";
import { type message } from "@/js/api";
const email = ref("");
const userName = ref("");
const article_title = ref("");
const article_content = ref("");
const like_count = ref("");
const message_count = ref("");
const article_number = ref(getCookie("article_number"));
const messageFloor = ref<message[] | undefined>();
// 開啟書籤換圖片
const bookMarkImgSrc = ref(`${bookmarkImg}`);
// 開啟書籤調整class
const bookMarkClass = ref("ar-bookmark");
//開啟書籤調整分享按鈕
// const share = ref("ar-share");
//開啟關閉開關
const myMarkToggle = ref(false);
const toggleMark = () => {
  myMarkToggle.value = !myMarkToggle.value;
  bookMarkImgSrc.value = myMarkToggle.value ? bookmarkImgOn : bookmarkImg;
  bookMarkClass.value = myMarkToggle.value ? "ar-bookmarkOn" : "ar-bookmark";
  // share.value = myMarkToggle.value ? "ar-shareOn" : "ar-share";
};
//抓取文章API
const getArticle = async () => {
  const api = `http://192.168.1.203:8000/searcharticlenumber`;
  await axios
    .post(api, {
      articleNumber: parseInt(article_number.value),
    })
    .then((response) => {
      email.value = response.data[0].email
      userName.value = response.data[0].username
      article_title.value = response.data[0].article_title
      article_content.value = response.data[0].article_content
      like_count.value = response.data[0].like_count
      message_count.value = response.data[0].message_count
      // console.log(response.data[0].email)
      // console.log(response.data[0])
    });
};

//抓取留言API
const getMessage = async () => {
  const api = `http://192.168.1.203:8000/articlemessage`;
  await axios
    .post(api, {
      articleNumber: parseInt(article_number.value),
    })
    .then((response) => {
      messageFloor.value = response.data;
      console.log(messageFloor.value)
    });
};
onMounted(() => {
  getArticle();
  getMessage();
})
//留言API
const inputMessage = async () => {
  const api = `http://192.168.1.203:8000/searcharticlenumber`;
  await axios
    .post(api, {
      articleNumber: parseInt(article_number.value),
    })
    .then((response) => {

    });
};
const test = () => {
  // console.log(messageFloor.value)
}
const isLike = ref(true);
const likeOn = ref("/src/img/heartempty.png")
const likely = () => {
  console.log(isLike.value)
  isLike.value = !isLike.value;
  likeOn.value = isLike.value ? "/src/img/heartempty.png" : "/src/img/heart.png";
  like_count.value = isLike.value ? String(parseInt(like_count.value) - 1) : String(parseInt(like_count.value) + 1)
  if (isLike.value) {
    const likeCount = async () => {
      const api = `http://192.168.1.203:8000/newarticle`;
      await axios
        .post(api, {

        })
        .then((response) => {

        })
        .catch(() => {

        });
    };
  }
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
      <img :src="likeOn" class="ar-heart" @click="likely" />
      <div class="ar-heartCount">{{ like_count }}</div>
      <img :src="bookMarkImgSrc" alt="bookmark" :class="bookMarkClass" @click="toggleMark" />
      <!-- <img src="@/img/share.png" alt="share  " :class="share" /> -->
    </div>
    <div class="ar-allMsg">所有留言</div>
    <!-- <button @click="test">測試</button> -->
  </div>
  <!-- 留言內容 -->
  <div class="ar-articleBox" v-for="(message, index) in messageFloor">
    <div class="ar-messageName">{{ message.username }}</div>
    <div class="ar-content">{{ message.msg_content }}</div>
    <div class="ar-floor">{{ message.msg_floor }}樓</div>
  </div>
  <div class="ar-articleBox">
    <input type="text">
  </div>




  <!-- <messageContent /> -->
</template>

<!-- <style scoped></style> -->

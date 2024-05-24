<script setup lang="ts">
import "@/assets/shareExp.css";
import { onMounted, ref } from "vue";
import axios from "axios";
import { type articles } from "@/js/api";
import { useArticleStore } from '@/stores/article'
import { useRouter } from "vue-router";
import { getCookie, setCookie } from "@/js/cookie";
const router = useRouter();
const store = useArticleStore()
//標題、內容
const articleText = ref<articles[] | undefined>();
const search = ref("");

const article = async () => {
  // const api = `http://192.168.1.203:8000/articlesearch`;
  const api = `http://192.168.1.200:8000/articlesearch`;
  await axios
    .post(api, {
      article_name: search.value,
    })
    .then((response) => {
      articleText.value = response.data;
      // console.log(articleText.value)
      //標題或內容太長做裁減
      if (articleText.value != undefined) {
        for (const item of articleText.value) {
          if (item.article_content.length > 30) {
            item.cut = item.article_content.substring(0, 20) + "...";
          } else {
            item.cut = item.article_content;
          }
        }
      }
    });
};
onMounted(() => {
  article();
});
const returnPage = (index: number) => {
  if (articleText.value != undefined) {
    setCookie("article_number",articleText.value[index].article_number.toLocaleString(),10) 
    // setCookie("email",articleText.value[index].email,10)
    // setCookie("username",articleText.value[index].username,10) 
    // setCookie("article_title",articleText.value[index].article_title,10) 
    // setCookie("article_content",articleText.value[index].article_content,10) 
    // setCookie("like_count",articleText.value[index].like_count,10) 
    // setCookie("message_count",articleText.value[index].message_count,10) 
  }
  router.push(`/article/${getCookie("article_number")}`)
};
</script>

<template>
  <!-- 回上一頁 -->
  <RouterLink to="/">
    <img
      src="@/img/back.png"
      className="sh-back"
      title="back"
      alt="this is back"
    />
  </RouterLink>
  <div class="sh-middle">
    <!-- 搜尋框 -->
    <input type="text" placeholder="請輸入職業" class="sh-searchBox" />
    <!-- 搜尋框查詢按鈕 -->
    <img
      src="@/img/magnifying-glass.png"
      title="searchBoxGlass"
      alt="this is searchBoxGlass"
      class="sh-searchBoxGlass"
    />
  </div>
  <!-- 到時候用for迴圈跑 *要注意第一個框是 class="articleBox"與第二個框class="articleBox2" 
    margin設定不同 要額外處理-->
  <div
    v-for="(item, index) in articleText"
    :key="index"
    @click="returnPage(index)"
  >
    <div class="articleBox">
      <div class="sh-title">{{ item.article_title }}</div>
      <div class="sh-content">{{ item.cut }}</div>
      <div class="heartAndMsg-container">
        <img src="@/img/heart.png" alt="heart" class="heart" />
        <div class="heartAndMsgCount">{{ item.like_count }}</div>
        <img src="@/img/message2.png" alt="message" class="message" />
        <div class="heartAndMsgCount">{{ item.message_count }}</div>
      </div>
    </div>
  </div>
</template>
@/stores/article
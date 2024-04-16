<script setup lang="ts">
import "@/assets/article.css";
import bookmarkImg from "@/img/bookmark.png";
import bookmarkImgOn from "@/img/bookmarkon.png";
import { ref, onMounted, computed } from "vue";
import { getArticleList } from "@/js/api";
import { defineProps } from "vue";
import router from "@/router";

//傳入文章編號
const props = defineProps<{
  articleNumber: number;
}>();

// const articleIndex = computed<number>(() => props.articleNumber - 1);
const article = ref<Article>();
onMounted(async () => {
  const articleData: Article[] = await getArticleList();
  // if (api有error)

  const articleTmp = articleData.find(
    (v) => v.articleNumber === props.articleNumber
  );

  if (articleTmp) {
    article.value = articleTmp;
  } else {
    router.push("/error");
  }

  heartCount.value = getRandomNumber(0, 300);
});

//給api資料的陣列數字-1 articleNumber
// const an = ref(0);
function getRandomNumber(min: number, max: number) {
  return Math.floor(Math.random() * (max - min + 1)) + min;
}

//內文愛心數
const heartCount = ref(0);
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

//定義article物件告訴TS
interface Article {
  userName: string;
  title: string;
  completed: string;
  articleNumber: number;
}

//給予初始值避免API資料還沒抵達前渲染造成的問題
// const articleList = ref<Article[]>([
//   { userName: "", title: "", completed: "", articleNumber: 0 },
// ]);
// onMounted(async () => {
//   //先抓所有文章的api再判斷需要哪個並讀取該文章
//   const articleData = await getArticleList();
//   articleList.value = articleData;
//   if (
//     props.articleNumber ==
//     articleList.value[props.articleNumber - 1].articleNumber
//   ) {
//     an.value = props.articleNumber - 1;
//     console.log("測試");
//   } else {
//     router.push("/error");
//   }

//   heartCount.value = getRandomNumber(0, 300);
// });
</script>

<template>
  <div>
    <div class="article-title">
      {{ article?.title }}
    </div>
    <div class="article-user">{{ article?.userName }}</div>
    <div class="article-content">{{ article?.completed }}</div>

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
  </div>
</template>

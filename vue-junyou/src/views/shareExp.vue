<script setup lang="ts">
import "@/assets/shareExp.css";
import { ref } from 'vue';

//標題、內容
const artic = ref("前端工程師面試阿阿阿阿阿阿阿");
const msg = ref("最近去了很多家軟工面試，這家是我目前面試下來最有趣的，一開始一樣是正常的自我介紹與一些面試官的問題，在自我介紹與一些簡單的問題後");
//標題或內容太長做裁減
if (msg.value.length > 30) {
  msg.value = msg.value.substring(0, 30) + "....";
}
if (artic.value.length > 7) {
  artic.value = artic.value.substring(0, 7)+ "..";
}
//愛心數
var heartCount = ref(80) ;
//留言數
const msgCount = ref(8);
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
  <div>
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
  <RouterLink to="/article">
    <div class="articleBox">
    <div class="sh-title">{{ artic }}</div>
    <div class="sh-content">{{ msg }}</div>
    <div class="heartAndMsg-container">
      <img src="@/img/heart.png" alt="heart" class="heart" />
      <div class="heartAndMsgCount">{{ heartCount }}</div>
      <img src="@/img/message2.png" alt="message" class="message" />
      <div class="heartAndMsgCount">{{ msgCount }}</div>
    </div>
  </div>
  </RouterLink>
 
  <RouterLink to="/article">
    <div class="articleBox2">
    <div class="sh-title">{{ artic }}</div>
    <div class="sh-content">{{ msg }}</div>
    <div class="heartAndMsg-container">
      <img src="@/img/heart.png" alt="heart" class="heart" />
      <div class="heartAndMsgCount">{{ heartCount }}</div>
      <img src="@/img/message2.png" alt="heart" class="message" />
      <div class="heartAndMsgCount">{{ msgCount }}</div>
    </div>
  </div>
  </RouterLink>
  

  <RouterLink to="/article">
    <div class="articleBox2">
    <div class="sh-title">{{ artic }}</div>
    <div class="sh-content">{{ msg }}</div>
    <div class="heartAndMsg-container">
      <img src="@/img/heart.png" alt="heart" class="heart" />
      <div class="heartAndMsgCount">{{ heartCount }}</div>
      <img src="@/img/message2.png" alt="heart" class="message" />
      <div class="heartAndMsgCount">{{ msgCount }}</div>
    </div>
    </div>
  </RouterLink>
</template>

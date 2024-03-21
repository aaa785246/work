<script setup lang="ts">
import "@/assets/shareExp.css";
var artic = "前端工程師面試阿阿阿阿阿阿阿";
var msg =
  "最近去了很多家軟工面試，這家是我目前面試下來最有趣的，一開始一樣是正常的自我介紹與一些面試官的問題，在自我介紹與一些簡單的問題後";
if (msg.length > 30) {
  msg = msg.substring(0, 60) + "....";
}
if (artic.length > 7) {
  artic = artic.substring(0, 7) + "....";
}
var heartCount = "80";
</script>

<template>
  <!-- 回上一頁 -->
  <RouterLink to="/">
    <img
      src="@/img/back.png"
      className="back"
      title="back"
      alt="this is back"
    />
  </RouterLink>
  <div>
    <!-- 搜尋框 -->
    <input type="text" placeholder="請輸入職業" class="sh-searchBoxOn" />
    <!-- 搜尋框查詢按鈕 -->
    <img
      src="@/img/magnifying-glass.png"
      title="searchBoxGlass"
      alt="this is searchBoxGlass"
      class="sh-searchBoxGlassOn"
    />
  </div>
  <div class="articleBox">
    <div class="sh-title">{{ artic }}</div>
    <div class="sh-content">{{ msg }}</div>
    <div class="heartAndMsg-container">
      <img src="@/img/heart.png" alt="heart" class="heart" />
      <div class="heartCount">{{ heartCount }}</div>
      <img src="@/img/message2.png" alt="heart" class="message" />
      <div class="heartCount">{{ heartCount }}</div>
    </div>
  </div>

  <div class="articleBox2">
    <div class="sh-title">{{ artic }}</div>
    <div class="sh-content">{{ msg }}</div>
    <div class="heartAndMsg-container">
      <img src="@/img/heart.png" alt="heart" class="heart" />
      <div class="heartCount">{{ heartCount }}</div>
      <img src="@/img/message2.png" alt="heart" class="message" />
      <div class="heartCount">{{ heartCount }}</div>
    </div>
  </div>

  <div class="articleBox2">
    <div class="sh-title">{{ artic }}</div>
    <div class="sh-content">{{ msg }}</div>
    <div class="heartAndMsg-container">
      <img src="@/img/heart.png" alt="heart" class="heart" />
      <div class="heartCount">{{ heartCount }}</div>
      <img src="@/img/message2.png" alt="heart" class="message" />
      <div class="heartCount">{{ heartCount }}</div>
    </div>
  </div>
</template>

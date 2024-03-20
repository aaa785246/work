<script setup lang="ts">
import "@/assets/homepage.css";
import "@/assets/animate.css";
</script>

<template>
  <!-- <div className="header">
      </div> -->
  <img src="@/img/logo_75.jpg" id="logo" title="logo" alt="this is logo" />
  <img
    src="@/img/magnifying-glass.png"
    id="search"
    title="search"
    alt="this is search"
  />
  <img src="@/img/menu.png" id="menu" title="menu" alt="this is menu" />

  <div class="titleSet">
    <div className="titleBox">
      <div className="title ">輕鬆獲取</div>
      <div className="title2">所需要的面試資訊</div>
      <div className="subTitle">我們提供了什麼服務?</div>
    </div>
  </div>
  <div className="workingImg">
    <img
      src="@/img/work.jpg"
      className="work"
      title="work"
      alt="this is work"
    />
    <div id="mask"><p>面試心得分享</p></div>
  </div>
  <div className="workingImg">
    <img
      src="@/img/working.jpg"
      className="work"
      title="work"
      alt="this is work"
    />
    <div id="mask"><p>敬請期待</p></div>
  </div>
</template>

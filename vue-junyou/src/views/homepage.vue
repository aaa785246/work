<script setup lang="ts">
import "@/assets/homepage.css";
import "@/assets/animate.css";
import { ref, watch } from "vue";
//控制菜單
const myMenuToggle = ref(false);
const toggleMenu = () => {
  myMenuToggle.value = !myMenuToggle.value;
};
//控制搜尋
const mySearchToggle = ref(false);
const toggleSearch = () => {
  mySearchToggle.value = !mySearchToggle.value;
};



</script>

<template>
 
  <div class="navBar">
    <img src="@/img/logo_75.jpg" id="logo" title="logo" alt="this is logo" />
    <img src="@/img/magnifying-glass.png" id="search" title="search" alt="this is search" @click="toggleSearch"/>
    <img src="@/img/menu.png" id="menu" title="menu" alt="this is menu" @click="toggleMenu"/>
  </div>
  <!-- 搜尋開關 -->
  <div :class="mySearchToggle ? 'maskdivOn' : 'maskdivOff'">
  <!-- 遮罩 -->
  <div :class="mySearchToggle ? 'menuMaskOn' : 'menuMaskOff'" @click="toggleSearch"></div>
  <!-- 搜尋框 -->
  <input type="text" :class="mySearchToggle ? 'searchBoxOn' : 'searchBoxOff'" placeholder="請輸入職業">
  <!-- 搜尋框查詢按鈕 -->
  <RouterLink to="/shareExp" ><img src="@/img/magnifying-glass.png"  title="searchBoxGlass" alt="this is searchBoxGlass" :class="mySearchToggle ? 'searchBoxGlassOn' : 'searchBoxGlassOff'"/></RouterLink>
  
  </div>
  <!-- 菜單開關 -->
  <div :class="myMenuToggle ? 'maskdivOn' : 'maskdivOff'">
    <!-- 遮罩 -->
    <div :class="myMenuToggle ? 'menuMaskOn' : 'menuMaskOff'" @click="toggleMenu"></div>
    <!-- 菜單 -->
    <div  :class="myMenuToggle ? 'menuOn' : 'menuOff'"></div>
    <!-- 內容 -->
    <RouterLink to="/shareExp" :class="myMenuToggle ? 'menu-textOn' : 'menu-textOff'">
      <div>面試心得分享</div>
    </RouterLink>

    <RouterLink to="/login" :class="myMenuToggle ? 'menu-text2On' : 'menu-textOff'" > <div >會員中心</div></RouterLink>
    <!-- 關閉鈕 -->
    <img src="@/img/close.png" id="close" title="close" alt="this is close" @click="toggleMenu" :class="myMenuToggle ? 'closeButtonOn' : 'closeButtonOff'"/>
  </div>
  

  <div>
    <div className="title">輕鬆獲取</div>
    <div className="title2">所需要的面試資訊</div>
    <div className="subTitle">我們提供了什麼服務?</div>
  </div>

  <div className="center">
    <div className="workingImg">
      <img
        src="@/img/work.jpg"
        className="work"
        title="work"
        alt="this is work"
      />
      <RouterLink to="/shareExp"> <div id="mask"><p>面試心得分享</p></div></RouterLink>
    </div>
  </div>

  <div className="center">
    <div className="workingImg">
      <img
        src="@/img/working.jpg"
        className="work"
        title="work"
        alt="this is work"
      />
      <div id="mask"><p>敬請期待</p></div>
    </div>
  </div>
</template>



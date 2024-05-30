<script setup lang="ts">
import "@/assets/homepage.css";
import "@/assets/animate.css";
import { ref, watch } from "vue";
import { getCookie, setCookie,deleteCookie } from "@/js/cookie";
import { useRouter } from "vue-router";

const router = useRouter();
//如果他有在瀏覽網站並且有登入就保存它的登入狀態
const loginState = ref(getCookie("loginState"));
const userName = ref(getCookie("userName"));
const userEmail = ref(getCookie("userEmail"))
if (loginState.value == "true") {
  setCookie("loginState", loginState.value, 20);
  setCookie("userEmail",userEmail.value,60);
}
//控制菜單
const myMenuToggle = ref(false);
const toggleMenu = () => {
  myMenuToggle.value = !myMenuToggle.value;
};
//控制搜尋
const search = ref("");
const mySearchToggle = ref(false);
const toggleSearch = () => {
  mySearchToggle.value = !mySearchToggle.value;
};

const goShareExp = () => {
  router.push("/shareExp")
  setCookie("searchContent",search.value,10);
}
const goLogin = () => {
  router.push("/login")
  setCookie("arrivedPage","/member",10)
  if (loginState.value == "true") {
    router.push("/member")
  }
}
const postNeedLogin = () => {
  router.push("/login")
  setCookie("arrivedPage","/post",10)
  if (loginState.value == "true") {
    router.push("/post")
  }
}
const signOut = () => {
  deleteCookie("loginState");
  deleteCookie("userEmail");
  deleteCookie("userName");
  location.reload();
}
</script>

<template>
  <div class="homePage">
    <div class="navBar">
      <img src="@/img/logo_75.jpg" id="logo" title="logo" alt="this is logo" />
      <img src="@/img/magnifying-glass.png" id="search" title="search" alt="this is search" @click="toggleSearch" />
      <img src="@/img/menu.png" id="menu" title="menu" alt="this is menu" @click="toggleMenu" />
    </div>

    <!-- 遮罩 -->
    <div :class="mySearchToggle ? 'maskSearchOn' : 'maskSearchOff'" @click="toggleSearch"></div>
    <!-- 搜尋框 -->
    <input type="text" :class="mySearchToggle ? 'searchBoxOn' : 'searchBoxOff'" placeholder="請輸入職業" v-model="search" />
    <!-- 搜尋框查詢按鈕 -->
    <img src="@/img/magnifying-glass.png" title="searchBoxGlass" alt="this is searchBoxGlass"
      :class="mySearchToggle ? 'searchBoxGlassOn' : 'searchBoxGlassOff'" @click="goShareExp" />

    <!-- 菜單開關 -->

    <!-- 遮罩 -->
    <div :class="myMenuToggle ? 'maskMenuOn' : 'maskMenuOff'" @click="toggleMenu">
      <!-- 菜單 -->
      <div :class="myMenuToggle ? 'menuOn' : 'menuOff'" @click.stop>
        <!-- 關閉鈕 -->
        <img src="@/img/close.png" id="close" title="close" alt="this is close" @click="toggleMenu"
        :class="myMenuToggle ? 'closeButtonOn' : 'closeButtonOff'" />
        <div v-if="loginState" class="user">{{ userName }}<br><p>歡迎回來</p></div>
        <!-- 內容 -->
        <div :class="myMenuToggle ? 'menu-textOn' : 'menu-textOff'" @click="goShareExp">面試心得分享</div>
        <div :class="myMenuToggle ? 'menu-text2On' : 'menu-textOff'" @click="goLogin">會員中心</div>
        <div class="menu-text2On" @click="postNeedLogin">我要發文</div>
        <div v-if="loginState" class="menu-text3On" @click="signOut">我要登出</div>
      </div>
    </div>
    <!-- 主頁面 -->
    <div>
      <div className="title">輕鬆獲取</div>
      <div className="title2">所需要的面試資訊</div>
      <div className="subTitle">我們提供了什麼服務?</div>
    </div>

    <div className="center">
      <div className="workingImg">
        <img src="@/img/work.jpg" className="work" title="work" alt="this is work" />
          <div id="mask" @click="goShareExp">
            <p>面試心得分享</p>
          </div>
      </div>
    </div>

    <div className="center">
      <div className="workingImg">
        <img src="@/img/working.jpg" className="work" title="work" alt="this is work" />
        <div id="mask">
          <p>敬請期待</p>
        </div>
      </div>
    </div>
  </div>
</template>

<!-- <style src="@/assets/homepage.css" scoped></style> -->

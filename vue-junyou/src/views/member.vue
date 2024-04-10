<script setup lang="ts">
import "@/assets/member.css";
import "@/assets/animate.css";
import { ref } from "vue";
import { defineComponent } from "vue";
import { useRouter } from "vue-router";

const userName = ref("馬克");
const toggleMenu = () => {
  myMenuToggle.value = !myMenuToggle.value;
};
const myMenuToggle = ref(false);
const items = ref([{ othUser: "小火龍", artic: "包納德曾說過，戀愛有建立信心的必要，友情有建立觀察的必要。但願各位能從這段話中獲得心靈上的滋長。" },
{ othUser: "皮卡丘", artic: "滑蛋牛肉的出現，必將帶領人類走向更高的巔峰。滑蛋牛肉，到底應該如何實現。說到滑蛋牛肉，你會想到什麼呢？" }])
type Link = {
  aria: string;
};
const links = ref<Link[]>([{ aria: "管理收藏貼文" }, { aria: "管理個人貼文" }, { aria: "管理帳號" }])

function checkLink(text:string){
  console.log('Clicked content:', text);
}
</script>

<template>
  <div class="memberAria">
    <!-- 回上一頁 -->
    <RouterLink to="/">
      <img src="@/img/back.png" class="mem-back" title="back" alt="this is back" />
    </RouterLink>
    <div class="userName">{{ userName }}</div>
    <img src="@/img/bell.png" class="bell" title="bell" alt="this is bell" @click="toggleMenu" />
  </div>
  <div>
    <!-- 菜單開關 -->
    <div :class="myMenuToggle ? 'mem-maskdivOn' : 'mem-maskdivOff'">
      <!-- 遮罩 -->
      <div :class="myMenuToggle ? 'mem-menuMaskOn' : 'menuMaskOff'" @click="toggleMenu"></div>
      <!-- 菜單 -->
      <div :class="myMenuToggle ? 'mem-menuOn' : 'menuOff'">
        <div class="mem-m-top30">
          <div class="mem-articleBox2" v-for="(item, index) in items" :key="index">
            <div class="mem-title">
              {{ item.othUser }} 回覆了您:「{{ item.artic }}」
            </div>
          </div>
        </div>

      </div>
      <!-- 使用者名稱 -->
      <div :class="myMenuToggle ? 'userNameMaskOn' : 'userNameMaskOff'">
        {{ userName }}
      </div>

      <!-- 關閉鈕 -->
      <img src="@/img/close.png" id="close" title="close" alt="this is close"
        :class="myMenuToggle ? 'mem-closeButtonOn' : 'closeButtonOff'" @click="toggleMenu" />
    </div>
  </div>
  <div class="mem-middle">
    <div>
    <div class="linkAria" v-for="(link) in links">
      <div class="link" @click="checkLink(link.aria)">
        {{ link.aria }}
      </div>
    </div>
  </div>
  </div>
 
</template>

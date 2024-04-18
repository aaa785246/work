<script setup lang="ts">
import "@/assets/member.css";
import "@/assets/animate.css";
import { ref } from "vue";
import { defineComponent } from "vue";
import { useRouter } from "vue-router";
import router from "@/router";
import { useLoginStore } from "@/stores/login";
import { getCookie } from "@/js/cookie";
import { getUserReplyList, type reply } from "@/js/api";

const store = useLoginStore();
const userName = ref(store.userName);

// const cookieState = getCookie("loginState");
// if(cookieState != "Y") router.push("/login")

const replyText = ref<reply[]>();

type Link = {
  aria: string;
};
const links = ref<Link[]>([
  { aria: "管理收藏貼文" },
  { aria: "管理個人貼文" },
  { aria: "管理帳號" },
]);

function checkLink(text: string) {
  console.log("Clicked content:", text);
}

const myMenuToggle = ref(false);

function toggleMenu() {
  myMenuToggle.value = !myMenuToggle.value;
  console.log(myMenuToggle.value)
}

async function bellReply() {
  myMenuToggle.value = !myMenuToggle.value;
  const items: reply[] = await getUserReplyList();
  replyText.value = items;
}
</script>

<template>
  <div class="memberAria">
    <RouterLink to="/">
      <img
        src="@/img/back.png"
        class="mem-back"
        title="back"
        alt="this is back"
      />
    </RouterLink>
    <div class="userName">{{ userName }}</div>
    <img
      src="@/img/bell.png"
      class="bell"
      title="bell"
      alt="this is bell"
      @click="bellReply"
    />
  </div>
  <!-- 遮罩 -->
  <div :class="myMenuToggle ? 'maskOn' : 'menuMaskOff'" @click="toggleMenu">
    <!-- 菜單 -->
    <div :class="myMenuToggle ? 'menuOn' : 'menuOff'">
      <div class="userName">
        {{ userName }}
      </div>
      <!-- 關閉鈕 -->
      <img
        src="@/img/close.png"
        id="close"
        title="close"
        alt="this is close"
        @click="toggleMenu"
        :class="myMenuToggle ? 'closeButtonOn' : 'closeButtonOff'"
      />
      <!-- 內容 -->
      <div
        class="mem-articleBox"
        v-for="(item, index) in replyText"
        :key="index"
      >
        <div class="mem-title">
          {{ item.othUser }} 回覆了您:「{{ item.artic }}」
        </div>
      </div>
    </div>
  </div>
</template>

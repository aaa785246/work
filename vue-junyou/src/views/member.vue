<script setup lang="ts">
import axios from "axios";
import "@/assets/member.css";
import "@/assets/animate.css";
import { onMounted, ref } from "vue";
import { defineComponent } from "vue";
import { useRouter } from "vue-router";
import router from "@/router";
import { useLoginStore } from "@/stores/login";
import { getCookie } from "@/js/cookie";
import { getUserReplyList, type reply } from "@/js/api";

const cookieState = getCookie("loginState");
if(cookieState != "true") router.push("/login")

const store = useLoginStore();
const userName = ref(getCookie("userName"));
const userEmail = ref(getCookie("userEmail"))



const myMenuToggle = ref(false);
function toggleMenu() {
  myMenuToggle.value = !myMenuToggle.value;
}
const replyText = ref<reply[]>();

const noticefunc = async () => {
  const api = `http://192.168.50.193:8001/api/notice`;
  await axios
    .post(api, {
      user_email: userEmail.value,
    })
    .then((response) => {
        replyText.value = response.data;
        // console.log(response.data)
    })
};

onMounted(()=>{
  noticefunc()
})
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
      @click="toggleMenu"
    />
  </div>
  <!-- 遮罩 -->
  <div :class="myMenuToggle ? 'maskOn' : 'maskOff'" @click="toggleMenu">
    <!-- 菜單 -->
    <div :class="myMenuToggle ? 'menuOn' : 'menuOff'" @click.stop>
      <div class="MeunUserName">
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
          {{ item.username }} 回覆了您:「{{ item.msg_content }}」
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import axios from "axios";
import "@/assets/member.css";
import "@/assets/animate.css";
import { onMounted, ref } from "vue";
import { defineComponent } from "vue";
import { useRouter } from "vue-router";
import router from "@/router";
import { getCookie, setCookie } from "@/js/cookie";
import { type reply } from "@/js/api";

const loginState = getCookie("loginState");
if (loginState != "true") router.push("/login");
//如果他有在瀏覽網站並且有登入就保存它的登入狀態
if (loginState == "true") setCookie("loginState","true",20)
const userName = ref(getCookie("userName"));
const userEmail = ref(getCookie("userEmail"));
const noticeState = ref(false);

const myMenuToggle = ref(false);
function toggleMenu() {
  myMenuToggle.value = !myMenuToggle.value;
}
const replyText = ref<reply[]>();
//該顯示的訊息抓出來 並依據有沒有讀取過 去轉換通知的燈號
const noticefunc = async () => {
  // const api = `http://192.168.1.203:8000/notice`;
  const api = `http://192.168.1.200:8000/notice`;
  await axios
    .post(api, {
      user_email: userEmail.value,
    })
    .then((response) => {
      replyText.value = response.data;
      //沒有任何通知的情況下不呼叫API
      if (replyText.value?.length == 0) {
        return
      }
      replyText.value?.forEach((element) => {
        //只要有一則訊息沒看過 我通知燈就要亮
        if (element.viewed === "N") {
          //訊息有沒看過的
          noticeState.value = true;
        } else {
          //訊息都看過了
          noticeState.value = false;
        }
      });
    });
};
//點通知後 把未讀取過的訊息都設為已讀取
const noticeDisappear = async () => {
  myMenuToggle.value = !myMenuToggle.value;
  //沒有任何通知的情況下不呼叫API
  if (replyText.value?.length == 0) {
    return
  }
  const api = `http://192.168.50.193:8001/api/closenotice`;
  await axios
    .post(api, {
      user_email: userEmail.value,
    })
    .then((response) => {
      //訊息都看過了
      noticeState.value = false;
    });
};

onMounted(() => {
  noticefunc();
});
</script>

<template>
  <div class="memberAria">
    <RouterLink to="/">
      <img src="@/img/back.png" class="mem-back" title="back" alt="this is back" />
    </RouterLink>
    <div class="userName">{{ userName }}</div>
    <img :src="noticeState ? 'src/img/bellon.png' : 'src/img/bell.png'" class="bell" title="bell" alt="this is bell"
      @click="noticeDisappear" />
  </div>
  <!-- 遮罩 -->
  <div :class="myMenuToggle ? 'mem-maskOn' : 'maskOff'" @click="toggleMenu">
    <!-- 菜單 -->
    <div :class="myMenuToggle ? 'mem-menuOn' : 'menuOff'" @click.stop>
      <div class="MeunUserName">
        {{ userName }}
      </div>
      <!-- 關閉鈕 -->
      <img src="@/img/close.png" id="close" title="close" alt="this is close" @click="toggleMenu"
        :class="myMenuToggle ? 'mem-closeButtonOn' : 'closeButtonOff'" />
      <!-- 內容 -->
      <div class="mem-articleBox" v-for="(item, index) in replyText" :key="index">
        <div class="mem-title">
          {{ item.message_username }} 回覆了您:「{{ item.message_content }}」
        </div>
      </div>
    </div>
  </div>
</template>
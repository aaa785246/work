<script setup lang="ts">
import axios from "axios";
import "@/assets/article.css";
import { ref, onMounted, watch } from "vue";
import messageContent from "@/components/messageContent.vue";
import bookmarkImg from "@/img/bookmark.png";
import bookmarkImgOn from "@/img/bookmarkon.png";
import { getCookie } from "@/js/cookie";
import { type message } from "@/js/api";
import { useRouter } from "vue-router";

const router = useRouter();
const email = ref("");
const userName = ref("");
const article_title = ref("");
const article_content = ref("");
const like_count = ref("");
const message_count = ref("");
const article_number = ref(getCookie("article_number"));
const loginState = ref(getCookie("loginState"))
const messageFloor = ref<message[] | undefined>();
const cutMsgFloor = ref<message[] | undefined>();
const remind = ref("")
const remindMsg = ref("")
const isLike = ref(undefined);
// 開啟書籤換圖片
const bookMarkImgSrc = ref(`${bookmarkImg}`);
// 開啟書籤調整class
const bookMarkClass = ref("ar-bookmark");
//開啟書籤調整分享按鈕
// const share = ref("ar-share");
//開啟關閉開關
const myMarkToggle = ref(false);
const toggleMark = () => {
  myMarkToggle.value = !myMarkToggle.value;
  bookMarkImgSrc.value = myMarkToggle.value ? bookmarkImgOn : bookmarkImg;
  bookMarkClass.value = myMarkToggle.value ? "ar-bookmarkOn" : "ar-bookmark";
  // share.value = myMarkToggle.value ? "ar-shareOn" : "ar-share";
};
//抓取文章API
const getArticle = async () => {
  // const api = `http://192.168.1.203:8000/searcharticlenumber`;
  const api = `http://192.168.1.200:8000/searcharticlenumber`;
  await axios
    .post(api, {
      articleNumber: parseInt(article_number.value),
    })
    .then((response) => {
      email.value = response.data[0].email
      userName.value = response.data[0].username
      article_title.value = response.data[0].article_title
      article_content.value = response.data[0].article_content
      like_count.value = response.data[0].like_count
      message_count.value = response.data[0].message_count
    });
};

//抓取留言API
const getMessage = async () => {
  // const api = `http://192.168.1.203:8000/articlemessage`;
  const api = `http://192.168.1.200:8000/articlemessage`;
  await axios
    .post(api, {
      articleNumber: parseInt(article_number.value),
    })
    .then((response) => {
      messageFloor.value = response.data;
      cutMsgFloor.value = messageFloor.value?.filter(item => parseInt(item.msg_floor) < 4)
      console.log(cutMsgFloor.value)
    });
};
//抓取是否有喜歡(愛心)API
const getisLike = async () => {
  // const api = `http://192.168.1.203:8000/Islike`;
  const api = `http://192.168.1.200:8000/Islike`;
  await axios
    .post(api, {
      user_email: getCookie("userEmail"),
      articlenumber: parseInt(article_number.value)
    })
    .then((response) => {
      isLike.value = response.data;
    });
};
//留言API
const inputMessage = async () => {
  //有登入執行API
  if (loginState.value != "") {
     // const api = `http://192.168.1.203:8000/addmessage`;
  const api = `http://192.168.1.200:8000/addmessage`;
  await axios
    .post(api, {
      poster_email: email.value,
      msg_userName: getCookie("userName"),
      article_number: parseInt(article_number.value),
      msg_content: contentInput.value
    })
    .then((response) => {
      //留言成功讓網頁重整
      location.reload();
    })
    .catch(err => {
      console.log(err)
    })
  }else {
    remindMsg.value = "點此登入"
  }
 
};
//即時更新愛心數量API
const likeCount = async (state: boolean) => {
  // const api = `http://192.168.1.203:8000/likelycount`;
  const api = `http://192.168.1.200:8000/likelycount`;
  await axios
    .post(api, {
      user_email: getCookie("userEmail"),
      articlenumber: parseInt(article_number.value),
      count_like: state
    })
    .then((response) => {
      console.log(response.data)
    })
    .catch((err) => {
      console.log("錯誤:" + err)
    });
};
const contentInput = ref("");
const newRow = ref(0)
const showMoreBtn = ref(true);

onMounted(async () => {
  await getArticle();
  await getMessage();
  //留言區點選過顯示更多 按鈕要消失，放在API裁減完cutMsgFloor後偵測
  watch(() => cutMsgFloor.value, (newValue, oldValue) => {
    if (cutMsgFloor.value != oldValue) {
      showMoreBtn.value = false;
    }
  })

  // 有登入才去抓使用者有沒有對此文章點過愛心
  if (loginState.value != "") {
    await getisLike();
    //已經登入且點過愛心
    if (loginState.value != "" && isLike.value == true) {
      likeArticle();
    }
  }



  //留言的textarea
  const textarea = document.querySelector('.ar-inputContent') as HTMLTextAreaElement;
  newRow.value = textarea.maxLength * 0.95;
})
//偵測留言輸入
watch(() =>
  contentInput.value, (newValue, oldValue) => {
    // console.log(contentInput.value.length)
    if (contentInput.value.length >= 1000) {
      //輸入數字超過1600
      const textarea = document.querySelector('.ar-inputContent') as HTMLTextAreaElement;
      textarea.maxLength = 1200;
      textarea.rows = 112;
      // console.log("總字數限制:"+textarea.maxLength)   
    } else if (contentInput.value.length > newRow.value) {
      console.log("已達警告字數")
      //輸入字數超過總字數的0.9就新增行數以及增加最大字數限制
      const textarea = document.querySelector('.ar-inputContent') as HTMLTextAreaElement;
      textarea.maxLength = textarea.maxLength + 100;
      textarea.rows = textarea.rows + 4;
      newRow.value = textarea.maxLength * 0.95;
    }
  }
)

const test = () => {
  console.log(showMoreBtn.value)
}
const checkLike = ref(true);
const likeOn = ref("/src/img/heartempty.png")
const likely = () => {
  //未登入點愛心 要叫使用者去登入
  if (checkLike.value == true && loginState.value == "") {
    remind.value = "點此登入"
    //有登入點愛心 要送進資料庫
  } else if (checkLike.value == true && loginState.value != "") {
    likeState();
    likeCount(true);
    //有登入點過愛心了 又點愛心要取消他的愛心
  } else if (checkLike.value == false && loginState.value != "") {
    likeState();
    likeCount(false);
    // console.log("用戶取消讚")
  }

}
//愛心開啟與關閉
const likeState = () => {
  checkLike.value = !checkLike.value;
  likeOn.value = checkLike.value ? "/src/img/heartempty.png" : "/src/img/heart.png";
  like_count.value = checkLike.value ? String(parseInt(like_count.value) - 1) : String(parseInt(like_count.value) + 1)
}
//先前已經點過愛心要執行
const likeArticle = () => {
  likeOn.value = isLike.value ? "/src/img/heart.png" : "/src/img/heartempty.png";
  checkLike.value = false;
  // console.log("func有執行")
}
//去登入
const goLogin = () => {
  router.push("/login")
}
//顯示更多
const showMore = () => {
  cutMsgFloor.value = messageFloor.value
}
</script>

<template>
  <RouterLink to="/shareExp">
    <img src="@/img/back.png" className="ar-back" title="back" alt="this is back" />
  </RouterLink>
  <!-- 文章內容 -->
  <div>
    <div class="article-title">
      {{ article_title }}
    </div>
    <div class="article-user">{{ userName }}</div>
    <div class="article-content">{{ article_content }}</div>

    <div class="ar-heartAndMsg-container">
      <img :src="likeOn" class="ar-heart" @click="likely" />
      <div class="ar-heartCount">{{ like_count }}</div>
      <img :src="bookMarkImgSrc" alt="bookmark" :class="bookMarkClass" @click="toggleMark" />
      <!-- <img src="@/img/share.png" alt="share  " :class="share" /> -->
    </div>
    <div class="ar-remind" @click="goLogin">{{ remind }}</div>
    <div class="ar-allMsg">所有留言</div>
    <!-- <button @click="test">測試</button> -->
  </div>
  <!-- 留言內容 -->
  <div class="ar-articleBox" v-for="(message, index) in cutMsgFloor" :key="index">
    <div class="ar-messageName">{{ message.username }}</div>
    <div class="ar-content">{{ message.msg_content }}</div>
    <div class="ar-floor">{{ message.msg_floor }}樓</div>
  </div>
  <div class="ar-articleInputBox">
    <textarea name="content" id="" cols="36" rows="5" maxLength="90" class="ar-inputContent" placeholder="請輸入內文"
      v-model="contentInput">{{ contentInput }}</textarea>
    <div class="ar-msgArea">
      <div class="ar-MsgRemind" @click="goLogin">{{ remindMsg }}</div>
      <button class="ar-msgBtn" @click="inputMessage">我要留言</button>
    </div>

  </div>
  <button @click="showMore" class="ar-showMore" v-if="showMoreBtn == true">顯示更多</button>
</template>

<!-- <style scoped></style> -->

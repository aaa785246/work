<script setup lang="ts">
import axios from "axios";
import "@/assets/article.css";
import { ref, onMounted, watch } from "vue";
import messageContent from "@/components/messageContent.vue";
import bookmarkImg from "@/img/bookmark.png";
import bookmarkImgOn from "@/img/bookmarkon.png";
import { setCookie, getCookie } from "@/js/cookie";
import { type message } from "@/js/api";
import { useRouter } from "vue-router";
import sure from "@/components/successfulComponent.vue";
import deleteMsg from "@/components/failedComponent.vue"
const loginState = ref(getCookie("loginState"))
//如果他有在瀏覽網站並且有登入就保存它的登入狀態
if (loginState.value == "true") {
  setCookie("loginState", "true", 20)
  setCookie("userEmail", getCookie("userEmail"), 60),
    setCookie("userName", getCookie("userName"), 60)
}
const router = useRouter();
const email = ref("");
const posterName = ref("");
const userName = ref(getCookie("userName"));
const userEmail = ref(getCookie("userEmail"));
const article_title = ref("");
const article_content = ref("");
const article_contentModeify = ref("")
const like_count = ref("");
const message_count = ref("");
const article_number = ref(getCookie("article_number"));
const messageFloor = ref<message[] | undefined>();
const cutMsgFloor = ref<message[] | undefined>();
const remind = ref("")
const remindMsg = ref("")
const modeifyContent = ref("")
const isLike = ref(undefined);
const isCollect = ref(undefined);
// 開啟書籤換圖片
const bookMarkImgSrc = ref(`${bookmarkImg}`);
// 開啟書籤調整class
const bookMarkClass = ref("ar-bookmark");
//開啟書籤調整分享按鈕
// const share = ref("ar-share");
//抓取文章API
const getArticle = async () => {
  // const api = `http://192.168.1.203:8000/searcharticlenumber`;
  const api = `http://172.20.10.3:8000/searcharticlenumber`;
  await axios
    .post(api, {
      articleNumber: parseInt(article_number.value),
    })
    .then((response) => {
      email.value = response.data[0].email
      posterName.value = response.data[0].username
      article_title.value = response.data[0].article_title
      article_content.value = response.data[0].article_content
      like_count.value = response.data[0].like_count
      message_count.value = response.data[0].message_count
    });
};

//抓取留言API
const getMessage = async () => {
  // const api = `http://192.168.1.203:8000/articlemessage`;
  const api = `http://172.20.10.3:8000/articlemessage`;
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
  const api = `http://172.20.10.3:8000/Islike`;
  await axios
    .post(api, {
      user_email: getCookie("userEmail"),
      articlenumber: parseInt(article_number.value)
    })
    .then((response) => {
      isLike.value = response.data;
    });
};
//抓取是否有收藏文章API
const getisCollect = async () => {
  // const api = `http://192.168.1.203:8000/Iscollect`;
  const api = `http://172.20.10.3:8000/Iscollect`;
  await axios
    .post(api, {
      articlenumber: parseInt(article_number.value),
      user_email: getCookie("userEmail"),
    })
    .then((response) => {
      isCollect.value = response.data;
      // console.log("使用者是否有收藏:"+response.data)
    });
}
//即時更新收藏狀態API
const CollectState = async () => {
  // const api = `http://192.168.1.203:8000/collectarticle`;
  const api = `http://172.20.10.3:8000/collectarticle`;
  await axios
    .post(api, {
      articlenumber: parseInt(article_number.value),
      user_email: getCookie("userEmail"),
    })
    .then((response) => {
      console.log(response.data);
    });
}
//留言API
const inputMessage = async () => {
  //有登入執行API
  if (loginState.value != "") {
    // const api = `http://192.168.1.203:8000/addmessage`;
    const api = `http://172.20.10.3:8000/addmessage`;
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
  } else {
    remindMsg.value = "點此登入"
  }

};
//即時更新愛心數量API
const likeCount = async (state: boolean) => {
  // const api = `http://192.168.1.203:8000/likelycount`;
  const api = `http://172.20.10.3:8000/likelycount`;
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
//刪除訊息API
const deletemessage = async () => {
  // const api = `http://192.168.1.203:8000/deletemessage`;
  const api = `http://172.20.10.3:8000/deletemessage`;
  await axios
    .post(api, {
      articlenumber: parseInt(article_number.value),
      msg_floor: modeifyFloor.value
    })
    .then((response) => {
      getMessage()
      checkDeleteDialog.value = true;
    })
}
//修改訊息API
const modeifymessage = async () => {
  // const api = `http://192.168.1.203:8000/modeifymessage`;
  const api = `http://172.20.10.3:8000/modeifymessage`;
  await axios
    .post(api, {
      articlenumber: parseInt(article_number.value),
      msg_floor: modeifyFloor.value,
      msg_content: modeifyContent.value
    })
    .then((response) => {
      getMessage()
      switchCheckDialog.value = true;
    });
}
//修改文章內容
const modeifyArticle = async () => {
  // const api = `http://192.168.1.203:8000/modeifyarticle`;
  const api = `http://172.20.10.3:8000/modeifyarticle`;
  await axios
    .post(api, {
      articlenumber: parseInt(article_number.value),
      user_email: userEmail.value,
      article_content: article_contentModeify.value
    })
    .then((response) => {
      checkArticleModeifyDialog.value = true;
    });
}

//刪除文章
const deleteArticle = async () => {
  // const api = `http://192.168.1.203:8000/deletearticle`;
  const api = `http://172.20.10.3:8000/deletearticle`;
  await axios
    .post(api, {
      articlenumber: parseInt(article_number.value),
      user_email: userEmail.value,
    })
    .then((response) => {
      console.log(response.data)
    });
}


//textarea設定
const contentInput = ref("");
const newMsgRow = ref(0);
const articleRow = ref(0);
const msgRow = ref(0);
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

  // 有登入才去抓使用者有沒有對此文章點過愛心或收藏
  if (loginState.value != "") {
    await getisLike();
    await getisCollect()
    //已經登入且點過愛心
    if (loginState.value != "" && isLike.value == true) {
      likeArticle();
    }
    //已經登入且點過收藏
    if (loginState.value != "" && isCollect.value == true) {
      collectArticle()
    }
  }
  //我要留言的textarea
  newMsgRow.value = 72;
  //文章的textarea
  articleRow.value = 72;
  //留言修改的textarea
  msgRow.value = 72;
})

// 行數增加
const msgRowsUpdate = () => {
  //輸入字數超過總字數的0.8就新增行數以及增加最大字數限制
  const textarea = document.querySelector('.ar-inputContent') as HTMLTextAreaElement;
  textarea.maxLength = textarea.maxLength + 100;
  textarea.rows = textarea.rows + 5;
  msgRow.value = textarea.maxLength * 0.8;
}
//偵測修改留言
watch(() =>
modeifyContent.value, (newValue, oldValue) => {
    if (modeifyContent.value.length >= 1000) {
      //輸入數字超過1000
      const textarea = document.querySelector('.ar-inputContent') as HTMLTextAreaElement;
      textarea.maxLength = 1200;
      textarea.rows = 70;
      // console.log("總字數限制:"+textarea.maxLength)   
    } else if (modeifyContent.value.length > msgRow.value) {
      // console.log("已達警告字數")
      setTimeout(msgRowsUpdate,1);
    }
  }
)
// 行數增加
const newMsgRowsUpdate = () => {
  //輸入字數超過總字數的0.8就新增行數以及增加最大字數限制
  const textarea = document.querySelector('.ar-inputContentNew') as HTMLTextAreaElement;
  textarea.maxLength = textarea.maxLength + 100;
  textarea.rows = textarea.rows + 5;
  newMsgRow.value = textarea.maxLength * 0.8;
}
//偵測新留言輸入
watch(() =>
  contentInput.value, (newValue, oldValue) => {
    // console.log(contentInput.value.length)
    if (contentInput.value.length >= 1000) {
      //輸入數字超過1000
      const textarea = document.querySelector('.ar-inputContentNew') as HTMLTextAreaElement;
      textarea.maxLength = 1200;
      textarea.rows = 70;
      // console.log("總字數限制:"+textarea.maxLength)   
    } else if (contentInput.value.length > newMsgRow.value) {
      // console.log("已達警告字數")
      setTimeout(newMsgRowsUpdate,1)
    }
  }
)
const modeifyRows = ref<number>(0);
// 行數增加
const articleRowsUpdateFristTime = () => {
  //輸入字數超過總字數的0.8就新增行數以及增加最大字數限制
  const textarea = document.querySelector('.ar-inputArticleContent') as HTMLTextAreaElement;
  textarea.maxLength = textarea.maxLength + article_contentModeify.value.length;
  textarea.rows = textarea.rows  + Math.ceil(article_contentModeify.value.length/18);
  articleRow.value = textarea.maxLength * 0.9;
}
const articleRowsUpdate = () => {
  //輸入字數超過總字數的0.8就新增行數以及增加最大字數限制
  const textarea = document.querySelector('.ar-inputArticleContent') as HTMLTextAreaElement;
  textarea.maxLength = textarea.maxLength + 100;
  textarea.rows = textarea.rows + 6;
  articleRow.value = textarea.maxLength * 0.8;
}
//偵測文章修改輸入
watch(() =>
  article_contentModeify.value, (newValue, oldValue) => {
    // console.log(contentInput.value.length)
    if (article_contentModeify.value.length >= 2100) {
      //輸入數字超過2000
      const textarea = document.querySelector('.ar-inputArticleContent') as HTMLTextAreaElement;
      textarea.maxLength = 3000;
      textarea.rows = 184;
      // console.log("總字數限制:" + textarea.maxLength)
    } else if (article_contentModeify.value.length > articleRow.value &&article_contentModeify.value == article_content.value) {
      // console.log("已達警告字數")
      setTimeout(articleRowsUpdateFristTime, 1);
      console.log("frist")
    }else if (article_contentModeify.value.length > articleRow.value) {
      // console.log("已達警告字數")
      setTimeout(articleRowsUpdate, 1);
      console.log("two")
    }
  }
)



//喜歡與收藏圖片開關
const checkLike = ref(true);
const checkCollect = ref(true)
const likeOn = ref("/src/img/heartempty.png")
//依登入狀態對愛心做相應行為
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
//依登入狀態對收藏做相應行為
const collectly = () => {
  if (checkCollect.value == true && loginState.value == "") {
    remind.value = "點此登入"
    //有登入點收藏 要送進資料庫
  } else if (checkCollect.value == true && loginState.value != "") {
    //callapi
    CollectState()
    checkCollectState()
    //有登入點過收藏了 又點收藏要取消他的收藏
  } else if (checkCollect.value == false && loginState.value != "") {
    CollectState()
    checkCollectState()
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
//收藏開啟與關閉
const checkCollectState = () => {
  checkCollect.value = !checkCollect.value
  bookMarkImgSrc.value = checkCollect.value ? bookmarkImg : bookmarkImgOn;
  bookMarkClass.value = checkCollect.value ? "ar-bookmark" : "ar-bookmarkOn";
}
//先前已經點過收藏要執行
const collectArticle = () => {
  bookMarkImgSrc.value = isCollect.value ? bookmarkImgOn : bookmarkImg;
  bookMarkClass.value = isCollect.value ? "ar-bookmarkOn" : "ar-bookmark";
  checkCollect.value = false;
}
//到登入頁
const goLogin = () => {
  router.push("/login")
}
//顯示更多
const showMore = () => {
  cutMsgFloor.value = messageFloor.value
}
//使用者編輯哪一樓
const modeifyFloor = ref(0);
//留言樓圖片轉換
const modeifyImgChange = ref<boolean>(false);
//完成修改的dialog開關
const switchCheckDialog = ref<boolean>(false);
//確定刪除的dialog開關
const switchDeleteDialog = ref<boolean>(false);
//完成刪除的dialog開關
const checkDeleteDialog = ref<boolean>(false);
//取得該刪除或編輯哪筆資料
const modeifyMessageBtn = (index: number) => {
  modeifyFloor.value = index + 1;
  modeifyImgChange.value = true;
  if (cutMsgFloor.value != null) {
    modeifyContent.value = cutMsgFloor.value[index].msg_content
  }
  // console.log(modeifyFloor.value)
}
// 點擊垃圾桶img開啟dialog
const DeleteMsgBtn = () => {
  switchDeleteDialog.value = true;
}

//點選確定刪除
const callDeleteMessageApi = () => {
  switchDeleteDialog.value = false;
  deletemessage();
};

//點選取消
const componentClose = () => {
  switchDeleteDialog.value = false;
  console.log("DIALOG關閉")
};
//確認修改
const checkModifyMsgBtn = () => {
  modeifymessage();
  switchCheckDialog.value = true;
}
//點選更改文章IMG 轉換IMG
const articleImgChange = ref<boolean>(false);
const modeifyArticleBtn = () => {
  articleImgChange.value = true;
  article_contentModeify.value = article_content.value;
  modeifyRows.value = Math.ceil(article_contentModeify.value.length / 18);
}
// 文章修改成功dialog
const checkArticleModeifyDialog = ref<boolean>(false);
// 確認文章刪除diglog
const deleteArticleDialog = ref<boolean>(false);
// 文章刪除成功dialog
const CheckDeleteArticleDialog = ref<boolean>(false);
const checkModifyArticle = () => {
  modeifyArticle();
}
//點選垃圾桶IMG
const deleteArticleBtn = () => {
  deleteArticleDialog.value = true;
}
//點選確定刪除
const callDeleteArticleApi = () => {
  CheckDeleteArticleDialog.value = true;
  deleteArticle();
};

const deleteComponentClose = () => {
  deleteArticleDialog.value = false;
}

const goShareExp = () => {
  router.push("/shareExp")
}
</script>

<template>
  <img src="@/img/back.png" className="ar-back" title="back" alt="this is back" @click="goShareExp" />
  <!-- 文章內容 -->
  <div>
    <div class="test">
      <!-- 使用者修改文章內容 -->
      <img src='/src/img/modeify.png' alt="" class="ar-ArticleModeify"
        v-if="email == userEmail && articleImgChange == false" @click="modeifyArticleBtn">
      <!-- 確認按鈕 -->
      <img src="/src/img/check.png" alt="" class="ar-Articlecheck" v-if="articleImgChange == true"
        @click="checkModifyArticle">
      <!-- 刪除 -->
      <img src="/src/img/garbage.png" alt="" class="ar-Articledelete" v-if="articleImgChange == true"
        @click="deleteArticleBtn">
    </div>
    <div class="article-title">
      {{ article_title }}
    </div>

    <div class="article-user">{{ posterName }}</div>
    <textarea name="" id="" cols="36" rows="5" maxLength="90" v-if="articleImgChange == true" class="ar-inputArticleContent"
      v-model="article_contentModeify">{{ article_contentModeify }}</textarea>
    <div class="article-content" v-if="articleImgChange == false">{{ article_content }}</div>

    <div class="ar-heartAndMsg-container">
      <img :src="likeOn" class="ar-heart" @click="likely" />
      <div class="ar-heartCount">{{ like_count }}</div>
      <img :src="bookMarkImgSrc" alt="bookmark" :class="bookMarkClass" @click="collectly" />
      <!-- <img src="@/img/share.png" alt="share  " :class="share" /> -->
    </div>
    <div class="ar-remind" @click="goLogin">{{ remind }}</div>
    <div class="ar-allMsg">所有留言</div>
    <!-- <button @click="test">測試</button> -->
  </div>
  <!-- 留言內容 -->
  <div class="ar-articleBox" v-for="(message, index) in cutMsgFloor" :key="index">
    <div class="ar-messageName">{{ message.username }}</div>
    <div class="ar-content" v-if="modeifyFloor != index + 1">{{ message.msg_content }}</div>
    <!-- 顯示使用者自身的評論，加上修改img供修改 -->
    <img src='/src/img/modeify.png' alt="" class="ar-modeify"
      v-if="message.username == userName && modeifyImgChange == false" @click="modeifyMessageBtn(index)">
    <!-- 修改img替換成修改完成與垃圾桶-->
    <!-- 修改 -->
    <img src="/src/img/check.png" alt="" class="ar-check" v-if="modeifyImgChange == true && modeifyFloor == index + 1"
      @click="checkModifyMsgBtn">
    <!-- 刪除 -->
    <img src="/src/img/garbage.png" alt="" class="ar-delete"
      v-if="modeifyImgChange == true && modeifyFloor == index + 1" @click="DeleteMsgBtn">
    <textarea name="" id="" cols="36" rows="5"
      v-if="modeifyFloor != 0 && modeifyFloor == index + 1 && modeifyImgChange == true" placeholder="請輸入編輯內容"
      class="ar-inputContent" v-model="modeifyContent">{{ modeifyContent }}</textarea>
    <div class="ar-floor">{{ message.msg_floor }}樓</div>
  </div>
  <!-- 我要留言 -->
  <div class="ar-articleInputBox">
    <textarea name="content" id="" cols="36" rows="5" maxLength="90" class="ar-inputContentNew" placeholder="請輸入內文"
      v-model="contentInput">{{ contentInput }}</textarea>
    <div class="ar-msgArea">
      <div class="ar-MsgRemind" @click="goLogin">{{ remindMsg }}</div>
      <button class="ar-msgBtn" @click="inputMessage">我要留言</button>
    </div>

  </div>
  <button @click="showMore" class="ar-showMore" v-if="showMoreBtn == true">顯示更多</button>
  <!-- 文章修改成功 -->
  <sure v-if="checkArticleModeifyDialog == true" :state="checkArticleModeifyDialog" content="8" />
  <!-- 文章刪除成功 -->
  <sure v-if="CheckDeleteArticleDialog == true" :state="CheckDeleteArticleDialog" content="9" />
  <!-- 確認刪除文章 -->
  <deleteMsg :state="deleteArticleDialog" @call-api="callDeleteArticleApi" @close-dialog="deleteComponentClose"
    content="5" />
  <!-- 訊息修改成功 -->
  <sure v-if="switchCheckDialog == true" :state="switchCheckDialog" content="5" />
  <!-- 訊息刪除成功 -->
  <sure v-if="checkDeleteDialog == true" :state="checkDeleteDialog" content="6" />
  <!-- 確認刪除訊息 -->
  <deleteMsg :state="switchDeleteDialog" @call-api="callDeleteMessageApi" @close-dialog="componentClose" content="4" />
</template>

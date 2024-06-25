<script setup lang="ts">
import "@/assets/post.css"
import axios from "axios";
import { onMounted, ref, watch } from "vue";
import { useRouter } from "vue-router";
import { getCookie, setCookie,deleteCookie } from "@/js/cookie";
import successful from "@/components/successfulComponent.vue";
const router = useRouter();
const loginState = ref(getCookie("loginState"));
const userName = ref(getCookie("userName"));
const userEmail = ref(getCookie("userEmail"))
const switchDialog = ref<boolean>(false);
if (loginState.value == "true") {
  setCookie("loginState", loginState.value, 20);
}
const goHomePage = () => {
    router.push("/")
}
const titleInput = ref("");
const contentInput = ref("");
const newRow = ref(0)
onMounted(() => {
    const textarea = document.querySelector('.post-inputContent') as HTMLTextAreaElement;
    newRow.value = textarea.maxLength * 0.8;
})


watch(() =>
    contentInput.value, (newValue, oldValue) => {
        // console.log(contentInput.value.length)
        if (contentInput.value.length >= 1800)
        {
            //輸入數字超過1600
            const textarea = document.querySelector('.post-inputContent') as HTMLTextAreaElement;
            textarea.maxLength = 2000;
            textarea.rows =112;
            // console.log("總字數限制:"+textarea.maxLength)   
        }else if (contentInput.value.length > newRow.value) {
            //輸入字數超過總字數的0.9就新增行數以及增加最大字數限制
            const textarea = document.querySelector('.post-inputContent') as HTMLTextAreaElement;
            textarea.maxLength = textarea.maxLength+72;
            textarea.rows = textarea.rows+4;
            newRow.value = textarea.maxLength*0.9;
        }
    }
)

const post = async () => {
//   const api = `http://192.168.1.203:8000/newarticle`;
  const api = `http://172.20.10.3:8000/newarticle`;
  await axios
    .post(api, {
      title: titleInput.value,
      article_content: contentInput.value,
      user_email:userEmail.value,
      user_name:userName.value
    })
    .then((response) => {
        setCookie("arrivedPage",`/article/${response.data.article_number}`,10);
        setCookie("article_number",response.data.article_number,10);
        switchDialog.value = true;
    })
    .catch(() => {

    });
};

// watch(()=>titleInput.value,(newValue,oldValue)=>{
// if (titleInput.value != oldValue) {
//     console.log(titleInput.value);
// }
// })
// watch(()=>contentInput.value,(newValue,oldValue)=>{
// if (contentInput.value != oldValue) {
//     console.log(contentInput.value);
// }
// })
</script>
<template>
    <img src="@/img/back.png" class="post-back" title="back" alt="this is back" @click="goHomePage" />
    <div class="post-title">標題</div>
    <textarea name="title" id="" cols="36" rows="5" class="post-inputTitle" placeholder="請輸入標題" v-model="titleInput">{{ titleInput }}</textarea>
    <div class="post-title">內文</div>
    <textarea name="content" id="" cols="36" rows="5" maxLength="90" class="post-inputContent" placeholder="請輸入內文"
    v-model="contentInput">{{ contentInput }}</textarea>
    <div v-if="contentInput.length>1800" class="post-notice">文章字數不能超過2000個字，目前字數{{contentInput.length}}字</div>
    <div class="post-btnBox"><button class="post-btn" @click="post">發布</button></div>
    <successful v-if="switchDialog === true" :state="switchDialog"  content="4" />
</template>
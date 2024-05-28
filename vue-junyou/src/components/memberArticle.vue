<script setup lang="ts">
import axios from "axios";
import "@/assets/memberArticle.css"
import router from "@/router";
import { getCookie, setCookie } from "@/js/cookie";
import { onMounted, ref } from "vue";
import { type parsonalArticles } from "@/js/api";

const props = defineProps<{
  content: string;
}>();
const pageTitle = ref("");
const goMember = () => {
    router.push("/member")
}
const articleText = ref<parsonalArticles[] | undefined>();

const returnPage = (index: number) => {
    if (articleText.value != undefined) {
        setCookie("article_number", articleText.value[index].article_number.toLocaleString(), 10)
    }
    router.push(`/article/${getCookie("article_number")}`)
};

const personalArticle = async (content:string) => {
    const api = `http://192.168.1.200:8000/personalArticle`;
    await axios
        .post(api, {
            user_email: getCookie("userEmail"),
            content:content
        })
        .then((response) => {
            articleText.value = response.data;
            // console.log(articleText.value)
            //標題或內容太長做裁減
            if (articleText.value != undefined) {
                for (const item of articleText.value) {
                    if (item.article_content.length > 30) {
                        item.cut = item.article_content.substring(0, 20) + "...";
                    } else {
                        item.cut = item.article_content;
                    }
                }
            }

        });
}
onMounted(() => {
    if(props.content=="1") {
    personalArticle("1")
    pageTitle.value = "管理個人貼文";
    }else if (props.content =="2"){
    personalArticle("2")
    pageTitle.value = "管理收藏貼文";
    }
})
</script>
<template>
    <div class="pa-memberAria">
        <img src="@/img/back.png" class="pa-back" title="back" alt="this is back" @click="goMember" />
        <div class="pa-title">{{pageTitle}}</div>
    </div>
    <div v-for="(item, index) in articleText" :key="index" @click="returnPage(index)">
        <div class="pa-articleBox">
            <div class="pa-articletitle">{{ item.title }}</div>
            <div class="pa-content">{{ item.cut }}</div>
            <div class="heartAndMsg-container">
                <img src="@/img/heart.png" alt="heart" class="heart" />
                <div class="heartAndMsgCount">{{ item.like_couter }}</div>
                <img src="@/img/message2.png" alt="message" class="message" />
                <div class="heartAndMsgCount">{{ item.message_couter }}</div>
            </div>
        </div>
    </div>

</template>
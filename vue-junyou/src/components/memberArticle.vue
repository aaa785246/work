<script setup lang="ts">
import axios from "axios";
import "@/assets/memberArticle.css"
import router from "@/router";
import { getCookie, setCookie } from "@/js/cookie";
import { type articles } from "@/js/api";
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
const allArticle = ref<parsonalArticles[] | undefined>();
const PageLabel = ref<string[]>([]);
const totalPage = ref(0);
//頁數標籤
const arrayDetect = (total: parsonalArticles[]) => {
    if (total.length == 0) {
        return
    }
    const pageMath = Math.ceil(total.length / 4);
    totalPage.value = pageMath;
    for (let i = 1; i <= pageMath; i++) {
        PageLabel.value.push(i.toString());
    }
}

const returnPage = (index: number) => {
    if (articleText.value != undefined) {
        setCookie("article_number", articleText.value[index].article_number.toLocaleString(), 10)
    }
    router.push(`/article/${getCookie("article_number")}`)
};

const personalArticle = async (content: string) => {
    const api = `http://192.168.1.200:8000/personalarticle`;
    await axios
        .post(api, {
            user_email: getCookie("userEmail"),
            content: content
        })
        .then((response) => {
            articleText.value = response.data.slice(0, 4);
            allArticle.value = response.data;
            // console.log(articleText.value)
            if (allArticle.value != null) {
                arrayDetect(allArticle.value)
            }
            //標題或內容太長做裁減
            if (articleText.value != undefined && allArticle.value != undefined) {
                for (const item of articleText.value) {
                    if (item.article_content.length > 30) {
                        item.cut = item.article_content.substring(0, 20) + "...";
                    } else {
                        item.cut = item.article_content;
                    }
                }
                for (const item of allArticle.value) {
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
    if (props.content == "1") {
        personalArticle("1")
        pageTitle.value = "管理個人貼文";
    } else if (props.content == "2") {
        personalArticle("2")
        pageTitle.value = "管理收藏貼文";
    }
})
//所選頁數
const isChoose = ref(1);
//內容替換
const arrayChange = (index: number) => {
    
    const arraySize = 4;
    const start = index * arraySize;
    const end = start + arraySize;
    articleText.value = allArticle.value?.slice(start, end)
    isChoose.value = index + 1;
    // console.log("array:"+isChoose.value)
}
//頁籤的class轉換
const ButtonClassChange = (index: number) => {
    return isChoose.value === index + 1 ? 'pa-pageNumberNow' : 'pa-pageNumber';
};
//每組該顯示的頁籤
const shouldShowButton = (index: number) => {
    const indexChange = 4;
    let pageGroup = Math.floor(isChoose.value / indexChange);
    if (isChoose.value % indexChange === 0 && isChoose.value !== 0) {
        pageGroup -= 1;
    }
    const min = pageGroup * indexChange;
    const max = min + indexChange;
    return index >= min && index < max;
};

//上下頁
const forward = (count: number) => {
    if (count == 1) {
        arrayChange(0);
        isChoose.value = 1;
        // console.log("ischoose:"+isChoose.value)
    } else if (count == 2) {
        isChoose.value -= 1;
        arrayChange(isChoose.value - 1);
    } else if (count == 3) {
        isChoose.value += 1
        arrayChange(isChoose.value - 1)
    } else if (count == 4) {
        isChoose.value = totalPage.value;
        arrayChange(isChoose.value - 1)
    }
}
</script>
<template>
    <div class="pa-memberAria">
        <img src="@/img/back.png" class="pa-back" title="back" alt="this is back" @click="goMember" />
        <div class="pa-title">{{ pageTitle }}</div>
    </div>
    <div v-for="(item, index) in articleText" :key="index" @click="returnPage(index)">
        <div class="pa-articleBox">
            <div class="pa-articletitle">{{ item.title }}</div>
            <div class="pa-content">{{ item.cut }}</div>
            <div class="heartAndMsg-container">
                <img src="@/img/heart.png" alt="heart" class="heart" />
                <div class="heartAndMsgCount">{{ item.like_couter }}</div>
                <img src="@/img/message2.png" alt="message" class="message" />
                <div class="heartAndMsgCount">{{ item.like_couter }}</div>
            </div>
        </div>
    </div>
    <div class="pa-pagiNation">
        <div class="pa-btnArea">
            <button class="pa-pageNumber" @click="forward(1)" v-if="isChoose != 1">&lt&lt</button>
            <button class="pa-pageNumber" @click="forward(2)" v-if="isChoose != 1">&lt</button>
            <button :class="ButtonClassChange(index)" v-for="(item, index) in PageLabel" :key="index"
                @click="arrayChange(index)" v-show="shouldShowButton(index)">{{ item }}</button>
            <button class="pa-pageNumber" @click="forward(3)" v-if="isChoose != totalPage && totalPage != 0">></button>
            <button class="pa-pageNumber" @click="forward(4)" v-if="isChoose != totalPage && totalPage != 0">>></button>
        </div>
    </div>
</template>
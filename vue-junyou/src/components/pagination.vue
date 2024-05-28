<script setup lang="ts">
import "@/assets/shareExp.css";
import { onMounted, ref, watch } from "vue";
import axios from "axios";
import { type articles } from "@/js/api";
import { useRouter } from "vue-router";
import { getCookie, setCookie } from "@/js/cookie";
const router = useRouter();
const props = defineProps<{
  content: string;
  reload:string;
}>();
const emitClose = defineEmits(["finish"]);
//標題、內容
const articleText = ref<articles[] | undefined>();
const allArticle = ref<articles[] | undefined>();
// const search = ref("");
const PageLabel = ref<string[]>([]);
//頁數標籤
const arrayDetect = (total: articles[]) => {
  const pageMath = Math.ceil(total.length / 4);

  for (let i = 1; i <= pageMath; i++) {
    PageLabel.value.push(i.toString());
  }
}
//所有文章
const article = async () => {
  // const api = `http://192.168.1.203:8000/articlesearch`;
  const api = `http://192.168.1.200:8000/articlesearch`;
  await axios
    .post(api, {
      article_name: props.content,
    })
    .then((response) => {
      articleText.value = response.data.slice(0, 4);
      allArticle.value = response.data;
      if(PageLabel.value != null) {
        PageLabel.value.length = 0;
      }
      if (allArticle.value != null) {
        arrayDetect(allArticle.value)
        // PageLabel.value.push(1,2,3,4,5)
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
};
// 目前所選擇的頁數
const isChoose = ref(0);
//是否被選擇轉換class
const btnClass = (index:number) => {
    // console.log("這是ischoose:"+isChoose.value)
    // const lastindex: string = PageLabel.value.pop()!;
    // console.log(lastindex)
    if (isChoose.value === index) {
        return 'sh-pageNumberNow'
    }else if (isChoose.value === index - 1){
        return 'sh-pageNumber'
    }else {
        return 'sh-pageNumberNone'
    }

    // if (isChoose.value === index) {
    //     return 'sh-pageNumberNow'
    // }else if (isChoose.value === index - 1 ){
    //     PageLabel.value.splice(index -2,0,"...");
    //     return 'sh-pageNumber'
    // }else if (PageLabel.value.length -2 == index){
    //     return 'sh-pageNumber'
    // }else {
    //     return 'sh-pageNumberNone'
    // }


    };
//內容替換
const arrayChange = (index:number) => {
  const arraySize = 4 ;
  const start = index * arraySize;
  const end = start + arraySize;
  articleText.value = allArticle.value?.slice(start,end)
  isChoose.value = index;
}
//上下頁
const forward = (count:number) =>{
  if (count == 1) {
    isChoose.value -= 1;
  }else if(count == 2) {
    isChoose.value = parseInt(PageLabel.value[0]) -1;
  }else if (count == 3){
    isChoose.value += 1;
  }else if (count == 4){
    isChoose.value = PageLabel.value.length - 1;
  }
  const arraySize = 4;
  const start = isChoose.value * arraySize;
  const end = start + arraySize;
  articleText.value = allArticle.value?.slice(start,end)
} 
onMounted(() => {
  article();
});
const returnPage = (index: number) => {
  if (articleText.value != undefined) {
    setCookie("article_number", articleText.value[index].article_number.toLocaleString(), 10)
  }
  router.push(`/article/${getCookie("article_number")}`)
};
watch(()=>props.reload,(newValue,oldValue)=>{
if(props.reload == "true") {
    article()
    emitClose("finish");
    console.log(PageLabel.value)
}
})
</script>
<template>
    <div v-for="(item, index) in articleText" :key="index" @click="returnPage(index)">
    <div class="articleBox">
      <div class="sh-title">{{ item.article_title }}</div>
      <div class="sh-content">{{ item.cut }}</div>
      <div class="heartAndMsg-container">
        <img src="@/img/heart.png" alt="heart" class="heart" />
        <div class="heartAndMsgCount">{{ item.like_count }}</div>
        <img src="@/img/message2.png" alt="message" class="message" />
        <div class="heartAndMsgCount">{{ item.message_count }}</div>
      </div>
    </div>
  </div>
  <div class="sh-test">
    <div class="sh-btnArea">
    <button class="sh-pageNumber" @click="forward(2)" v-if="isChoose != 0">&lt&lt</button>
    <button class="sh-pageNumber" @click="forward(1)" v-if="isChoose != 0">&lt</button>
    <button  :class="btnClass(index)" v-for="(item,index) in PageLabel" :key="index" @click="arrayChange(index)">{{ item }}</button>
    <button class="sh-pageNumber" @click="forward(3)" v-if="isChoose != PageLabel.length-1">></button>
    <button class="sh-pageNumber" @click="forward(4)" v-if="isChoose != PageLabel.length-1">>></button>
  </div>
  </div>

</template>
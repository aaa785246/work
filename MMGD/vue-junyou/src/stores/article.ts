import { ref, computed } from 'vue'
import { defineStore } from 'pinia'

export const useArticleStore = defineStore('article', {
  state:()=>({userName:"",article_number:0,email:"",article_title:"",article_content:"",like_count:"",message_count:""})
}
)




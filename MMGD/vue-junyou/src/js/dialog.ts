import router from "@/router";
import type { Ref } from "vue"
import type { Router } from "vue-router"

export function transformRoutes(sec: Ref<number>, path: string): void {
    const reciprocal = sec.value + 1;
    const set = setInterval(() => {
        sec.value--;
        if (sec.value == 0 ||sec.value<0) {
            router.push(path)
        }  
    }, 1000);
    setTimeout(()=>clearInterval(set),reciprocal*1000 + 0.1)
}


export function transformlocation(sec: Ref<number>): void {
    const reciprocal = sec.value + 1;
    const set = setInterval(() => {
        sec.value--;
        if (sec.value <= 0 ) {
            location.reload()
        }  
    }, 1000);
    setTimeout(()=>clearInterval(set),reciprocal*1000 + 1)
}


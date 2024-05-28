
export interface reply{
    message_username:string;
    message_content:string;
    article_number:string;
    viewed:string;
};

export interface articles {
    article_number:number;
    article_title:string;
    article_content:string;
    email:string;
    username:string;
    like_count:string;
    message_count:string;
    cut:string;
}

export interface parsonalArticles {
    article_number:number;
    title:string;
    article_content:string;
    like_couter:string;
    message_couter:string;
    cut:string;
}

export interface message {
    username:string,
    msg_floor:string,
    article_number:number,
    msg_content:string,
}
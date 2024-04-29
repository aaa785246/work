import axios from 'axios';

export const getArticleList = async() =>{
    const response = await fetch("http://localhost:3001/article", {method:"GET",});
    const data = await response.json();
    return data
} 

export const getMessageList = async() =>{
    const response = await fetch("http://localhost:3001/message", {method:"GET",});
    const data = await response.json();
    return data
} 

export const getUserDataList = async() =>{
    const response = await fetch("http://192.168.50.193:8001/api/loginCheck", {method:"POST",});
    const data = await response.json();
    return data
} 

export const getUserReplyList = async() =>{
    const response = await fetch("http://localhost:3001/reply", {method:"GET",});
    const data = await response.json();
    return data
} 




export interface reply{
    message_username:string;
    message_content:string;
    article_number:string;
    viewed:string;
};

export interface articles {
    article_number:string;
    article_title:string;
    article_content:string;
    email:string;
    username:string;
    like_count:string;
    message_count:string;
    cut:string;
}
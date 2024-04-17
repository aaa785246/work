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
    const response = await fetch("http://localhost:3001/userdata", {method:"GET",});
    const data = await response.json();
    return data
} 

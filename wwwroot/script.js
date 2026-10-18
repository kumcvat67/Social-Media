const UpdateButton = document.getElementById("update")
const NickInput = document.getElementById("NickInput")
const TextInput = document.getElementById("TextInput")
const Enter = document.getElementById("enter")
const url = "http://localhost:5036/"
let login 
let usertext

NickInput.addEventListener("input", (event)=>{
    login = event.target.value
})
TextInput.addEventListener("input", (event)=>{
    usertext = event.target.value
})
Enter.addEventListener("click", async ()=>{
    console.log("Enter if click")
    const responce = await fetch("api/post/make", {
        method: "POST",
        headers: {"Content-Type": 'application/json' }, 
        body: JSON.stringify({username:login, text:usertext})
    })
    if (responce.ok){
        console.log("   ok")
        await UpdateLent()
    }
})
UpdateButton.addEventListener("click", async ()=>{
    console.log("update button click")
    await UpdateLent()
})
function AddPost(Nickname, text){
    const Newpost = document.createElement("div")
    Newpost.classList.add("post")
    const nick = document.createElement("button")
    nick.textContent=Nickname
    Newpost.append(nick)
    const newposttext = document.createElement("p")
    newposttext.textContent=text
    Newpost.append(newposttext)
    document.getElementById("lent").append(Newpost)
}
async function UpdateLent(){
    console.log("Update lent")
    const responce = await fetch("api/post/get")
    if (responce.ok){
        console.log("   прийняв")
        document.getElementById("lent").textContent=""
        const posts = await responce.json()
        posts.forEach(post => {
            AddPost(post.username, post.text)
        });
    }
}
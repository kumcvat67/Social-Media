const UpdateButton = document.getElementById("update")
const NickInput = document.getElementById("NickInput")
const TextInput = document.getElementById("TextInput")
const Enter = document.getElementById("enter")
const url = "http://localhost:5036/"
let login 
let password
let usertext
let token = null;

//Sign
document.getElementById("NickInput").addEventListener("input", (event)=>{login = event.target.value})
document.getElementById("Password").addEventListener("input", (event)=>{password=event.target.value})

document.getElementById("SignIn").addEventListener("click", async ()=>{
    console.log("Sign In was clicked")
    const responce = await fetch("api/user/signIn",{
        method:"post",
        headers:{"Content-Type":'application/json'},
        body: JSON.stringify({username:login,password:password})
    })
    if(responce.status===200){
        const data = await responce.json();
        token = data.token;
    }
    else if(responce.status===401){
        alert("Невірний логін або пароль")
    }
})


//make post
document.getElementById("textinput").addEventListener("input", (event)=>{usertext = event.target.value})

Enter.addEventListener("click", async ()=>{
    console.log("Enter if click")

    if(registr==false){return}

    const response = await fetch("api/post/make", {
        method: "POST",
        headers: {"Content-Type": 'application/json' }, 
        body: JSON.stringify({token:token, text:usertext})
    })
    if (response.ok){
        console.log("   ok")
        await UpdateLent()
    }
    else if(response.Unauthorized){
        alert("Помилка авторизації")
    }
    else if(response.BadRequest){
        const data = response.json()
        alert(data.error)
    }
})

//Update lent
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
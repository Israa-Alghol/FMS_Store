async function get() {
    const response = await fetch("https://localhost:44314/api/ProductApi/GetAll")
    const data = await response.json()
    console.log(data)


   
    document.querySelector("#content img").src = data[0].imageUrl
    document.querySelector("#content h5").innerHTML = data[0].name
    document.querySelector("#content p").innerHTML = data[0].description


}
get()
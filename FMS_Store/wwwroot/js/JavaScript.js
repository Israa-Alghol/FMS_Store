const postSection = document.querySelector('#content');


const row = document.querySelector("#row");

async function get() {
    const response = await fetch("https://localhost:44314/api/ProductApi/GetAll");
    const data = await response.json();
    console.log(data);
    data.forEach(p => {

        row.innerHTML +=
            `
                <div class="col-4">
                <div class="card" style="width: 18rem;">
                    <img src= "/Uploads/'+${p.data}+'" class="card-img-top" width="250">
                    <div class="card-body">
                        <h5 class="card-title">${p.name}</h5>
                        <p class="card-text">${p.description}</p>
                        <a href="#" class="btn btn-primary">Go somewhere</a>
                    </div>
                </div>
            </div>
            `;       
    })

}
get().then();

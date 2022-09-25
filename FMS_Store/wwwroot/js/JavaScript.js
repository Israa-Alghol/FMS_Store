const postSection = document.querySelector('#content');
const row = document.querySelector("#row");
const cat = document.querySelector("#cat");
var filterInput = document.getElementById("cat");
var values = [];
var Id = [];
var input = "";

async function getcat() {
    const responsecat = await fetch("https://localhost:44314/api/ProductApi/GetCat");
    const datacat = await responsecat.json();
    //console.log(datacat);

    datacat.forEach(c => {
        Id.push(c.id);
        values.push(c.type);
        
        cat.innerHTML +=
        `
        <option>${c.type}</option>
        `
    })
}
function MyData() {
    input = filterInput.value;
    //console.log(input);
    //console.log(Id);
    //console.log(values);
    for (i = 0; i < values.length; i++) {
        if (input == values[i]) {
            //console.log(input)
            //console.log()
            filterProducts(Id[i]).then;
        }
    }
}

async function get() {
    const response = await fetch("https://localhost:44314/api/ProductApi/GetAll");
    const data = await response.json();
    //console.log(data);
    //input = filterInput.value;

    data.forEach(p => {

        row.innerHTML +=
            `
                <div class="col-4">
                <div class="card" style="width: 18rem;">
                    <img src= "/Uploads/${p.imageUrl}" class="card-img-top" width="250">
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

async function filterProducts(i) {
    while (row.childNodes.length > 1)
        row.removeChild(row.lastChild);
    const response = await fetch(`https://localhost:44314/api/ProductApi/GetAll?categoryId=${i}`)
    const data = await response.json();
    console.log(data);
    data.forEach(p => {

        row.innerHTML +=
            `
                <div class="col-4">
                <div class="card" style="width: 18rem;">
                    <img src= "/Uploads/${p.imageUrl}" class="card-img-top" width="250">
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

getcat().then();
get().then();

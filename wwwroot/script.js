const apiBaseUrl = "https://localhost:1000/api";
const loadCatsBtn = document.getElementById("btn");
const statusMessage = document.getElementById("statusMessage");
const catList = document.getElementById("catList");

loadCatsBtn.addEventListener("click", loadCats);

async function loadCats() {
    statusMessage.textContent = "loading a cat...";
    catList.innerHTML = "";

    try {
        const response = await fetch(`${apiBaseUrl}/cats`);

        if (!response.ok) {
            throw new Error(`HTTP error: ${reponse.status}`);
        }

        const data = await response.json();

        statusMessage.textContent = `Total cats: ${data.totalCount}`;

        renderCats(data.items);
    }

    catch (error) {
        statusMessage.textContent = `Error: ${error.Message}`
    }
}

function renderCats(cats) {
    catList.innerHTML = "";

    if (cats.length == 0) {
        catList.innerHTML = "No cats found.";
        return;
    }

    cats.forEach(cat => {
        const article = document.createElement("article");
        article.className = "cat-item";

        article.innerHTML = `
        <h3>${cat.name}</h3>
        <div class="cat-meta">Breed: ${cat.breedName} * Id: ${cat.id}</div>
            <p>${cat.description}</p>
        `;

        catList.appendChild(article);
    })
}
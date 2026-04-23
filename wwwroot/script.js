const apiBaseUrl = "https://localhost:1000/api";

const loadCatsBtn = document.getElementById("loadCatsBtn");
const resetBtn = document.getElementById("resetBtn");
const breedFilter = document.getElementById("breedFilter");
const sortBy = document.getElementById("sortBy");
const sortOrder = document.getElementById("sortOrder");
const pageSizeSelect = document.getElementById("pageSize");

const statusMessage = document.getElementById("statusMessage");
const resultInfo = document.getElementById("resultInfo");
const pageInfo = document.getElementById("pageInfo");
const catList = document.getElementById("catList");
const prevBtn = document.getElementById("prevBtn");
const nextBtn = document.getElementById("nextBtn");

let currentPage = 1;
let totalCount = 0;


// Slideshow function
const hero = document.getElementById("hero");
const heroCatName = document.getElementById("heroCatName");
const heroBreed = document.getElementById("heroBreed");
const heroFact = document.getElementById("heroFact");


const heroSlides = [
    {
        image: "https://images.unsplash.com/photo-1519052537078-e6302a4968d4?auto=format&fit=crop&w=1600&q=80",
        name: "Milo",
        breed: "Ragdoll",
        fact: "Ragdolls are known for their calm personality and affectionate nature."
        
    },
    {
        image: "https://images.unsplash.com/photo-1574158622682-e40e69881006?auto=format&fit=crop&w=1600&q=80",
        name: "Luna",
        breed: "Bengal",
        fact: "Bengals are energetic cats with a wild-looking coat and playful behavior."
    },
    {
        image: "https://images.unsplash.com/photo-1511044568932-338cba0ad803?auto=format&fit=crop&w=1600&q=80",
        name: "Oliver",
        breed: "Maine Coon",
        fact: "Maine Coons are large, friendly cats with a fluffy coat and social personality.",
        position: "50% 20"
    }
];

let currentHeroSlide = 0;

function showHeroSlide(index) {
    const slide = heroSlides[index];

    hero.style.backgroundImage = `url("${slide.image}")`;
    hero.style.backgroundPosition = slide.position || "center";
    hero.style.backgroundSize = "cover";
    console.log(slide.position);

    heroCatName.textContent = slide.name;
    heroBreed.textContent = slide.breed;
    heroFact.textContent = slide.fact;
}

function nextHeroSlide() {
    currentHeroSlide++;

    if (currentHeroSlide >= heroSlides.length) {
        currentHeroSlide = 0;
    }

    showHeroSlide(currentHeroSlide);
}

showHeroSlide(currentHeroSlide);
setInterval(nextHeroSlide, 5000);

window.addEventListener('scroll', function () {
    const header = document.querySelector('.site-header');
    if (window.scrollY > 50) {
        header.classList.add('scrolled');
    } else {
        header.classList.remove('scrolled');
    }
});

//---------------------

//loadCatsBtn.addEventListener("click", () => {
//    currentPage = 1;
//    loadCats();
//});

//resetBtn.addEventListener("click", () => {
//    breedFilter.value = "";
//    sortBy.value = "";
//    sortOrder.value = "asc";
//    pageSizeSelect.value = "8";
//    currentPage = 1;
//    loadCats();
//});

//prevBtn.addEventListener("click", () => {
//    if (currentPage > 1) {
//        currentPage--;
//        loadCats();
//    }
//});

//nextBtn.addEventListener("click", () => {
//    const pageSize = Number(pageSizeSelect.value);
//    const totalPages = Math.ceil(totalCount / pageSize);

//    if (currentPage < totalPages) {
//        currentPage++;
//        loadCats();
//    }
//});

//async function loadCats() {
//    statusMessage.textContent = "Laddar katter...";
//    catList.innerHTML = "";

//    try {
//        const url = buildCatsUrl();
//        const response = await fetch(url);

//        if (!response.ok) {
//            throw new Error(`HTTP error: ${ response.status }`);
//        }

//        const data = await response.json();
//        console.log("Cats response:", data);

//        totalCount = data.totalCount ?? 0;

//        resultInfo.textContent = `Totalt antal katter: ${ totalCount }`;
//        pageInfo.textContent = `Sida ${ data.pageNumber } • Visar ${ data.items.length } st`;

//        renderCats(data.items);
//        statusMessage.textContent = "";
//    } catch (error) {
//        console.error(error);
//        statusMessage.textContent = `Fel: ${ error.message }`;
//        catList.innerHTML = `<div class="empty-state">Kunde inte ladda katter.</div>`;
//    }
//}

//function buildCatsUrl() {
//    const url = new URL(`${apiBaseUrl}/cats`);

//    const breedId = breedFilter.value.trim();
//    const selectedSortBy = sortBy.value;
//    const selectedSortOrder = sortOrder.value;
//    const pageSize = pageSizeSelect.value;

//    url.searchParams.set("pageNumber", currentPage);
//    url.searchParams.set("pageSize", pageSize);

//    if (breedId) {
//        url.searchParams.set("breedId", breedId);
//    }

//    if (selectedSortBy) {
//        url.searchParams.set("sortBy", selectedSortBy);
//        url.searchParams.set("sortOrder", selectedSortOrder);
//    }

//    return url.toString();
//}

//function renderCats(cats) {
//    if (!cats || cats.length === 0) {
//        catList.innerHTML = `<div class="empty-state">Inga katter hittades.</div>`;
//        return;
//    }

//    catList.innerHTML = cats.map(cat => {
//        const imageUrl = cat.imageUrl || "https://placehold.co/600x400?text=Cat";

//        return `
//        <article class="cat-card">
//            <img class="cat-image" src="${imageUrl}" alt="${escapeHtml(cat.name)}" />
//            <div class="cat-content">
//                <h3>${escapeHtml(cat.name)}</h3>
//                <div class="cat-meta">
//                    Breed: ${escapeHtml(cat.breedName || "Unknown")} • Id: ${cat.id}
//                </div>
//                <p class="cat-description">${escapeHtml(cat.description)}</p>
//            </div>
//        </article>`
//            ;
//    }).join("");
//}

//function escapeHtml(value) {
//    return String(value)
//        .replace(/&/g, "&amp;")
//        .replace(/</g, "&lt;")
//        .replace(/>/g, "&gt;")
//        .replace(/\"/g, "&quot;")
//        .replace(/'/g, "&#39;");
//}

//loadCats();
function addUniversity(button) {
    let id = button.getAttribute('data-id');
    document.querySelector('a.compare-add[data-id="' + id + '"]').style.display = "none";
    document.querySelector('a.compare-added[data-id="' + id + '"]').style.display = "inline-block";
    let uni = {
        id: button.getAttribute('data-id'),
        name: button.getAttribute('data-name'),
        variety: button.getAttribute('data-variety'),
        availability: button.getAttribute('data-availability'),
        accessability: button.getAttribute('data-accessability'),
        quality: button.getAttribute('data-quality'),
        unions: button.getAttribute('data-unions'),
        cost: button.getAttribute('data-cost'),
        worldrank: button.getAttribute('data-worldrank'),
        countryrank: button.getAttribute('data-countryrank')
    };
    let universities = localStorage.getItem('unicompare');
    if (universities === null) {
        localStorage.setItem('unicompare', JSON.stringify([uni]));
    } else {
        let universitiesArray = JSON.parse(universities);
        universitiesArray.push(uni);
        localStorage.removeItem('unicompare');
        localStorage.setItem('unicompare', JSON.stringify(universitiesArray));
    }
    appendUniversity(uni);
}

function appendUniversity(university) {
    let header = document.createElement('th'),
        button = '<a onclick="removeUniversity(this)" data-id="' + university.id + '"><i class="fa fa-times-circle ml-1 fa-lg"></i></a>';
    header.innerHTML = university.name + button;
    header.classList.add('text-center');
    document.querySelector('#uni-row').appendChild(header);

    let cell = document.createElement('td');
    cell.textContent = university.worldrank;
    document.querySelector('#worldrank-row').appendChild(cell);
    cell = cell.cloneNode();
    cell.textContent = university.countryrank;
    document.querySelector('#countryrank-row').appendChild(cell);
    cell = cell.cloneNode();
    cell.textContent = university.variety;
    document.querySelector('#variety-row').appendChild(cell);
    cell = cell.cloneNode();
    cell.textContent = university.availability;
    document.querySelector('#availability-row').appendChild(cell);
    cell = cell.cloneNode();
    cell.textContent = university.accessability;
    document.querySelector('#accessability-row').appendChild(cell);
    cell = cell.cloneNode();
    cell.textContent = university.quality;
    document.querySelector('#quality-row').appendChild(cell);
    cell = cell.cloneNode();
    cell.textContent = university.unions;
    document.querySelector('#unions-row').appendChild(cell);
    cell = cell.cloneNode();
    cell.textContent = university.cost;
    document.querySelector('#cost-row').appendChild(cell);

}

function removeUniversity(btn) {
    let table = document.querySelector('#uni-compare-wrapper table'),
        universities = JSON.parse(localStorage.getItem('unicompare')),
        index,
        id = btn.getAttribute('data-id');

    //get index in table
    for (let i = 0; i < universities.length; i++) {
        if (universities[i].id == id) {
            index = i;
            universities.splice(index, 1);
            localStorage.removeItem('unicompare');
            localStorage.setItem('unicompare', JSON.stringify(universities));
        }
    }

    //remove column
    for (let i = 0; i < table.rows.length; i++) {
        table.rows[i].deleteCell(index+1);
    }
    document.querySelector('a.compare-add[data-id="' + id + '"]').style.display = "inline-block";
    document.querySelector('a.compare-added[data-id="' + id + '"]').style.display = "none";
}

function displayCompare() {
    var hiddenElements = document.querySelectorAll('.uni-hidden');
    for (let i = 0; i < test.length; i++) {
        hiddenElements[i].classList.remove('d-none')
    }
}

function expandFooter() {
    let footer = document.querySelector('#collapseFooter'),
        hidden = document.querySelectorAll('.uni-hidden'),
        icon = document.querySelector('#expandIcon');
    if (footer.style.transform === "translate(-100%)") {
        //expand
        footer.style.transform = "translate(0%)";
        //footer.style.height = window.innerHeight - document.querySelector('nav').clientHeight + "px";
        for (let i = 0; i < hidden.length; i++) {
            hidden[i].classList.remove('d-none');
        }
    } else {
        //collapse
        footer.style.transform = "translate(-100%)";
        for (let i = 0; i < hidden.length; i++) {
            hidden[i].classList.add('d-none');
        }
    }
}

$(function () {
    let universities = JSON.parse(localStorage.getItem('unicompare')),id;
    for (let i = 0; i < universities.length; i++) {
        appendUniversity(universities[i]);
        id = universities[i].id;
        document.querySelector('a.compare-add[data-id="' + id + '"]').style.display = "none";
        document.querySelector('a.compare-added[data-id="' + id + '"]').style.display = "inline-block";
    }
})
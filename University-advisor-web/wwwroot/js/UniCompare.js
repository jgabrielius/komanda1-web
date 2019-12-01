function addUniversity(button) {
    button.style.opacity = 0;
    let uni = {
        id: button.getAttribute('data-id'),
        name: button.getAttribute('data-name'),
        variety: button.getAttribute('data-variety'),
        availability: button.getAttribute('data-availability'),
        accessability: button.getAttribute('data-accessability'),
        quality: button.getAttribute('data-quality'),
        unions: button.getAttribute('data-unions'),
        cost: button.getAttribute('data-cost')
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
    document.querySelector('button[data-id="' + id + '"]').style.opacity=1;
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
    if (footer.style.height === "20%") {
        //expand
        footer.style.height = window.innerHeight - document.querySelector('nav').clientHeight + "px";
        icon.style.transform = "rotate(180deg)";
        for (let i = 0; i < hidden.length; i++) {
            hidden[i].classList.remove('d-none');
        }
    } else {
        //collapse
        footer.style.height = "20%";
        icon.style.transform = "rotate(0deg)";
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
        document.querySelector('button[data-id="' + id + '"]').style.opacity = 0;
    }
})
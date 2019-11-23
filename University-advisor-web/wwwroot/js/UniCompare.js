function addUniversity(button) {
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
    let header = document.createElement('th');
    header.textContent = university.name;
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
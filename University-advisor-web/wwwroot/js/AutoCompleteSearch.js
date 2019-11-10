const getUniversities = () => $.ajax({
    type: "GET",
    url: "/api/university",
    success: (res) => res
})

const getCourses = () => $.ajax({
    type: "GET",
    url: "/api/course",
    success: (res) => res
})

getUniversities().then(universityData => {
    getCourses().then(courseData => {
        autocomplete(document.getElementById("quickUniversitySearch"), universityData.concat(courseData))
    })
});

function autocomplete(inp, arr) {
    inp.addEventListener("input", function (e) {
        let listOfItems;
        let listItem;
        let countOfShownItems = 0;
        const currentInput = this.value;

        closeAllLists();
        if (!currentInput) { return false; }

        listOfItems = document.createElement("DIV");
        listOfItems.setAttribute("id", this.id + "autocomplete-list");
        listOfItems.setAttribute("class", "autocomplete-items");
        this.parentNode.appendChild(listOfItems);

        arr.forEach(obj => {
            if (obj.name.toLowerCase().includes(currentInput.toLowerCase()) && countOfShownItems < 10) {
                countOfShownItems++;
                const lowerCaseInput = currentInput.toLowerCase();
                const lowerCaseName = obj.name.toLowerCase();
                const nameStart = obj.name.substr(0, lowerCaseName.indexOf(lowerCaseInput));
                const nameMiddle = obj.name.substr(lowerCaseName.indexOf(lowerCaseInput), currentInput.length);
                const nameEnd = obj.name.substr(lowerCaseName.indexOf(lowerCaseInput) + currentInput.length);
                const combinedName = nameStart + "<strong>" + nameMiddle + "</strong>" + nameEnd;

                listItem = document.createElement("DIV");
                listItem.setAttribute("class", "bg-dark");
                listItem.innerHTML += `<a class="text-white" href="/Review/${obj.aspAction}/${obj.itemId}">${combinedName}</a>`;
                listItem.addEventListener("click", function (e) {
                    window.location.href = `/Review/${obj.aspAction}/${obj.itemId}`;
                    closeAllLists();
                });
                listOfItems.appendChild(listItem); 
            }
        })
    });

    function closeAllLists(elmnt) {
        var x = document.getElementsByClassName("autocomplete-items");
        for (var i = 0; i < x.length; i++) {
            if (elmnt != x[i] && elmnt != inp) {
                x[i].parentNode.removeChild(x[i]);
            }
        }
    }

    document.addEventListener("click", function (e) {
        closeAllLists(e.target);
    });
}


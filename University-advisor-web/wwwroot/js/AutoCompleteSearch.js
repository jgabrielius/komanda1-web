let arrayOfElements = [];
const updateInterval = 60000; //60 seconds

const updateArray = () => {
    getUniversities().then(universityData => {
        getCourses().then(courseData => {
            arrayOfElements = universityData.concat(courseData);
        }).always(() => setTimeout(updateArray, updateInterval))
    })
};

const getUniversities = () => $.ajax({
    type: "GET",
    url: "/api/AutoCompleteSeach/universities",
    success: (res) => res
})

const getCourses = () => $.ajax({
    type: "GET",
    url: "/api/AutoCompleteSeach/courses",
    success: (res) => res
})

function autocomplete(inp) {
    inp.addEventListener("input", function (e) {
        let listOfItems, listItem, countOfShownItems = 0;
        const currentInput = this.value;

        closeAllLists();
        if (!currentInput) { return false; }

        listOfItems = document.createElement("DIV");
        listOfItems.setAttribute("id", this.id + "autocomplete-list");
        listOfItems.setAttribute("class", "autocomplete-items");
        this.parentNode.appendChild(listOfItems);

        arrayOfElements.forEach(obj => {
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
                listItem.innerHTML += combinedName;
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

updateArray();
autocomplete(document.getElementById("quickUniversitySearch"));
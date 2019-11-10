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
//autocomplete(document.getElementById("quickUniversitySearch"),
getUniversities().then(universityData => {
    getCourses().then(courseData => {
        autocomplete(document.getElementById("quickUniversitySearch"), universityData.concat(courseData))
    })
});


function autocomplete(inp, arr) {
    /*the autocomplete function takes two arguments,
    the text field element and an array of possible autocompleted values:*/
    var currentFocus;
    /*execute a function when someone writes in the text field:*/
    inp.addEventListener("input", function (e) {
        let listOfItems;
        let listItem;
        let countOfShownItems = 0;
        const currentInput = this.value;
        /*close any already open lists of autocompleted values*/
        closeAllLists();
        if (!currentInput) { return false; }
        currentFocus = -1;
        /*create a DIV element that will contain the items (values):*/
        listOfItems = document.createElement("DIV");
        listOfItems.setAttribute("id", this.id + "autocomplete-list");
        listOfItems.setAttribute("class", "autocomplete-items");
        this.parentNode.appendChild(listOfItems);
        arr.forEach(obj => {
            if (obj.name.toLowerCase().includes(currentInput.toLowerCase()) && countOfShownItems < 10) {
                countOfShownItems++;
                const lowerCaseInput = currentInput.toLowerCase();
                const lowerCaseName = obj.name.toLowerCase();
                listItem = document.createElement("DIV");
                //listItem.setAttribute("class", "bg-dark");
                const nameStart = obj.name.substr(0, lowerCaseName.indexOf(lowerCaseInput));
                const nameMiddle = obj.name.substr(lowerCaseName.indexOf(lowerCaseInput), currentInput.length);
                const nameEnd = obj.name.substr(lowerCaseName.indexOf(lowerCaseInput) + currentInput.length);
                const combinedName = nameStart + "<strong>" + nameMiddle + "</strong>" + nameEnd;
                listItem.innerHTML += `<a class="text-white" href="/Review/${obj.aspAction}/${obj.itemId}">${combinedName}</a>`;

                listItem.addEventListener("click", function (e) {
                    window.location.href = `/Review/${obj.aspAction}/${obj.itemId}`;
                    closeAllLists();
                });
                listOfItems.appendChild(listItem); 
            }
        })
    });
    /*execute a function presses a key on the keyboard:*/
    inp.addEventListener("keydown", function (e) {
        var x = document.getElementById(this.id + "autocomplete-list");
        if (x) x = x.getElementsByTagName("div");
        if (e.keyCode == 40) {
            /*If the arrow DOWN key is pressed,
            increase the currentFocus variable:*/
            currentFocus++;
            /*and and make the current item more visible:*/
            addActive(x);
        } else if (e.keyCode == 38) { //up
            /*If the arrow UP key is pressed,
            decrease the currentFocus variable:*/
            currentFocus--;
            /*and and make the current item more visible:*/
            addActive(x);
        } else if (e.keyCode == 13) {
            /*If the ENTER key is pressed, prevent the form from being submitted,*/
            e.preventDefault();
            if (currentFocus > -1) {
                /*and simulate a click on the "active" item:*/
                if (x) x[currentFocus].click();
            }
        }
    });
    function addActive(x) {
        /*a function to classify an item as "active":*/
        if (!x) return false;
        removeActive(x);
        if (currentFocus >= x.length) currentFocus = 0;
        if (currentFocus < 0) currentFocus = (x.length - 1);
        /*add class "autocomplete-active":*/
        x[currentFocus].classList.add("autocomplete-active");
    }
    function removeActive(x) {
        /*a function to remove the "active" class from all autocomplete items:*/
        for (var i = 0; i < x.length; i++) {
            x[i].classList.remove("autocomplete-active");
        }
    }
    function closeAllLists(elmnt) {
        /*close all autocomplete lists in the document,
        except the one passed as an argument:*/
        var x = document.getElementsByClassName("autocomplete-items");
        for (var i = 0; i < x.length; i++) {
            if (elmnt != x[i] && elmnt != inp) {
                x[i].parentNode.removeChild(x[i]);
            }
        }
    }
    /*execute a function when someone clicks in the document:*/
    document.addEventListener("click", function (e) {
        closeAllLists(e.target);
    });
}


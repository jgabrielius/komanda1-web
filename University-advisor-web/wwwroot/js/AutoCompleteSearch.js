$.ajax({
    type: "GET",
    url: "/api/university",
    success: (data) => autocomplete(document.getElementById("quickUniversitySearch"), data)
})



function autocomplete(inp, arr) {
    /*the autocomplete function takes two arguments,
    the text field element and an array of possible autocompleted values:*/
    var currentFocus;
    /*execute a function when someone writes in the text field:*/
    inp.addEventListener("input", function (e) {
        let listOfItems;
        let listItem;
        const currentInput = this.value;
        /*close any already open lists of autocompleted values*/
        closeAllLists();
        if (!currentInput) { return false; }
        currentFocus = -1;
        /*create a DIV element that will contain the items (values):*/
        listOfItems = document.createElement("DIV");
        listOfItems.setAttribute("id", this.id + "autocomplete-list");
        listOfItems.setAttribute("class", "autocomplete-items");
        /*append the DIV element as a child of the autocomplete container:*/
        this.parentNode.appendChild(listOfItems);
        /*for each item in the array...*/
        arr.forEach(obj => {
            if (obj.name.toLowerCase().includes(currentInput.toLowerCase())) {
                listItem = document.createElement("DIV");
                const nameStart = obj.name.substr(0, obj.name.indexOf(currentInput))
                const nameEnd = obj.name.substr(obj.name.indexOf(currentInput) + currentInput.length)
                const combinedName = nameStart + "<strong>" + currentInput + "</strong>" + nameEnd;
                /*insert a input field that will hold the current array item's value:*/
                //listItem.innerHTML += "<input type='hidden' value='" + obj.name + "'>";
                listItem.innerHTML += `<a asp-controller="Review" asp-action=${obj.AspAction} asp-route-id="${obj.ItemId}">${combinedName}</a>`;
                /*execute a function when someone clicks on the item value (DIV element):
                listItem.addEventListener("click", function (e) {
                    
                    /*insert the value for the autocomplete text field:
                    inp.value = this.getElementsByTagName("input")[0].value;
                    /*close the list of autocompleted values,
                    (or any other open lists of autocompleted values:
                    closeAllLists();
                });
                */
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
        /*start by removing the "active" class on all items:*/
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


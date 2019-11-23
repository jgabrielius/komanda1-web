﻿let arrayOfElements = [];
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

const mergeSort = (array) => {

    const mergeSortedArrays = (leftArray, rightArray) => {
        let sortedArray = [];

        // In case if arrays are not of size 1.
        while (leftArray.length && rightArray.length) {
            let minimumElement = null;

            // Find minimum element of two arrays.
            if ((leftArray[0].index <= rightArray[0].index)) {
                minimumElement = leftArray.shift();
            } else {
                minimumElement = rightArray.shift();
            }

            // Call visiting callback.
            //this.callbacks.visitingCallback(minimumElement);

            // Push the minimum element of two arrays to the sorted array.
            sortedArray.push(minimumElement);
        }

        // If one of two array still have elements we need to just concatenate
        // this element to the sorted array since it is already sorted.
        if (leftArray.length) {
            sortedArray = sortedArray.concat(leftArray);
        }

        if (rightArray.length) {
            sortedArray = sortedArray.concat(rightArray);
        }

        return sortedArray;
    }

    const sort = (originalArray) => {

        if (originalArray.length <= 1) return originalArray;
        console.log
        const middleIndex = Math.floor(originalArray.length / 2);
        const leftArray = originalArray.slice(0, middleIndex);
        const rightArray = originalArray.slice(middleIndex, originalArray.length);

        const leftSortedArray = sort(leftArray);
        const rightSortedArray = sort(rightArray);

        return mergeSortedArrays(leftSortedArray, rightSortedArray);
    }

    return sort(array);
};

function autocomplete(inp) {
    let listOfItems, listItem, arrayOfSelectedItems;
    inp.addEventListener("input", function (e) {
        const currentInput = this.value;
        arrayOfSelectedItems = [];
        closeAllLists();
        if (!currentInput) { return false; }

        listOfItems = document.createElement("DIV");
        listOfItems.setAttribute("id", this.id + "autocomplete-list");
        listOfItems.setAttribute("class", "autocomplete-items");
        this.parentNode.appendChild(listOfItems);

        arrayOfElements.forEach(obj => {
            if (obj.name.toLowerCase().includes(currentInput.toLowerCase())) {
                obj["index"] = obj.name.toLowerCase().indexOf(currentInput.toLowerCase());
                arrayOfSelectedItems.push(obj);
            }
        });

        arrayOfSelectedItems = mergeSort(arrayOfSelectedItems);


        for (i = 0; i < 10; i++) {
            const lowerCaseInput = currentInput.toLowerCase();
            const lowerCaseName = arrayOfSelectedItems[i].name.toLowerCase();
            const nameStart = arrayOfSelectedItems[i].name.substr(0, lowerCaseName.indexOf(lowerCaseInput));
            const nameMiddle = arrayOfSelectedItems[i].name.substr(lowerCaseName.indexOf(lowerCaseInput), currentInput.length);
            const nameEnd = arrayOfSelectedItems[i].name.substr(lowerCaseName.indexOf(lowerCaseInput) + currentInput.length);
            const combinedName = nameStart + "<strong>" + nameMiddle + "</strong>" + nameEnd;

            listItem = document.createElement("DIV");
            listItem.setAttribute("class", "bg-dark");
            listItem.innerHTML += combinedName;
            listItem.addEventListener("click", function (e) {
                window.location.href = `/Review/${arrayOfSelectedItems[i].aspAction}/${arrayOfSelectedItems[i].itemId}`;
                closeAllLists();
            });
            listOfItems.appendChild(listItem)
        }
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
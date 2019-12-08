const getGroups = () => $.ajax({
    type: "GET",
    url: "/api/courses/groups",
    success: (res) => res
}).catch(e => {
    console.log(e);
});

const getCoursesWithDetails = () => $.ajax({
    type: "GET",
    url: "/api/courses/details",
    success: (res) => res
}).catch(e => {
    console.log(e);
});

let groupArray = [];
let courseArray = []

let cities;
let groups;
let directions;

getGroups().then(data => groupArray = data);
getCoursesWithDetails().then(data => courseArray = data);

let selectedCities, selectedGroups, selectedDirections;
const tabIdList = $(".tab-pane").map(function () {
    return this.id;
}).get()

function changeNextBtn() {
    if ($("#groups-tab").hasClass("active")) {
        if ($(".selected[id*='groupBtn']").length !== 0) $("#nextBtn").attr("disabled", false);
        else $("#nextBtn").attr("disabled", true);
    }
    if ($("#directions-tab").hasClass("active")) {
        if ($(".selected[id*='directionBtn']").length !== 0) $("#nextBtn").attr("disabled", false);
        else $("#nextBtn").attr("disabled", true);
    }
}

function selectButton(id) {
    let item = $("#" + id);
    if (item.hasClass("btn-success")) {
        item.removeClass("btn-success");
        item.removeClass("selected")
        item.addClass("btn-primary")
    }
    else if (item.hasClass("btn-primary")) {
        item.removeClass("btn-primary");
        item.addClass("btn-success")
        item.addClass("selected")
    }
    changeNextBtn();
}

$("#nextBtn").on('click', nextBtnClick);

function nextBtnClick() {
    if ($("#cities-tab").hasClass("active")) {
        selectedCities = $(".selected[id*='cityBtn']").map(function () {
            return this.id.replace("cityBtn", "");
        }).get();
    }

    if ($("#groups-tab").hasClass("active")) {
        selectedGroups = $(".selected[id*='groupBtn']").map(function () {
            return this.id.replace("groupBtn", "");
        }).get();

    }

    for (i = 0; i < tabIdList.length; i++) {
        if ($("#" + tabIdList[i]).hasClass("active")) {
            if (i === tabIdList.length - 3) {
                $("#nextBtn").html("Finish")
                $("#nextBtn").off('click').on('click', setPreferences)
                addCourses();
            }
            if (i === 0) $("#backBtn").removeAttr("disabled");
            $("#" + tabIdList[i]).removeClass("active");
            $("#" + tabIdList[i + 1]).addClass("active");
            i = tabIdList.length;
        }
    }
    changeNextBtn();
};

function courseFilter(course) {
    return selectedCities.includes(course.city) && selectedGroups.includes(course.group)
}

function addCourses() {
    //get selected courses
    let filteredArray;
    if (selectedCities.length === 0) filteredArray = courseArray.filter(course => selectedGroups.includes(groupArray.find(item => item.group === course.group).groupId.toString()));
    else filteredArray = courseArray.filter(course => selectedCities.includes(course.city) && selectedGroups.includes(groupArray.find(item => item.group === course.group).groupId.toString()));
    //get unique directions and groups of courses
    directions = [...new Set(filteredArray.map(course => {
        return {
            direction: course.direction,
            group: course.group,
        }
    }))].map(directionInfo => {
        return {
            id: directionInfo.direction.replace(/\ /g, ""),
            name: directionInfo.direction,
            group: directionInfo.group,
        }
    })
    //filter to get only unique ones
    directions = directions.filter((direction, index, self) =>
        index === self.findIndex((t) => (
            t.name === direction.name
        ))
    )

    function createHeading(name) {
        const heading = document.createElement("H3");
        const nameNode = document.createTextNode(name);
        heading.appendChild(nameNode);
        return heading
    }

    directions.sort((a, b) => (a.group > b.group) ? 1 : ((b.group > a.group) ? -1 : 0));
    let directionTab = document.getElementById("directions-container");
    let groupName = ""
    let groupGridBox = null;
    directions.forEach(course => {
        if (course.group !== groupName) {
            if (groupGridBox !== null) directionTab.appendChild(groupGridBox);

            groupName = course.group
            directionTab.appendChild(createHeading(groupName));

            groupGridBox = document.createElement("DIV");
            groupGridBox.setAttribute("class", "grid-fit-max");
        }
        groupGridBox.appendChild(createCardElement(course));
    })
    directionTab.appendChild(groupGridBox);
}

function createCardElement(item) {
    let card = document.createElement("DIV");
    card.setAttribute("class", "card card-preference");

    let cardBody = document.createElement("DIV");
    cardBody.setAttribute("class", "card-body card-preference-body");

    let cardTitle = document.createElement("DIV");
    cardTitle.setAttribute("class", "card-preference-title");

    let cardTitleText = document.createElement("h6");
    cardTitleText.setAttribute("class", "text-center d-flex justify-content-center");
    cardTitleText.innerHTML = item.name;

    let cardButton = document.createElement("input");
    cardButton.type = "button";
    cardButton.value = "Select"
    cardButton.onclick = function (event) {
        return selectButton.call(this, item.id + 'directionBtn');
        return selectButton.call(this, item.id + 'directionBtn');
    };
    cardButton.setAttribute("id", `${item.id}directionBtn`)
    cardButton.setAttribute("class", `btn btn-primary card-preference-btn`)

    cardTitle.appendChild(cardTitleText);
    cardBody.appendChild(cardTitle);
    cardBody.appendChild(cardButton);
    card.appendChild(cardBody);

    return card;
}

function setPreferences() {
    if ($("#directions-tab").hasClass("active")) {
        selectedDirections = $(".selected[id*='directionBtn']").map(function () {
            return this.id.replace("directionBtn", "");
        }).get();

    }
    cities = selectedCities
    directions = directions.filter(direction => selectedDirections.includes(direction.id));
    groups = groupArray.filter(group => selectedGroups.includes(group.groupId.toString()))
    $(function () {
        $("#postGroupPreferences").val(`${groups.map(group => group.group).join()}`);
        $("#postDirectionPreferences").val(`${directions.map(direction => direction.name).join()}`);
        $("#postCityPreferences").val(`${cities.join()}`);
        $("#preferencesForm").submit();
    });
}

$("#backBtn").click(() => {
    $("#nextBtn").attr("disabled", false);
    for (i = 0; i < tabIdList.length; i++) {
        if ($("#" + tabIdList[i]).hasClass("active") && i > 0) {
            if (i === tabIdList.length - 2) {
                $("#nextBtn").html("Next")
                $("#nextBtn").off('click').on('click', nextBtnClick)
                $("#directions-container").empty();
            }
            if (i === 1) $("#backBtn").attr("disabled", true);
            $("#" + tabIdList[i]).removeClass("active");
            $("#" + tabIdList[i - 1]).addClass("active");
            i = tabIdList.length;
        }
    }
})
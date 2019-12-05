let universityArray = [];
let courseArray = [];
const updateInterval = 60000; //60 seconds

$(function () {
    updateArray(() => { renewCourseList(); });
    changeCourseList();
})

const updateArray = (callback) => {
    getUniversities(callback).then(universityData => {
        getCourses().then(courseData => {
            universityArray = universityData;
            courseArray = courseData;
            if (callback !== null) {
                callback();
            }
        }).always(() => setTimeout(updateArray, updateInterval))
    })
};

const getUniversities = () => $.ajax({
    type: "GET",
    url: "/api/Universities/autoCompleteSearch",
    success: (res) => res
})

const getCourses = () => $.ajax({
    type: "GET",
    url: "/api/Courses",
    success: (res) => res
})

function renewCourseList() {
        if(this.value == null) this.value = "Alytus College";
        const universityInfo = universityArray.find(university => university.name === this.value);
        const filteredCourses = courseArray
                                    .filter(course => course.universityId === universityInfo.itemId)
                                    .map(course => course.program);
        $(`#courseSelect`).empty();
        let courseSelectEl = document.getElementById("courseSelect");
        filteredCourses.forEach(name =>
        {
            let opt = document.createElement('option');
            opt.value = name;
            opt.innerHTML = name;
            courseSelectEl.appendChild(opt);
        });
}

function changeCourseList() {
    $("#universitySelect").change(renewCourseList);
};

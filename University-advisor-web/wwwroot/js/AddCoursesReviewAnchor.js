const courses = JSON.parse(document.getElementById('value1').value);
const userCourseId = JSON.parse(document.getElementById('value2').value);

courses.forEach(course => {
    if (course === userCourseId) {
        let reviewTableRow = document.getElementById(`${course}`);
        let aTag = document.createElement('a');
        aTag.setAttribute('class', "btn btn-primary");
        aTag.setAttribute('href', `/Review/CourseReview/${userCourseId}`);
        aTag.innerText = "Review";
        reviewTableRow.appendChild(aTag);
    }
})
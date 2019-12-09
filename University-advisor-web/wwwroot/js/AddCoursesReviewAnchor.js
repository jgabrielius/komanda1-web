﻿courseId = JSON.parse(document.getElementById('value1').value);
userCourseId = JSON.parse(document.getElementById('value2').value);

if (courseId === userCourseId) {

    let reviewDiv = document.getElementById(`${courseId}reviewAnchor`);
    let aTag = document.createElement('a');
    aTag.setAttribute('class', "btn btn-outline-primary");
    aTag.innerText = "Review course";
    aTag.setAttribute('href', `/Review/CourseReview/${userCourseId}`);
    reviewDiv.appendChild(aTag);
}

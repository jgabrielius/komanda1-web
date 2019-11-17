const universityId = JSON.parse(document.getElementById('value1').value);
const userUniversityId = JSON.parse(document.getElementById('value2').value);

if(universityId === userUniversityId)
{
    let reviewDiv = document.getElementById("reviewAnchorDiv");
    let aTag = document.createElement('a');
    aTag.setAttribute('class', "btn btn-lg btn-default");
    aTag.innerText = "Leave a review for university";
    aTag.setAttribute('href',`/Review/UniversityReview/${universityId}`);
    reviewDiv.appendChild(aTag);
}

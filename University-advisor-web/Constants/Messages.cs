using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University_advisor_web.Constants
{
    public static class Messages
    {
        //Log in
        public static string wrongUsernameOrPassword = "Wrong username or password.";
        public static string userLoggedIn = "User logged in.";
        public static string userLogInError = "User log in failed.";
        //Map
        public static string nearbyUniversitiesDisplayed = "Nearby universities were successfully displayed.";
        //Registration
        public static string userRegistered = "User has been sucessfully registered.";
        public static string userRegistrationError = "User cannot be created.";
        //Reviews
        public static string reviewAlreadySubmitted = "You have already submitted this type of review.";
        public static string courseReviewSubmitted = "Course review has been successfully submitted.";
        public static string universityReviewSubmitted = "University review has been successfully submitted.";
        //Email
        public static string newEmailSameAsOldError = "New email can't be the same as old one.";
        public static string emailsDontMatch = "Emails don't match.";
        public static string incorrectEmail = "Incorrect email.";
        public static string sameEmailExists = "Email aleady taken.";
        //Password
        public static string newPasswordSameAsOldError = "New password can't be the same as old one.";
        public static string passwordsDontMatch = "Passwords don't match.";
        public static string incorrectPassword = "Incorrect password.";
        //Username
        public static string newUsernameSameAsOldError = "New username can't be the same as old one.";
        public static string sameUsernameExists = "Username already taken.";
        //FirstName
        public static string newFirstNameSameAsOldError = "New first name can't be the same as old one.";
        //LastName
        public static string newLastNameSameAsOldError = "New last name can't be the same as old one.";
        //Settings
        public static string emailChangeSuccessfull = "Email changed successfully.";
        public static string passwordChangeSuccessfull = "Password changed successfully.";
        public static string universityChangeSuccessfull = "University changed successfully.";
        public static string courseChangeSuccessfull = "Course changed successfully.";
        public static string statusChangeSuccessfull = "Status changed successfully.";
        public static string usernameChangedSuccessfull = "Username changed successfully.";
        public static string firstNameChangedSuccessfull = "First name changed successfully.";
        public static string lastNameChangedSuccessfull = "Last name changed successfully.";
        //Vision API
        public static string visionApiError = "Something is not right with Vision API. Try again later";
        public static string uploadedDocumentIsValid = "Uploaded document is valid. Match is: ";
        public static string uploadedDocumentIsInvalid = "Uploaded document is invalid. Match is only: ";
        //Question
        public static string questionSubmitted = "Question submitted successfully";
    }
}

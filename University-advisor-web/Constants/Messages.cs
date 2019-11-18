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
        //Password
        public static string newPasswordSameAsOldError = "New password can't be the same as old one.";
        public static string passwordsDontMatch = "Passwords don't match.";
        public static string incorrectPassword = "Incorrect password.";
    }
}

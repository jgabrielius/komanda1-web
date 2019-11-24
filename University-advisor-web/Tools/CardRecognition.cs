
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Google.Cloud.Vision.V1;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using University_advisor_web.Interfaces;

namespace University_advisor_web.Tools
{
    public class CardRecognition
    {
        private readonly ILogger _logger;

        public CardRecognition(ILogger logger)
        {
            _logger = logger;
        }
        public ValidationResponse StartStudentCardValidation(IFormFile file)
        {
            var validationResponse = new ValidationResponse();
            try
            {
                using var fileStream = file.OpenReadStream();
                var image = Image.FromStream(fileStream);
                var client = ImageAnnotatorClient.Create();
                var response = client.DetectText(image);
                var stringResponse = TransformToString(response);
                var rate = Match(stringResponse);
                if (rate > 40)
                {
                    validationResponse.Successful = true;
                    validationResponse.SetInformation(rate);
                    return validationResponse;
                }
                else
                {
                    validationResponse.Successful = false;
                    validationResponse.SetInformation(rate);
                    return validationResponse;
                }
            }
            catch (Exception e)
            {
                _logger.Log("Something is not right with Vision API" + e.StackTrace, "ERROR");
                validationResponse.Successful = false;
                return validationResponse;
            }
        }

        // Transforms quite messy response to readable string. 
        private string TransformToString(IReadOnlyList<EntityAnnotation> response)
        {
            var description = String.Empty;
            if (response.Count != 0)
            {
                foreach (var annotation in response)
                {
                    if (annotation.Description != null)
                    {
                        description += annotation.Description;
                    }
                }
            }
            return description;
        }

        // Checks whether provided card contains following fields. Method returns percentage of matched words.
        private int Match(string response)
        {
            int matchCount = 0;
            string[] keyWords = { "lithuanian", "student", "identity", "card", "id", "valid", "personal", "studying", "name", "surname" };
            var culture = CultureInfo.InvariantCulture;
            if (!String.IsNullOrEmpty(response))
            {
                foreach (var keyWord in keyWords)
                {
                    if (culture.CompareInfo.IndexOf(response, keyWord, CompareOptions.IgnoreCase) >= 0)
                    {
                        matchCount++;
                    }
                }
            }
            return matchCount * 10;
        }
    }
}

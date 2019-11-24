
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
        public int StartStudentCardValidation()
        {
            try
            {
                var client = ImageAnnotatorClient.Create();
                var image = Image.FromFile("./Tools/studentCard.jpg");
                var response = client.DetectText(image);
                var stringResponse = TransformToString(response);
                return Match(stringResponse);
            }
            catch (Exception e)
            {
                _logger.Log("Something is not right with Vision API" + e.StackTrace, "ERROR");
                return 0;
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
            return matchCount*10;
        }
    }
}

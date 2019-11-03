using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using University_advisor_web.Interfaces;
using University_advisor_web.Models;

namespace University_advisor_web.Tools
{
    public class Logger : ILogger
    {
        public void Log(string message, string level = "INFO")
        {
            var messageLog = new LogBodyModel
            {
                Timestamp = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"),
                Message = message,
                Level = level,
            };
            var json = CreateElasticLogStructure(messageLog, "custom-log");
            SendToElasticsearch(JsonParser(json));
        }

        public void LogStats(object logObject)
        {
            var json = CreateElasticLogStructure(logObject, "user-statistics");
            SendToElasticsearch(JsonParser(json));
        }

        private List<object> CreateElasticLogStructure(object logObject, string indexName)
        {
            var logList = new List<object>();

            var header = new TemporaryHeader()
            {
                index = new LogHeaderModel()
                {
                    _index = indexName,
                    _type = "_doc",
                    _id = Guid.NewGuid().ToString("N"),
                },
            };

            logList.Add(header);
            logList.Add(logObject);

            return logList;
        }

        private List<string> JsonParser(List<object> logList)
        {
            var jsonList = new List<string>();
            foreach (var item in logList)
            {
                string json = JsonConvert.SerializeObject(item);
                jsonList.Add(json);
            }
            jsonList.Add(Environment.NewLine);
            return jsonList;
        }

        private void SendToElasticsearch(List<string> jsonList)
        {
            try
            {
                var settings = new ConnectionConfiguration(new Uri("http://34.90.149.218:9200")).
                                   BasicAuthentication("user", "JVVyX6FCgBWo").
                                   RequestTimeout(TimeSpan.FromMinutes(2));
                var lowLevelClient = new ElasticLowLevelClient(settings);
                lowLevelClient.Bulk<StringResponse>(PostData.MultiJson(jsonList));
            }
            catch
            {
                // If logging to elasticsearch failed, it could be logged in a file.
                // But I guess that question could be discussed with lecturer.
            }
        }
    }
}

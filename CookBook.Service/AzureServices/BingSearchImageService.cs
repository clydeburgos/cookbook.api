﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.Service.AzureServices
{
    public class BingSearchImageService
    {
        const string subscriptionKey = "ec11e192dd8640e2baad94713f2d8e60";
        const string uriBase = "https://centralus.api.cognitive.microsoft.com/bing/v7.0/images/search";

        public BingSearchImageService()
        {
            //some DI here
        }

        public async Task<ImageSearchResultModel> SearchImage(string keyword)
        {
            var uriQuery = uriBase + "?q=" + Uri.EscapeDataString(keyword);
            WebRequest request = WebRequest.Create(uriQuery);
            request.Headers["Ocp-Apim-Subscription-Key"] = subscriptionKey;
            var response = await request.GetResponseAsync();
            string json = new StreamReader(response.GetResponseStream()).ReadToEnd();

            var searchResult = new ImageSearchResultModel()
            {
                jsonResult = json,
                relevantHeaders = new Dictionary<string, string>()
            };

            // Extract Bing HTTP headers
            foreach (string header in response.Headers)
            {
                if (header.StartsWith("BingAPIs-") || header.StartsWith("X-MSEdge-"))
                    searchResult.relevantHeaders[header] = response.Headers[header];
            }
            return searchResult;
        }
    }

    public class ImageSearchResultModel
    {
        public string jsonResult;
        public Dictionary<string, string> relevantHeaders;
    }
}

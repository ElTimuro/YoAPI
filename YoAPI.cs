using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace YoAPI
{
    /// <summary>
    /// Provides a simple access to the Yo api for yoing subscribers.
    /// </summary>
    public class YoAPIClass
    {
        /// <summary>
        /// Constant url of the Yo api.
        /// </summary>
        private const string YO_API_URL = "http://api.justyo.co/yoall/";

        private static Exception exceptionObject;

        private static HttpResponseMessage response;

        private static bool lastCallSuccessful;

        /// <summary>
        /// This api token associated with your Yo api account.
        /// </summary>
        public static string APIToken { get; set; }

        private static HttpResponseMessage Response
        {
            get
            {
                return response;
            }
        }

        /// <summary>
        /// True if no errors occured in the last call and the response had a success status code.
        /// </summary>
        public static bool LastCallSuccessful
        {
            get
            {
                return lastCallSuccessful;
            }
        }

        /// <summary>
        /// Holds the exception object of any error, that occured during API operations or null if no errors occured.
        /// </summary>
        public static Exception ExceptionObject
        {
            get
            {
                return exceptionObject;
            }
        }

        /// <summary>
        /// Sends a Yo to all subscribers of Yo-Account associated with the given api token set in <see cref="APIToken" />
        /// </summary>
        public static async Task YoALL()
        {
            exceptionObject = null;
            response = null;

            using (var client = new HttpClient())
            {
                if (APIToken == string.Empty || APIToken == null)
                {
                    exceptionObject = new InvalidOperationException("No api token was set. Set the api token using the API_TOKEN property.");
                    return;
                }

                try
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    var requestContent = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("api_token", APIToken) });
                    response = await client.PostAsync(YO_API_URL, requestContent);
                    lastCallSuccessful = response.IsSuccessStatusCode;
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new HttpRequestException("Status code: " + response.StatusCode + " - " + response.ReasonPhrase);
                    }
                }
                catch (Exception ex)
                {
                    lastCallSuccessful = false;
                    exceptionObject = ex;
                }
            }
        }
    }
}

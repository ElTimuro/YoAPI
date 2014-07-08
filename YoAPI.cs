using System;
using System.Collections.Generic;
using System.Net.Http;
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
        private const string YoApiUrl = "http://api.justyo.co/yoall/";

        /// <summary>
        /// The the last occured exception.
        /// </summary>
        private static Exception exceptionObject;

        /// <summary>
        /// The response to the last call.
        /// </summary>
        private static HttpResponseMessage response;

        /// <summary>
        /// True if the last call was successful.
        /// </summary>
        private static bool lastCallSuccessful;

        /// <summary>
        /// Gets or sets the api token associated with your Yo api account.
        /// </summary>
        public static string APIToken { get; set; }

        /// <summary>
        /// Gets a value indicating whether no errors occured in the last call and the response had a success status code.
        /// </summary>
        public static bool LastCallSuccessful
        {
            get
            {
                return lastCallSuccessful;
            }
        }

        /// <summary>
        /// Gets the response to the last call.
        /// </summary>
        public static HttpResponseMessage Response
        {
            get
            {
                return response;
            }
        }

        /// <summary>
        /// Gets the exception object of any error, that occured during API operations or null if no errors occured.
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
        /// Useage: await YoApi.YoAll();
        /// </summary>
        /// <returns>A task that enables you to (optionally) use the await keyword to wait until the call is finished.</returns>
        public static async Task YoAll()
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
                    response = await client.PostAsync(YoApiUrl, requestContent);
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

using System;
using System.Text;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using System.Collections;

namespace goedle_sdk.detail
{
    public class GoogleWrappedHttpClient
    {


    
        public GoogleWrappedHttpClient ()
        {
        }
            
        public void send(postData)
            {
                var request = (HttpWebRequest) WebRequest.Create(GoedleConstants.GOOGLE_MP_TRACK_URL);
                request.Method = "POST";

                var postDataString = postData.Aggregate("", (data, next) => string.Format("{0}&{1}={2}", data, next.Key, HttpUtility.UrlEncode(next.Value))).TrimEnd('&');
                // set the Content-Length header to the correct value
                request.ContentLength = Encoding.UTF8.GetByteCount(postDataString);

                // write the request body to the request
                using (var writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(postDataString);
                }

                try
                {
                    var webResponse = (HttpWebResponse) request.GetResponse();
                    if (webResponse.StatusCode != HttpStatusCode.OK)
                    {
                        throw new HttpException((int) webResponse.StatusCode,
                                                "Google Analytics tracking did not return OK 200");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Google Analytics tracking failed! Because of: "+ ex.ToString())
                }
            }
}
}
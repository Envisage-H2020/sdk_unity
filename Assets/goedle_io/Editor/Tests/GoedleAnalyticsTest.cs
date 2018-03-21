#define TESTING

using SimpleJSON;

namespace goedle_sdk.detail
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;
    using NSubstitute;
    using UnityEngine;
    using UnityEngine.Networking;
    /// <summary>
    /// 
    /// </summary>
    /// 
    [TestFixture]
    public class GoedleAnalyticsTest
    {
        public string app_key;
        public string api_key;
        public string user_id;
        public string app_version;
        public string app_name;
        public int ts;
        public int timezone;
        public string event_name;
        public string event_id;
        public string event_value;
        public string trait_key;
        public string trait_value;
        public string GA_TRACKIND_ID;
        public bool ga_active;
        public int GA_CD_1;
        public int GA_CD_2;
        public string GA_CD_EVENT;
        public string anonymous_id;
        public Dictionary<string, object> goedleAtomExpected;

        /// <summary>
        /// 
        /// </summary>
        /// 
        [SetUp]
        public void SetUp()
        {
            this.app_key = "test_app_key";
            this.user_id = "u1";
            this.app_version = "v_test";
            this.ts = 0;
            this.app_name = "test_app";
            this.timezone = 360000;
            this.event_name = "test_event";
            this.event_id = null;
            this.event_value = null;
            this.ga_active = false;
            this.anonymous_id = null;
            this.trait_key = null;
            this.trait_value = null;
            this.api_key = "test_api_key";
            this.GA_TRACKIND_ID = null;
            this.GA_CD_1 = 0;
            this.GA_CD_2 = 0;
            this.GA_CD_EVENT = "group";
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /*[Test]
        public void CheckLaunchEventReceived()
        {
            var counter = 0;
            IGoedleHttpClient gio_http_client = (new GameObject("GoedleHTTPClient")).AddComponent<GoedleHttpClient>();
            IUnityWebRequests www = Substitute.For<IUnityWebRequests>();
            #if TESTING
            Console.WriteLine("HERE TEST");
            #endif
            string url = null;
            string method = null;
            www.Post(Arg.Do<string>(x => url = x), Arg.Do<string>(x => counter++));
            www.url = ("http://127.0.0.1/track");

            www.responseCode.Returns(200);
            www.isHttpError.Returns(true);
            www.isNetworkError.Returns(true);

            www.When(x => x.SendWebRequest()).Do(x => www.responseCode.Returns(200));

            GoedleAnalytics gio_interface = new GoedleAnalytics(this.api_key, this.app_key, this.user_id, this.app_version, this.GA_TRACKIND_ID, this.app_name, this.GA_CD_1, this.GA_CD_2, this.GA_CD_EVENT, gio_http_client, www);

            Console.WriteLine(method);
            Console.WriteLine(url);
            www.SendWebRequest().Received(1);

        }*/


        [Test]
        public void CheckTrackedContent()
        {
            IGoedleHttpClient gio_http_client = Substitute.For<IGoedleHttpClient>();

            string url = null;
            string content = null;
            string authentification = null;
            GoedleAnalytics gio_interface = new GoedleAnalytics(this.api_key, this.app_key, this.user_id, this.app_version, this.GA_TRACKIND_ID, this.app_name, this.GA_CD_1, this.GA_CD_2, this.GA_CD_EVENT, gio_http_client);

            gio_http_client.sendPost(Arg.Do<string>(x => url = x), Arg.Do<string>(x => content = x), Arg.Do<string>(x => authentification = x));
            gio_interface.track("event");


            var N = JSON.Parse(content);
            Assert.AreEqual("event", N["event"].Value);

            gio_http_client.sendPost(Arg.Do<string>(x => url = x), Arg.Do<string>(x => content = x), Arg.Do<string>(x => authentification = x));
            N = JSON.Parse(content);

            gio_interface.track("event", "event_id");
            N = JSON.Parse(content);
            Assert.AreEqual("event",N["event"].Value);
            Assert.AreEqual("event_id", N["event_id"].Value);

            gio_http_client.sendPost(Arg.Do<string>(x => url = x), Arg.Do<string>(x => content = x), Arg.Do<string>(x => authentification = x));
            gio_interface.track("event", "event_id", "event_value");
            N = JSON.Parse(content);
            Assert.AreEqual("event",N["event"].Value);
            Assert.AreEqual("event_id", N["event_id"].Value);
            Assert.AreEqual("event_value", N["event_value"].Value);

        }


        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void CheckTrackedTraits()
        {
            IGoedleHttpClient gio_http_client = Substitute.For<IGoedleHttpClient>();

            string url = null;
            string content = null;
            string authentification = null;

            GoedleAnalytics gio_interface = new GoedleAnalytics(this.api_key, this.app_key, this.user_id, this.app_version, this.GA_TRACKIND_ID, this.app_name, this.GA_CD_1, this.GA_CD_2, this.GA_CD_EVENT, gio_http_client);
            gio_interface.getSceneName();

            gio_http_client.sendPost( Arg.Do<string>(x => url = x), Arg.Do<string>(x => content = x), Arg.Do<string>(x => authentification = x));
           
            gio_interface.trackTraits("first_name", "marc");
            var N = JSON.Parse(content);
            Assert.AreEqual("marc", N["first_name"].Value);
            gio_http_client.sendPost(Arg.Do<string>(x => url = x), Arg.Do<string>(x => content = x), Arg.Do<string>(x => authentification = x));
            gio_interface.trackTraits("last_name", "mueller");
            N = JSON.Parse(content);
            Assert.AreEqual("mueller", N["last_name"].Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void CheckTrackedGroup()
        {
            IGoedleHttpClient gio_http_client = Substitute.For<IGoedleHttpClient>();

            string url = null;
            string content = null;
            string authentification = null;

            GoedleAnalytics gio_interface = new GoedleAnalytics(this.api_key, this.app_key, this.user_id, this.app_version, this.GA_TRACKIND_ID, this.app_name, this.GA_CD_1, this.GA_CD_2, this.GA_CD_EVENT, gio_http_client);

            gio_http_client.sendPost( Arg.Do<string>(x => url = x), Arg.Do<string>(x => content = x), Arg.Do<string>(x => authentification = x));
            gio_interface.trackGroup("school", "ggs goedle");
            var N = JSON.Parse(content);
            Assert.AreEqual("group", N["event"].Value);
            Assert.AreEqual("school", N["event_id"].Value);
            Assert.AreEqual("ggs goedle", N["event_value"].Value);
        }


        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void CheckSetUserId()
        {
            IGoedleHttpClient gio_http_client = Substitute.For<IGoedleHttpClient>();

            string url = null;
            string content = null;
            string authentification = null;

            GoedleAnalytics gio_interface = new GoedleAnalytics(this.api_key, this.app_key, this.user_id, this.app_version, this.GA_TRACKIND_ID, this.app_name, this.GA_CD_1, this.GA_CD_2, this.GA_CD_EVENT, gio_http_client);

            gio_http_client.sendPost(Arg.Do<string>(x => url = x), Arg.Do<string>(x => content = x), Arg.Do<string>(x => authentification = x));
           
            gio_interface.set_user_id("u2");
            var N = JSON.Parse(content);
            Assert.AreEqual("u2", N["user_id"].Value);
            Assert.AreEqual("u1", N["anonymous_id"].Value);
            Assert.AreEqual("identify", N["event"].Value);
        }

        /// <summary>
        /// TODO create an test for async tasks and IENumerators
        /// </summary>
        /// 
        /*
        [Test]
        public void CheckStrategy()
        {
            string strategy_string = "{\"config\": { \"scenario\": \"seashore\" , \"wind_speed\": \"fast\"}, \"id\":1}";
            IUnityWebRequests www = null;

            IGoedleHttpClient gio_http_client = Substitute.For<IGoedleHttpClient>(www);


            GoedleAnalytics gio_interface = new GoedleAnalytics(this.api_key, this.app_key, this.user_id, this.app_version, this.GA_TRACKIND_ID, this.app_name, this.GA_CD_1, this.GA_CD_2, this.GA_CD_EVENT, gio_http_client);
            var N = JSON.Parse(strategy_string);
            gio_http_client.getStrategy(Arg.Any<string>(), Arg.Any<string>()).Returns(N);
            IEnumerator strategy_returned = gio_interface.getStrategy();

            Assert.AreEqual("seashore", N["config"]["scenario"].Value);
            Assert.AreEqual("fast", N["config"]["wind_speed"].Value);
            Assert.AreEqual("1", N["id"].Value);

        }*/

        /// <summary>
        /// Test for testing Google Analytics Tracking
        /// TODO; We need to mock the scence manager
        /// </summary>
        /// 
        [Test]
        public void CheckTrackedContentWithGA()
        {
            IGoedleHttpClient gio_http_client = Substitute.For<IGoedleHttpClient>();
            GA_TRACKIND_ID = "12345";
            GA_CD_1 = 1;
            GA_CD_2 = 2;
            string url = null;
            string url_get = null;
            string content = null;
            string authentification = null;
            event_name = "event_ga";
            event_id = "event_id_ga";
            event_value = "500";

            GoedleAnalytics gio_interface = new GoedleAnalytics(this.api_key, this.app_key, this.user_id, this.app_version, GA_TRACKIND_ID, this.app_name, GA_CD_1, GA_CD_2, this.GA_CD_EVENT, gio_http_client);
            gio_http_client.sendGet( Arg.Do<string>(x => url_get = x));
            gio_http_client.sendPost(Arg.Do<string>(x => url = x), Arg.Do<string>(x => content = x), Arg.Do<string>(x => authentification = x));
            gio_interface.track("event_ga");
            // TODO: We have to mock the gameManager to test also the scence name
            DecodeQueryParametersTest(url_get, 
                new Dictionary<string, string> { 
                { "v", GoedleConstants.GOOGLE_MP_VERSION.ToString() }, 
                {"av", this.app_version},
                {"an", this.app_name},
                {"tid", this.GA_TRACKIND_ID},
                {"t", "event"},
                {"cid",this.user_id},
                {"ec","NoScence"},
                {"ea", event_name}

            });


            var N = JSON.Parse(content);
            Assert.AreEqual("event_ga", N["event"].Value);

            gio_http_client.sendGet( Arg.Do<string>(x => url_get = x));
            gio_http_client.sendPost( Arg.Do<string>(x => url = x), Arg.Do<string>(x => content = x), Arg.Do<string>(x => authentification = x));            
            gio_interface.track(event_name, event_id);

            N = JSON.Parse(content);
            Assert.AreEqual(event_name, N["event"].Value);
            Assert.AreEqual(event_id, N["event_id"].Value);
            DecodeQueryParametersTest(url_get,
            new Dictionary<string, string> {
                { "v", GoedleConstants.GOOGLE_MP_VERSION.ToString() },
                {"av", this.app_version},
                {"an", this.app_name},
                {"tid", this.GA_TRACKIND_ID},
                {"t", "event"},
                {"cid",this.user_id},
                {"el", event_id},
                {"ec","NoScence"},
                {"ea", event_name}
            });

            gio_http_client.sendGet( Arg.Do<string>(x => url_get = x));
            gio_http_client.sendPost( Arg.Do<string>(x => url = x), Arg.Do<string>(x => content = x), Arg.Do<string>(x => authentification = x));
            gio_interface.track(event_name, event_id, event_value);
            N = JSON.Parse(content);
            Assert.AreEqual(event_name, N["event"].Value);
            Assert.AreEqual(event_id, N["event_id"].Value);
            Assert.AreEqual(event_value, N["event_value"].Value);
            DecodeQueryParametersTest(url_get,
            new Dictionary<string, string> {
                {"v", GoedleConstants.GOOGLE_MP_VERSION.ToString() },
                {"av", this.app_version},
                {"an", this.app_name},
                {"tid", this.GA_TRACKIND_ID},
                {"t", "event"},
                {"cid",this.user_id},
                {"el", event_id},
                {"ev", event_value},
                {"ec","NoScence"},
                {"ea", event_name}
            });
                
        }

        /// <summary>
        /// Test for testing Google Analytics Tracking
        /// TODO; We need to mock the scence manager
        /// </summary>
        /// 
        [Test]
        public void CheckTrackedGroupWithGA()
        {
            IGoedleHttpClient gio_http_client = Substitute.For<IGoedleHttpClient>();
            GA_TRACKIND_ID = "12345";
            GA_CD_1 = 1;
            GA_CD_2 = 2;
            string url = null;
            string url_get = null;

            string content = null;
            string authentification = null;
            event_name = "group";
            event_id = "school";
            event_value = "ggs_goedle";

            GoedleAnalytics gio_interface = new GoedleAnalytics(this.api_key, this.app_key, this.user_id, this.app_version, this.GA_TRACKIND_ID, this.app_name, this.GA_CD_1, this.GA_CD_2, this.GA_CD_EVENT, gio_http_client);
            gio_http_client.sendGet( Arg.Do<string>(x => url_get = x));

            gio_http_client.sendPost( Arg.Do<string>(x => url = x), Arg.Do<string>(x => content = x), Arg.Do<string>(x => authentification = x));
            gio_interface.trackGroup(event_id,event_value);
            var N = JSON.Parse(content);
            Assert.AreEqual(event_name, N["event"].Value);
            Assert.AreEqual(event_id, N["event_id"].Value);
            Assert.AreEqual(event_value, N["event_value"].Value);


            // TODO: We have to mock the gameManager to test also the scence name
            DecodeQueryParametersTest(url_get,
                new Dictionary<string, string> {
                { "v", GoedleConstants.GOOGLE_MP_VERSION.ToString() },
                {"av", this.app_version},
                {"an", this.app_name},
                {"tid", this.GA_TRACKIND_ID},
                {"t", "event"},
                {"cid",this.user_id},
                {"ec","NoScence"},
                {"ea", event_name},
                {"cd1", event_id},
                {"cd2", event_value},

            });


        }


        /// <summary>
        /// Test for testing Google Analytics Tracking
        /// TODO; We need to mock the scence manager
        /// </summary>
        /// 
        [Test]
        public void CheckSetUserIdWithGA()
        {
            IGoedleHttpClient gio_http_client = Substitute.For<IGoedleHttpClient>();
            GA_TRACKIND_ID = "12345";
            GA_CD_1 = 1;
            GA_CD_2 = 2;
            string url = null;
            string url_get = null;

            string content = null;
            string authentification = null;
            event_name = "identify";

            GoedleAnalytics gio_interface = new GoedleAnalytics(this.api_key, this.app_key, this.user_id, this.app_version, this.GA_TRACKIND_ID, this.app_name, this.GA_CD_1, this.GA_CD_2, this.GA_CD_EVENT, gio_http_client);
            gio_http_client.sendGet(Arg.Do<string>(x => url_get = x));

            gio_http_client.sendPost(Arg.Do<string>(x => url = x), Arg.Do<string>(x => content = x), Arg.Do<string>(x => authentification = x));

            // u1 is the default user_id now u2 replaces u1 and u1 get the anonymous id
            gio_interface.set_user_id("u2");
            var N = JSON.Parse(content);
            Assert.AreEqual("u2", N["user_id"].Value);
            Assert.AreEqual("u1", N["anonymous_id"].Value);
            Assert.AreEqual(event_name, N["event"].Value);


            // TODO: We have to mock the gameManager to test also the scence name
            DecodeQueryParametersTest(url_get,
                new Dictionary<string, string> {
                { "v", GoedleConstants.GOOGLE_MP_VERSION.ToString() },
                {"av", this.app_version},
                {"an", this.app_name},
                {"tid", this.GA_TRACKIND_ID},
                {"t", "event"},
                {"cid","u1"},
                {"ec","NoScence"},
                {"uid", "u2"},
                {"ea", "identify"}
            });


        }

        /// <summary>
        /// Test for Test decoding function, to check the created get urls
        /// </summary>
        /// 
        [Test]
        public void DecodeQueryParameters()
        {
            DecodeQueryParametersTest("http://test/test.html", new Dictionary<string, string>());
            DecodeQueryParametersTest("http://test/test.html?", new Dictionary<string, string>());
            DecodeQueryParametersTest("http://test/test.html?key=bla/blub.xml", new Dictionary<string, string> { { "key", "bla/blub.xml" } });
            DecodeQueryParametersTest("http://test/test.html?eins=1&zwei=2", new Dictionary<string, string> { { "eins", "1" }, { "zwei", "2" } });
            DecodeQueryParametersTest("http://test/test.html?empty", new Dictionary<string, string> { { "empty", "" } });
            DecodeQueryParametersTest("http://test/test.html?empty=", new Dictionary<string, string> { { "empty", "" } });
            DecodeQueryParametersTest("http://test/test.html?key=1&", new Dictionary<string, string> { { "key", "1" } });
            DecodeQueryParametersTest("http://test/test.html?key=value?&b=c", new Dictionary<string, string> { { "key", "value?" }, { "b", "c" } });
            DecodeQueryParametersTest("http://test/test.html?key=value=what", new Dictionary<string, string> { { "key", "value=what" } });
            DecodeQueryParametersTest("http://www.google.com/search?q=energy+edge&rls=com.microsoft:en-au&ie=UTF-8&oe=UTF-8&startIndex=&startPage=1%22",
                new Dictionary<string, string>
                {
                { "q", "energy+edge" },
                { "rls", "com.microsoft:en-au" },
                { "ie", "UTF-8" },
                { "oe", "UTF-8" },
                { "startIndex", "" },
                { "startPage", "1%22" },
                });
            DecodeQueryParametersTest("http://test/test.html?key=value;key=anotherValue", new Dictionary<string, string> { { "key", "value,anotherValue" } });
        }

        private static void DecodeQueryParametersTest(string uri, Dictionary<string, string> expected)
        {
            Dictionary<string, string> parameters = new Uri(uri).DecodeQueryParameters();
            Assert.AreEqual(expected.Count, parameters.Count, "Wrong parameter count. Uri: {0}", uri);
            foreach (var key in expected.Keys)
            {
                Assert.IsTrue(parameters.ContainsKey(key), "Missing parameter key {0}. Uri: {1}", key, uri);
                Assert.AreEqual(expected[key], parameters[key], "Wrong parameter value for {0}. Uri: {1}", parameters[key], uri);
            }
        }



    }

}
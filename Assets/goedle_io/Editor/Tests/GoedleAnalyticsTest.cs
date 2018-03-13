using SimpleJSON;

namespace goedle_sdk.detail
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;
    using NSubstitute;

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
        [Test]
        public void CheckLaunchEventReceived()
        {
            IGoedleHttpClient Upload = Substitute.For<IGoedleHttpClient>();
            GoedleAnalytics gio_interface = new GoedleAnalytics(this.api_key, this.app_key, this.user_id, this.app_version, this.GA_TRACKIND_ID, this.app_name, this.GA_CD_1, this.GA_CD_2, this.GA_CD_EVENT, Upload);
            Upload.Received(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void CheckTrackEventsReceived()
        {
            IGoedleHttpClient Upload = Substitute.For<IGoedleHttpClient>();
            GoedleAnalytics gio_interface = new GoedleAnalytics(this.api_key, this.app_key, this.user_id, this.app_version, this.GA_TRACKIND_ID, this.app_name, this.GA_CD_1, this.GA_CD_2, this.GA_CD_EVENT, Upload);
            gio_interface.track("event");
            gio_interface.track("event", "event_id");
            gio_interface.track("event", "event_id", "event_value");
            Upload.Received(4);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void CheckTrackedContent()
        {
            IGoedleHttpClient Upload = Substitute.For<IGoedleHttpClient>();
            string content = null;
            string authentification = null;
            Upload.sendPost(Arg.Do<string>(x => content = x),Arg.Do<string>(x => authentification = x));
            GoedleAnalytics gio_interface = new GoedleAnalytics(this.api_key, this.app_key, this.user_id, this.app_version, this.GA_TRACKIND_ID, this.app_name, this.GA_CD_1, this.GA_CD_2, this.GA_CD_EVENT, Upload);
            var N = JSON.Parse(content);
            Assert.AreEqual("u1", N["user_id"].Value);
            Assert.AreEqual(GoedleConstants.BUILD_NR, N["build_nr"].Value);
            Assert.AreEqual("v_test", N["app_version"].Value);


            Upload.sendPost(Arg.Do<string>(x => content = x), Arg.Do<string>(x => authentification = x));
            gio_interface.track("event");
            N = JSON.Parse(content);
            Assert.AreEqual("event", N["event"].Value);

            Upload.sendPost(Arg.Do<string>(x => content = x), Arg.Do<string>(x => authentification = x));
            gio_interface.track("event", "event_id");
            N = JSON.Parse(content);
            Assert.AreEqual("event",N["event"].Value);
            Assert.AreEqual("event_id", N["event_id"].Value);

            Upload.sendPost(Arg.Do<string>(x => content = x), Arg.Do<string>(x => authentification = x));
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
            IGoedleHttpClient Upload = Substitute.For<IGoedleHttpClient>();
            string content = null;
            string authentification = null;
            GoedleAnalytics gio_interface = new GoedleAnalytics(this.api_key, this.app_key, this.user_id, this.app_version, this.GA_TRACKIND_ID, this.app_name, this.GA_CD_1, this.GA_CD_2, this.GA_CD_EVENT, Upload);
            Upload.sendPost(Arg.Do<string>(x => content = x), Arg.Do<string>(x => authentification = x));
            gio_interface.trackTraits(null, null, null, "first_name", "marc");
            var N = JSON.Parse(content);
            Assert.AreEqual("marc", N["first_name"].Value);
            Upload.sendPost(Arg.Do<string>(x => content = x), Arg.Do<string>(x => authentification = x));
            gio_interface.trackTraits(null, null, null, "last_name", "mueller");
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
            IGoedleHttpClient Upload = Substitute.For<IGoedleHttpClient>();
            string content = null;
            string authentification = null;
            GoedleAnalytics gio_interface = new GoedleAnalytics(this.api_key, this.app_key, this.user_id, this.app_version, this.GA_TRACKIND_ID, this.app_name, this.GA_CD_1, this.GA_CD_2, this.GA_CD_EVENT, Upload);
            Upload.sendPost(Arg.Do<string>(x => content = x), Arg.Do<string>(x => authentification = x));
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
        public void CheckChangeUserId()
        {
            IGoedleHttpClient Upload = Substitute.For<IGoedleHttpClient>();
            string content = null;
            string authentification = null;
            GoedleAnalytics gio_interface = new GoedleAnalytics(this.api_key, this.app_key, this.user_id, this.app_version, this.GA_TRACKIND_ID, this.app_name, this.GA_CD_1, this.GA_CD_2, this.GA_CD_EVENT, Upload);
            Upload.sendPost(Arg.Do<string>(x => content = x), Arg.Do<string>(x => authentification = x));
            gio_interface.set_user_id("u2");
            var N = JSON.Parse(content);
            Assert.AreEqual("u2", N["user_id"].Value);
            Assert.AreEqual("u1", N["anonymous_id"].Value);
            Assert.AreEqual("identify", N["event"].Value);
        }


        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void CheckStrategy()
        {
            IGoedleHttpClient Download = Substitute.For<IGoedleHttpClient>();
            string strategy_string = "{\"config\": { \"scenario\": \"seashore\" , \"wind_speed\": \"fast\"}, \"id\":1}";
            var N = JSON.Parse(strategy_string);

            GoedleAnalytics gio_interface = new GoedleAnalytics(this.api_key, this.app_key, this.user_id, this.app_version, this.GA_TRACKIND_ID, this.app_name, this.GA_CD_1, this.GA_CD_2, this.GA_CD_EVENT, Download);
            Download.getStrategy(Arg.Any<string>(), Arg.Any<string>()).Returns(N);
            JSONNode strategy_returned = gio_interface.getStrategy();

            Assert.AreEqual("seashore", N["config"]["scenario"].Value);
            Assert.AreEqual("fast", N["config"]["wind_speed"].Value);
            Assert.AreEqual("1", N["id"].Value);

        }

    }

}
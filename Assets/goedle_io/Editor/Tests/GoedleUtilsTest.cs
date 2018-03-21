using System.Text.RegularExpressions;

namespace goedle_sdk.detail
{
    using SimpleJSON;
    using System.Collections.Generic;
    using NUnit.Framework;
    /// <summary>
    /// 
    /// </summary>
    /// 
    [TestFixture]
    public class GoedleUtilsTest
    {
        public string app_key;
        public string user_id;
        public string app_version;
        public int ts;
        public int timezone;
        public string event_name;
        public string event_id;
        public string event_value;
        public string trait_key;
        public string trait_value;
        public bool ga_active;
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
            this.timezone = 360000;
            this.event_name = "test_event";
            this.event_id = null;
            this.event_value = null;
            this.ga_active = false;
            this.anonymous_id = null;
            this.trait_key = null;
            this.trait_value = null;
        }

        /// <summary>
        /// This test checks if float or int returns the right bool
        /// </summary>
        /// 
        [Test]
        public void TestFloatorInt()
        {
            GoedleUtils g_utils = new GoedleUtils();
            Assert.IsFalse(g_utils.IsFloatOrInt("a1234"));
            Assert.IsFalse(g_utils.IsFloatOrInt("0x1234"));
            Assert.IsTrue(g_utils.IsFloatOrInt("1234"));
            Assert.IsTrue(g_utils.IsFloatOrInt("12.34"));
        }

        /// <summary>
        /// This test checks if float or int returns the right bool
        /// </summary>
        /// 
        [Test]
        public void TestCreateUserHash()
        {
            Regex regex = new Regex(@"[[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}");
            string hash = GoedleUtils.userHash("hash");
            Match match = regex.Match(hash);
            Assert.IsTrue(match.Success);
        }


    }
}

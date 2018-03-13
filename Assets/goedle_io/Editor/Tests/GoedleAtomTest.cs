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
    public class GoedleAtomTest 
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
        /// This test creates the simplest goedle Atom with an event
        /// </summary>
        /// 
        [Test]
        public void CreateEventAtom()
        {
            GoedleAtom rt = new GoedleAtom(this.app_key, this.user_id, this.ts, this.event_name, this.event_id, this.event_value, this.timezone, this.app_version, this.anonymous_id, this.trait_key, this.trait_value, this.ga_active);
            JSONNode goedleAtom = rt.getGoedleAtomDictionary();

            Assert.AreEqual(goedleAtom["app_key"].Value, this.app_key);
            Assert.AreEqual(goedleAtom["user_id"].Value, this.user_id);
            Assert.AreEqual(goedleAtom["app_version"].Value, this.app_version);
            Assert.AreEqual(goedleAtom["event"].Value, this.event_name);
            Assert.AreEqual(goedleAtom["timezone"].AsInt, this.timezone);
            Assert.AreEqual(goedleAtom["build_nr"].Value, GoedleConstants.BUILD_NR);
        }

        /// <summary>
        /// This test creates the a goedle Atom with an event and event_id
        /// </summary>
        /// 
        [Test]
        public void CreateEventWithIdAtom() 
        {
            event_id = "test_event_id";
            GoedleAtom rt = new GoedleAtom (this.app_key, this.user_id, this.ts, this.event_name, event_id, this.event_value, this.timezone, this.app_version, this.anonymous_id, this.trait_key, this.trait_value, this.ga_active);
            JSONNode goedleAtom = rt.getGoedleAtomDictionary();
            Assert.AreEqual(goedleAtom["event_id"].Value, event_id);

            //CollectionAssert.AreEquivalent(goedleAtom, this.goedleAtomExpected);
        }

        /// <summary>
        /// This test creates the a goedle Atom with an event, event_id and event_value
        /// </summary>
        /// 
        [Test]
        public void CreateEventWithIdAndValueAtom() 
        {
            event_id = "test_event_id";
            event_value = "test_event_value";
            GoedleAtom rt = new GoedleAtom (this.app_key, this.user_id, this.ts, this.event_name, event_id, event_value, this.timezone, this.app_version, this.anonymous_id, this.trait_key, this.trait_value, this.ga_active);
            JSONNode goedleAtom = rt.getGoedleAtomDictionary();
            //CollectionAssert.AreEquivalent(goedleAtom, this.goedleAtomExpected);
            Assert.AreEqual(goedleAtom["event_id"].Value, event_id);
            Assert.AreEqual(goedleAtom["event_value"].Value, event_value);

        }

        /// <summary>
        /// This test creates the a goedle Atom with an identify and Google Analytics deactivated
        /// </summary>
        /// 
        [Test]
        public void CreateEventIdentifyNoGAAtom() 
        {
            anonymous_id = "123e4567-e89b-12d3-a456-426655440000";
            event_name = "identify"; 
            GoedleAtom rt = new GoedleAtom (this.app_key, this.user_id, this.ts, event_name, this.event_id, this.event_value, this.timezone, this.app_version, anonymous_id, this.trait_key, this.trait_value, this.ga_active);
            JSONNode goedleAtom = rt.getGoedleAtomDictionary();
            //CollectionAssert.AreEquivalent(goedleAtom, this.goedleAtomExpected);
            Assert.AreEqual(goedleAtom["event"].Value, event_name);
            Assert.AreEqual(goedleAtom["anonymous_id"].Value, anonymous_id);

        }

        /// <summary>
        /// This test creates the a goedle Atom with an identify and Google Analytics activated
        /// </summary>
        /// 
        [Test]
        public void CreateEventIdentifyGAAtom() 
        {
            event_name = "identify";
            anonymous_id = "123e4567-e89b-12d3-a456-426655440000";

            this.ga_active = true;
            GoedleAtom rt = new GoedleAtom (this.app_key, this.user_id, this.ts, event_name, this.event_id, this.event_value, this.timezone, this.app_version, anonymous_id, this.trait_key, this.trait_value, this.ga_active);
            JSONNode goedleAtom = rt.getGoedleAtomDictionary();
            //CollectionAssert.AreEquivalent(goedleAtom, this.goedleAtomExpected);
            Assert.AreEqual(goedleAtom["event"].Value, event_name);
            Assert.AreEqual(goedleAtom["anonymous_id"].Value, anonymous_id);

        }


        /// <summary>
        /// This test creates the a goedle Atom with an identify traits
        /// </summary>
        /// 
        [Test]
        public void CreateIdentifyTraitsAtom() 
        {
            event_name = "identify"; 
            trait_key = "first_name";
            trait_value = "Marc";

            GoedleAtom rt = new GoedleAtom (this.app_key, this.user_id, this.ts, this.event_name, this.event_id, this.event_value, this.timezone, this.app_version, this.anonymous_id, this.trait_key, this.trait_value, this.ga_active);
            JSONNode goedleAtom = rt.getGoedleAtomDictionary();
            //CollectionAssert.AreEquivalent(goedleAtom, this.goedleAtomExpected);
            Assert.AreEqual(goedleAtom["event"].Value, event_name);
            Assert.AreEqual(goedleAtom["first_name"].Value, trait_value);


        }
    }
}

namespace goedle_sdk.detail
{
    using System;
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
            this.goedleAtomExpected = new Dictionary<string, object>()
            {
                {"app_key", this.app_key},
                {"user_id", this.user_id},
                {"app_version", this.app_version},
                {"ts", this.ts},
                {"event", this.event_name},
                {"timezone", this.timezone},
                {"build_nr", GoedleConstants.BUILD_NR}
            };
        }

        /// <summary>
        /// This test creates the simplest goedle Atom with an event
        /// </summary>
        /// 
        [Test]
        public void CreateEventAtom() 
        {
            GoedleAtom rt = new GoedleAtom (this.app_key, this.user_id, this.ts, this.event_name, this.event_id, this.event_value, this.timezone, this.app_version, this.anonymous_id, this.trait_key, this.trait_value, this.ga_active);
            Dictionary<string, object> goedleAtom = rt.getGoedleAtomDictionary();
            CollectionAssert.AreEquivalent(goedleAtom, this.goedleAtomExpected);
        }

        /// <summary>
        /// This test creates the a goedle Atom with an event and event_id
        /// </summary>
        /// 
        [Test]
        public void CreateEventWithIdAtom() 
        {
            this.goedleAtomExpected.Add("event_id" , "test_event_id");
            this.event_id = "test_event_id";
            GoedleAtom rt = new GoedleAtom (this.app_key, this.user_id, this.ts, this.event_name, this.event_id, this.event_value, this.timezone, this.app_version, this.anonymous_id, this.trait_key, this.trait_value, this.ga_active);
            Dictionary<string, object> goedleAtom = rt.getGoedleAtomDictionary();
            CollectionAssert.AreEquivalent(goedleAtom, this.goedleAtomExpected);
        }

        /// <summary>
        /// This test creates the a goedle Atom with an event, event_id and event_value
        /// </summary>
        /// 
        [Test]
        public void CreateEventWithIdAndValueAtom() 
        {
            this.goedleAtomExpected.Add("event_id" , "test_event_id");
            this.goedleAtomExpected.Add("event_value" , "test_event_value");
            this.event_id = "test_event_id";
            this.event_value = "test_event_value";
            GoedleAtom rt = new GoedleAtom (this.app_key, this.user_id, this.ts, this.event_name, this.event_id, this.event_value, this.timezone, this.app_version, this.anonymous_id, this.trait_key, this.trait_value, this.ga_active);
            Dictionary<string, object> goedleAtom = rt.getGoedleAtomDictionary();
            CollectionAssert.AreEquivalent(goedleAtom, this.goedleAtomExpected);
        }

        /// <summary>
        /// This test creates the a goedle Atom with an identify and Google Analytics deactivated
        /// </summary>
        /// 
        [Test]
        public void CreateEventIdentifyNoGAAtom() 
        {
            this.goedleAtomExpected["event"] =  "identify";
            this.goedleAtomExpected["anonymous_id"] = "123e4567-e89b-12d3-a456-426655440000";
            this.anonymous_id = "123e4567-e89b-12d3-a456-426655440000";
            this.event_name = "identify"; 
            GoedleAtom rt = new GoedleAtom (this.app_key, this.user_id, this.ts, this.event_name, this.event_id, this.event_value, this.timezone, this.app_version, this.anonymous_id, this.trait_key, this.trait_value, this.ga_active);
            Dictionary<string, object> goedleAtom = rt.getGoedleAtomDictionary();
            CollectionAssert.AreEquivalent(goedleAtom, this.goedleAtomExpected);
        }

        /// <summary>
        /// This test creates the a goedle Atom with an identify and Google Analytics activated
        /// </summary>
        /// 
        [Test]
        public void CreateEventIdentifyGAAtom() 
        {
            this.goedleAtomExpected["event"] =  "identify";
            this.goedleAtomExpected["uuid"] = "123e4567-e89b-12d3-a456-426655440000";
            this.goedleAtomExpected["anonymous_id"] = "123e4567-e89b-12d3-a456-426655440000";
            this.event_name = "identify";
            this.anonymous_id = "123e4567-e89b-12d3-a456-426655440000";

            this.ga_active = true;
            GoedleAtom rt = new GoedleAtom (this.app_key, this.user_id, this.ts, this.event_name, this.event_id, this.event_value, this.timezone, this.app_version, this.anonymous_id, this.trait_key, this.trait_value, this.ga_active);
            Dictionary<string, object> goedleAtom = rt.getGoedleAtomDictionary();
            CollectionAssert.AreEquivalent(goedleAtom, this.goedleAtomExpected);
        }


        /// <summary>
        /// This test creates the a goedle Atom with an identify traits
        /// </summary>
        /// 
        [Test]
        public void CreateIdentifyTraitsAtom() 
        {
            this.event_name = "identify"; 
            this.trait_key = "first_name";
            this.trait_value = "Marc";
            this.goedleAtomExpected["event"] = "identify";
            this.goedleAtomExpected.Add(trait_key , trait_value);

            GoedleAtom rt = new GoedleAtom (this.app_key, this.user_id, this.ts, this.event_name, this.event_id, this.event_value, this.timezone, this.app_version, this.anonymous_id, this.trait_key, this.trait_value, this.ga_active);
            Dictionary<string, object> goedleAtom = rt.getGoedleAtomDictionary();
            CollectionAssert.AreEquivalent(goedleAtom, this.goedleAtomExpected);
        }
    }
}

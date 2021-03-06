/*
 * Licensed to the Apache Software Foundation (ASF) under one or more
 * contributor license agreements.  See the NOTICE file distributed with
 * this work for additional information regarding copyright ownership.
 * The ASF licenses this file to You under the Apache License, Version 2.0
 * (the "License"); you may not use this file except in compliance with
 * the License.  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.IO;
using Xunit;
using PdxTests;
using System.Collections;
using System.Collections.Generic;

namespace Apache.Geode.Client.IntegrationTests
{

    [Trait("Category", "Integration")]
    public class SerializationTests : IDisposable
    {
        private GeodeServer GeodeServer;
        private CacheXml CacheXml;
        private Cache Cache;

        public SerializationTests()
        {
            GeodeServer = new GeodeServer();
            CacheXml = new CacheXml(new FileInfo("cache.xml"), GeodeServer);

            var cacheFactory = new CacheFactory();
            Cache = cacheFactory.Create();
            Cache.InitializeDeclarativeCache(CacheXml.File.FullName);
        }

        public void Dispose()
        {
            try
            {
                if (null != Cache)
                {
                    Cache.Close();
                }
            }
            finally
            {
                try
                {
                    if (null != CacheXml)
                    {
                        CacheXml.Dispose();
                    }
                }
                finally
                {
                    GeodeServer.Dispose();
                }
            }
        }


        private void putAndCheck(IRegion<object, object> region, object key, object value)
        {
            region[key] = value;
            Assert.Equal(value, region[key]);
        }

        [Fact]
        public void BuiltInSerializableTypes()
        {
            var region = Cache.GetRegion<object, object>("testRegion");
            Assert.NotNull(region);

            putAndCheck(region, "CacheableString", "foo");
            putAndCheck(region, "CacheableByte", (Byte)8);
            putAndCheck(region, "CacheableInt16", (Int16)16);
            putAndCheck(region, "CacheableInt32", (Int32)32);
            putAndCheck(region, "CacheableInt64", (Int64)64);
            putAndCheck(region, "CacheableBoolean", (Boolean)true);
            putAndCheck(region, "CacheableCharacter", 'c');
            putAndCheck(region, "CacheableDouble", (Double)1.5);
            putAndCheck(region, "CacheableFloat", (float)2.5);

            putAndCheck(region, "CacheableStringArray", new String[] { "foo", "bar" });
            putAndCheck(region, "CacheableBytes", new Byte[] { 8, 8 });
            putAndCheck(region, "CacheableInt16Array", new Int16[] { 16, 16 });
            putAndCheck(region, "CacheableInt32Array", new Int32[] { 32, 32 });
            putAndCheck(region, "CacheableInt64Array", new Int64[] { 64, 64 });
            putAndCheck(region, "CacheableBooleanArray", new Boolean[] { true, false });
            putAndCheck(region, "CacheableCharacterArray", new Char[] { 'c', 'a' });
            putAndCheck(region, "CacheableDoubleArray", new Double[] { 1.5, 1.7 });
            putAndCheck(region, "CacheableFloatArray", new float[] { 2.5F, 2.7F });

            putAndCheck(region, "CacheableDate", new DateTime());

            putAndCheck(region, "CacheableHashMap", new Dictionary<int, string>() { { 1, "one" }, { 2, "two" } });
            putAndCheck(region, "CacheableHashTable", new Hashtable() { { 1, "one" }, { 2, "two" } });
            putAndCheck(region, "CacheableVector", new ArrayList() { "one", "two" });
            putAndCheck(region, "CacheableArrayList", new List<string>() { "one", "two" });
            putAndCheck(region, "CacheableLinkedList", new LinkedList<object>(new string[] { "one", "two" }));
            putAndCheck(region, "CacheableStack", new Stack<object>(new string[] { "one", "two" }));

            {
                var cacheableHashSet = new CacheableHashSet();
                cacheableHashSet.Add("one");
                cacheableHashSet.Add("two");
                putAndCheck(region, "CacheableHashSet", cacheableHashSet);
            }

            {
                var cacheableLinkedHashSet = new CacheableLinkedHashSet();
                cacheableLinkedHashSet.Add("one");
                cacheableLinkedHashSet.Add("two");
                putAndCheck(region, "CacheableLinkedHashSet", cacheableLinkedHashSet);
            }

            Cache.TypeRegistry.RegisterPdxType(PdxType.CreateDeserializable);
            putAndCheck(region, "PdxType", new PdxType());
        }
    }

}
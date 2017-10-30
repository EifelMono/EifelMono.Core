using System;
using EifelMono.Core;
using Xunit;

namespace Test
{
    public class UnitTestValueX
    {
        [Fact]
        public void TestInt()
        {
            var x = new ValueX<int>();
            Assert.Equal(x.Value, 0);
            Assert.Equal(x.LastValue, 0);
            Assert.Equal(x.IsFirst, true);

            x.Value = 1;
            Assert.Equal(x.Value, 1);
            Assert.Equal(x.LastValue, 0);
            Assert.Equal(x.IsFirst, false);

            x.Value = 2;
            Assert.Equal(x.Value, 2);
            Assert.Equal(x.LastValue, 1);
            Assert.Equal(x.IsFirst, false);

            x.IsFirst = true;
            Assert.Equal(x.Value, 2);
            Assert.Equal(x.LastValue, 1);
            Assert.Equal(x.IsFirst, true);
        }

        [Fact]
        public void TestString()
        {
            var x = new ValueX<string>();
            Assert.Equal(x.Value, "");
            Assert.Equal(x.LastValue, "");
            Assert.Equal(x.IsFirst, true);

            x.Value = "First";
            Assert.Equal(x.Value, "First");
            Assert.Equal(x.LastValue, "");
            Assert.Equal(x.IsFirst, false);

            x.Value = "Second";
            Assert.Equal(x.Value, "Second");
            Assert.Equal(x.LastValue, "First");
            Assert.Equal(x.IsFirst, false);

            x.IsFirst = true;
            Assert.Equal(x.Value, "Second");
            Assert.Equal(x.LastValue, "First");
            Assert.Equal(x.IsFirst, true);
        }

        public class XObj
        {
            public string Name
            {
                get;
                set;
            }
        }
        [Fact]
        public void TestObject()
        {
            var x = new ValueX<XObj>();
            Assert.Equal(x.Value.Name, "");
            Assert.Equal(x.LastValue.Name, "");
            Assert.Equal(x.IsFirst, true);

            x.Value.Name = "First";
            Assert.Equal(x.Value.Name, "First");
            Assert.Equal(x.LastValue.Name, "");
            Assert.Equal(x.IsFirst, false);

            x.Value.Name = "Second";
            Assert.Equal(x.Value.Name, "Second");
            Assert.Equal(x.LastValue.Name, "First");
            Assert.Equal(x.IsFirst, false);

            x.IsFirst = true;
            Assert.Equal(x.Value.Name, "Second");
            Assert.Equal(x.LastValue.Name, "First");
            Assert.Equal(x.IsFirst, true);
        }
    }
}

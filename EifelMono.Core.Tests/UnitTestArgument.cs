
using System;
using Xunit;
using EifelMono.Core.Extensions;
using EifelMono.Core;
using EifelMono.Core.Classes;
using EifelMono.Core.System;

namespace EifelMono.Core.Test
{
    public class UnitTestArgument
    {
        [Fact]
        public void TestArgument()
        {
            var KeyValue = "Hello=World";
            Assert.Equal("World", KeyValue.Argument<string>("Hello"));
            Assert.Equal("World", KeyValue.Argument("Hello"));

            KeyValue = "TestBool=true";
            Assert.True(KeyValue.Argument<bool>("TestBool"));
            Assert.Equal("true", KeyValue.Argument("TestBool"));
            Assert.Equal("true", KeyValue.Argument<string>("TestBool"));

            KeyValue = "SwitchIt=On";
            Assert.Equal("On", KeyValue.Argument("SwitchIt"));
            Assert.Equal(TriState.On, KeyValue.Argument<TriState>("SwitchIt"));
            Assert.Throws<StringExtensions.KeyNotFoundException>(() => KeyValue.Argument<TriState>("SwitchItX"));
            Assert.Equal(TriState.Off, KeyValue.Argument<TriState>("SwitchItX", TriState.Off));

            KeyValue = "SwitchIt=OnX";
            Assert.Equal("OnX", KeyValue.Argument("SwitchIt"));
            Assert.Throws<StringExtensions.ArgumentConvertValueException>(() => KeyValue.Argument<TriState>("SwitchIt"));
            Assert.Equal(TriState.Off, KeyValue.Argument<TriState>("SwitchIt", TriState.Off));

            var CommandLine = "Param1=1 Param11=a Param2=true Param22=Test Param3=false Hello=World";
            Assert.Equal("World", CommandLine.Argument("Hello"));
            Assert.Equal(1, CommandLine.Argument<int>("Param1"));
            Assert.True(CommandLine.Argument<bool>("Param2"));
            Assert.False(CommandLine.Argument<bool>("Param3"));

            Assert.Equal(123, CommandLine.Argument<int>("Param1x", 123));
            Assert.False(CommandLine.Argument<bool>("Param1x", false));
            Assert.True(CommandLine.Argument<bool>("Param1x", true));
        }
    }
}
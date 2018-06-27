using System;
using Xunit;
using EifelMono.Core.Extensions;
using EifelMono.Core;
using EifelMono.Core.Classes;

namespace EifelMono.Core.Test
{
    public class UnitTestArgument
    {
        [Fact]
        public void TestArgument()
        {
            var KeyValue = "Hello=World";
            Assert.Equal(KeyValue.Argument<string>("Hello"), "World");
            Assert.Equal(KeyValue.Argument("Hello"), "World");

            KeyValue = "TestBool=true";
            Assert.Equal(KeyValue.Argument<bool>("TestBool"), true);
            Assert.Equal(KeyValue.Argument("TestBool"), "true");
            Assert.Equal(KeyValue.Argument<string>("TestBool"), "true");

            KeyValue = "SwitchIt=On";
            Assert.Equal(KeyValue.Argument("SwitchIt"), "On");
            Assert.Equal(KeyValue.Argument<TriState>("SwitchIt"), TriState.On);
            Assert.Throws<StringExtensions.KeyNotFoundException>(() => KeyValue.Argument<TriState>("SwitchItX"));
            Assert.Equal(KeyValue.Argument<TriState>("SwitchItX", TriState.Off), TriState.Off);

            KeyValue = "SwitchIt=OnX";
            Assert.Equal(KeyValue.Argument("SwitchIt"), "OnX");
            Assert.Throws<StringExtensions.ArgumentConvertValueException>(()=> KeyValue.Argument<TriState>("SwitchIt"));
            Assert.Equal(KeyValue.Argument<TriState>("SwitchIt", TriState.Off), TriState.Off);

            var CommandLine = "Param1=1 Param11=a Param2=true Param22=Test Param3=false Hello=World";
            Assert.Equal(CommandLine.Argument("Hello"), "World");
            Assert.Equal(CommandLine.Argument<int>("Param1"), 1);
            Assert.Equal(CommandLine.Argument<bool>("Param2"), true);
            Assert.Equal(CommandLine.Argument<bool>("Param3"), false);

            Assert.Equal(CommandLine.Argument<int>("Param1x", 123), 123);
            Assert.Equal(CommandLine.Argument<bool>("Param1x", false), false);
            Assert.Equal(CommandLine.Argument<bool>("Param1x", true), true);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using RS.WPF.Framework.Composition.Types;
using Shouldly;
using System;

namespace RS.WPF.Framework.UnitTest.Composition.Types
{
    [TestClass]
    public class OptionTests
    {
        [TestClass]
        public class ObjectExtensionTests
        {
            [TestMethod]
            public void AsOption_ValueNotNull_ReturnsSome()
            {
                string value = "Hello";

                value.AsOption().IsSome.ShouldBeTrue();
            }

            [TestMethod]
            public void AsOption_ValueIsNull_ReturnsNone()
            {
                string value = null;

                value.AsOption().IsSome.ShouldBeFalse();
            }

            [TestMethod]
            public void AsOption_ValueIs5_ReturnsSomeWithValue5()
            {
                Option<int> five = 5.AsOption();

                ((Some<int>)five).Value.ShouldBe(5);
            }
        }

        [TestClass]
        public class OptionStaticTests
        {
            // Option.Some

            [TestMethod]
            public void Some_ValueNotNull_ReturnsSome()
            {
                Option<string> option = Option.Some("Hello");

                option.IsSome.ShouldBeTrue();
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void Some_ValueIsNull_ThrowsArgumentNullException()
            {
                Option<string> option = Option.Some<string>(null);

                option.IsSome.ShouldBeFalse();
            }

            [TestMethod]
            public void Some_ValueIs5_ReturnsSomeWithValue5()
            {
                Option<int> five = Option.Some(5);

                ((Some<int>)five).Value.ShouldBe(5);
            }

            // Option.None

            [TestMethod]
            public void None_ReturnsOptionNone()
            {
                Option<string> option = Option.None<string>();

                option.IsSome.ShouldBeFalse();
            }
        }

        [TestClass]
        public class OptionExtensionTests
        {
            [TestMethod]
            public void Map_OnSomeInt_ReturnsSomeString()
            {
                Option<int> intOption = Option.Some(5);
                Option<string> strOption = intOption.Map(nr => nr.ToString());

                strOption.IsSome.ShouldBeTrue();
            }

            [TestMethod]
            public void Map_OnNoneInt_ReturnsNoneString()
            {
                Option<int> intOption = Option.None<int>();
                Option<string> strOption = intOption.Map(nr => nr.ToString());

                strOption.IsSome.ShouldBeFalse();
            }
        }

    }
}

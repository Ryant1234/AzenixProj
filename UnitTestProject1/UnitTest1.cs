using AzenixProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
  

        [TestMethod]
        public void TestFileNameWorks()
        {
            Program program = new Program();
            Assert.IsTrue(program.CheckFileName("test-log-name.log"));
        }


        [TestMethod]
        public void TestIncorrectFileType()
        {
            Program program = new Program();
            Assert.IsFalse(program.CheckFileName("test-log-name.dog"));
        }

        [TestMethod]
        public void TestEmptyFile()
        {
            Program program = new Program();
            Assert.IsFalse(program.CheckFileName(""));
        }
    }
}

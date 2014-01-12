using System;
using Moq;
using KeyboardHook;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KeyCapture.Tests
{
    [TestClass]
    public class KeyCapturerTests
    {
        private readonly Mock _mockKeyCapturer = new Mock<IKeyCapturer>();

        [TestInitialize]
        public void Init()
        {
            
        }

        [TestMethod]
        public void ShouldFireCallback_WhenSingleKeyIsPressed()
        {
            _mockKeyCapturer.Verify();
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Dispose
        }
    }
}

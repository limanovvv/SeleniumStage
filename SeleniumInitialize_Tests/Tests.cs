using OpenQA.Selenium;
using SeleniumInitialize_Builder;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SeleniumInitialize_Tests
{
    public class Tests
    {
        private SeleniumBuilder _builder;
        [SetUp]
        public void Setup()
        {
            _builder = new SeleniumBuilder();
        }
        [TearDown]
        public void TearDown()
        {
            _builder.Dispose();
        }

        [Test(Description = "BuildTest1")]
        public void BuildTest1()
        {
            IWebDriver driver = _builder.Build();
            Assert.IsNotNull(driver);
        }

        [Test(Description = "DisposeTest1")]
        public void DisposeTest1()
        {
            IWebDriver driver = _builder.Build();
            Assert.IsFalse(_builder.IsDisposed);
            _builder.Dispose();
            Assert.IsTrue(_builder.IsDisposed);
            var processes = Process.GetProcesses("chromedriver.exe");
            Assert.IsFalse(processes.Any());
        }

       

        [Test(Description = "PortTest1")]
        public void PortTest1()
        {
            IWebDriver driver = _builder.ChangePort(3737).Build();
            Assert.That(_builder.Port, Is.EqualTo(3737));
        }

        [Test(Description = "PortTest2")]
        public void PortTest2()
        {
            int port = new Random().Next(6000, 32000);
            IWebDriver driver = _builder.ChangePort(port).Build();
            Assert.That(_builder.Port, Is.EqualTo(port));
        }

        [Test(Description = "ArgumentTest1")]
        public void ArgumentTest1()
        {
            string argument = "--start-maximized";
            IWebDriver driver = _builder.SetArgument(argument).Build();
            Assert.Contains(argument, _builder.ChangedArguments);
            var startingSize = driver.Manage().Window.Size;
            driver.Manage().Window.Maximize();
            Assert.That(driver.Manage().Window.Size, Is.EqualTo(startingSize));
        }

        [Test(Description = "UserOptionTest")]
        public void UserOptionTest()
        {
            string key = "safebrowsing.enabled";
            IWebDriver driver = _builder.SetUserOption(key, true).Build();
            Assert.That(_builder.ChangedUserOptions.ContainsKey(key));
            Assert.That(_builder.ChangedUserOptions[key], Is.True);
        }

        [Test(Description = "UserOptionStressTest")]
        public void UserOptionStressTest()
        {
            string key = "safebrowsing.enabled";
            IWebDriver driver = _builder.SetUserOption(key, true)
                .SetUserOption(key, true)
                .Build();
            Assert.That(_builder.ChangedUserOptions.ContainsKey(key));
            Assert.That(_builder.ChangedUserOptions[key], Is.True);
        }

        [Test(Description = "TimeoutTest")]
        public void TimeoutTest()
        {
            TimeSpan timeout = TimeSpan.FromSeconds(20);
            IWebDriver driver = _builder.WithTimeout(timeout).Build();
            Assert.That(driver.Manage().Timeouts().ImplicitWait, Is.EqualTo(timeout));
            Assert.That(_builder.Timeout, Is.EqualTo(timeout));
        }

        [Test(Description = "URLTest")]
        public void URLTest()
        {
            string url = @"https://ib.psbank.ru/store/products/your-cashback-new";
            IWebDriver driver = _builder.WithURL(url).Build();
            Assert.That(driver.Url, Is.EqualTo(url));
            Assert.That(_builder.StartingURL, Is.EqualTo(url));
        }

        [Test(Description = "ComplexTest")]
        public void ComplexTest()
        {
            string url = @"https://ib.psbank.ru/store/products/your-cashback-new";
            string key = "safebrowsing.enabled";
            string argument = "--start-maximized";
            int port = new Random().Next(6000, 32000);
            TimeSpan timeout = TimeSpan.FromSeconds(20);
            IWebDriver driver = _builder.WithTimeout(timeout)
                .WithURL(url)
                .ChangePort(port)
                .SetArgument(argument)
                .SetUserOption(key, true)
                .Build();
            Assert.Multiple(() =>
            {
                Assert.That(driver.Manage().Timeouts().ImplicitWait, Is.EqualTo(timeout));
                Assert.That(_builder.Timeout, Is.EqualTo(timeout));
                Assert.That(driver.Url, Is.EqualTo(url));
                Assert.That(_builder.StartingURL, Is.EqualTo(url));
                Assert.IsTrue(_builder.ChangedArguments.Contains(argument));
                Assert.That(_builder.ChangedUserOptions.ContainsKey(key));
                Assert.That(_builder.ChangedUserOptions[key], Is.True);
            });
        }
    }
}
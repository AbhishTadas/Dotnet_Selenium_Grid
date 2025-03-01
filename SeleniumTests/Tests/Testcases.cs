﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

namespace SeleniumTests.Tests
{
    /// <summary>
    /// This class contains test cases for the Selenium tests using NUnit framework.
    /// </summary>
    [TestFixture("chrome")]
    [TestFixture("firefox")]
    [TestFixture("edge")]
    [Parallelizable(ParallelScope.All)]
    public class Testcases
    {
         public static string LT_USERNAME = Environment.GetEnvironmentVariable("LT_USERNAME") ?? "abhish_tadas";
         public static string LT_ACCESS_KEY = Environment.GetEnvironmentVariable("LT_ACCESS_KEY") ?? "LT_4LfAbpvu9cDmzEF2aB4lYQCQAXYPnPl1Jyj9UWW2sx8bGv6";
         public static bool tunnel = bool.Parse(Environment.GetEnvironmentVariable("LT_TUNNEL") ?? "false");
         public static string build = Environment.GetEnvironmentVariable("LT_BUILD") ?? "LambdatestBuildCrossBrowser";
         public static string seleniumUri = "https://hub.lambdatest.com:443/wd/hub";

        IWebDriver? driver;
        TestLocators? testLocator;
        private string currentBrowser = "chrome";

        public Testcases(string currentBrowser){
            this.currentBrowser= currentBrowser;
        }

        [SetUp]
        /// <summary>
        /// Initializes the WebDriver and sets up the necessary capabilities for the tests.
        /// </summary>
        public void Init()
        {
            DriverOptions capabilities;
            switch (currentBrowser?.ToLower())
            {
                case "chrome":
                    capabilities = new ChromeOptions();
                    break;
                case "firefox":
                    capabilities = new FirefoxOptions();
                    break;
                case "edge":
                    capabilities = new EdgeOptions();
                    break;
                default:
                    throw new ArgumentException("Browser not supported");
            }

            capabilities.BrowserVersion = "dev";
            Dictionary<string, object> ltOptions = new Dictionary<string, object>
            {
                { "username", LT_USERNAME },
                { "accessKey", LT_ACCESS_KEY },
                { "platformName", "Windows 10" },
                { "project", "Selenium101_Test_Project" },
                { "w3c", true },
                { "plugin", "c#-nunit" }
            };

            if (tunnel)
            {
                ltOptions.Add("tunnel", tunnel);
            }
            if (build != null)
            {
                ltOptions.Add("build", build);
            }

            capabilities.AddAdditionalOption("lt:options", ltOptions);
            capabilities.AddAdditionalOption("name",
                string.Format("{0}:{1}",
                TestContext.CurrentContext.Test.ClassName,
                TestContext.CurrentContext.Test.MethodName));

            driver = new RemoteWebDriver(new Uri(seleniumUri), capabilities.ToCapabilities(), TimeSpan.FromSeconds(600));
            Console.Out.WriteLine(driver);
        }

        [Test]
        /// <summary>
        /// Test scenario for simple form submission and validation.
        /// </summary>
        public void TestScenario1()
        {
            testLocator = new TestLocators(driver);
            driver.Navigate().GoToUrl("https://www.lambdatest.com/selenium-playground/");
            driver.Manage().Window.Maximize();
            testLocator.Click(testLocator.SimpleFrmDemo);
            testLocator.EnterText(testLocator.SimplefrmIp, "Welcome to LambdaTest");
            testLocator.Click(testLocator.GetCheckedValue);
            Assert.That(driver.Url, Does.Contain("simple-form-demo"));
            Assert.That(testLocator.SampleMsg.Text, Is.EqualTo("Welcome to LambdaTest"));
        }

        [Test]
        /// <summary>
        /// Test scenario for drag and drop functionality.
        /// </summary>
        public void TestScenario2()
        {
            testLocator = new TestLocators(driver);
            driver.Navigate().GoToUrl("https://www.lambdatest.com/selenium-playground/");
            driver.Manage().Window.Maximize();
            testLocator.Click(testLocator.DragAndDrop);
            Actions action = new Actions(driver);
            action.DragAndDropToOffset(testLocator.Slider, 215, 0).Perform();
            Assert.That(testLocator.SliderValue.GetAttribute("value"), Is.EqualTo("95"));

        }
    
        [Test]
        /// <summary>
        /// Test scenario for input form validation and submission.
        /// </summary>
        public void TestScenario3()
        {
            testLocator = new TestLocators(driver);
            driver.Navigate().GoToUrl("https://www.lambdatest.com/selenium-playground/");
            driver.Manage().Window.Maximize();
            testLocator.Click(testLocator.InputForm);
            testLocator.Click(testLocator.SubmitForm);
            IWebElement activeElement = driver.SwitchTo().ActiveElement();
            var validationMessage = activeElement.GetAttribute("validationMessage");
            Assert.That(validationMessage, Is.EqualTo("Please fill out this field."));
            testLocator.EnterText(testLocator.Name, "Lambda");
            testLocator.EnterText(testLocator.Email, "lambda@email.com");
            testLocator.EnterText(testLocator.Password, "PaSsWoRd");
            testLocator.EnterText(testLocator.Company, "LambdaTest");
            testLocator.EnterText(testLocator.Website, "https://www.lambdatest.com");
            SelectElement s = new SelectElement(testLocator.Country);
            s.SelectByText("United States");
            testLocator.EnterText(testLocator.City, "City");
            testLocator.EnterText(testLocator.Address1, "Address1");
            testLocator.EnterText(testLocator.Address2, "Address2");
            testLocator.EnterText(testLocator.State, "State");
            testLocator.EnterText(testLocator.Zip, "12345");
            testLocator.Click(testLocator.SubmitForm);
            Assert.Multiple(() =>
            {
                Assert.That(testLocator.SuccessMsg.Displayed, Is.True);
                Assert.That(testLocator.SuccessMsg.Text, Is.EqualTo("Thanks for contacting us, we will get back to you shortly."));
            });

        }

        [TearDown]
        /// <summary>
        /// Closes the WebDriver after the tests are executed.
        /// </summary>
        public void Close()
        {
            driver?.Close();
        }
    }
}
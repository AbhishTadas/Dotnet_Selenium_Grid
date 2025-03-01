using OpenQA.Selenium;
namespace SeleniumTests.Tests
{
    public class TestLocators
    {
        private readonly IWebDriver _driver;

        public TestLocators(IWebDriver _driver)
        {
            this._driver = _driver;
        }

        public IWebElement SimpleFrmDemo => _driver.FindElement(By.XPath("//*[contains(text(),'Simple Form Demo')]"));
        public IWebElement SimplefrmIp => _driver.FindElement(By.Id("user-message"));
        public IWebElement GetCheckedValue => _driver.FindElement(By.Id("showInput"));
        public IWebElement SampleMsg => _driver.FindElement(By.Id("message"));
        public IWebElement DragAndDrop => _driver.FindElement(By.XPath("//*[contains(text(),'Drag & Drop Sliders')]"));
        public IWebElement Slider => _driver.FindElement(By.XPath("//*[@id='slider3']/div/input"));
        public IWebElement SliderValue => _driver.FindElement(By.Id("rangeSuccess"));
        public IWebElement InputForm => _driver.FindElement(By.XPath("//*[contains(text(),'Input Form Submit')]"));
        public IWebElement SubmitForm => _driver.FindElement(By.XPath("//*[contains(text(),'Submit')]"));
        public IWebElement Name => _driver.FindElement(By.Id("name"));
        public IWebElement Email => _driver.FindElement(By.Id("inputEmail4"));
        public IWebElement Password => _driver.FindElement(By.Name("password"));
        public IWebElement Company => _driver.FindElement(By.Name("company"));
        public IWebElement Website => _driver.FindElement(By.Name("website"));
        public IWebElement Country => _driver.FindElement(By.Name("country"));
        public IWebElement City => _driver.FindElement(By.Name("city"));
        public IWebElement Address1 => _driver.FindElement(By.Id("inputAddress1"));
        public IWebElement Address2 => _driver.FindElement(By.Id("inputAddress2"));
        public IWebElement State => _driver.FindElement(By.Id("inputState"));
        public IWebElement Zip => _driver.FindElement(By.Name("zip"));
        public IWebElement SuccessMsg => _driver.FindElement(By.XPath("//*[contains(text(),'Thanks for contacting us, we will get back to you shortly.')]"));



        public void Click(IWebElement webElement)
        {
            webElement.Click();
        }

        public void EnterText(IWebElement webElement, string text)
        {
            webElement.SendKeys(text);
        }
    }
}
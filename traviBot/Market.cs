using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using Selenium;
using OpenQA.Selenium;


namespace traviBot
{
    public class Market
    {
        private string marketplaceID;
        Village village;
        IWebDriver driver;

        public Market(Village village)
        {
            this.village = village;
            this.driver = Account.driver;
            this.marketplaceID = this.village.getMarketId();
        }

        public void sendResources(int wood, int clay, int iron, int crop, int x, int y)
        {
            marketplaceID = village.getMarketId();

            this.driver.Navigate().GoToUrl(village.url + "build.php?" + "t=5&" + marketplaceID);
            ReadOnlyCollection<IWebElement> inputs = this.driver.FindElement(By.Name("marketSend")).FindElements(By.TagName("input"));
            int count = 0;
            foreach (IWebElement input in inputs) {
                if (this.driver.FindElement(By.Name("marketSend")).FindElements(By.TagName("input"))[count].GetAttribute("id") == "r1")
                    this.driver.FindElement(By.Name("marketSend")).FindElements(By.TagName("input"))[count].SendKeys(wood.ToString());

                else if (this.driver.FindElement(By.Name("marketSend")).FindElements(By.TagName("input"))[count].GetAttribute("id") == "r2")
                    this.driver.FindElement(By.Name("marketSend")).FindElements(By.TagName("input"))[count].SendKeys(clay.ToString());

                else if (this.driver.FindElement(By.Name("marketSend")).FindElements(By.TagName("input"))[count].GetAttribute("id") == "r3")
                    this.driver.FindElement(By.Name("marketSend")).FindElements(By.TagName("input"))[count].SendKeys(iron.ToString());

                else if (this.driver.FindElement(By.Name("marketSend")).FindElements(By.TagName("input"))[count].GetAttribute("id") == "r4")
                    this.driver.FindElement(By.Name("marketSend")).FindElements(By.TagName("input"))[count].SendKeys(crop.ToString());

                else if (this.driver.FindElement(By.Name("marketSend")).FindElements(By.TagName("input"))[count].GetAttribute("name") == "x")
                    this.driver.FindElement(By.Name("marketSend")).FindElements(By.TagName("input"))[count].SendKeys(x.ToString());

                else if (this.driver.FindElement(By.Name("marketSend")).FindElements(By.TagName("input"))[count].GetAttribute("name") == "y")
                    this.driver.FindElement(By.Name("marketSend")).FindElements(By.TagName("input"))[count].SendKeys(y.ToString());
                count++;
            }
            IWebElement button = this.driver.FindElement(By.Id("enabledButton")); // Pripravi
            button.Click();
            button = this.driver.FindElement(By.Id("enabledButton")); // Pošlji
            button.Click();
        }
    }
}

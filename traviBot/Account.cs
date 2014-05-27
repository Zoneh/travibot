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
    public static class Account
    {
        public static string username { get; set; }
        public static string password { get; set; }
        public static string url { get; set; }
        public static IWebDriver driver;
        public static List<Village> villages { get; set; }
        public static Login login;


        static Account()
        {
            Account.villages = new List<Village>();
            Account.url = "https://ts3.travian.si/";

        }
        public static void Login(string username, string password)
        {
            try
            {
                Account.driver.Navigate().GoToUrl(url);
                var userNameField = Account.driver.FindElement(By.Name("name"));
                var passwordField = Account.driver.FindElement(By.Name("password"));
                var loginButton = Account.driver.FindElement(By.Id("s1"));

                userNameField.SendKeys(username);
                passwordField.SendKeys(password);
                loginButton.Click();
            }
            catch (SeleniumException ex)
            {
                throw ex;
            }
        }


        public static void Start() {
            Account.driver = new FirefoxDriver();

            Account.Login(Account.username, Account.password);
            // Start village creation
            Account.villages = new List<Village>();

            Account.driver.Navigate().GoToUrl(Account.url + "dorf1.php");

            try
            {
                TimeSpan timespan = TimeSpan.FromSeconds(2);
                WebDriverWait wait = new WebDriverWait(Account.driver, timespan);
                wait.Until(ExpectedConditions.ElementIsVisible(By.Id("sidebarBoxVillagelist")));
                Account.driver.FindElement(By.Id("sidebarBoxVillagelist")).FindElement(By.ClassName("addHoverClick")).Click();
                ReadOnlyCollection<IWebElement> villageElements = Account.driver.FindElement(By.Id("sidebarBoxVillagelist")).FindElement(By.ClassName("sidebarBoxInnerBox")).FindElements(By.TagName("li"));

                string name;
                string id;
                string x;
                string y;
                int count = 0;
                foreach (IWebElement village in villageElements)
                {

                    // Parse name and id
                    name = Account.driver.FindElement(By.Id("sidebarBoxVillagelist")).FindElement(By.ClassName("sidebarBoxInnerBox")).FindElements(By.TagName("li"))[count].FindElement(By.ClassName("name")).Text;
                    id = Account.driver.FindElement(By.Id("sidebarBoxVillagelist")).FindElement(By.ClassName("sidebarBoxInnerBox")).FindElements(By.TagName("li"))[count].FindElement(By.TagName("a")).GetAttribute("href");
                    id = id.Replace("&","").Split('=')[1];


                    //Parse X

                    x = Account.driver.FindElement(By.Id("sidebarBoxVillagelist")).FindElement(By.ClassName("sidebarBoxInnerBox")).FindElements(By.TagName("li"))[count].FindElement(By.ClassName("coordinateX")).Text;

                    x = x.Replace("(", "");

                    //Parse Y

                    y = Account.driver.FindElement(By.Id("sidebarBoxVillagelist")).FindElement(By.ClassName("sidebarBoxInnerBox")).FindElements(By.TagName("li"))[count].FindElement(By.ClassName("coordinateY")).Text;
                    y = y.Replace(")", "");

                    Village vil = new Village(id, name, x, y);
                    vil.setNoOfResources();
                    vil.setBuildingsData();
                    vil.setProductionPH();
                    vil.setResourcesData();
                    vil.setWareGranCapacity();
                    villages.Add(vil);
                    count++;
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBoxResult result = System.Windows.MessageBox.Show(ex.Message);
            }

        }
    }
}

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
using System.Globalization;


namespace traviBot
{
    class Map
    {
        Village village;
        IWebDriver driver;

        public string animalFinder(int distance, Village _village, string _animal)
        {
            this.village = _village;
            this.driver = Account.driver;
            string tmp = "";


            for (int y = (int.Parse(this.village.y) - distance); y < (int.Parse(this.village.y) + distance); y++)
            {
                for (int x = (int.Parse(this.village.x) - distance); x < (int.Parse(this.village.x) + distance); x++)
                {
                    this.driver.Navigate().GoToUrl("https://ts3.travian.si/position_details.php?x=" + x + "&y=" + y);
                    string[] str = this.driver.FindElement(By.ClassName("contentContainer")).Text.Split('\n');
                    string fullstring = this.driver.FindElement(By.ClassName("contentContainer")).Text;

                    if (str[2].Contains("Ropaj nezasedeno oazo"))
                    {
                        switch (_animal)
                        {
                            case "Medved":
                                if (fullstring.Contains("Medvedov"))
                                {
                                    tmp += str[0];
                                }
                                break;
                            case "Krokodil":
                                if (fullstring.Contains("Krokodilov"))
                                {
                                    tmp += str[0];
                                }
                                break;
                            case "Tiger":
                                if (fullstring.Contains("Tigrov"))
                                {
                                    tmp += str[0];
                                }
                                break;
                            case "Slon":
                                if (fullstring.Contains("Slonov"))
                                {
                                    tmp += str[0];
                                }
                                break;
                        }
                    }

                    System.Threading.Thread.Sleep(120);
                }
            }

            return tmp;
        }
    }
}

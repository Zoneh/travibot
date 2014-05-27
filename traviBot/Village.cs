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
    public class Village
    {
        private IWebDriver driver;
        public string villageName { get; set; }
        public string villageID { get; set; }
        public string x { get; set; }
        public string y { get; set; }
        public string lumber { get; set; }
        public string clay { get; set; } 
        public string iron { get; set; }
        public string crop { get; set; }
        public string lumberPH { get; set; }
        public string clayPH { get; set; }
        public string ironPH { get; set; } 
        public string cropPH { get; set; }
        public string warehouse { get; set; }
        public string granary { get; set; }
        public string url { get; set; }

        List<Building> Buildings;
        List<Building> resources;

        string[] ownTroops;

        public Village(string VillageID, string VillageName, string X, string Y)
        {
            this.Buildings = new List<Building>();
            this.resources = new List<Building>();
            this.villageID = VillageID;
            this.villageName = VillageName;
            this.url = Account.url;
            this.driver = Account.driver;
            this.x = X;
            this.y = Y;
        }

        public void setBuildingsData()
        {
            this.driver.Navigate().GoToUrl(this.url + "dorf2.php?newdid=" + this.villageID);
            ReadOnlyCollection<IWebElement> areas = this.driver.FindElement(By.Id("clickareas")).FindElements(By.TagName("area")); 

            //TODO: new system
            int count = 0;
            foreach (IWebElement area in areas)
            {
                if (this.driver.FindElement(By.Id("clickareas")).FindElements(By.TagName("area"))[count].GetAttribute("Alt") != "Gradbeno mesto")//če je prazno gre naprej
                {
                    Building building = new Building();
                    string[] splitAlt = this.driver.FindElement(By.Id("clickareas")).FindElements(By.TagName("area"))[count].GetAttribute("Alt").Split('<');

                    building.name = splitAlt[0].Substring(0, (splitAlt[0].Length) - 1);
                    splitAlt = splitAlt[1].Split(' ');
                    building.level = splitAlt[(splitAlt.Length) - 1];
                    building.id = this.driver.FindElement(By.Id("clickareas")).FindElements(By.TagName("area"))[count].GetAttribute("href").Split('=')[1];

                    this.Buildings.Add(building);
                    count++;
                }
                else
                {

                }
            }
        }

        
        public void setResourcesData()
        {
            this.driver.Navigate().GoToUrl(this.url + "dorf1.php?newdid=" + this.villageID);
            ReadOnlyCollection<IWebElement> areas = this.driver.FindElement(By.Id("rx")).FindElements(By.TagName("area"));

            int count = 0;
            foreach (IWebElement area in areas)
            {
                
                Building building = new Building();

                string[] splitAlt = this.driver.FindElement(By.Id("rx")).FindElements(By.TagName("area"))[count].GetAttribute("Alt").Split(' ');
                building.name = splitAlt[0];

                building.level = splitAlt[(splitAlt.Length) - 1];
                if (!(area.GetAttribute("href").Split('=').Length == 1))
                {
                    building.id = area.GetAttribute("href").Split('=')[1];
                }

                this.resources.Add(building);
                count++;
            }
        }

        
        public void setNoOfResources()
        {
            this.driver.Navigate().GoToUrl(this.url + "dorf2.php?newdid=" + this.villageID);
            ReadOnlyCollection<IWebElement> spans = this.driver.FindElement(By.Id("stockBar")).FindElements(By.TagName("span"));

            int count = 0;
            foreach (IWebElement span in spans)
            {
                
                if (this.driver.FindElement(By.Id("stockBar")).FindElements(By.TagName("span"))[count].GetAttribute("id") == "l1")
                    this.lumber = this.driver.FindElement(By.Id("stockBar")).FindElements(By.TagName("span"))[count].Text;
                else if (this.driver.FindElement(By.Id("stockBar")).FindElements(By.TagName("span"))[count].GetAttribute("id") == "l2")
                    this.clay = this.driver.FindElement(By.Id("stockBar")).FindElements(By.TagName("span"))[count].Text;
                else if (this.driver.FindElement(By.Id("stockBar")).FindElements(By.TagName("span"))[count].GetAttribute("id") == "l3")
                    this.iron = this.driver.FindElement(By.Id("stockBar")).FindElements(By.TagName("span"))[count].Text;
                else if (this.driver.FindElement(By.Id("stockBar")).FindElements(By.TagName("span"))[count].GetAttribute("id") == "l4")
                    this.crop = this.driver.FindElement(By.Id("stockBar")).FindElements(By.TagName("span"))[count].Text;
                count++;
            }
        }

        
        public void setWareGranCapacity()
        {
            this.driver.Navigate().GoToUrl(this.url + "dorf1.php?newdid=" + this.villageID);
            IWebElement span = this.driver.FindElement(By.Id("stockBarWarehouse"));

            this.warehouse = span.Text;
            span = this.driver.FindElement(By.Id("stockBarGranary"));
            this.granary = span.Text;
        }

        
        public void setProductionPH()
        {
            this.driver.Navigate().GoToUrl(this.url + "dorf1.php?newdid=" + this.villageID);
            ReadOnlyCollection<IWebElement> tablerows = this.driver.FindElement(By.Id("production")).FindElements(By.ClassName("num"));

            this.lumberPH = tablerows[0].Text;
            this.clayPH = tablerows[1].Text;
            this.ironPH = tablerows[2].Text;
            this.cropPH = tablerows[3].Text;
        }

        public string getMarketId()
        {
            foreach (Building b in this.Buildings)
            {
                if ("Marketplace" == b.name)
                    return b.id;
            }

            return "";
        }
    }
}

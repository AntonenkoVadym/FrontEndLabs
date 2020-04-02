using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace GymWebTests
{
    [TestClass]
    public class GymProjectTest
    {
        IWebDriver driver;
        private readonly string startPage = "https://localhost:44300";
        private readonly string roomsPageCreation = "https://localhost:44300/Rooms/Create";
        private readonly string roomsPage = "https://localhost:44300/Rooms";

        [TestInitialize]
        public void Start_Browser()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(startPage);
        }

        [TestCleanup]
        public void Clean()
        {
            driver.Close();
        }

        [TestMethod]
        public void Test_Start_Page()
        {
            Assert.IsTrue(driver.PageSource.Contains("Welcome to the Gym Project"));
        }

        [TestMethod]
        public void Test_Creating_Room()
        {
            driver.Navigate().GoToUrl(roomsPageCreation);
            driver.FindElement(By.Id("Name")).SendKeys("SeleniumTestCreationName");
            driver.FindElement(By.Id("Floor")).SendKeys("15");
            driver.FindElement(By.Id("Sport")).SendKeys("SeleniumTestCreationSport");
            driver.FindElement(By.Id("submit")).Click();
            Assert.IsTrue(driver.PageSource.Contains("SeleniumTestCreationName"));
            Assert.IsTrue(driver.PageSource.Contains("15"));
            Assert.IsTrue(driver.PageSource.Contains("SeleniumTestCreationSport"));
        }

        [TestMethod]
        public void Test_Editing_Room()
        {
            driver.Navigate().GoToUrl(roomsPageCreation);
            driver.FindElement(By.Id("Name")).SendKeys("EditName");
            driver.FindElement(By.Id("Floor")).SendKeys("155");
            driver.FindElement(By.Id("Sport")).SendKeys("EditSport");
            driver.FindElement(By.Id("submit")).Click();
            driver.Navigate().GoToUrl(roomsPage);
            var elements = driver.FindElement(By.ClassName("table")).FindElements(By.TagName("tr"));
            foreach (var e in elements)
            {
                var flag = false;
                foreach (var e2 in e.FindElements(By.TagName("td")))
                {
                    if (e2.Text.Equals("EditName"))
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    e.FindElement(By.LinkText("Edit")).Click();
                    break;
                }
            }
            var i = driver.FindElement(By.Id("Name"));
            i.Clear();
            i.SendKeys("EditedName");
            i = driver.FindElement(By.Id("Sport"));
            i.Clear();
            i.SendKeys("EditedSport");
            driver.FindElement(By.ClassName("btn")).Click();
            Thread.Sleep(1000);
            Assert.IsTrue(driver.PageSource.Contains("EditedName"));
            Assert.IsTrue(driver.PageSource.Contains("EditedSport"));
        }

        [TestMethod]
        public void Test_Deleting_Room()
        {
            driver.Navigate().GoToUrl(roomsPageCreation);
            driver.FindElement(By.Id("Name")).SendKeys("DeleteName");
            driver.FindElement(By.Id("Floor")).SendKeys("85");
            driver.FindElement(By.Id("Sport")).SendKeys("DeleteSport");
            driver.FindElement(By.Id("submit")).Click();
            driver.Navigate().GoToUrl(roomsPage);
            var elements = driver.FindElement(By.ClassName("table")).FindElements(By.TagName("tr"));
            foreach (var e in elements)
            {
                var flag = false;
                foreach (var e2 in e.FindElements(By.TagName("td")))
                {
                    if (e2.Text.Equals("DeleteName"))
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    e.FindElement(By.LinkText("Delete")).Click();
                    break;
                }
            }
            Assert.IsTrue(driver.PageSource.Contains("DeleteName"));
            Assert.IsTrue(driver.PageSource.Contains("85"));
            Assert.IsTrue(driver.PageSource.Contains("DeleteSport"));
            driver.FindElement(By.Id("submit")).Click();
            Assert.IsFalse(driver.PageSource.Contains("DeleteName"));
            Assert.IsFalse(driver.PageSource.Contains("85"));
            Assert.IsFalse(driver.PageSource.Contains("DeleteSport"));
        }
        [TestMethod]
        public void Test_Detail_Info_Room()
        {
            driver.Navigate().GoToUrl(roomsPageCreation);
            driver.FindElement(By.Id("Name")).SendKeys("DetailName");
            driver.FindElement(By.Id("Floor")).SendKeys("1");
            driver.FindElement(By.Id("Sport")).SendKeys("DetailSport");
            driver.FindElement(By.Id("submit")).Click();
            driver.Navigate().GoToUrl(roomsPage);
            var elements = driver.FindElement(By.ClassName("table")).FindElements(By.TagName("tr"));
            foreach (var e in elements)
            {
                var flag = false;
                foreach (var e2 in e.FindElements(By.TagName("td")))
                {
                    if (e2.Text.Equals("DetailName"))
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    e.FindElement(By.LinkText("Details")).Click();
                    break;
                }
            }
            Assert.IsTrue(driver.PageSource.Contains("DetailName"));
            Assert.IsTrue(driver.PageSource.Contains("1"));
            Assert.IsTrue(driver.PageSource.Contains("DetailSport"));
        }
    }
}

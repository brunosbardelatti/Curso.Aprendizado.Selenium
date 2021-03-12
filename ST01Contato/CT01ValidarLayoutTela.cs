using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using loginGestao.PageObject;
using OpenQA.Selenium.Support.PageObjects;
using System.Configuration;
using loginGestao;

namespace ST01Contato
{
    [TestFixture]
    public class CT01ValidarLayoutTela
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        
        [SetUp]
        public void SetupTest()
        {
            //aqui passamos a chamada do navegador onde se fro GetBrowserLocal, nao precisamos informar a URi"http://10.0.75.1:5555/wd/hub"
            //driver = Comandos.GetBrowserRemote(driver, ConfigurationManager.AppSettings["browser"], "http://10.0.75.1:5555/wd/hub");
            //aqui um exemplo usando a chamada Local
            driver = Comandos.GetBrowserLocal(driver, ConfigurationManager.AppSettings["browser"]);
            baseURL = "https://livros.inoveteste.com.br/";
            verificationErrors = new StringBuilder();
        }
        
        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }
        
        [Test]
        public void TheCT01ValidarLayoutTelaTest()
        {
            // Acessa o site
            driver.Navigate().GoToUrl(baseURL + "/contato");
            // Acessa o menu Contato
            //driver.FindElement(By.CssSelector("em.fa.fa-bars")).Click();
            //driver.FindElement(By.CssSelector("div.sidr-inner > #nav-wrap > #primary_menu > #menu-item-80 > a > span")).Click();
            // Valida o layout da tela
            Assert.AreEqual("Envie uma mensagem", driver.FindElement(By.CssSelector("h1")).Text);

            //Page Object
            Contato contato = new Contato();
            PageFactory.InitElements(driver, contato);

            Assert.IsTrue(contato.name.Enabled);
            Assert.IsTrue(contato.email.Enabled);
            Assert.IsTrue(contato.subject.Enabled);
            Assert.IsTrue(contato.message.Enabled);
            Assert.IsTrue(contato.enviar.Enabled);
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        
        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }
        
        private string CloseAlertAndGetItsText() {
            try {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert) {
                    alert.Accept();
                } else {
                    alert.Dismiss();
                }
                return alertText;
            } finally {
                acceptNextAlert = true;
            }
        }
    }
}
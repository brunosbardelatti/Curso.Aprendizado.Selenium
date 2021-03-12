using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using loginGestao;
using System.Configuration;

namespace ST01Contato
{
    [TestFixture]
    public class CT03EnviarMensagem
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        private CT01ValidarLayoutTela ct01;

        [SetUp]
        public void SetupTest()
        {

            //aqui passamos a chamada do navegador onde se for GetBrowserLocal, nao precisamos informar a URi"http://10.0.75.1:5555/wd/hub"
            //driver = Comandos.GetBrowserRemote(driver, ConfigurationManager.AppSettings["browser"], "http://10.0.75.1:5555/wd/hub");
            //aqui um exemplo usando a chamada Local
            driver = Comandos.GetBrowserLocal(driver, ConfigurationManager.AppSettings["browser"]);
            baseURL = "https://livros.inoveteste.com.br/";
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            verificationErrors = new StringBuilder();
            ct01 = new CT01ValidarLayoutTela();
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
        public void TheCT03EnviarMensagemTest()
        {
            //Pré requisito: validar Layout da tela(irá executaqr o ct01 como sef osse pré requisito)
            //ct01.SetupTest();
            //ct01.TheCT01ValidarLayoutTelaTest();
            //ct01.TeardownTest();
            
            // Acessa o site
            driver.Navigate().GoToUrl(baseURL + "/contato");
            // Acessa o menu Contato
            //driver.FindElement(By.CssSelector("em.fa.fa-bars")).Click();
            //driver.FindElement(By.CssSelector("div.sidr-inner > #nav-wrap > #primary_menu > #menu-item-80 > a > span")).Click();
            // Preenche todos os campos do formulário
            driver.FindElement(By.Name("your-name")).Clear();
            driver.FindElement(By.Name("your-name")).SendKeys(ConfigurationManager.AppSettings["nome"]);
            driver.FindElement(By.Name("your-email")).Clear();
            driver.FindElement(By.Name("your-email")).SendKeys(ConfigurationManager.AppSettings["email"]);
            driver.FindElement(By.Name("your-subject")).Clear();
            driver.FindElement(By.Name("your-subject")).SendKeys(ConfigurationManager.AppSettings["assunto"]);
            driver.FindElement(By.Name("your-message")).Clear();
            driver.FindElement(By.Name("your-message")).SendKeys(ConfigurationManager.AppSettings["mensagem"]);
            // Clica no botão Enviar após preencher todos os campos obrigatórios
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector("input.wpcf7-form-control.wpcf7-submit")));
            driver.FindElement(By.CssSelector("input.wpcf7-form-control.wpcf7-submit")).Click();
            // Valida a mensagem de sucesso do envio da mensagem.
            //wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@id='wpcf7-f372-p24-o1']/form/div[2]")));
            //Assert.AreEqual("Agradecemos a sua mensagem. Responderemos em breve.", driver.FindElement(By.XPath("//div[@id='wpcf7-f372-p24-o1']/form/div[2]")).Text);
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
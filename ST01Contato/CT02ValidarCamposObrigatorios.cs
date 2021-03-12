using System;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using loginGestao;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI; 

namespace ST01Contato
{
    [TestFixture]
    public class CT02ValidarCamposObrigatorios
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        
        [SetUp]
        public void SetupTest()
        {
            //aqui passamos a chamada do navegador onde se for GetBrowserLocal, nao precisamos informar a URi"http://10.0.75.1:5555/wd/hub"
            //driver = Comandos.GetBrowserRemote(driver, ConfigurationManager.AppSettings["browser"], "http://10.0.75.1:5555/wd/hub");
            //aqui um exemplo usando a chamada Local
            driver = Comandos.GetBrowserLocal(driver, ConfigurationManager.AppSettings["browser"]);
            //espera implicita, aloca o tempo definido apra todas as ações do projeto
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            //espera Explicita, ira esperar por um elemento em especifico o tempo determinado.
            //para isso ai precisa ser declarada a variavel e depois chamado o metodo
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
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
        public void TheCT02ValidarCamposObrigatoriosTest()
        {
            // Acessa o site
            driver.Navigate().GoToUrl(baseURL + "/contato");
            // Acessa o menu Contato
            //driver.FindElement(By.CssSelector("em.fa.fa-bars")).Click();
            //driver.FindElement(By.CssSelector("div.sidr-inner > #nav-wrap > #primary_menu > #menu-item-80 > a > span")).Click();
            // Clica no botão Salvar sem preencher os campos obrigatórios
            driver.FindElement(By.CssSelector("input.wpcf7-form-control.wpcf7-submit")).Click();
            // Valida as mensagens de crítica dos campos obrigatórios
            //para usar espera Explicita fazer a chamada de verificação do elemento
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector("span.wpcf7-not-valid-tip")));
            Assert.AreEqual("O campo é obrigatório.", driver.FindElement(By.CssSelector("span.wpcf7-not-valid-tip")).Text);
            Assert.AreEqual("O campo é obrigatório.", driver.FindElement(By.CssSelector("span.wpcf7-form-control-wrap.your-email > span.wpcf7-not-valid-tip")).Text);
            Assert.AreEqual("O campo é obrigatório.", driver.FindElement(By.CssSelector("span.wpcf7-form-control-wrap.your-message > span.wpcf7-not-valid-tip")).Text);
            Assert.AreEqual("O campo é obrigatório.", driver.FindElement(By.CssSelector("span.wpcf7-form-control-wrap.your-subject > span.wpcf7-not-valid-tip")).Text);
            Assert.AreEqual("Um ou mais campos possuem um erro. Verifique e tente novamente.", driver.FindElement(By.XPath("//div[@id='wpcf7-f372-p24-o1']/form/div[2]")).Text);
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
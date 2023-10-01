using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace MetaMask_Bot
{
    public class Tests
    {
        #region Private Variable

        private IWebDriver Driver;
        private Wallets.Metamask_Wallet metamask_Wallet;
        private string MetamaskExtension = "C:\\Users\\asusm\\AppData\\Local\\Google\\Chrome\\User Data\\Default\\Extensions\\nkbihfbeogaeaoehlefnkodbefgpgknn\\10.34.1_0.crx";  // Your MetaMask Wallet Extension File Path
        private string MetamaskHomeURL = "chrome-extension://nkbihfbeogaeaoehlefnkodbefgpgknn/home.html#onboarding/welcome";
        private string MetamaskPopupURL = "chrome-extension://nkbihfbeogaeaoehlefnkodbefgpgknn/Popup.html";

        #endregion

        [SetUp]
        public void Setup()
        {
            string MetamaskExtensionPath = MetamaskExtension;
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddExtension(MetamaskExtensionPath);
            Driver = new ChromeDriver(chromeOptions);
            metamask_Wallet = new Wallets.Metamask_Wallet(Driver);
            Driver.Manage().Window.Maximize();
        }

        [Test, Order(1)]
        public void ImportWallet()
        {
            Driver.Navigate().GoToUrl(MetamaskHomeURL);
            metamask_Wallet.ImportWallet();
            metamask_Wallet.CreateAccounts(5);
            metamask_Wallet.AddNetwork();
            metamask_Wallet.ImportToken();
            Thread.Sleep(500);
            metamask_Wallet.SendMoney();

            Assert.Pass();
        }
    }
}
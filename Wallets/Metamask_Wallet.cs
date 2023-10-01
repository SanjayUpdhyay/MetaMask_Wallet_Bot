using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaMask_Bot.Wallets
{
    internal class Metamask_Wallet
    {
        #region Constructor and Variable
        public IWebDriver Driver;
        public string phaseKey = "askskda jasjkjas jasjkdjsa jasajxkxjsa";  // Your MetaMask Wallet PhaseKey
        public string password = "vcndvh@gasgd";  // Your MetaMask Wallet Password

        public Metamask_Wallet(IWebDriver Driver)
        {
            this.Driver = Driver;
        }

        #endregion

        #region Main Metamask Task
        public void ImportWallet()
        {
            Driver.SwitchTo().Window(Driver.WindowHandles[0]);
            bool isPresent = false;
            while (!isPresent)
            {
                try
                {
                    if (Driver.FindElement(By.Id("onboarding__terms-checkbox")).Displayed)
                    {
                        Driver.FindElement(By.Id("onboarding__terms-checkbox")).Click();
                        Driver.FindElement(By.XPath("//button[text()='Import an existing wallet']")).Click();
                        Driver.FindElement(By.XPath("//button[text()='No thanks']")).Click();
                        isPresent = true;
                    }
                    else Thread.Sleep(200);
                }
                catch { Thread.Sleep(200); }
            }


            string[] phaseKeys = phaseKey.Split(" ");

            for (int i = 0; i < phaseKeys.Length; i++)
            {
                Driver.FindElement(By.Id($"import-srp__srp-word-{i}")).SendKeys(phaseKeys[i]);
            }

            Driver.FindElement(By.XPath("//button[text()='Confirm Secret Recovery Phrase']")).Click();

            Driver.FindElement(By.XPath("//input[@data-testid='create-password-new']")).SendKeys(password);
            Driver.FindElement(By.XPath("//input[@data-testid='create-password-confirm']")).SendKeys(password);
            Driver.FindElement(By.XPath("//input[@data-testid='create-password-terms']")).Click();
            Driver.FindElement(By.XPath("//button[text()='Import my wallet']")).Click();

            isPresent = false;
            while (!isPresent)
            {
                try
                {
                    if (Driver.FindElement(By.XPath("//button[@data-testid='onboarding-complete-done']")).Displayed)
                    {
                        Driver.FindElement(By.XPath("//button[@data-testid='onboarding-complete-done']")).Click();
                        Driver.FindElement(By.XPath("//button[@data-testid='pin-extension-next']")).Click();
                        Driver.FindElement(By.XPath("//button[@data-testid='pin-extension-done']")).Click();
                        Driver.FindElement(By.XPath("//button[text()='Try it out']")).Click();
                        isPresent = true;
                    }
                    else Thread.Sleep(200);
                }
                catch
                {
                    Thread.Sleep(200);
                }
            }

            isPresent = false;
            while (!isPresent)
            {
                try
                {
                    if (Driver.FindElement(By.XPath("//a[text()='No, thanks.']")).Displayed)
                    {
                        Driver.FindElement(By.XPath("//a[text()='No, thanks.']")).Click();
                        Driver.FindElement(By.XPath("//span[@title='Cancel']")).Click();
                        isPresent = true;
                    }
                    else Thread.Sleep(200);
                }
                catch
                {
                    Thread.Sleep(200);
                }
            }
        }


        public void CreateAccounts(int noOfAccount)
        {
            Driver.SwitchTo().Window(Driver.WindowHandles[0]);
            for (int account = 0; account < noOfAccount - 1; account++)
            {
                bool isPresent = false;
                while (!isPresent)
                {
                    try
                    {
                        if (Driver.FindElement(By.XPath("//button[@data-testid='account-menu-icon']")).Displayed)
                        {
                            Driver.FindElement(By.XPath("//button[@data-testid='account-menu-icon']")).Click();
                            Driver.FindElement(By.XPath("//button[@data-testid='multichain-account-menu-popover-add-account']")).Click();
                            Driver.FindElement(By.XPath("//input[@type='text']")).SendKeys("Account " + (account + 2).ToString());
                            Driver.FindElement(By.XPath("//button[text()='Create']")).Click();
                            isPresent = true;
                        }
                        else Thread.Sleep(200);
                    }
                    catch
                    {
                        Thread.Sleep(200);
                    }
                }
            }
        }

        public int CreateAccount(int accountNo)
        {
            Driver.SwitchTo().Window(Driver.WindowHandles[0]);
            bool isPresent = false;
            while (!isPresent)
            {
                try
                {
                    if (Driver.FindElement(By.XPath("//button[@data-testid='account-menu-icon']")).Displayed)
                    {
                        Driver.FindElement(By.XPath("//button[@data-testid='account-menu-icon']")).Click();
                        Driver.FindElement(By.XPath("//button[@data-testid='multichain-account-menu-popover-add-account']")).Click();
                        Driver.FindElement(By.XPath("//input[@type='text']")).SendKeys("Account " + (accountNo + 2).ToString());
                        Driver.FindElement(By.XPath("//button[text()='Create']")).Click();
                        accountNo++;
                        isPresent = true;
                    }
                    else Thread.Sleep(200);
                }
                catch
                {
                    Thread.Sleep(200);
                }
            }
            return accountNo;
        }

        public void ImportAccount(string privateKey)
        {
            Driver.SwitchTo().Window(Driver.WindowHandles[0]);
            bool isPresent = false;
            while (!isPresent)
            {
                try
                {
                    if (Driver.FindElement(By.XPath("//button[@data-testid='account-menu-icon']")).Displayed)
                    {
                        Driver.FindElement(By.XPath("//button[@data-testid='account-menu-icon']")).Click();
                        Driver.FindElement(By.XPath("//button[text()='Import account']")).Click();
                        Driver.FindElement(By.XPath("//input[@id='private-key-box']")).SendKeys(privateKey);
                        Driver.FindElement(By.XPath("//Button[@data-testid='import-account-confirm-button']")).Click();
                        Thread.Sleep(2000);
                        isPresent = true;
                    }
                    else Thread.Sleep(200);
                }
                catch
                {
                    Thread.Sleep(200);
                }
            }
        }

        public void ChangeNetwork(string chainName, bool isFirst)
        {
            Driver.SwitchTo().Window(Driver.WindowHandles[0]);
            bool isPresent = false;

            while (!isPresent)
            {
                try
                {
                    if (Driver.FindElement(By.XPath("//button[@data-testid='network-display']")).Displayed)
                    {
                        Driver.FindElement(By.XPath("//button[@data-testid='network-display']")).Click();
                        Driver.FindElement(By.XPath($"//span[text()='{chainName}']")).Click();
                        isPresent = true;
                    }
                    else Thread.Sleep(200);
                }
                catch { Thread.Sleep(200); }
            }


            isPresent = false;

            while (!isPresent && isFirst)
            {
                try
                {
                    if (Driver.FindElement(By.XPath("//button[text()='Got it']")).Displayed)
                    {
                        Driver.FindElement(By.XPath("//button[text()='Got it']")).Click();
                        isPresent = true;
                    }
                    else Thread.Sleep(200);
                }
                catch { Thread.Sleep(200); }
            }
        }

        public void AddNetwork()
        {
            Driver.SwitchTo().Window(Driver.WindowHandles[0]);
            Driver.FindElement(By.XPath("//button[@data-testid='network-display']")).Click();
            Driver.FindElement(By.XPath("//button[text()='Add network']")).Click();
            Driver.FindElement(By.XPath("//a[@data-testid='add-network-manually']")).Click();

            Driver.FindElement(By.XPath("//input[@class='form-field__input' and @value = '']")).SendKeys("5ireChain");
            Driver.FindElement(By.XPath("//input[@class='form-field__input' and @value = '']")).SendKeys("https://rpc-testnet.5ire.network");
            Driver.FindElement(By.XPath("//input[@class='form-field__input' and @value = '']")).SendKeys("997");
            Driver.FindElement(By.XPath("//input[@class='form-field__input' and @value = '']")).SendKeys("5ire");
            Driver.FindElement(By.XPath("//input[@class='form-field__input' and @value = '']")).SendKeys("https://explorer.5ire.network");
            Driver.FindElement(By.XPath("//button[text()='Save']")).Click();

            bool isPresent = false;
            while (!isPresent)
            {
                try
                {
                    if (Driver.FindElement(By.XPath("//button[@class='button btn--rounded btn-primary home__new-network-added__switch-to-button']")).Displayed)
                    {
                        Driver.FindElement(By.XPath("//button[@class='button btn--rounded btn-primary home__new-network-added__switch-to-button']")).Click();
                        //Driver.FindElement(By.XPath("//button[text()='Got it']")).Click();
                        isPresent = true;
                    }
                    else Thread.Sleep(200);
                }
                catch
                {
                    Thread.Sleep(200);
                }
            }
        }

        public void DisconnectSites()
        {
            Driver.SwitchTo().Window(Driver.WindowHandles[0]);
            bool isPresent = false;
            while (!isPresent)
            {
                try
                {
                    if (Driver.FindElement(By.XPath("//button[@data-testid='account-options-menu-button']")).Displayed)
                    {
                        Driver.FindElement(By.XPath("//button[@data-testid='account-options-menu-button']")).Click();
                        Driver.FindElement(By.XPath("//button[@data-testid='global-menu-connected-sites']")).Click();
                        Driver.FindElement(By.XPath("//a[text()='Disconnect']")).Click();
                        Driver.FindElement(By.XPath("//button[text()='Disconnect']")).Click();
                        isPresent = true;
                    }
                    else Thread.Sleep(200);
                }
                catch { Thread.Sleep(200); }
            }
        }

        public void EnableDisableTestNet()
        {
            Driver.SwitchTo().Window(Driver.WindowHandles[0]);
            bool isPresent = false;
            while (!isPresent)
            {
                try
                {
                    if (Driver.FindElement(By.XPath("//button[@data-testid='network-display']")).Displayed)
                    {
                        Driver.FindElement(By.XPath("//button[@data-testid='network-display']")).Click();
                        Driver.FindElement(By.XPath("//label[@class='toggle-button toggle-button--off']")).Click();
                        isPresent = true;
                    }
                    else Thread.Sleep(200);
                }
                catch { Thread.Sleep(200); }
            }

            Driver.Navigate().Refresh();
        }

        public void ImportToken()
        {
            Driver.SwitchTo().Window(Driver.WindowHandles[0]);
            bool isPresent = false;
            while (!isPresent)
            {
                try
                {
                    if (Driver.FindElement(By.XPath("//button[@data-testid='import-token-button']")).Displayed)
                    {
                        Driver.FindElement(By.XPath("//button[@data-testid='import-token-button']")).Click();
                        isPresent = true;
                    }
                }
                catch
                {
                    Thread.Sleep(200);
                }
            }

            isPresent = false;
            while (!isPresent)
            {
                try
                {
                    if (Driver.FindElement(By.XPath("//input[@id='custom-decimals' and @value = '0']")).Displayed)
                    {
                        Driver.FindElement(By.XPath("//input[@id='custom-address' and @value = '']")).SendKeys("0x337610d27c682E347C9cD60BD4b3b107C9d34dDd");
                        Driver.FindElement(By.XPath("//input[@id='custom-symbol' and @value = '']")).SendKeys("USDT");
                        Driver.FindElement(By.XPath("//input[@id='custom-decimals' and @value = '0']")).SendKeys("18");

                        if (Driver.FindElement(By.XPath("//button[@data-testid='page-container-footer-next' and text() = 'Add custom token']")).Enabled)
                        {
                            Driver.FindElement(By.XPath("//button[@data-testid='page-container-footer-next' and text() = 'Add custom token']")).Click();
                            isPresent = true;
                        }
                    }
                    else Thread.Sleep(200);
                }
                catch
                {
                    Thread.Sleep(200);
                }
            }

            isPresent = false;
            while (!isPresent)
            {
                try
                {
                    if (Driver.FindElement(By.XPath("//button[text()='Import tokens']")).Enabled)
                    {
                        Driver.FindElement(By.XPath("//button[text()='Import tokens']")).Click();
                        isPresent = true;
                    }
                    else Thread.Sleep(200);
                }
                catch
                {
                    Thread.Sleep(200);
                }
            }

            isPresent = false;
            while (!isPresent)
            {
                try
                {
                    if (Driver.FindElement(By.XPath("//button[@class='asset-breadcrumb']")).Enabled)
                    {
                        Driver.FindElement(By.XPath("//button[@class='asset-breadcrumb']")).Click();
                        isPresent = true;
                    }
                    else Thread.Sleep(200);
                }
                catch
                {
                    Thread.Sleep(200);
                }
            }

        }

        public void SendMoney()
        {
            Driver.SwitchTo().Window(Driver.WindowHandles[0]);
            bool isPresent = false;
            while (!isPresent)
            {
                try
                {
                    if (Driver.FindElement(By.XPath("//p[text()='5ire']")).Displayed)
                    {
                        Driver.FindElement(By.XPath("//p[text()='5ire']")).Click();
                        isPresent = true;
                    }
                    else Thread.Sleep(200);
                }
                catch { Thread.Sleep(200); }
            }

            isPresent = false;

            while (!isPresent)
            {
                try
                {
                    if (Driver.FindElement(By.XPath("//button[@data-testid='eth-overview-send']")).Displayed)
                    {
                        Driver.FindElement(By.XPath("//button[@data-testid='eth-overview-send']")).Click();
                        isPresent = true;
                    }
                    else Thread.Sleep(200);
                }
                catch { Thread.Sleep(200); }
            }

            isPresent = false;

            while (!isPresent)
            {
                try
                {
                    if (Driver.FindElement(By.XPath("//input[@data-testid='ens-input']")).Displayed)
                    {
                        Driver.FindElement(By.XPath("//input[@data-testid='ens-input']")).Clear();
                        Driver.FindElement(By.XPath("//input[@data-testid='ens-input']")).SendKeys("0xB6e94D536f4A96705B49F92Aab6A877293390784");
                        isPresent = true;
                    }
                    else Thread.Sleep(200);
                }
                catch { Thread.Sleep(200); }
            }

            isPresent = false;

            while (!isPresent)
            {
                try
                {
                    if (Driver.FindElement(By.XPath("//button[@class='send-v2__amount-max']")).Displayed)
                    {
                        Driver.FindElement(By.XPath("//button[@class='send-v2__amount-max']")).Click();
                        isPresent = true;
                    }
                    else Thread.Sleep(200);
                }
                catch { Thread.Sleep(200); }
            }

            isPresent = false;

            while (!isPresent)
            {
                try
                {
                    if (Driver.FindElement(By.XPath("//button[@data-testid='page-container-footer-next' and text() = 'Next']")).Enabled)
                    {
                        Driver.FindElement(By.XPath("//button[@data-testid='page-container-footer-next' and text() = 'Next']")).Click();
                        isPresent = true;
                    }
                    else Thread.Sleep(200);
                }
                catch { Thread.Sleep(200); }
            }

            isPresent = false;

            while (!isPresent)
            {
                try
                {
                    if (Driver.FindElement(By.XPath("//button[@data-testid='page-container-footer-next' and text() = 'Confirm']")).Enabled)
                    {
                        Driver.FindElement(By.XPath("//button[@data-testid='page-container-footer-next' and text() = 'Confirm']")).Click();
                        isPresent = true;
                    }
                    else Thread.Sleep(200);
                }
                catch { Thread.Sleep(200); }
            }
        }

        public void SwitchNetwork()
        {
            Driver.SwitchTo().Window(Driver.WindowHandles[0]);
            bool isPresent = false;
            while (!isPresent)
            {
                try
                {
                    if (Driver.FindElement(By.XPath("//button[text()='Switch network']")).Displayed)
                    {
                        Driver.FindElement(By.XPath("//button[text()='Switch network']")).Click();
                        isPresent = true;
                    }
                    else Thread.Sleep(200);
                }
                catch
                {
                    Thread.Sleep(200);
                }
            }
        }

        #endregion

        #region Metamsk PopUp Task
        public void ApproveConfirmation()
        {
            Driver.SwitchTo().Window(Driver.WindowHandles[1]);
            Driver.Navigate().Refresh();
            bool isPresent = false;

            while (!isPresent)
            {
                try
                {
                    if (Driver.FindElement(By.XPath("//button[@data-testid='page-container-footer-next' and text() = 'Next']")).Enabled)
                    {
                        Driver.FindElement(By.XPath("//button[@data-testid='page-container-footer-next' and text() = 'Next']")).Click();
                        isPresent = true;
                    }
                    else Thread.Sleep(200);
                }
                catch { Thread.Sleep(200); }
            }

            isPresent = false;

            while (!isPresent)
            {
                try
                {
                    if (Driver.FindElement(By.XPath("//button[@data-testid='page-container-footer-next' and text() = 'Connect']")).Enabled)
                    {
                        Driver.FindElement(By.XPath("//button[@data-testid='page-container-footer-next' and text() = 'Connect']")).Click();
                        isPresent = true;
                    }
                    else Thread.Sleep(200);
                }
                catch { Thread.Sleep(200); }
            }
        }
        public void Cancel()
        {
            Driver.SwitchTo().Window(Driver.WindowHandles[1]);
            Driver.Navigate().Refresh();
            bool isPresent = false;
            while (!isPresent)
            {
                try
                {
                    if (Driver.FindElement(By.XPath("//button[text()='Cancel']")).Displayed)
                    {
                        Driver.FindElement(By.XPath("//button[text()='Cancel']")).Click();
                        isPresent = true;
                    }
                    else Thread.Sleep(200);
                }
                catch
                {
                    Thread.Sleep(200);
                }
            }
        }

        public void Confirm()
        {
            Driver.SwitchTo().Window(Driver.WindowHandles[1]);
            Driver.Navigate().Refresh();
            bool isPresent = false;
            while (!isPresent)
            {
                try
                {
                    if (Driver.FindElement(By.XPath("//button[@data-testid='page-container-footer-next' and text() = 'Confirm']")).Displayed)
                    {
                        Driver.FindElement(By.XPath("//button[@data-testid='page-container-footer-next' and text() = 'Confirm']")).Click();
                        isPresent = true;
                    }
                    else Thread.Sleep(200);
                }
                catch
                {
                    Thread.Sleep(200);
                }
            }
        }

        public void ConnectAccount(string index)
        {
            Driver.SwitchTo().Window(Driver.WindowHandles[1]);
            Driver.Navigate().Refresh();
            bool isPresent = false;
            while (!isPresent)
            {
                try
                {
                    if (Driver.FindElement(By.XPath("//input[@class='check-box choose-account-list__header-check-box fa fa-minus-square check-box__indeterminate']")).Displayed)
                    {
                        Driver.FindElement(By.XPath("//input[@class='check-box choose-account-list__header-check-box far fa-square']")).Click();
                        Driver.FindElement(By.XPath("//input[@class='check-box choose-account-list__header-check-box far fa-square']")).Click();
                        Driver.FindElement(By.XPath($"(//input[@class='check-box choose-account-list__list-check-box far fa-square'])[{index}]")).Click();
                        isPresent = true;
                    }
                    else Thread.Sleep(200);
                }
                catch
                {
                    Thread.Sleep(200);
                }
            }
        }

        #endregion
    }

}

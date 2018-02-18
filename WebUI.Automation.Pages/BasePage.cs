using Automation.Core.SeleniumUtility;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Diagnostics;

namespace WebUI.Automation.Pages
{
	public abstract class BasePage
	{
		protected BasePage(IExtendedWebDriver webDriver)
		{
			WebDriver = webDriver;
			Toast = new ToastComponent(WebDriver);
			WarningDialog = new WarningDialogComponent(WebDriver);

			WebDriver.WaitUntilPageIsLoaded();
			PageFactory.InitElements(WebDriver, this);
		}

		public WarningDialogComponent WarningDialog { get; set; }

		//[FindsBy(How = How.CssSelector, Using = "[data-auto='PageTitle']")]
		[FindsBy(How = How.TagName, Using = "title")]
		public IWebElement PageTitle { get; set; }

		public string ExpectedPageTitle { get; set; }

		protected IWebElement LoadingOverlay => WebDriver.FindElementByDataAuto("LoadingOverlay");

		protected IExtendedWebDriver WebDriver { get; }
		public ToastComponent Toast { get; }

		public virtual void VerifyPage()
		{
			//WebDriver.WaitUntilElementExists(PageTitle);

			//return PageTitle.Text.Equals(PageTitleName);
			Debug.Assert(PageTitle.Text.Equals(ExpectedPageTitle));
		}

		public void WaitForLoadingToComplete()
		{
			WebDriver.Wait(driver => !LoadingOverlay.Displayed);
		}
	}
}
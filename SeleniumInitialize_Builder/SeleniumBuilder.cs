using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V119.DOMSnapshot;

namespace SeleniumInitialize_Builder
{
    public class SeleniumBuilder : IDisposable
    {
        public IWebDriver WebDriver { get; set; }
        public int Port { get; private set; }
        public bool IsDisposed { get; private set; }
        public List<string> ChangedArguments { get; private set; } = new List<string>();
        public Dictionary<string, object> ChangedUserOptions { get; private set; } = new Dictionary<string, object>();
        public TimeSpan Timeout { get; private set; } = TimeSpan.FromSeconds(15);
        public string StartingURL { get; private set; }

        private ChromeOptions options { get; set; }
        private ChromeDriverService chromeDriverService { get; set; }

        public SeleniumBuilder()
        {

            options = new ChromeOptions();
            options.AddArgument("no-sandbox");

            string driverPath = "/Users/valero/Projects/1.-IWebDriverConfig/SeleniumInitialize_Builder/bin/Debug/net6.0/";
            chromeDriverService = ChromeDriverService.CreateDefaultService(driverPath);
            

        }

        public IWebDriver Build()
        {
            //Создать экземпляр драйвера, присвоить получившийся результат переменной WebDriver, вернуть в качестве результата данного метода.

            try
            {
                WebDriver = new ChromeDriver(chromeDriverService, options);
                WebDriver.Manage().Timeouts().ImplicitWait = Timeout;
                WebDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
                if (StartingURL != null)
                {
                    WebDriver.Navigate().GoToUrl(StartingURL);
                }

                return WebDriver;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Dispose()
        {
            //Закрыть браузер, очистить использованные ресурсы, по завершении переключить IsDisposed на состояние true
            try
            {

                WebDriver.Quit();

                //KillChromeDriverProcesses();
              
                IsDisposed = true;

            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }


        }

        //private void KillChromeDriverProcesses()
        //{
        //    if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        //    {
        //        try
        //        {
        //            var process = new Process
        //            {
        //                StartInfo = new ProcessStartInfo
        //                {
        //                    FileName = "/bin/bash",
        //                    Arguments = "-c \"pkill chromedriver\"",
        //                    RedirectStandardOutput = true,
        //                    UseShellExecute = false,
        //                    CreateNoWindow = true,
        //                }
        //            };

        //            process.Start();
        //            process.WaitForExit();
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"Error killing chromedriver processes: {ex.Message}");
        //        }
        //    }
        //}

        public SeleniumBuilder ChangePort(int port)
        {
            //Реализовать смену порта, на котором развёрнут IWebDriver для этого необходимо ознакомиться с классом DriverService соответствующего драйвера (ChromeDriverService для хрома)
            //Изменить свойство Port на тот порт, на который поменяли.
            //Builder в данном методе должен возвращать сам себя
            try
            {

                chromeDriverService.Port = port;

                Port = port;

                return this;
            }
            catch (Exception ex)
            {

                throw new NotImplementedException();
            }


        }

        public SeleniumBuilder SetArgument(string argument)
        {
            //Реализовать добавление аргумента. При решении данной задаче ознакомитесь с классом Options соответствующего драйвера (ChromeOptions для браузера Chrome)
            //Все изменённые/добавленные аргументы необходимо отразить в свойстве СhangedArguments, которое перед этим нужно где-то будет проинициализировать.
            //Builder в данном методе должен возвращать сам себя
            try
            {

                options.AddArgument(argument);

                ChangedArguments.Add(argument);

                return this;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }

        }

        public SeleniumBuilder SetUserOption(string option, object value)
        {
            //Реализовать добавление пользовательской настройки.
            //Все изменения сохранить в свойстве ChangedUserOptions
            //Builder в данном методе должен возвращать сам себя
            try
            {

                //подробнее бы узнать про метод
                options.AddAdditionalOption(option, value);

                ChangedUserOptions[option] = value;
                 
                return this;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }


        }

        public SeleniumBuilder WithTimeout(TimeSpan timeout)
        {
            //Реализовать изменение изначального таймаута запускаемого браузера
            //Отслеживать изменения в свойстве Timeout
            //Builder возвращает себя
           
            Timeout = timeout;

            return this;
        }

        public SeleniumBuilder WithURL(string url)
        {
            //Реализовать изменения изначального URL запускаемого браузера
            //Отслеживать изменения в строке StartingURL
            //Builder возвращает себя
            StartingURL = url;

            return this;

        }
    }
}
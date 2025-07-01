using System.Globalization;

namespace DemoTradingApp
{
    namespace DemoTradingApp
    {
        internal static class Program
        {
            [STAThread]
            static void Main()
            {
                ApplicationConfiguration.Initialize();
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                using (LoginForm loginForm = new LoginForm())
                {
                    if (loginForm.ShowDialog() == DialogResult.OK)
                    {
                        Application.Run(new DashboardForm(loginForm.LoggedInUser!));
                    }
                }
            }
        }
    }
}
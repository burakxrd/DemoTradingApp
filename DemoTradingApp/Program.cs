namespace DemoTradingApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Sadece Giriþ Formu ile baþla
            using (LoginForm loginForm = new LoginForm())
            {
                // Eðer giriþ baþarýlý olursa (DialogResult.OK dönerse)
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // Ana uygulama olarak Dashboard'u çalýþtýr.
                    // Bu form kapandýðýnda uygulama da kapanýr.
                    Application.Run(new DashboardForm(loginForm.LoggedInUser!));
                }
            }
            // Giriþ formu kapatýlýrsa veya giriþ yapýlmazsa, uygulama sessizce sonlanýr.
        }
    }
}
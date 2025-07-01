namespace DemoTradingApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Sadece Giri� Formu ile ba�la
            using (LoginForm loginForm = new LoginForm())
            {
                // E�er giri� ba�ar�l� olursa (DialogResult.OK d�nerse)
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // Ana uygulama olarak Dashboard'u �al��t�r.
                    // Bu form kapand���nda uygulama da kapan�r.
                    Application.Run(new DashboardForm(loginForm.LoggedInUser!));
                }
            }
            // Giri� formu kapat�l�rsa veya giri� yap�lmazsa, uygulama sessizce sonlan�r.
        }
    }
}
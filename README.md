# DemoTradingApp 📈

**DemoTradingApp** is a comprehensive C# Windows Forms desktop trading simulation application that allows users to perform virtual currency trading, manage their wallets, and even purchase luxury assets. This project was developed to demonstrate the fundamental dynamics of a trading platform, real-time API integration, and a modern user interface design.

The application enables users to trade between different fiat currencies (USD, EUR, TRY) and cryptocurrencies (Bitcoin, Ethereum, etc.), while also offering the experience of purchasing various "luxury" items from a virtual market.

## 🌟 Key Features

* **User Management:** Secure registration, login, and a "Forgot Password" feature protected with BCrypt-hashed passwords.
* **Wallet Management:** Create and delete wallets of different types (e.g., Cryptocurrency).
* **Live Market Data:** Real-time cryptocurrency (Bitcoin, Ethereum, etc.) and foreign exchange (USD, EUR, TRY) rates fetched via the CoinGecko API. Prices are retrieved from the database if the API is unavailable.
* **Buy/Sell (Trade):** Allows users to buy and sell crypto assets using different currency balances they own. Estimated results are shown before transactions, and balance checks are performed.
* **Luxury Market:** Ability to purchase expensive virtual assets like private jets and luxury watches using in-app balance. Includes shopping cart management and wallet balance verification.
* **Asset Tracking:** View and manage all purchased items and assets on the "My Possessions" screen.
* **Dynamic User Interface:** Developed using Krypton Toolkit for a user-friendly and modern appearance.
* **Database Integration:** User, wallet, asset, trade, and purchase data are managed in Microsoft SQL Server.
* **Multi-Currency Support:** Display balances and prices in USD, EUR, or TRY within the user interface.

## 🛠️ Technologies and Libraries Used

* **Platform:** .NET 8 & Windows Forms
* **Language:** C#
* **Database:** Microsoft SQL Server
* **UI Library:** [Krypton Toolkit](https://github.com/Krypton-Suite/Krypton-Toolkit)
* **Email Sending:** [MailKit](https://github.com/jstedfast/MailKit) (for SMTP operations)
* **Hashing:** [BCrypt.Net-Next](https://github.com/BcryptNet/bcrypt.net-next) (for password hashing)
* **Data Access:** [Microsoft.Data.SqlClient](https://github.com/dotnet/SqlClient) (for ADO.NET SQL Server connectivity)
* **API Integration:** [CoinGecko API](https://www.coingecko.com/api) (for real-time pricing)
* **JSON Processing:** `System.Text.Json` (for API response handling)

## 🚀 Setup and Getting Started

Follow these steps to set up and run the project on your local machine.

### Prerequisites

* **Git:** Version control system. You can download it from [here](https://git-scm.com/downloads).
* **Visual Studio 2022 (or later):** With the ".NET desktop development" workload installed.
* **Microsoft SQL Server:** Any edition (e.g., free "Developer" or "Express" edition).
* **SQL Server Management Studio (SSMS):** For database management.

### Database Setup

1.  Open SQL Server Management Studio (SSMS) and connect to your SQL Server instance.
2.  Locate the `DemoTrading.sql` file in the project's root directory and open it within SSMS.
3.  Execute this SQL script (using the `Execute` button) to automatically create a database named `DemoTrading`, all necessary tables, and initial data.

### Application Configuration

Open your project in Visual Studio (`DemoTradingApp.sln`).

1.  **Database Connection String:**
    * Open the `DatabaseHelper.cs` file located within the `DemoTradingApp` project.
    * Find the `ConnectionString` constant at the top. Update the `Server=` part to your SQL Server instance name.
        ```
        public static readonly string ConnectionString = "Server=YOUR_SERVER_NAME;Database=DemoTrading;Trusted_Connection=True;TrustServerCertificate=True;";
        ```
    * **Note on `localhost`:** If your SQL Server is running on the same machine and configured for default local access, `Server=localhost` or `Server=.` (a single dot) might also work without explicitly specifying your PC's name.

2.  **Email Settings (for Password Reset):**
    * Open the `LoginForm.cs` file located within the `DemoTradingApp` project.
    * Locate the `client.Authenticate(...)` line within the `btnSendCode_Click` method. Update it with your email address and an "App Password" generated from your email provider (e.g., for Gmail, you need to generate an App Password).
        ```
        // client.Authenticate("YourEmailAddress", "YourAppPassword");
        client.Authenticate("your_email@gmail.com", "your_app_password");
        ```
    * **Security Note:** For production applications, sensitive information like email passwords and full connection strings should never be hardcoded directly into source files. Instead, they should be stored in configuration files (like `appsettings.json`, which is already included in this project for connection strings) and excluded from version control using `.gitignore`. This project simplifies it for demonstration purposes.

### Running the Application

1.  Open the project in Visual Studio.
2.  Necessary NuGet packages (Krypton Toolkit, MailKit, BCrypt.Net, Microsoft.Data.SqlClient, etc.) should be automatically restored.
3.  Press `F5` or click the "Start" button to launch the application.

## 📋 Application Usage Steps

Follow these steps to experience all features of the application:

1.  **Create an Account:** Upon first opening the application, click on the "Don't have an account? Register" link to create a new user account.
2.  **Reset Password (Optional):** You can test the "Forgot Password" feature to reset your password using a verification code sent to your email.
3.  **Log In:** Log in to the application with your new credentials.
4.  **Create a Wallet:** Go to the main menu and select "Add/Delete Wallet" to create at least one wallet of type "Cryptocurrency".
5.  **Add Balance:** Use the "Add/Delete Balance" menu to load virtual USD, EUR, or TRY into your wallets.
6.  **Perform Trades:** Navigate to the "Trade" screen to buy or sell cryptocurrencies using your available balance.
7.  **Explore the Market:** Visit the "Market" to add luxury items to your cart and complete virtual purchases.
8.  **View Possessions:** Check the "My Possessions" menu to see and manage all the items you've purchased from the market.

## 🤝 Contributing

This project is developed for personal demonstration and learning purposes. Contributions are not currently accepted from external sources. However, you are welcome to share your ideas and suggestions via the "Issues" section.

## 📄 License

This project is not distributed under any open-source license and is intended for personal portfolio/demo purposes.

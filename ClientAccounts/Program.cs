using ClientAccounts.Interfaces;
using System;
using System.Runtime.Serialization.Json;
using System.Text;

namespace ClientAccounts
{
    public static class Program
    {
        internal static string fileName = "client_accounts.json";
        internal static ConsoleWrapperProduction console = new ConsoleWrapperProduction();
        internal static ClientAccounts data = new ClientAccounts();

        public static void Main(string[] args)
        {
            data.LoadAccounts(fileName);

            while (true)
            {
                int selection = DisplayMainMenu(console);
                switch (selection)
                {
                    case 1:
                        AddAccounts(console);
                        break;
                    case 2:
                        EditAccounts(console);
                        break;
                    case 3:
                        ViewAccounts(console);
                        break;
                    case 4:
                        DeleteAccounts(console);
                        break;
                    case 5:
                        SaveAccountsToFile(console);
                        break;
                    case 6:
                    default:
                        return;
                };
            }
        }

        internal static int DisplayMainMenu(IConsole console)
        {
            console.WriteLine("\nClient Accounts");
            console.WriteLine("___________________________________________");
            console.WriteLine("1. Add Client Accounts");
            console.WriteLine("2. Edit Client Accounts");
            console.WriteLine("3. View Client Accounts");
            console.WriteLine("4. Delete Client Accounts");
            console.WriteLine("5. Save Accounts To File");
            console.WriteLine("6. Exit");
            console.WriteLine("___________________________________________");

            console.Write("Enter your Selection: ");

            int m = -1;
            while (true)
            {
                string selection = console.ReadLine();
                if (!Int32.TryParse(selection, out m))
                {
                    console.WriteLine("Invalid selection");
                }
                else
                {
                    break;
                }
            }

            return m;
        }

        internal static void AddAccounts(IConsole console)
        {
            while (true)
            {
                ClientAccount account = new ClientAccount();

                console.Write("Account Number: ");
                account.AccountNumber = console.ReadLine();

                if (data.RetrieveAccount(account.AccountNumber) != null)
                {
                    console.WriteLine("This account number is already in use. Please enter a new account number.");
                    continue;
                }

                console.Write("Name: ");
                account.Name = console.ReadLine();

                Console.Write("Phone Number: ");
                account.PhoneNumber = console.ReadLine();

                if (data.IsValidAccount(account) && data.CreateAccount(account))
                {
                    console.WriteLine("The account was created successfully.");
                }
                else
                {
                    console.WriteLine("The account you entered is invalid.");
                }

                console.Write("Do you want to add another account (Y/N)? ");
                string response = console.ReadLine();
                if (response.ToLower() == "y" || response.ToLower() == "yes")
                    continue;
                else
                    break;
            }
        }

        internal static void EditAccounts(IConsole console)
        {
            while (true)
            {
                console.Write("Account Number: ");
                string accountNumber = console.ReadLine();

                ClientAccount account = data.RetrieveAccount(accountNumber);

                if (data.IsValidAccount(account))
                {
                    console.WriteLine("___________________________________________");
                    console.WriteLine("Current Account Number: " + account.AccountNumber);
                    console.WriteLine("Current Name: " + account.Name);
                    console.WriteLine("Current Phone Number: " + account.PhoneNumber);
                    console.WriteLine("___________________________________________");

                    console.Write("Name: ");
                    account.Name = console.ReadLine();

                    console.Write("Phone Number: ");
                    account.PhoneNumber = console.ReadLine();

                    if (data.UpdateAccount(account))
                    {
                        console.WriteLine("The account was updated successfully.");
                    }
                    else
                    {
                        console.WriteLine("There was an issue updating the account.");
                    }
                }
                else
                {
                    console.WriteLine("The account you entered was not found.");
                }

                console.Write("Do you want to edit another account (Y/N)? ");
                string response = console.ReadLine();
                if (response.ToLower() == "y" || response.ToLower() == "yes")
                    continue;
                else
                    break;
            }
        }

        internal static void ViewAccounts(IConsole console)
        {
            console.WriteLine("___________________________________________");

            foreach (ClientAccount account in data.accounts.Values)
            {
                console.WriteLine(string.Format("Account Number: {0} \\ Name: {1} \\ Phone Number: {2}", 
                    account.AccountNumber, account.Name, account.PhoneNumber));
            }

            console.WriteLine("___________________________________________");

            console.Write("Press the return key to return to the Main Manu. ");
            console.ReadLine();
        }

        internal static void DeleteAccounts(IConsole console)
        {
            while (true)
            {
                console.Write("Account Number: ");
                string accountNumber = console.ReadLine();

                if (data.DeleteAccount(accountNumber))
                {
                    console.WriteLine("The account was deleted.");
                }
                else
                {
                    console.WriteLine("The account number you entered is invalid.");
                }

                console.Write("Do you want to delete another account (Y/N)? ");
                string response = console.ReadLine();
                if (response.ToLower() == "y" || response.ToLower() == "yes")
                    continue;
                else
                    break;
            }
        }

        internal static void SaveAccountsToFile(IConsole console)
        {
            if (data.SaveAccountsToFile(fileName))
            {
                console.WriteLine("Client accounts saved successfully.");
            }
            else
            {
                console.WriteLine("There was an issue saving the accounts to file.");
            }
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientAccounts.Interfaces;
using System.Collections.Generic;
using ClientAccounts;
using System.IO;

namespace ClientAccountTests
{
    [TestClass]
    public class ClientAccountsTests
    {
        public ConsoleWrapperTest console;
        public const string fileName = "client_accounts_test.json";
        public List<string> linesToReadDisplayMenu = new List<string>
        {
	        "1"
	    };
        public List<string> linesToReadAddAccount = new List<string>
        {
	        "12345",
            "Joe Doe",
            "111-111-1111",
            "n"
	    };
        public List<string> linesToReadEditAccount = new List<string>
        {
	        "12345",
            "Joe Doe",
            "111-111-1111",
            "n"
	    };
        public List<string> linesToReadDeleteAccount = new List<string>
        {
	        "12345",
            "n"
	    };

        [TestMethod]
        public void TestDisplayMenu()
        {
            console = new ConsoleWrapperTest(linesToReadDisplayMenu);
            int selection = Program.DisplayMainMenu(console);

            Assert.AreEqual("1", selection.ToString());
        }

        [TestMethod]
        public void TestAddAccounts()
        {
            console = new ConsoleWrapperTest(linesToReadAddAccount);

            Program.data.LoadAccounts(fileName);

            Program.AddAccounts(console);

            Assert.AreEqual("The account was created successfully.", console.LinesRead[2]);
        }

        [TestMethod]
        public void TestEditAccounts()
        {
            Program.data.LoadAccounts(fileName);

            console = new ConsoleWrapperTest(linesToReadAddAccount);
            Program.AddAccounts(console);

            console = new ConsoleWrapperTest(linesToReadEditAccount);
            Program.EditAccounts(console);

            Assert.AreEqual("The account was updated successfully.", console.LinesRead[8]);
        }

        [TestMethod]
        public void TestViewAccounts()
        {
            Program.data.LoadAccounts(fileName);

            console = new ConsoleWrapperTest(linesToReadAddAccount);
            Program.AddAccounts(console);

            console = new ConsoleWrapperTest(linesToReadEditAccount);
            Program.ViewAccounts(console);

            Assert.AreEqual("Account Number: 12345 \\ Name: Joe Doe \\ Phone Number: 111-111-1111", console.LinesRead[1]);
        }

        [TestMethod]
        public void TestDeleteAccounts()
        {
            Program.data.LoadAccounts(fileName);

            console = new ConsoleWrapperTest(linesToReadAddAccount);
            Program.AddAccounts(console);

            console = new ConsoleWrapperTest(linesToReadDeleteAccount);
            Program.DeleteAccounts(console);

            Assert.AreEqual("The account was deleted.", console.LinesRead[1]);
        }

        [TestMethod]
        public void TestSaveAccountsToFile()
        {
            Program.fileName = fileName;
            Program.data.LoadAccounts(fileName);

            console = new ConsoleWrapperTest(linesToReadAddAccount);
            Program.AddAccounts(console);
            Program.SaveAccountsToFile(console);

            File.Delete(fileName);

            Assert.AreEqual("Client accounts saved successfully.", console.LinesRead[4]);
        }
    }
}

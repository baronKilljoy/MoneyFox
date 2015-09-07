﻿using System.Linq;
using MoneyManager.Core;
using MoneyManager.Core.DataAccess;
using MoneyManager.Foundation.Model;
using MoneyManager.Windows.Core.Tests.Helper;
using SQLite.Net.Platform.WinRT;
using Xunit;

namespace MoneyManager.Windows.Core.Tests.DataAccess
{
    public class AccountDataAccessTest
    {
        [Fact]
        [Trait("Category", "Integration")]
        public void AccountDataAccess_CrudAccount()
        {
            var accountDataAccess =
                new AccountDataAccess(new DbHelper(new SQLitePlatformWinRT(), new TestDatabasePath()));

            const string firstName = "fooo Name";
            const string secondName = "new Foooo";

            var account = new Account
            {
                CurrentBalance = 20,
                Iban = "CHF20 0000 00000 000000",
                Name = firstName,
                Note = "this is a note"
            };

            accountDataAccess.Save(account);

            accountDataAccess.LoadList();
            var list = accountDataAccess.LoadList();

            Assert.Equal(1, list.Count);
            Assert.Equal(firstName, list.First().Name);

            account.Name = secondName;

            accountDataAccess.Save(account);

            list = accountDataAccess.LoadList();

            Assert.Equal(1, list.Count);
            Assert.Equal(secondName, list.First().Name);

            accountDataAccess.Delete(account);

            list = accountDataAccess.LoadList();

            Assert.False(list.Any());
        }
    }
}
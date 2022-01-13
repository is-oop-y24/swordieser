using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Accounts;
using Banks.Banks;
using Banks.Clients;
using Banks.Transactions;

namespace Banks
{
    internal static class Program
    {
        private static void Main()
        {
            string interfaceType;
            CentralBank centralBank;
            var clients = new List<Person>();

            void ChooseInterfaceType()
            {
                Console.WriteLine("Choose your fighter\n1.manager\n2.client\n3.stop): ");
                interfaceType = Console.ReadLine();
                InterfaceType();
            }

            void Manager()
            {
                while (true)
                {
                    Console.WriteLine("You can choose:" +
                                      "\n1. Create bank" +
                                      "\n2. Get list of banks" +
                                      "\n3. Rewind time" +
                                      "\n4. Go to main menu");

                    string command = Console.ReadLine();

                    switch (command)
                    {
                        case "1":
                        case "Create bank":
                            Console.WriteLine("Type parameters of the bank:");
                            Console.WriteLine("Bank name");
                            string name = Console.ReadLine();
                            Console.WriteLine("Percent");
                            double percent = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Commission");
                            double commission = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Credit limit");
                            double creditLimit = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Maximal withdraw");
                            double maxWithdraw = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Maximal transfer");
                            double maxTransfer = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Percents for deposit account (amount, percent, upper bound)");
                            int amount = Convert.ToInt32(Console.ReadLine());
                            var borders = new Dictionary<double, double>();
                            for (int i = 0; i < amount; i++)
                            {
                                double depositPercent = Convert.ToDouble(Console.ReadLine());
                                double upperBound = Convert.ToInt32(Console.ReadLine());
                                borders.Add(depositPercent, upperBound);
                            }

                            centralBank.CreateBank(
                                name,
                                percent,
                                commission,
                                creditLimit,
                                maxWithdraw,
                                maxTransfer,
                                borders);
                            break;
                        case "2":
                        case "Get list of banks":
                            foreach (Bank bank in centralBank.GetBanks())
                            {
                                Console.WriteLine($"{bank.Id} - {bank.Name}\n");
                            }

                            break;
                        case "3":
                        case "Rewind time":
                            Console.WriteLine("Type date to rewind (year, month, day");
                            var date = new DateTime(
                                Convert.ToInt32(Console.ReadLine()),
                                Convert.ToInt32(Console.ReadLine()),
                                Convert.ToInt32(Console.ReadLine()));
                            TimeMachine.TimeRewind(centralBank, date);
                            break;
                        case "4":
                        case "Go to main menu":
                            ChooseInterfaceType();
                            break;
                        default:
                            Console.WriteLine("Try again");
                            continue;
                    }

                    break;
                }
            }

            void Client()
            {
                while (true)
                {
                    Console.WriteLine("You can choose:" +
                                      "\n1. Register" +
                                      "\n2. Create account" +
                                      "\n3. Get list of accounts" +
                                      "\n4. Balance" +
                                      "\n5. Withdraw" +
                                      "\n6. Replenishment" +
                                      "\n7. Transfer" +
                                      "\n8. Transaction history" +
                                      "\n9. Cancel transaction" +
                                      "\n10. Go to main menu");
                    string command = Console.ReadLine();
                    switch (command)
                    {
                        case "1":
                        case "Register":
                            Console.WriteLine("Type your name and surname");
                            string personName = Console.ReadLine();
                            string personSurname = Console.ReadLine();
                            Console.WriteLine("Type your address to verify your account");
                            string personAddress = Console.ReadLine();
                            Console.WriteLine("Type your passport to end verification");
                            long passport = Convert.ToInt32(Console.ReadLine());
                            var person = new Person(personName, personSurname, personAddress, passport);
                            person.CheckDoubtfulness();
                            clients.Add(person);
                            break;
                        case "2":
                        case "Create account":
                            Console.WriteLine("Choose the bank by number:");
                            foreach (Bank bank in centralBank.GetBanks())
                            {
                                Console.WriteLine($"{bank.Id} - {bank.Name}\n");
                            }

                            int bankId = Convert.ToInt32(Console.ReadLine());
                            foreach (Bank bank in centralBank.GetBanks())
                            {
                                if (bank.Id == bankId)
                                {
                                    Console.WriteLine("Type your full name");
                                    string name = Console.ReadLine();
                                    foreach (Person client in clients.Where(client =>
                                        $"{client.Name} {client.Surname}" == name))
                                    {
                                        Console.WriteLine("Choose type of account:\nDebit\nCredit\nDeposit");
                                        string type = Console.ReadLine();
                                        switch (type)
                                        {
                                            case "Debit":
                                                bank.CreateDebitAccount(client, 0);
                                                break;
                                            case "Credit":
                                                bank.CreateCreditAccount(client, 0);
                                                break;
                                            case "Deposit":
                                                Console.WriteLine("Type amount of deposit");
                                                int deposit = Convert.ToInt32(Console.ReadLine());
                                                Console.WriteLine("Type the end of deposit (year, month, day");
                                                int year = Convert.ToInt32(Console.ReadLine());
                                                int month = Convert.ToInt32(Console.ReadLine());
                                                int day = Convert.ToInt32(Console.ReadLine());
                                                bank.CreateDepositAccount(
                                                    client,
                                                    deposit,
                                                    new DateTime(year, month, day));
                                                break;
                                        }
                                    }
                                }
                            }

                            break;
                        case "3":
                        case "Get list of accounts":
                            Console.WriteLine("Type your full name");
                            string tempName = Console.ReadLine();
                            foreach (Person client in clients.Where(client =>
                                $"{client.Name} {client.Surname}" == tempName))
                            {
                                foreach (IAccount account in client.GetAccounts())
                                {
                                    Console.WriteLine($"{account.Id}:" +
                                                      $"\nBalance: {account.Balance}");
                                }
                            }

                            break;
                        case "4":
                        case "Balance":
                            Console.WriteLine("Type your full name and then type your account id");
                            string accName = Console.ReadLine();
                            int accId = Convert.ToInt32(Console.ReadLine());
                            foreach (Person client in clients.Where(client =>
                                $"{client.Name} {client.Surname}" == accName))
                            {
                                foreach (IAccount account in client.GetAccounts().Where(account => account.Id == accId))
                                {
                                    Console.WriteLine($"Balance: {account.Balance}");
                                }
                            }

                            break;
                        case "5":
                        case "Withdraw":
                            Console.WriteLine("Type your full name and then type your account id");
                            string withName = Console.ReadLine();
                            int withId = Convert.ToInt32(Console.ReadLine());
                            foreach (Person client in clients.Where(client =>
                                $"{client.Name} {client.Surname}" == withName))
                            {
                                foreach (IAccount account in client.GetAccounts().Where(account => account.Id == withId))
                                {
                                    foreach (Bank bank in centralBank.GetBanks()
                                        .Where(bank => bank.GetAccounts().Contains(account)))
                                    {
                                        Console.WriteLine("Type amount of withdraw");
                                        int withdraw = Convert.ToInt32(Console.ReadLine());
                                        bank.Withdraw(account, withdraw);
                                        break;
                                    }
                                }
                            }

                            break;
                        case "6":
                        case "Replenishment":
                            Console.WriteLine("Type your full name and then type your account id");
                            string repName = Console.ReadLine();
                            int repId = Convert.ToInt32(Console.ReadLine());
                            foreach (Person client in clients.Where(client =>
                                $"{client.Name} {client.Surname}" == repName))
                            {
                                foreach (IAccount account in client.GetAccounts().Where(account => account.Id == repId))
                                {
                                    foreach (Bank bank in centralBank.GetBanks()
                                        .Where(bank => bank.GetAccounts().Contains(account)))
                                    {
                                        Console.WriteLine("Type amount of withdraw");
                                        int replenishment = Convert.ToInt32(Console.ReadLine());
                                        bank.Replenishment(account, replenishment);
                                        break;
                                    }
                                }
                            }

                            break;
                        case "7":
                        case "Transfer":
                            Console.WriteLine("Type your full name and then type your account id");
                            string transferName = Console.ReadLine();
                            int transferId = Convert.ToInt32(Console.ReadLine());
                            foreach (Person client in clients.Where(client =>
                                $"{client.Name} {client.Surname}" == transferName))
                            {
                                foreach (IAccount account in client.GetAccounts()
                                    .Where(account => account.Id == transferId))
                                {
                                    Console.WriteLine("Type full name and account id of the recipient");
                                    string recipName = Console.ReadLine();
                                    int recipId = Convert.ToInt32(Console.ReadLine());
                                    foreach (Person client1 in clients.Where(client1 =>
                                        $"{client1.Name} {client1.Surname}" == recipName))
                                    {
                                        foreach (IAccount account1 in client1.GetAccounts()
                                            .Where(account1 => account.Id == recipId))
                                        {
                                            foreach (Bank bank in centralBank.GetBanks()
                                                .Where(bank => bank.GetAccounts().Contains(account)))
                                            {
                                                Console.WriteLine("Type the amount of transfer");
                                                double amount = Convert.ToDouble(Console.ReadLine());
                                                bank.Transfer(account, account1, amount);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }

                            break;
                        case "8":
                        case "Transaction history":
                            Console.WriteLine("Type your full name and then type your account id");
                            string transName = Console.ReadLine();
                            int transId = Convert.ToInt32(Console.ReadLine());
                            foreach (Person client in clients.Where(client =>
                                $"{client.Name} {client.Surname}" == transName))
                            {
                                foreach (IAccount account in client.GetAccounts()
                                    .Where(account => account.Id == transId))
                                {
                                    foreach (Transaction transaction in account.GetTransactionsHistory())
                                    {
                                        Console.WriteLine($"Transaction id: {transaction.Id}");
                                        if (transaction.Sender != null)
                                        {
                                            Console.WriteLine($"Transaction sender: {transaction.Sender.Id}");
                                        }

                                        if (transaction.Recipient != null)
                                        {
                                            Console.WriteLine($"Transaction recipient: {transaction.Recipient.Id}");
                                        }

                                        Console.WriteLine($"Transaction amount: {transaction.Amount}");
                                    }
                                }
                            }

                            break;
                        case "9":
                        case "Cancel transaction":
                            Console.WriteLine("Type your full name and then type your account id");
                            string cancelName = Console.ReadLine();
                            int cancelId = Convert.ToInt32(Console.ReadLine());
                            foreach (Person client in clients.Where(client =>
                                $"{client.Name} {client.Surname}" == cancelName))
                            {
                                foreach (IAccount account in client.GetAccounts()
                                    .Where(account => account.Id == cancelId))
                                {
                                    Console.WriteLine("Type the id of the transaction");
                                    int transCancelId = Convert.ToInt32(Console.ReadLine());
                                    foreach (Transaction transaction in account.GetTransactionsHistory()
                                        .Where(trans => trans.Id == transCancelId))
                                    {
                                        foreach (Bank bank in centralBank.GetBanks()
                                            .Where(bank => bank.GetAccounts().Contains(account)))
                                        {
                                            bank.Cancellation(account, transaction);
                                            break;
                                        }
                                    }
                                }
                            }

                            break;
                        case "10":
                        case "Go to main menu":
                            ChooseInterfaceType();
                            break;
                        default:
                            Console.WriteLine("Try again");
                            continue;
                    }

                    break;
                }
            }

            void InterfaceType()
            {
                switch (interfaceType)
                {
                    case "1":
                    case "manager":
                        Manager();
                        break;
                    case "2":
                    case "client":
                        Client();
                        break;
                    case "3":
                    case "stop":
                        return;
                    default:
                        Console.WriteLine("Try again");
                        ChooseInterfaceType();
                        break;
                }
            }
        }
    }
}
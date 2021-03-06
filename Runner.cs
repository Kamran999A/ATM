using System;

namespace BankAccount
{
    public class Runner
    {


        public static void Runnerr()
        {


            Console.Title = "ATM: Kapital Bank";
            var bank = new Bank() { Name = "Kapital Bank" };

            try
            {
                var c1 = new Client() { Name = "John", Surname = "Johnlu", Age = 25, Mail = "johnjohnlu00@gmail.com", Salary = 1000 };
                var c2 = new Client() { Name = "Tofiq", Surname = "Alabala", Age = 42, Mail = "johnjohnlu00@gmail.com", Salary = 1700 };
                var c3 = new Client() { Name = "Tofiq", Surname = "Gulbala", Age = 22, Mail = "johnjohnlu00@gmail.com", Salary = 1550 };
                var c4 = new Client() { Name = "Tofiq", Surname = "Qarabala", Age = 52, Mail = "johnjohnlu00@gmail.com", Salary = 1150 };
                var c5 = new Client() { Name = "Tofiq", Surname = "Bircebala", Age = 19, Mail = "johnjohnlu00@gmail.com", Salary = 2000 };

                c1.ClientCard = bank.CreateBankCard(c1, "9999", 2);
                c2.ClientCard = bank.CreateBankCard(c2, "2222", 3);
                c3.ClientCard = bank.CreateBankCard(c3, "3333", 1);
                c4.ClientCard = bank.CreateBankCard(c4, "4444", 3);
                c5.ClientCard = bank.CreateBankCard(c5, "5555", 2);

                c1.ClientCard.CardNumber = "9999999999999999";
                c2.ClientCard.CardNumber = "2222222222222222";
                c3.ClientCard.CardNumber = "3333333333333333";
                c4.ClientCard.CardNumber = "4444444444444444";
                c5.ClientCard.CardNumber = "5555555555555555";


                bank.AddClient(c1);
                bank.AddClient(c2);
                bank.AddClient(c3);
                bank.AddClient(c4);
                bank.AddClient(c5);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("-------------------- ATM --------------------");
                Console.ResetColor();

                GetClientPan(out string PAN);
                GetClientPin(out string PIN);

                try
                {
                    Console.Clear();
                    var client = bank.GetClient(PAN.Trim(), PIN.Trim());
                    Console.WriteLine($"Welcome {client.ClientCard.Fullname}");
                    ClearScreen();
                    while (true)
                    {
                        var choice = 0;
                        do
                        {
                            Console.WriteLine("1. Balance");
                            Console.WriteLine("2. Withdraw");
                            Console.WriteLine("3. Card to card");
                            Console.WriteLine("4. Logs");
                            Console.WriteLine("5. Exit");

                            Console.Write(">> ");
                            try
                            {
                                choice = Convert.ToInt32(Console.ReadLine());
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            Console.Clear();
                        } while (Convert.ToInt32(choice) < 1 || Convert.ToInt32(choice) > 5);


                        if (choice == 1)
                        {
                            client.ClientCard.ShowBalance();
                            ClearScreen();
                        }
                        else if (choice == 2)
                        {
                            do
                            {
                                Console.WriteLine("1. 5 USD\n2. 10 USD\n3. 20 USD\n4. 50 USD\n5. 100 USD\n6. Other\n7. Back");

                                Console.Write(">> ");
                                try
                                {
                                    choice = Convert.ToInt32(Console.ReadLine());
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                                Console.Clear();
                            } while (Convert.ToInt32(choice) < 1 || Convert.ToInt32(choice) > 7);

                            if (choice == 7)
                                continue;
                            try
                            {
                                double amount = 0;
                                switch ((Amounts)choice)
                                {
                                    case Amounts.USD5:
                                        {
                                            amount = 5;
                                            break;
                                        }
                                    case Amounts.USD10:
                                        {
                                            amount = 10;
                                            break;
                                        }
                                    case Amounts.USD20:
                                        {
                                            amount = 20;
                                            break;
                                        }
                                    case Amounts.USD50:
                                        {
                                            amount = 50;
                                            break;
                                        }
                                    case Amounts.USD100:
                                        {
                                            amount = 100;
                                            break;
                                        }
                                    case Amounts.OTHER:
                                        {

                                            Console.Clear();

                                            InputAmount(out amount);
                                            break;
                                        }

                                    default:
                                        break;

                                }
                                bank.Withdraw(client.ClientCard, amount);
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.WriteLine($"Operation successfully! Withdraw amount -> {amount:C2}");
                                Console.ResetColor();
                                client.AddLog(new Log { Title = $"Withdraw from {client.ClientCard.Fullname}", Info = $"Withdraw from {client.ClientCard.CardNumber}. Amount {amount:C2}" });
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            ClearScreen();

                        }
                        else if (choice == 3)
                        {
                            GetClientPan(out string toPan);

                            InputAmount(out double amount);

                            try
                            {
                                bank.CardToCard(client.ClientCard, toPan, amount);
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.WriteLine("Transaction completed");
                                Console.ResetColor();
                                client.AddLog(new Log
                                {
                                    Title = $"Transaction from {client.ClientCard.Fullname}",
                                    Info = $@"Transaction from {client.ClientCard.CardNumber} to {toPan}.
Amount is {amount:C2}"
                                });
                                ClearScreen();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                                throw;
                            }
                        }
                        else if (choice == 4)
                        {
                            try
                            {
                                do
                                {
                                    Console.WriteLine("1. Last 1 day\n2. Last 5 day\n3. Last 10 day\n");

                                    Console.Write(">> ");
                                    try
                                    {
                                        choice = Convert.ToInt32(Console.ReadLine());
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                    Console.Clear();
                                } while (Convert.ToInt32(choice) < 1 || Convert.ToInt32(choice) > 3);

                                var days = 0;

                                if (choice == 1)
                                    days = 1;
                                else if (choice == 5)
                                    days = 5;
                                else
                                    days = 10;

                                client.LogFilter(days);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            ClearScreen();
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Try Again!");
                    ClearScreen();
                }
            }

        }


        enum Amounts
        {
            USD5 = 1, USD10, USD20, USD50, USD100, OTHER
        };
        public static void GetClientPan(out string PAN)
        {
            while (true)
            {
                Console.Write("Enter PAN: ");

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                PAN = Console.ReadLine();
                Console.ResetColor();
                if (BankCardValidation.PanValidate(PAN))
                    break;

                Console.WriteLine("Your PAN format is wrong! Try again!");
                ClearScreen();
            }
        }
        public static void GetClientPin(out string PIN)
        {
            while (true)
            {
                Console.Write("Enter PIN: ");

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                PIN = Console.ReadLine();
                Console.ResetColor();

                if (BankCardValidation.PinValidate(PIN))
                    break;

                Console.WriteLine("Your PIN format is wrong! Try again!");
                ClearScreen();
            }
        }
        public static void ClearScreen()
        {
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
            Console.Clear();
        }
        public static void InputAmount(out double amount)
        {
            while (true)
            {
                Console.Write("Enter amount: ");
                var input = Console.ReadLine();
                var result = double.TryParse(input, out amount);

                if (result)
                {
                    break;
                }
                Console.WriteLine("Amount must be numeric value!");
                ClearScreen();
            }
        }
    }

}
    

    


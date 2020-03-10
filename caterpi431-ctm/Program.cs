// Author - Ganapati Sarkar

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

// Atm program 
namespace caterpi431_ctm
{
    class Program
    {
        static void Main(string[] args)
        {
            var atm = new Atm();

            atm.WithdrawMoney(500444);
            atm.WithdrawMoney(31900);
            atm.WithdrawMoney(200);
            atm.WithdrawMoney(500); 
            atm.WithdrawMoney(1000);
            atm.WithdrawMoney(10);
            //Console.WriteLine(atm.WithdrawMoney(-100)); // Not allowed as its an uint
            atm.WithdrawMoney(100);
            atm.WithdrawMoney(16321);
            atm.WithdrawMoney(16321);
            atm.WithdrawMoney(16321);
            atm.WithdrawMoney(26300);
            atm.WithdrawMoney(26300);
            atm.WithdrawMoney(26300);
            atm.WithdrawMoney(26300);
            atm.WithdrawMoney(26300);
            atm.WithdrawMoney(16300);
            atm.WithdrawMoney(5300);
            atm.WithdrawMoney(5300);
            atm.WithdrawMoney(5600);
            atm.WithdrawMoney(5600);
            atm.WithdrawMoney(26300);
        }
    }


    public interface ICurrency : IComparable
    {
        ushort Value { get; }
    }

    public abstract class CurrencyBase : ICurrency
    {
        public abstract ushort Value { get; }
        
        public virtual int CompareTo(object obj)
        {
            if (!(obj is ICurrency currency)) return -1;
            var val = currency.Value;

            if (val > Value) return 1;
            if (val < Value) return -1;
            return 0;
        }
    }

    public class Currency1000 : CurrencyBase
    {
        public override ushort Value => 1000;
    }
    
    public class Currency500 : CurrencyBase
    {
        public override ushort Value => 500;
    }

    public class Currency100 : CurrencyBase
    {
        public override ushort Value => 100;
    }


    // Not required.. just for extending the atm currency slots
    public class Currency50 : CurrencyBase
    {
        public override ushort Value => 50;
    }

    public class Currency20 : CurrencyBase
    {
        public override ushort Value => 20;
    }

    public class Currency10 : CurrencyBase
    {
        public override ushort Value => 10;
    }

    //-----------------


    public interface IAtm
    {
        SortedDictionary<ICurrency, uint> Currencies { get; set; }

        uint AtmBalance { get; }

        SortedDictionary<ICurrency, uint> WithdrawMoney(uint amount);
    }

    public class Atm : IAtm
    {
        public Atm()
        {
            // Initialize the atm currencies
            //Currencies = new SortedDictionary<ICurrency, uint>()
            //{
            //    [new Currency1000()] = 50,
            //    [new Currency500()] = 100,
            //    [new Currency100()] = 1000
            //};

            Currencies = new SortedDictionary<ICurrency, uint>()
            {
                [new Currency1000()] = 50,
                [new Currency500()] = 100,
                [new Currency100()] = 1000,
                [new Currency50()] = 1000,
                [new Currency20()] = 1000,
                [new Currency10()] = 1000
            };
        }

        public SortedDictionary<ICurrency, uint> Currencies { get; set; }

        public uint AtmBalance =>
            Currencies.Aggregate<KeyValuePair<ICurrency, uint>, uint>(0, (current, currency) 
                => current + currency.Key.Value * currency.Value);

        public SortedDictionary<ICurrency, uint> WithdrawMoney(uint amount)
        {
            var tempAmount = amount;

            SortedDictionary<ICurrency, uint> withdrawnCurrencies = new SortedDictionary<ICurrency, uint>();

            if (tempAmount > AtmBalance)
            {
                Console.WriteLine($"WITHDRAW:{amount}. Cannot withdraw. ATM is poorer than you !");

                return withdrawnCurrencies;
            }


            // Get withdrawn currencies
            foreach (var currency in Currencies.ToDictionary(c => c.Key, c => c.Value))
            {
                var noOfNotes = Math.DivRem((int) tempAmount, (int) currency.Key.Value, out var balance);

                if (noOfNotes < currency.Value)
                {
                    withdrawnCurrencies.Add(currency.Key, (uint) noOfNotes);
                    tempAmount = (uint) balance;

                    // Update the currencies
                    Currencies[currency.Key] = Currencies[currency.Key] - (uint)noOfNotes;
                }
                else
                {
                    withdrawnCurrencies.Add(currency.Key, currency.Value);
                    tempAmount = amount - (currency.Value * currency.Key.Value);

                    // Empty the coffers
                    Currencies[currency.Key] = 0;
                }
            }


            // Print just for sake

            Console.WriteLine($"WITHDRAWN:{amount}");
            foreach (var keyValuePair in withdrawnCurrencies)
            {
                Console.Write($"{keyValuePair.Key.Value}:{keyValuePair.Value}, ");
            }
            Console.WriteLine("");

            Console.WriteLine("ATM BALANCE");
            foreach (var keyValuePair in Currencies)
            {
                Console.Write($"{keyValuePair.Key.Value}:{keyValuePair.Value}, ");
            }
            Console.WriteLine("");
            Console.WriteLine("");


            return withdrawnCurrencies;
        }
    }

}

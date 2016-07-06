using System;
using Akka.Actor;
using ProcessManager.Messages;

namespace ProcessManager
{
    public class Bank : ReceiveActor
    {
        private readonly string _bankId;
        private readonly double _primeRate;
        private readonly double _ratePremium;
        private readonly Random _randomDiscount = new Random();
        private readonly Random _randomQuoteId = new Random();

        public Bank(string bankId, double primeRate, double ratePremium)
        {
            _bankId = bankId;
            _primeRate = primeRate;
            _ratePremium = ratePremium;

            Receive<QuoteLoanRate>(msg =>
            {
                Console.WriteLine($"Bank received {msg.GetType().Name}. {msg}");
                var interestRate = CalculateInterestRate(msg.Amount, msg.TermInMonths, msg.CreditScore);
                Sender.Tell(new BankLoanRateQuoted(
                    _bankId,
                    _randomQuoteId.Next(0,1000).ToString(),
                    msg.LoanQuoteReferenceId,
                    msg.TaxId,
                    interestRate
                    ));
            });
        }

        private double CalculateInterestRate(double amount, double months, double creditScore)
        {
            var creditScoreDiscount = creditScore/100.0/10.0 - (_randomDiscount.Next(0, 5)*5);
            return _primeRate + _ratePremium + ((months/12.0)/10.0) - creditScoreDiscount;
        }

        public static Props DefaultProps(string bankId, double primeRate, double ratePremium)
        {
            return Props.Create(() => new Bank(bankId, primeRate, ratePremium));
        }
    }
}
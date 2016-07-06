using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using ProcessManager.Messages;

namespace ProcessManager
{
    public class LoanRateQuote : ReceiveActor
    {
        private readonly ActorSystem _system;
        private readonly string _loanRateQuoteId;
        private readonly string _taxId;
        private readonly int _amount;
        private readonly int _termInMonths;
        private readonly IActorRef _loanBroker;
        private readonly List<BankLoanRateQuote> _bankLoanRateQuotes;
        private int _creditRatingScore;
        private int _expectedLoanRateQuotes;

        public static Props CreateProps(ActorSystem system, string loanRateQuoteId, string taxId, int amount, int termInMonths, IActorRef loanBroker)
        {
            return Props.Create(() => new LoanRateQuote(system, loanRateQuoteId, taxId, amount, termInMonths, loanBroker));
        }

        public LoanRateQuote(ActorSystem system, string loanRateQuoteId, string taxId, int amount, int termInMonths, IActorRef loanBroker)
        {
            _system = system;
            _loanRateQuoteId = loanRateQuoteId;
            _taxId = taxId;
            _amount = amount;
            _termInMonths = termInMonths;
            _loanBroker = loanBroker;
            _bankLoanRateQuotes = new List<BankLoanRateQuote>();
            _creditRatingScore = 0;
            _expectedLoanRateQuotes = 0;

            Receive<StartLoanRateQuote>(msg =>
            {
                Console.WriteLine($"LoanRateQuote received {msg.GetType().Name}. {msg}");
                _expectedLoanRateQuotes = msg.ExpectedLoanRateQuotes;
                _loanBroker.Tell(new LoanRateQuoteStarted(_loanRateQuoteId, _taxId));
            });

            Receive<EstablishCreditScoreForLoanRateQuote>(msg =>
            {
                Console.WriteLine($"LoanRateQuote received {msg.GetType().Name}. {msg}");
                _creditRatingScore = msg.Score;
                if (QuotableCreditScore(_creditRatingScore))
                {
                    _loanBroker.Tell(new CreditScoreForLoanRateQuoteEstablished(
                        _loanRateQuoteId,
                        _taxId,
                        _creditRatingScore,
                        _amount,
                        _termInMonths));
                }
                else
                {
                    _loanBroker.Tell(new CreditScoreForLoanRateQuoteDenied(
                        _loanRateQuoteId,
                        _taxId,
                        _amount,
                        _termInMonths,
                        _creditRatingScore));
                }
            });

            Receive<RecordLoanRateQuote>(msg =>
            {
                Console.WriteLine($"LoanRateQuote received {msg.GetType().Name}. {msg}");
                var bankLoanRateQuote = new BankLoanRateQuote(
                    msg.BankId,
                    msg.BankLoanRateQuoteId,
                    msg.InterestRate);
                _bankLoanRateQuotes.Add(bankLoanRateQuote);
                _loanBroker.Tell(new LoanRateQuoteRecorded(
                    _loanRateQuoteId,
                    _taxId,
                    bankLoanRateQuote));

                if (_bankLoanRateQuotes.Count >= _expectedLoanRateQuotes)
                {
                    _loanBroker.Tell(new LoanRateBestQuoteFilled(
                        _loanRateQuoteId,
                        _taxId,
                        _amount,
                        _termInMonths,
                        _creditRatingScore,
                        BestBankLoanRateQuote()));
                }
            });

            Receive<TerminateLoanRateQuote>(msg =>
            {
                Console.WriteLine($"LoanRateQuote {msg.GetType().Name}. received {msg}");
                _loanBroker.Tell(new LoanRateQuoteTerminated(
                    _loanRateQuoteId,
                    _taxId));
            });
        }

        private BankLoanRateQuote BestBankLoanRateQuote() => _bankLoanRateQuotes
            .OrderBy(blrq => blrq.InterestRate)
            .First();

        private bool QuotableCreditScore(int score) => score > 399;

        public static IActorRef Apply(ActorSystem system, string loanRateQuoteId, string taxId, int amount,
            int termInMonths, IActorRef loanBroker)
        {
            return system.ActorOf(CreateProps(system, loanRateQuoteId, taxId, amount, termInMonths, loanBroker), $"loanRateQuote-{loanRateQuoteId}");
        }
    }
}
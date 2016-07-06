using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using Akka.Util.Internal;
using ProcessManager.Messages;

namespace ProcessManager
{
    public class LoanBroker : ProcessManager
    {
        private readonly IActorRef _creditBureau;
        private readonly IEnumerable<IActorRef> _banks;

        public static Props DefaultProps(IActorRef creditBureau, IEnumerable<IActorRef> banks)
        {
            return Props.Create(() => new LoanBroker(creditBureau, banks));
        }

        public LoanBroker(IActorRef creditBureau, IEnumerable<IActorRef> banks)
        {
            _creditBureau = creditBureau;
            _banks = banks;

            Receive<ProcessStarted>(msg =>
            {
                Console.WriteLine($"LoanBroker received {msg.GetType().Name}. {msg}");
                msg.Process.Tell(new StartLoanRateQuote(_banks.Count()));
            });

            Receive<ProcessStopped>(msg =>
            {
                Console.WriteLine($"LoanBroker received {msg.GetType().Name}. {msg}");
                Context.Stop(msg.Process);   
            });

            Receive<BankLoanRateQuoted>(msg =>
            {
                Console.WriteLine($"LoanBroker received {msg.GetType().Name}. {msg}");
                ProcessOf(msg.LoanQuoteReferenceId)?.Tell(
                    new RecordLoanRateQuote(
                        msg.BankId, 
                        msg.BankLoanRateQuoteId,
                        msg.InterestRate));
            });

            Receive<CreditChecked>(msg =>
            {
                Console.WriteLine($"LoanBroker received {msg.GetType().Name}. {msg}");
                ProcessOf(msg.CreditProcessingReferenceId)?.Tell(
                    new EstablishCreditScoreForLoanRateQuote(
                        msg.CreditProcessingReferenceId,
                        msg.TaxId,
                        msg.Score));
            });

            Receive<CreditScoreForLoanRateQuoteDenied>(msg =>
            {
                Console.WriteLine($"LoanBroker received {msg.GetType().Name}. {msg}");
                ProcessOf(msg.LoanRateQuoteId)?.Tell(new TerminateLoanRateQuote());
                //ProcessManagerDriver.CompleteAll();
                var denied = new BestLoanRateDenied(
                    msg.LoanRateQuoteId,
                    msg.TaxId,
                    msg.Amount,
                    msg.TermInMonths,
                    msg.Score);
                Console.WriteLine($"Would be sent to original requester: {denied}");
            });

            Receive<CreditScoreForLoanRateQuoteEstablished>(msg =>
            {
                Console.WriteLine($"LoanBroker received {msg.GetType().Name}. {msg}");
                banks.ForEach(bank =>
                {
                    ActorRefImplicitSenderExtensions.Tell(bank, new QuoteLoanRate(
                        msg.LoanRateQuoteId,
                        msg.TaxId,
                        msg.Score,
                        msg.Amount,
                        msg.TermInMonths));
                });
            });

            Receive<LoanRateBestQuoteFilled>(msg =>
            {
                Console.WriteLine($"LoanBroker received {msg.GetType().Name}. {msg}");
                StopProcess(msg.LoanRateQuoteId);

                var best = new BestLoanRateQuoted(
                    msg.BestBankLoanRateQuote.BankId,
                    msg.LoanRateQuoteId,
                    msg.TaxId,
                    msg.Amount,
                    msg.TermInMonths,
                    msg.CreditScore,
                    msg.BestBankLoanRateQuote.InterestRate);
                Console.WriteLine($"Would be sent to original requester: {best}");
            });

            Receive<LoanRateQuoteRecorded>(msg =>
            {
                Console.WriteLine($"LoanBroker received {msg.GetType().Name}. {msg}");
                // ProcessManagerDriver.completedStep
            });

            Receive<LoanRateQuoteStarted>(msg =>
            {
                Console.WriteLine($"LoanBroker received {msg.GetType().Name}. {msg}");
                _creditBureau.Tell(new CheckCredit(msg.LoanRateQuoteId, msg.TaxId));
            });

            Receive<LoanRateQuoteTerminated>(msg =>
            {
                Console.WriteLine($"LoanBroker received {msg.GetType().Name}. {msg}");
                StopProcess(msg.LoanRateQuoteId);
            });
            
            Receive<ProcessStarted>(msg =>
            {
                Console.WriteLine($"LoanBroker received {msg.GetType().Name}. {msg}");
                msg.Process.Tell(new StartLoanRateQuote(_banks.Count()));
            });

            Receive<ProcessStopped>(msg =>
            {
                Console.WriteLine($"LoanBroker received {msg.GetType().Name}. {msg}");
                Context.Stop(msg.Process);
            });

            Receive<QuoteBestLoanRate>(msg =>
            {
                Console.WriteLine($"LoanBroker received {msg.GetType().Name}. {msg}");
                var loanRateQuoteId = Guid.NewGuid().ToString();

                var loanRateQuote = Context.ActorOf( 
                    LoanRateQuote.CreateProps(
                        Context.System,
                        loanRateQuoteId,
                        msg.TaxId,
                        msg.Amount,
                        msg.TermInMonths,
                        Self));
                StartProcess(loanRateQuoteId, loanRateQuote);
            });
        }
    }
}
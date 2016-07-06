using System;
using System.Collections.Generic;
using Akka.Actor;
using ProcessManager.Messages;

namespace ProcessManager
{
    public class CreditBureau : ReceiveActor
    {
        private readonly List<int> _creditRanges = new List<int>() {300,400,500,600,700};
        private readonly Random _randomCreditRangeGenerator = new Random();
        private readonly Random _randomCreditScoreGenerator = new Random();

        public CreditBureau()
        {
            Receive<CheckCredit>(msg =>
            {
                Console.WriteLine($"CreditBureau received {msg.GetType().Name}. {msg}");
                var range = _creditRanges[_randomCreditRangeGenerator.Next(0, 4)];
                var score = range + _randomCreditScoreGenerator.Next(0, 20);
                Sender.Tell(new CreditChecked(
                    msg.CreditProcessingReferenceId,
                    msg.TaxId,
                    score));
            });
        }

        public static Props DefaultProps()
        {
            return Props.Create(() => new CreditBureau());
        }
    }
}
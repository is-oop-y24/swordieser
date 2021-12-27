using System;
using Banks.Banks;

namespace Banks
{
    public class TimeMachine
    {
        public static void TimeRewind(CentralBank centralBank, DateTime dateTime)
        {
            centralBank.NotifyBanks(dateTime);
        }
    }
}
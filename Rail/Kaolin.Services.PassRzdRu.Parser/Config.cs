namespace Kaolin.Services.PassRzdRu.Parser
{
    public class Config
    {
        public PollingConfig Polling { get; set; }

        public Config()
        {
        }

        public class PollingConfig
        {
            public Polling TrainList { get; set; }
            public Polling CarList { get; set; }
            public Polling Order { get; set; }
            public Polling ElReg { get; set; }

            public PollingConfig(int maxRetry, int interval)
            {
                TrainList = new Polling(maxRetry, interval);
                CarList = new Polling(maxRetry, interval);
                Order = new Polling(maxRetry, interval);
                ElReg = new Polling(maxRetry, interval);
            }

            public PollingConfig()
            {
            }

            public class Polling
            {
                public int MaxRetry { get; set; }
                public int Interval { get; set; }

                public Polling()
                {
                }

                public Polling(int maxRetry, int interval)
                {
                    MaxRetry = maxRetry;
                    Interval = interval;
                }
            }
        }
    }
}

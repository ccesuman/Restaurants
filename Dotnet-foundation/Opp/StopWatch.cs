namespace Encapsulation
{
    public class StopWatch
    {
        private DateTime? _startTime;

        private void Start()
        {
            if (_startTime != null)
            {
                throw new InvalidOperationException("Stopwatch is already running.");
            }
            _startTime = DateTime.Now;
        }

        private TimeSpan Stop()
        {
            if (_startTime == null)
            {
                throw new InvalidOperationException("Stopwatch is not running.");
            }
            var elapsed = DateTime.Now - _startTime.Value;
            _startTime = null;
            return elapsed;
        }

        public static void ManageStopWatchFlow()
        {
            Console.WriteLine("Please start the Watch by typaing Start");

            string? input = Console.ReadLine()?.ToLower();
            var watch = new StopWatch();

            if (input == "start")
            {
                watch.Start();
                Console.WriteLine("Watch started. Type Stop to stop the Watch.");
                input = Console.ReadLine()?.ToLower();
                if (input == "stop")
                {
                    TimeSpan elapsed = watch.Stop();
                    Console.WriteLine($"Watch stopped. at:  {(int)elapsed.TotalHours}h {elapsed.Minutes}m {elapsed.Seconds}sec.");
                }
                else
                {
                    Console.WriteLine("Invalid input. Please type Stop to stop the Watch.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please type Start to start the Watch.");
            }
        }
    }
}
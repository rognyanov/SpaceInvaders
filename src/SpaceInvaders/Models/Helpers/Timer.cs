namespace SpaceInvaders.Models.Helpers
{
    /// <summary>
    /// Represents a ticking timer that is counting up to some limit
    /// </summary>
    public sealed class Timer
    {
        private readonly int _limit;
        private int _counter;

        public Timer(int limit)
        {
            _limit = limit;
            _counter = 0;
        }

        /// <summary>
        /// Check if the timer has reached the target limit
        /// </summary>
        /// <returns></returns>
        public bool IsCounting()
        {
            var result = true;

            _counter++;

            if (_counter >= _limit)
            {
                result = false;
                _counter = 0;
            }

            return result;
        }
    }
}
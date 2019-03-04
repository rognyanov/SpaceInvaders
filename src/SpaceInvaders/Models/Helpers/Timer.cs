namespace SpaceInvaders.Models.Helpers
{
    public sealed class Timer
    {
        private readonly int _limit;
        private int _counter;

        public Timer(int limit)
        {
            _limit = limit;
            _counter = 0;
        }

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
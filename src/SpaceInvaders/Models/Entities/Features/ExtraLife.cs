using SpaceInvaders.Contracts.Features;
using SpaceInvaders.Contracts.Visual;
using SpaceInvaders.Models.Entities.Visual;
using SpaceInvaders.Models.Grid;
using SpaceInvaders.Models.Helpers;

namespace SpaceInvaders.Models.Entities.Features
{
    public sealed class ExtraLife : IExtraLife
    {
        private readonly IRenderer<string> _renderer;
        private readonly IGameHeader _gameHeader;
        private const int EXTRA_LIFE_SCORE = 20000;
        private const int EXTRA_LIFE_TIME = 50;
        private const int LIFE_BLINKER_INTERVAL = 8;
        private readonly Timer _counter;
        private readonly Timer _lifeBlinker;
        private readonly TextBlinker _blinker;
        private bool _isDisplayed;
        private bool _displayLife;
        private int _nextExtraLifeScore;

        public ExtraLife(IRenderer<string> renderer, IGameHeader gameHeader)
        {
            _renderer = renderer;
            _gameHeader = gameHeader;
            _counter = new Timer(EXTRA_LIFE_TIME);
            _blinker = new TextBlinker(new ConsolePosition(40,8), _renderer, "E X T R A   L I F E!" );
            _lifeBlinker = new Timer(LIFE_BLINKER_INTERVAL);
            _nextExtraLifeScore = EXTRA_LIFE_SCORE;
        }

        public bool Invoke(int score, int lifes)
        {
            var result = false;

            if (_isDisplayed)
            {
                if (_counter.IsCounting())
                {
                    BlinkTextAndLifes(lifes);
                }
                else
                {
                    _isDisplayed = false;
                    result = true;
                }
            }
            else if (score>=_nextExtraLifeScore)
            {
                _isDisplayed = true;
                _nextExtraLifeScore += EXTRA_LIFE_SCORE;
            }

            return result;
        }

        private void BlinkTextAndLifes(int lifes)
        {
            _blinker.Invoke();
            if (_lifeBlinker.IsCounting())
                return;

            _displayLife = !_displayLife;
            if (_displayLife)
            {
                _gameHeader.RenderLifes(lifes);
            }
            else
            {
                _gameHeader.UnrenderLifes();
            }
        }
    }
}

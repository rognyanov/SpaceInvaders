using SpaceInvaders.Contracts;
using SpaceInvaders.Models.Entities.Visual;
using SpaceInvaders.Models.Grid;
using SpaceInvaders.Models.Helpers;

namespace SpaceInvaders.Models.Entities.Features
{
    public class ExtraLife
    {
        private readonly IRenderer<string> _renderer;
        private readonly GameHeader _gameHeader;
        private const int EXTRA_LIFE_SCORE = 10000;
        private const int EXTRA_LIFE_TIME = 50;
        private const int LIFE_BLINKER_INTERVAL = 8;
        private Timer _counter;
        private Timer _lifeBlinker;
        private TextBlinker _blinker;
        private bool _isDisplayed;
        private bool _displayLife;
        private int _nextExtraLifeScore;

        public ExtraLife(IRenderer<string> renderer, GameHeader gameHeader)
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

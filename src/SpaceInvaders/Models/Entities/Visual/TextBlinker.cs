using SpaceInvaders.Contracts;
using SpaceInvaders.Models.Helpers;
using System;

namespace SpaceInvaders.Models.Entities.Visual
{
    public class TextBlinker
    {
        private IRenderer<string> _renderer;
        private Timer _counter;
        private IPosition _position;
        private string _text;
        private bool _diplayText;

        public TextBlinker(IPosition position, IRenderer<string> renderer, string text)
        {
            _renderer = renderer;
            _counter = new Timer(8); //TODO: BLINK_INTERVAL
            _position = position;
            _text = text;
            _diplayText = false;
        }

        public void Invoke()
        {
            if (!_counter.IsCounting())
            {
                _diplayText = !_diplayText;
                _renderer.SetColor(ConsoleColor.Yellow);

                if (_diplayText)
                {
                    _renderer.DrawAtPosition(_position.X, _position.Y, _text);
                }
                else
                {
                    for (var i = 0; i < _text.Length; i++)
                    {
                        _renderer.DrawAtPosition(_position.X + i, _position.Y, " ");
                    }
                }
            }
        }
    }
}
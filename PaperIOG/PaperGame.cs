using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Framework.WpfInterop;
using MonoGame.Framework.WpfInterop.Input;
using PaperIOG.DataContracts;

namespace PaperIOG
{
    public class PaperGame : WpfGame
    {
        private WpfMouse _mouse;
        private WpfKeyboard _keyboard;
        private IGraphicsDeviceService _graphicsDeviceManager;

        SpriteBatch _spriteBatch;

        //"name" : [player, line, territory]
        private readonly Dictionary<string, Color[]> _colors = new Dictionary<string, Color[]>();

        private readonly Dictionary<JBonusType, Texture2D> _bonuses = new Dictionary<JBonusType, Texture2D>();

        private int _infoIndex;
        private readonly JVisio _visio;
        private readonly int _cellWidth;

        private SpriteFont _font;
        private Texture2D _rectangleBlock;
        
        public PaperGame(JVisio visio)
        {
            _visio = visio;
            _cellWidth = _visio.Info.First().Width;

            Content = new ContentManager(Services) {RootDirectory = "Content"};
        }

        protected override void Initialize()
        {
            _mouse = new WpfMouse(this);
            _keyboard = new WpfKeyboard(this);

            _graphicsDeviceManager = new WpfGraphicsDeviceService(this);

            Width = _visio.Info.First().XCellsCount * _visio.Info.First().Width;
            Height = _visio.Info.First().YCellsCount * _visio.Info.First().Width;

            _infoIndex = 0;

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _font = Content.Load<SpriteFont>("Font");

            _rectangleBlock = new Texture2D(GraphicsDevice, 1, 1);
            _rectangleBlock.SetData(Enumerable.Repeat(Color.AliceBlue, _rectangleBlock.Width * _rectangleBlock.Height).ToArray());

            _colors.Add("1", new[] { new Color(65, 134, 128), new Color(138, 189, 187), new Color(90, 159, 153) });
            _colors.Add("2", new[] { new Color(191, 2, 71), new Color(217, 106, 151), new Color(216, 27, 96) });
            _colors.Add("3", new[] { new Color(71, 100, 114), new Color(142, 168, 178), new Color(96, 125, 139) });
            _colors.Add("4", new[] { new Color(67, 82, 167), new Color(140, 157, 211), new Color(92, 107, 192) });
            _colors.Add("5", new[] { new Color(220, 99, 0), new Color(236, 167, 91), new Color(245, 124, 0) });

            _bonuses.Add(JBonusType.Explorer, Content.Load<Texture2D>("explorer"));
            _bonuses.Add(JBonusType.Flash, Content.Load<Texture2D>("flash"));
            _bonuses.Add(JBonusType.Saw, Content.Load<Texture2D>("saw"));

            base.Initialize();
        }

        protected override void Update(GameTime time)
        {
            // every update we can now query the keyboard & mouse for our WpfGame
            var mouseState = _mouse.GetState();
            var keyboardState = _keyboard.GetState();

            if (++_infoIndex >= _visio.Info.Count)
                _infoIndex = 0;
        }

        protected override void Draw(GameTime time)
        {
            GraphicsDevice.Clear(Color.AliceBlue);

            _spriteBatch.Begin();

            if (_visio.Info[_infoIndex].Players != null)
            {
                foreach (var player in _visio.Info[_infoIndex].Players)
                    foreach (var point in player.Value.Territory)
                        _spriteBatch.Draw(_rectangleBlock,
                            new Rectangle(point.X - _cellWidth / 2, (int)Height - (point.Y + _cellWidth / 2), _cellWidth, _cellWidth), _colors[player.Key][2]);

                foreach (var player in _visio.Info[_infoIndex].Players)
                    foreach (var point in player.Value.Lines)
                        _spriteBatch.Draw(_rectangleBlock,
                            new Rectangle(point.X - _cellWidth / 2, (int)Height - (point.Y + _cellWidth / 2), _cellWidth, _cellWidth), _colors[player.Key][1]);

                foreach (var player in _visio.Info[_infoIndex].Players)
                    _spriteBatch.Draw(_rectangleBlock,
                        new Rectangle(player.Value.Position.X - _cellWidth / 2, (int)Height - (player.Value.Position.Y + _cellWidth / 2), _cellWidth, _cellWidth), _colors[player.Key][0]);
            }

            if (_visio.Info[_infoIndex].Bonuses != null)
            {
                foreach (var bonus in _visio.Info[_infoIndex].Bonuses)
                {
                    _spriteBatch.Draw(_bonuses[bonus.BonusType],
                        new Rectangle(bonus.Position.X - _cellWidth / 2,
                            (int) Height - (bonus.Position.Y + _cellWidth / 2), _cellWidth, _cellWidth), Color.White);

                    _spriteBatch.DrawString(_font, bonus.Position.ToString(), new Vector2(bonus.Position.X - (float)_cellWidth / 2, (float)Height - (bonus.Position.Y + (float)_cellWidth / 2)), Color.Black);
                }
            }

            _spriteBatch.End();
        }
    }
}
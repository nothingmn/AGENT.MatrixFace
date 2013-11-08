using System;
using System.Collections;
using System.Threading;
using AGENT.Contrib.Util;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Media;
using Random = AGENT.Contrib.Util.Random;

namespace MatrixFace
{
    public class Matrix
    {
        public void MakeItRain(Bitmap Display, Font Font, Color Color, int ThreadCount, int PerThreadCount)
        {
            _display = Display;
            _font = Font;
            _color = Color;
            for (int d = 0; d <= ThreadCount; d++)
            {
                DropMultiple(PerThreadCount);
            }
            while(drops>0) Thread.Sleep(100);
        }

        private Color _color;
        private Font _font;
        private Bitmap _display;
        private int drops = 0;
        public void DropMultiple(int Count)
        {
            ThreadUtil.SafeQueueWorkItem(new ThreadStart(() =>
                {
                    for (int counter = 0; counter <= Count; counter++)
                    {
                        var c = Random.Next(65, 90);
                        var x = Random.Next(1, 128-_font.CharWidth((char)c));
                        SingleDrop(x, (char) c);

                    }
                }));

        }
        private void SingleDrop(int X, char C)
        {
            int increment = _font.Height;
            drops++;
            var speed = Random.Next(20, 60);
            for (int y = 0; y <= 128; y += increment)
            {
                _display.DrawText(C.ToString(), _font, _color, X, y);
                _display.Flush(X, y, _font.CharWidth(C), _font.Height);

                if (y > 0)
                {
                    _display.DrawRectangle(Color.Black, 0, X, y - increment, _font.CharWidth(C), _font.Height, 0, 0, 0, 0, 0, Color.Black, 0, 0, 255);
                    _display.Flush(X, y - increment, _font.CharWidth(C), _font.Height);
                }


                Thread.Sleep(speed);
            }

            drops--;
            
        }
        
    }
}

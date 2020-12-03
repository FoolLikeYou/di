﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using HomeExerciseTDD.settings;

namespace HomeExerciseTDD
{
    public class WordCloudPainter : IPainter<Word>
    {
        private readonly Bitmap bitmap;
        private readonly Graphics graphics;
        private Color color;
        private readonly Random randomazer;
        private readonly List<ISizedWord> words;
        private readonly int offsetX;
        private readonly int offsetY;
        
        
        private readonly PainterSettings settings;
        private readonly IWordCloud wordCloud;

        public WordCloudPainter(IWordCloud wordCloud, PainterSettings settings)
        {
            wordCloud.BuildCloud();
            this.wordCloud = wordCloud;
            this.settings = settings;

            offsetX = settings.Width/2;
            offsetY = settings.Height/2;
            randomazer= new Random();

            color = settings.Color;
            bitmap = new Bitmap(settings.Width, settings.Height);
            graphics = Graphics.FromImage(bitmap);
        }

        public void DrawFigures()
        {
            var center = wordCloud.Center;
            
            foreach (var word in wordCloud.SizedWords)
            {
                DrawWord(word, center);
            }

            var fileName = $"{settings.FileName}.{settings.Format}";
            bitmap.Save(fileName, settings.Format);
        }

        private void DrawWord(ISizedWord word, Point center)
        {
            var newX= word.Rectangle.X + offsetX - center.X;
            var newY = word.Rectangle.Y + offsetY - center.Y;
            var point = new Point(newX, newY);
            var newRectangle = new Rectangle(point, word.Rectangle.Size);
            var wordFont = new Font(word.Font, word.Size, FontStyle.Bold, GraphicsUnit.Point);//????
                
            var brush = new SolidBrush(settings.Color);
                
            graphics.DrawString(word.Text, wordFont, brush, newRectangle.Location);
        }
    }
}
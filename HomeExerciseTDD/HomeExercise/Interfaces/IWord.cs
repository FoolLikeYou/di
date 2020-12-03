﻿using System.Drawing;

namespace HomeExerciseTDD
{
    public interface IWord
    {
        public string Text { get; }
        public FontFamily Font { get; }
        public int Size { get; }
    }
}
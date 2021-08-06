﻿using Cosmos.System;
using Cosmos.System.Graphics;

namespace PrismProject.System2.Drawing
{
    /// <summary>
    /// The mouse manager.
    /// </summary>
    internal class Mouse
    {
            private static readonly SVGAIICanvas Screen = Drivers.Video.Screen;
            private static readonly int SW = Drivers.Video.SW, SH = Drivers.Video.SH;
            public static int lastX, lastY;
            public static int X { get => (int)MouseManager.X; }
            public static int Y { get => (int)MouseManager.Y; }
            public static MouseState State { get => MouseManager.MouseState; }

            public Mouse()
            {
                MouseManager.ScreenWidth = (uint)SW;
                MouseManager.ScreenHeight = (uint)SH;
            }

            public static void Update()
            {
                Screen.DrawImageAlpha(Images.mouse, X, Y);
                lastX = X;
                lastY = Y;
            }
        }
    }

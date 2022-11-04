using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA.GUI
{
    public struct Panel
    {
        /// <summary>
        /// x value of rect containing Panel
        /// </summary>
        public float x { get; private set; }
        /// <summary>
        /// y value of rect containing Panel
        /// </summary>
        public float y { get; private set; }
        /// <summary>
        /// width of rect containing Panel
        /// </summary>
        public float w { get; private set; }
        /// <summary>
        /// height of rect containing Panel
        /// </summary>
        public float h { get; private set; }

        /// <summary>
        /// Temporary horizontal marker.
        /// </summary>
        public float x2 { get; set; }
        /// <summary>
        /// Temporary vertical marker.
        /// </summary>
        public float y2 { get; set; }

        public float nudgeValue { get; private set; }

        /// <summary>
        /// Icon dimensions (height and width)
        /// </summary>
        public float iconSize { get; private set; }
        /// <summary>
        /// y difference between lines
        /// </summary>
        public float lineHeight { get; private set; }
        /// <summary>
        /// Style of Panel
        /// </summary>
        public GUIStyle style { get; private set; }

        /// <summary>
        /// Create a panel
        /// </summary>
        /// <param name="rect">Assigned to x, y, w, h variables</param>
        /// <param name="lineHeight"></param>
        /// <param name="iconSize"></param>
        /// <param name="style"></param>
        public Panel(Rect rect, float lineHeight, float iconSize, GUIStyle style)
        {
            x = rect.x;
            y = rect.y;
            w = rect.width;
            h = rect.height;
            
            x2 = x;
            y2 = y;

            nudgeValue = 5;

            this.lineHeight = lineHeight;
            this.iconSize = iconSize;
            this.style = style;
        }

        /// <summary>
        /// Reset horizontal marker
        /// </summary>
        public void ResetX() { x2 = x; }
        /// <summary>
        /// Reset vertical marker
        /// </summary>
        public void ResetY() { y2 = y; }
        
        /// <summary>
        /// Move vertical marker down by lineHeight, reset horizontal marker
        /// </summary>
        public void NextLine() 
        {
            x2 = x;
            y2 += lineHeight; 
        }
        /// <summary>
        /// Move vertical marker up by lineHeight, reset horizontal marker
        /// </summary>
        public void PrevLine() 
        {
            x2 = x;
            y2 -= lineHeight;  
        }

        /// <summary>
        /// Move horizontally 5 units.
        /// </summary>
        /// <param name="right">Move right if true, left if false.  Default true.</param>
        public void NudgeX(bool right = true)
        {
            if (right)
                x2 += nudgeValue;
            else
                x2 -= nudgeValue;
        }

        /// <summary>
        /// Move vertically 5 units.
        /// </summary>
        /// <param name="down">Move down if true, left if false.  Default true.</param>
        public void NudgeY(bool down = true)
        {
            if (down)
                y2 += nudgeValue;
            else
                y2 -= nudgeValue;
        }

        /// <summary>
        /// Returns height/lineHeight rounded down
        /// </summary>
        public int Lines { get {return (int)Mathf.Floor(h / lineHeight);} }

        /// <summary>
        /// Returns Rect the size of Panel.
        /// </summary>
        public Rect FullBox { get { return new Rect(x, y, w, h); } }

        /// <summary>
        /// Returns Rect the width of Panel, but lineHeight tall at vertical marker.  
        /// Advances marker by lineHeight.
        /// </summary>
        public Rect LineBox 
        { 
            get 
            {
                Rect box = new Rect(x, y2, w, lineHeight);
                NextLine();
                return box; 
            }  
        }

        /// <summary>
        /// Returns panel the width of Panel, and lineHeight tall.
        /// Advances vertical marker by lineHeight.
        /// Panel copies lineHeight, iconSize, style.
        /// </summary>
        public Panel LinePanel
        {
            get
            {
                return new Panel(LineBox, lineHeight, iconSize, style);
            }
        }

        /// <summary>
        /// Returns box at horizontal and vertical markers of iconSize width and height.
        /// Advanced horizontal marker by iconSize.
        /// </summary>
        public Rect IconBox 
        { 
            get 
            {
                Rect box = new Rect(x2, y2, iconSize, iconSize);
                x2 += iconSize;
                return box;
            } 
        }

        /// <summary>
        /// Returns tall, narrow Rect on right side of Panel
        /// </summary>
        public Rect ScrollBox 
        { 
            get 
            { 
                return new Rect((x + w - 15), y, 30, h); 
            } 
        }


        /// <summary>
        /// Returns Rect the width of Panel, with variable height.
        /// Advances vertical marker by Rect height.
        /// </summary>
        /// <param name="height">If less than 0, throws exception.
        /// If between 0 and 1 is Panel height multiplier.
        /// If greater than 1 is lineHeight multiplier.</param>
        /// <returns></returns>
        public Rect TallWideBox(float height)
        {
            Rect rect;
            if (height < 0)
                throw new ArgumentOutOfRangeException();
            if (height <= 1)
                rect = new Rect(x, y2, w, height * h);
            else
                rect = new Rect(x, y2, w, lineHeight * height);

            y2 += rect.height;
            ResetX();
            return rect;
        }

        /// <summary>
        /// Returns rect of lineHeight height, and variable width, 
        /// at veritcal and horizontal markers.
        /// Advances horizontal marker by Rect width.
        /// </summary>
        /// <param name="width">If less than 0, exception thrown.
        /// If between 0 and 1, Panel width multiplier.
        /// If greater than 1, width.</param>
        /// <returns></returns>
        public Rect Box(float width)
        {
            Rect rect;
            if (width < 0)
                throw new ArgumentOutOfRangeException();
            if (width <= 1)
                rect = new Rect(x2, y2, w * width, lineHeight); 
            else
                rect = new Rect(x2, y2, width, lineHeight); 

            x2 += rect.width;
            return rect;
        }

        /// <summary>
        /// Returns rect of custom width and height at vertical and horizontal markers.
        /// Advances markers width and height.
        /// </summary>
        /// <param name="width">If less than 0, exception thrown.
        /// If between 0 and 1, Panel width multiplier.
        /// If greater than 1, width.</param>
        /// <param name="height">If less than 0, throws exception.
        /// If between 0 and 1 is Panel height multiplier.
        /// If greater than 1 is lineHeight multiplier.</param>
        /// <returns></returns>
        public Rect Box(float width, float height)
        {
            if (width < 0 || height < 0)
                throw new ArgumentOutOfRangeException();

            float width2 = (width <= 1 ? (w * width) : width);
            float height2 = (height <= 1 ? (h * height) : height);
            Rect rect = new Rect(x2, y2, width2, height2);
            x2 += rect.width;
            y2 += rect.height;
            return rect;
        }

        /// <summary>
        /// Return Panel with same lineHeight, iconSize, and style, 
        /// but nudged down and right, and 2*nudgeValue smaller in width and height.
        /// </summary>
        /// <returns></returns>
        public Panel Inner()
        {
            Rect box = new Rect(
                x + nudgeValue,
                y + nudgeValue,
                w - (2 * nudgeValue),
                h - (2 * nudgeValue)
            );
            return new Panel(box, lineHeight, iconSize, style);
        }

        /// <summary>
        /// Return Panel with same lineHeight, iconSize, and style, 
        /// but nudged up and left, and 2*nudgeValue larger in width and height.
        /// </summary>
        /// <returns></returns>
 
        public Panel Outer()
        {
            Rect box = new Rect(
                x - nudgeValue,
                y - nudgeValue,
                w + (2 * nudgeValue),
                h + (2 * nudgeValue)
            );
            return new Panel(box, lineHeight, iconSize, style);
        }

        /// <summary>
        /// Returns panel of custom size with same lineHeight, iconSize, and style.
        /// Advances markers width and height of Subpanel
        /// </summary>
        /// <param name="box"></param>
        /// <returns></returns>
        public Panel Subpanel(Rect box)
        {
            Panel p = new Panel(box, lineHeight, iconSize, style);
            x2 += p.w;
            if (x2 > x + w)
                NextLine();
            y2 += p.h;
            return p;
        }

    }
}
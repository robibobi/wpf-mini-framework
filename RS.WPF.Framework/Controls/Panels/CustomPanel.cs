using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace RS.WPF.Framework.Controls.Panels
{
    /// <summary>
    /// Testimplenetierung von Panel/IScrollInfo
    /// </summary>
    class CustomPanel : Panel, IScrollInfo
    {
        private const int MouseWheelOffset = 10;
        private const int LineOffset = 3;

        private bool _canVerticallyScroll;
        private bool _canHorizontallyScroll;
        private ScrollViewer _scrollOwner;
        private Size _extent = new Size();
        private Size _viewPort = new Size();
        private Point _scrollOffset = new Point();
        private TranslateTransform _translateTransfrom = new TranslateTransform();

        public CustomPanel()
        {
            this.RenderTransform = _translateTransfrom;
        }

        public bool CanVerticallyScroll
        {
            get { return _canVerticallyScroll; }
            set { _canVerticallyScroll = value; }
        }

        public bool CanHorizontallyScroll
        {
            get { return _canHorizontallyScroll; }
            set { _canHorizontallyScroll = value; }
        }

        public ScrollViewer ScrollOwner
        {
            get { return _scrollOwner; }
            set { _scrollOwner = value; }
        }

        public double ExtentWidth => _extent.Width;

        public double ExtentHeight => _extent.Height;

        public double ViewportWidth => _viewPort.Width;

        public double ViewportHeight => _viewPort.Height;

        public double HorizontalOffset => _scrollOffset.X;

        public double VerticalOffset => _scrollOffset.Y;

        protected override Size MeasureOverride(Size availableSize)
        {
            var mySize = new Size();
            foreach (UIElement child in InternalChildren)
            {
                // vertikales Stackpanel
                child.Measure(availableSize);
                mySize.Height += child.DesiredSize.Height;
                mySize.Width = (child.DesiredSize.Width > mySize.Width ?
                    child.DesiredSize.Width :
                    mySize.Width);
            }

            // IScroll Info
            var extent = new Size(availableSize.Width, mySize.Height);
            if (_extent != extent)
            {
                _extent = extent;
                _scrollOwner?.InvalidateScrollInfo();
            }

            if (_viewPort != availableSize)
            {
                _viewPort = availableSize;
                _scrollOwner?.InvalidateScrollInfo();
            }


            return mySize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var location = new Point();
            location.Y = 5;

            foreach (UIElement child in this.InternalChildren)
            {
                child.Arrange(new Rect(location, child.DesiredSize));
                location.Y += child.DesiredSize.Height + 5;
            }


            // IScrollInfo
            Size extent = new Size(
                finalSize.Width,
                location.Y);

            //if (extent != _extent)
            //{
            //    _extent = extent;
            //    _scrollOwner?.InvalidateScrollInfo();
            //}

            //if (finalSize != _viewPort)
            //{
            //    _viewPort = finalSize;
            //    _scrollOwner?.InvalidateScrollInfo();
            //}

            return finalSize;
        }

        public void LineUp()
        {
            this.SetVerticalOffset(VerticalOffset - LineOffset);
        }

        public void LineDown()
        {
            this.SetVerticalOffset(VerticalOffset + LineOffset);
        }

        public void LineLeft()
        {
            throw new NotImplementedException();
        }

        public void LineRight()
        {
            throw new NotImplementedException();
        }

        public void PageUp()
        {
            // Festlegung: Page == 25% des extents
            this.SetVerticalOffset(VerticalOffset - 0.25 * _extent.Height);
        }

        public void PageDown()
        {
            this.SetVerticalOffset(VerticalOffset + 0.25 * _extent.Height);
        }

        public void PageLeft()
        {
            throw new NotImplementedException();
        }

        public void PageRight()
        {
            throw new NotImplementedException();
        }

        public void MouseWheelUp()
        {
            SetVerticalOffset(VerticalOffset - MouseWheelOffset);
        }

        public void MouseWheelDown()
        {
            SetVerticalOffset(VerticalOffset + MouseWheelOffset);
        }

        public void MouseWheelLeft()
        {
            throw new NotImplementedException();
        }

        public void MouseWheelRight()
        {
            throw new NotImplementedException();
        }

        public void SetHorizontalOffset(double offset)
        {
            throw new NotImplementedException();
        }

        public void SetVerticalOffset(double offset)
        {
            if (offset <= 0 || _viewPort.Height >= _extent.Height)
            {
                offset = 0;
            }
            else
            {
                // offset größer als maximaler offset?
                if (offset + ViewportHeight >= _extent.Height)
                {
                    // offset auf max. setzen
                    offset = _extent.Height - _viewPort.Height;
                }
            }
            _scrollOffset.Y = offset;
            _scrollOwner?.InvalidateScrollInfo();
            _translateTransfrom.Y = -offset;
        }

        public  /*Rect of the thing we made visible*/ Rect MakeVisible(
                /*Thing that we need to make visual*/ Visual visual,
                /* Part of the thing we need to make visible */ Rect rectangle)
        {
            double yOffset = 0;
            foreach (UIElement child in InternalChildren)
            {
                if (child == visual)
                {
                    SetVerticalOffset(yOffset);
                    break;
                }
                yOffset += child.RenderSize.Height;
            }
            return rectangle; // !!

        }
    }
}

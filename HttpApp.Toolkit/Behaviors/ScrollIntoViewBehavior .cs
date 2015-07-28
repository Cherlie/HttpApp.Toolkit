using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace HttpApp.Toolkit.Behaviors
{
    public sealed class ScrollIntoViewBehavior : DependencyObject, IBehavior
    {
        private static readonly DependencyProperty LastFocusedItemProperty = DependencyProperty.Register("LastFocusedItem", typeof(object), typeof(ScrollIntoViewBehavior), null);
        public object LastFocusedItem
        {
            get
            {
                return (object)base.GetValue(LastFocusedItemProperty);
            }
            set
            {
                base.SetValue(LastFocusedItemProperty, (object)value);
            }
        }

        private DependencyObject _associatedObject;
        public DependencyObject AssociatedObject
        {
            get
            {
                return _associatedObject;
            }
        }

        public void Attach(DependencyObject associatedObj)
        {
            if ((associatedObj != _associatedObject) && !DesignMode.DesignModeEnabled)
            {
                if (_associatedObject != null)
                {
                    throw new InvalidOperationException(string.Format((IFormatProvider)CultureInfo.CurrentCulture, "CannotAttachBehaviorMultipleTimesExceptionMessage", new object[] { associatedObj, _associatedObject }));
                }
                _associatedObject = associatedObj;
                var lv = _associatedObject as ListViewBase;
                if (lv != null)
                {

                    lv.Loaded += lv_Loaded;
                    lv.Unloaded += lv_Unloaded;
                }

            }
        }

        void lv_Loaded(object sender, RoutedEventArgs e)
        {
            var sz = FindSemanticZoom();
            if (sz != null)
            {
                if (_associatedObject != null)
                {
                    ListViewBase lv = _associatedObject as ListViewBase;
                    SemanticZoomLocation szLocation = new SemanticZoomLocation() { Item = LastFocusedItem };
                    lv.MakeVisible(szLocation);
                }
            }
            else
            {
                var page = FindPage();
                if (page != null)
                {
                    page.Loaded += page_Loaded;

                }
            }
        }

        void lv_Unloaded(object sender, RoutedEventArgs e)
        {
            ListViewBase lv = _associatedObject as ListViewBase;
            if (lv != null)
            {
                lv.Loaded -= lv_Loaded;
                lv.Unloaded -= lv_Unloaded;
            }
            var sz = FindSemanticZoom();
            if (sz == null)
            {
                var page = FindPage();
                if (page != null)
                {
                    page.Loaded -= page_Loaded;
                }
            }

            this.Detach();
        }

        void page_Loaded(object sender, RoutedEventArgs e)
        {
            if (_associatedObject != null)
            {
                ListViewBase lv = _associatedObject as ListViewBase;
                lv.ScrollIntoView(LastFocusedItem);
            }
        }

        private Page FindPage()
        {
            DependencyObject parent;
            for (DependencyObject obj = _associatedObject; obj != null; obj = parent)
            {
                parent = VisualTreeHelper.GetParent(obj);
                Page page = parent as Page;
                if (page != null)
                {
                    return page;
                }
            }
            return null;
        }

        private SemanticZoom FindSemanticZoom()
        {
            DependencyObject parent;
            for (DependencyObject obj = _associatedObject; obj != null; obj = parent)
            {
                parent = VisualTreeHelper.GetParent(obj);
                SemanticZoom sz = parent as SemanticZoom;
                if (sz != null)
                {
                    return sz;
                }
            }
            return null;
        }



        public void Detach()
        {
            if (_associatedObject != null)
            {
                _associatedObject = null;
            }
        }
    }
}

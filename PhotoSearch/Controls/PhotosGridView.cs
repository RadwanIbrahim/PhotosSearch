using Microsoft.Toolkit.Uwp.UI.Animations;
using Microsoft.Toolkit.Uwp.UI.Controls;
using Microsoft.Toolkit.Uwp.UI.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace PhotoSearch.Controls
{
    public class PhotosGridView : GridView
    {
        private static readonly bool _isCreatorsUpdateOrAbove = ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 4);

        public PhotosGridView()
        {
            this.ChoosingItemContainer += OnChoosingItemContainer;
        }

        private void OnChoosingItemContainer(Windows.UI.Xaml.Controls.ListViewBase sender, ChoosingItemContainerEventArgs args)
        {
            if (args.ItemContainer != null)
            {
                return;
            }

            GridViewItem container = (GridViewItem)args.ItemContainer ?? new GridViewItem();
            container.Loaded += OnItemContainerLoaded;
            container.PointerEntered += OnItemContainerPointerEntered;
            container.PointerExited += OnItemContainerPointerExited;

            args.ItemContainer = container;
        }

        private void OnItemContainerLoaded(object sender, RoutedEventArgs e)
        {
            var itemsPanel = (ItemsWrapGrid)this.ItemsPanelRoot;
            var itemContainer = (GridViewItem)sender;
            itemContainer.Loaded -= this.OnItemContainerLoaded;

            if (!_isCreatorsUpdateOrAbove)
            {
                return;
            }

            var itemIndex = this.IndexFromContainer(itemContainer);

            var referenceIndex = itemsPanel.FirstVisibleIndex;

            if (this.SelectedIndex >= 0)
            {
                referenceIndex = this.SelectedIndex;
            }

            var relativeIndex = Math.Abs(itemIndex - referenceIndex);

            if (itemIndex >= 0 && itemIndex >= itemsPanel.FirstVisibleIndex && itemIndex <= itemsPanel.LastVisibleIndex)
            {
                var staggerDelay = TimeSpan.FromMilliseconds(relativeIndex * 30);

                var animationCollection = new AnimationCollection()
                {
                    new OpacityAnimation() { From = 0, To = 1, Duration = TimeSpan.FromMilliseconds(400), Delay = staggerDelay, SetInitialValueBeforeDelay = true },
                    new ScaleAnimation() { From = "0.9", To = "1", Duration = TimeSpan.FromMilliseconds(400), Delay = staggerDelay }
                };

                VisualEx.SetNormalizedCenterPoint(itemContainer, "0.5");

                animationCollection.StartAnimation(itemContainer);
            }
        }
        private void OnItemContainerPointerExited(object sender, PointerRoutedEventArgs e)
        {
            var panel = (sender as FrameworkElement).FindDescendant<DropShadowPanel>();
            if (panel != null)
            {
                var animation = new OpacityAnimation() { To = 0, Duration = TimeSpan.FromMilliseconds(1200) };
                animation.StartAnimation(panel);

                var parentAnimation = new ScaleAnimation() { To = "1", Duration = TimeSpan.FromMilliseconds(1200) };
                parentAnimation.StartAnimation(panel.Parent as UIElement);
            }
        }

        private void OnItemContainerPointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
            {
                var panel = (sender as FrameworkElement).FindDescendant<DropShadowPanel>();
                if (panel != null)
                {
                    panel.Visibility = Visibility.Visible;
                    var animation = new OpacityAnimation() { To = 1, Duration = TimeSpan.FromMilliseconds(600) };
                    animation.StartAnimation(panel);

                    var parentAnimation = new ScaleAnimation() { To = "1.1", Duration = TimeSpan.FromMilliseconds(600) };
                    parentAnimation.StartAnimation(panel.Parent as UIElement);
                }
            }
        }


    }
}

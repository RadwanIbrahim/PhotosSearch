using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace PhotoSearch.Triggers
{
    public class SearchPageStateTrigger : StateTriggerBase
    {
        public bool IsSearchStateActive
        {
            get { return (bool)GetValue(IsSearchStateActiveProperty); }
            set { SetValue(IsSearchStateActiveProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSearchStateActive.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSearchStateActiveProperty =
            DependencyProperty.Register("IsSearchStateActive", typeof(bool), typeof(SearchPageStateTrigger), new PropertyMetadata(false, OnSearchStateChanged));

        private static void OnSearchStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SearchPageStateTrigger searchPageStateTrigger = d as SearchPageStateTrigger;
            searchPageStateTrigger.SetActive(searchPageStateTrigger.IsSearchStateActive);

        }
    }

}

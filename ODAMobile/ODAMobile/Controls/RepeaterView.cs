﻿using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Windows.Input;

namespace Xamarin.Forms.Controls
{
    public class RepeaterView : StackLayout
    {
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(RepeaterView), default(IList), BindingMode.OneWay, propertyChanged: ItemsChanged);

        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(RepeaterView), default(DataTemplate));

        public static readonly BindableProperty ItemTappedCommandProperty = BindableProperty.Create(nameof(ItemTappedCommand), typeof(ICommand), typeof(RepeaterView), default(ICommand));

        public static readonly BindableProperty ItemTappedCommandParameterProperty = BindableProperty.Create(nameof(ItemTappedCommandParameter), typeof(object), typeof(RepeaterView), default);

        public ICommand ItemTappedCommand
        {
            get { return (ICommand)GetValue(ItemTappedCommandProperty); }
            set { SetValue(ItemTappedCommandProperty, value); }
        }

        public object ItemTappedCommandParameter
        {
            get { return GetValue(ItemTappedCommandParameterProperty); }
            set { SetValue(ItemTappedCommandParameterProperty, value); }
        }

        public IList ItemsSource
        {
            get => (IList)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        public event EventHandler<ItemTappedEventArgs> ItemTapped;

        public RepeaterView()
        {
            Spacing = 0;
        }

        private static void ItemsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            try
            {
                if (!(bindable is RepeaterView control))
                    return;

                if (oldValue is INotifyCollectionChanged oldCollection)
                {
                    oldCollection.CollectionChanged -= control.OnItemsSourceCollectionChanged;
                }

                if (newValue is INotifyCollectionChanged newCollection)
                {
                    newCollection.CollectionChanged += control.OnItemsSourceCollectionChanged;
                }

                if (!(newValue is IList items))
                    return;

                control.Children.Clear();

                foreach (var item in items)
                {
                    View view = control.ViewFor(item);
                    if (view != null)
                    {
                        var tap = new TapGestureRecognizer();
                        tap.Tapped += (s, e) => control.SendItemTapped(items.IndexOf(item), item);
                        view.GestureRecognizers.Add(tap);

                        //view.BindingContext = item;

                        control.Children.Add(view);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            try
            {
                if (e.Action == NotifyCollectionChangedAction.Reset)
                {
                    Children.Clear();
                }
                else if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    if (!(e.NewItems is IList items))
                        return;

                    foreach (var item in items)
                    {
                        View view = ViewFor(item);
                        if (view != null)
                        {
                            var tap = new TapGestureRecognizer();
                            tap.Tapped += (s, args) => SendItemTapped(items.IndexOf(item), item);
                            view.GestureRecognizers.Add(tap);

                            Children.Add(view);
                        }
                    }
                }
                else if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    Children.RemoveAt(e.OldStartingIndex);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private View ViewFor(object item)
        {
            View view = null;

            if (ItemTemplate != null)
            {
                var content = ItemTemplate.CreateContent();

                view = content is View ? content as View : ((ViewCell)content).View;

                view.BindingContext = item;
            }

            return view;
        }

        private void SendItemTapped(object index, object item)
        {
            try
            {
                var param = ItemTappedCommandParameter ?? item;

                if (ItemTappedCommand != null && ItemTappedCommand.CanExecute(param))
                {
                    ItemTappedCommand.Execute(param);
                }

                ItemTapped?.Invoke(this, new ItemTappedEventArgs(index, item));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}
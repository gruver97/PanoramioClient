using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Xaml.Interactivity;
using PanoramioClient.Enumerations;

namespace PanoramioClient
{
    public class ChangeVisualStateBehavior : DependencyObject, IBehavior
    {
        public static readonly DependencyProperty StateProperty = DependencyProperty.Register(
            "State", typeof (LoadingStatesEnumeration), typeof (ChangeVisualStateBehavior),
            new PropertyMetadata(default(LoadingStatesEnumeration), PropertyChangedCallback));

        public LoadingStatesEnumeration State
        {
            get { return (LoadingStatesEnumeration) GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        public void Attach(DependencyObject associatedObject)
        {
            var control = associatedObject as Control;
            if (control == null)
                throw new ArgumentException(
                    "EnumStateBehavior can be attached only to Control");

            AssociatedObject = associatedObject;
        }

        public void Detach()
        {
            AssociatedObject = null;
        }

        public DependencyObject AssociatedObject { get; private set; }

        private static void PropertyChangedCallback(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var behavior = dependencyObject as ChangeVisualStateBehavior;
            if (behavior.AssociatedObject == null) return;
            VisualStateManager.GoToState(behavior.AssociatedObject as Control, dependencyPropertyChangedEventArgs.NewValue.ToString(), false);
        }
    }
}
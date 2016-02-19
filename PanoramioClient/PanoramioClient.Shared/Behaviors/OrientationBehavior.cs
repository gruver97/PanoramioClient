using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Xaml.Interactivity;
using PanoramioClient.Enumerations;

namespace PanoramioClient.Behaviors
{
    public class OrientationBehavior : DependencyObject, IBehavior
    {
        public static readonly DependencyProperty PropertyTypeProperty = DependencyProperty.Register(
            "Orientation", typeof (OrientationEnumeration), typeof (OrientationBehavior),
            new PropertyMetadata(default(OrientationEnumeration), PropertyChangedCallback));

        public OrientationEnumeration Orientation
        {
            get { return (OrientationEnumeration) GetValue(PropertyTypeProperty); }
            set { SetValue(PropertyTypeProperty, value); }
        }

        public void Attach(DependencyObject associatedObject)
        {
            var control = associatedObject as Control;
            if (control == null)
                throw new ArgumentException(
                    "OrientationBehavior can be attached only to Control");

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
            var behavior = dependencyObject as OrientationBehavior;
            if (behavior.AssociatedObject == null) return;
            VisualStateManager.GoToState(behavior.AssociatedObject as Control,
                dependencyPropertyChangedEventArgs.NewValue.ToString(), false);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace NZTravelMate.Behaviours
{
    public class CompletedEntry : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryCompleted;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryCompleted;
            base.OnDetachingFrom(entry);
        }

        void OnEntryCompleted(object sender, EventArgs e)
        {
            var entry = ((Entry)sender);
            var text = entry.Text;
            Debug.WriteLine($"{entry.ClassId}: {text}");
        }
    }
    public class FocusedEntry : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.Focused += OnEntryFocused;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.Focused -= OnEntryFocused;
            base.OnDetachingFrom(entry);
        }
        
        void OnEntryFocused(object sender, EventArgs e)
        {
            var entry = ((Entry)sender);
            entry.Text = "";
        }
    }

    public class PickerChanged : Behavior<Picker>
    {
        protected override void OnAttachedTo(Picker picker)
        {
            picker.SelectedIndexChanged += OnPickerChanged;
            base.OnAttachedTo(picker);
        }

        protected override void OnDetachingFrom(Picker picker)
        {
            picker.SelectedIndexChanged -= OnPickerChanged;
            base.OnDetachingFrom(picker);
        }

        void OnPickerChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            //debugging
            Debug.WriteLine($"{picker.ClassId}: {selectedIndex}");
        }
    }
}

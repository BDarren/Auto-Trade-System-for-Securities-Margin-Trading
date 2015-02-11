using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FirstFloor.ModernUI.Windows.Controls;

namespace AutoCompleteComboBox.Controls
{
    /// <summary>
    /// Interaction logic for AutoCompleteComboBox.xaml
    /// </summary>
    public partial class AutoCompleteComboBox : UserControl
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCompleteComboBox"/> class.
        /// </summary>
        public AutoCompleteComboBox()
        {
            InitializeComponent();

            // Attach events to the controls
            autoTextBox.TextChanged += 
                new TextChangedEventHandler(autoTextBox_TextChanged);
            autoTextBox.PreviewKeyDown += 
                new KeyEventHandler(autoTextBox_PreviewKeyDown);
            suggestionListBox.SelectionChanged += 
                new SelectionChangedEventHandler(suggestionListBox_SelectionChanged);
            suggestionListBox.MouseLeave+=
                new MouseEventHandler(suggestionListBox_MouseDown);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the items source.
        /// </summary>
        /// <value>The items source.</value>
        public IEnumerable<string> ItemsSource
        {
            get { return (IEnumerable<string>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsSource.  
        // This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource"
                                , typeof(IEnumerable<string>)
                                , typeof(AutoCompleteComboBox)
                                , new UIPropertyMetadata(null));

        /// <summary>
        /// Gets or sets the selected value.
        /// </summary>
        /// <value>The selected value.</value>
        public string SelectedValue
        {
            get { return (string)GetValue(SelectedValueProperty); }
            set { SetValue(SelectedValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedValue.  
        // This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedValueProperty =
            DependencyProperty.Register("SelectedValue"
                            , typeof(string)
                            , typeof(AutoCompleteComboBox)
                            , new UIPropertyMetadata(string.Empty));

        #endregion


        #region Methods
        /// <summary>
        /// Handles the TextChanged event of the autoTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        void autoTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Only autocomplete when there is text
            if (autoTextBox.Text.Length > 0)
            {
                // Use Linq to Query ItemsSource for resultdata
                string condition = string.Format("{0}%", autoTextBox.Text);
                IEnumerable<string> results = ItemsSource.Where(delegate(string s) { return s.ToLower().StartsWith(autoTextBox.Text.ToLower());});

                if (results.Count() > 1)
                {
                    suggestionListBox.ItemsSource = results;
                    suggestionListBox.Visibility = Visibility.Visible;
                }
                else if (results.Count() == 1)
                {
                    List<string> s = results.ToList();

                    if (s[0].Substring(0,6) == autoTextBox.Text)
                    {
                        suggestionListBox.Visibility = Visibility.Collapsed;
                    }
                    else
                        suggestionListBox.Visibility = Visibility.Visible;
                    suggestionListBox.ItemsSource = results;
                }
                else
                {
                    suggestionListBox.Visibility = Visibility.Collapsed;
                    suggestionListBox.ItemsSource = null;
                }
            }
            else
            {
                suggestionListBox.Visibility = Visibility.Collapsed;
                suggestionListBox.ItemsSource = null;
            }
        }

        /// <summary>
        /// Handles the PreviewKeyDown event of the autoTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        void autoTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                if (suggestionListBox.SelectedIndex < suggestionListBox.Items.Count)
                {
                    suggestionListBox.SelectedIndex = suggestionListBox.SelectedIndex + 1;
                }
            }
            if (e.Key == Key.Up)
            {
                if (suggestionListBox.SelectedIndex > -1)
                {
                    suggestionListBox.SelectedIndex = suggestionListBox.SelectedIndex - 1;
                }
            }
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                // Commit the selection
                suggestionListBox.Visibility = Visibility.Collapsed;
                e.Handled = (e.Key == Key.Enter);
            }
                
            if (e.Key == Key.Escape)
            {
                // Cancel the selection
                suggestionListBox.ItemsSource = null;
                suggestionListBox.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Handles the SelectionChanged event of the suggestionListBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void suggestionListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (suggestionListBox.ItemsSource != null)
            {
                autoTextBox.TextChanged -= new TextChangedEventHandler(autoTextBox_TextChanged);
                if (suggestionListBox.SelectedIndex != -1)
                {
                    autoTextBox.Text = suggestionListBox.SelectedItem.ToString().Substring(0,6);
                    //suggestionListBox.Visibility = Visibility.Collapsed;
                    //suggestionListBox.SelectedIndex = -1;
                }
                autoTextBox.TextChanged += new TextChangedEventHandler(autoTextBox_TextChanged);
            }
        }

        public string getstring()
        {
            return autoTextBox.Text;
        }
        #endregion

        private void suggestionListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (suggestionListBox.SelectedIndex != -1)
            {
                suggestionListBox.Visibility = Visibility.Collapsed;
                suggestionListBox.SelectedIndex = -1;
            }
        }
    }
}

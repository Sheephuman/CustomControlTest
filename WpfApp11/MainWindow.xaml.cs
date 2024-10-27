using System.Globalization;
using System.IO;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace WpfApp11
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            PulldownTextBox.Language = System.Windows.Markup.XmlLanguage.GetLanguage("en-US");
            testBox.Language = System.Windows.Markup.XmlLanguage.GetLanguage("en-US");

          　///test
        

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            FrameworkElement.LanguageProperty.OverrideMetadata(
               typeof(FrameworkElement),
               new FrameworkPropertyMetadata(XmlLanguage.GetLanguage("en-US")));

            testBox.Language = XmlLanguage.GetLanguage("en-US");
            testBox.SpellCheck.IsEnabled = true;
        }


        private void Window_Closed(object sender, EventArgs e)
        {
            PulldownTextBox._popup.IsOpen = false;
          
        }

        private void test_Click(object sender, RoutedEventArgs e)
        {
            // 現在の文化情報を取得
            CultureInfo currentCulture = CultureInfo.CurrentCulture;

            // 表示言語を返す
            string displayLanguage = currentCulture.Name;
            test.Content = $"現在の表示言語: {displayLanguage}";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            testBox.Language = XmlLanguage.GetLanguage("en-US");

            testBox.SpellCheck.IsEnabled = true;
        }
    }
}
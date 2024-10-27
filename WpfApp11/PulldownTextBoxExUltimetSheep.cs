using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace WpfApp11
{
    /// <summary>
    /// このカスタム コントロールを XAML ファイルで使用するには、手順 1a または 1b の後、手順 2 に従います。
    ///
    /// 手順 1a) 現在のプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfApp11"
    ///
    ///
    /// 手順 1b) 異なるプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfApp11;assembly=WpfApp11"
    ///
    /// また、XAML ファイルのあるプロジェクトからこのプロジェクトへのプロジェクト参照を追加し、
    /// リビルドして、コンパイル エラーを防ぐ必要があります:
    ///
    ///     ソリューション エクスプローラーで対象のプロジェクトを右クリックし、
    ///     [参照の追加] の [プロジェクト] を選択してから、このプロジェクトを参照し、選択します。
    ///
    ///
    /// 手順 2)
    /// コントロールを XAML ファイルで使用します。
    ///
    ///     <MyNamespace:PulldownTextBoxExUltimetSheep/>
    ///
    /// </summary>
    public class PulldownTextBoxExUltimetSheep : TextBox
    {
        private ListBox _dropdownList = null!;
        private TextBox? _textBox = null!;

        static PulldownTextBoxExUltimetSheep()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PulldownTextBoxExUltimetSheep), new FrameworkPropertyMetadata(typeof(PulldownTextBoxExUltimetSheep)));
        }
        public Popup _popup { get; set; } 
        public PulldownTextBoxExUltimetSheep()
        {

            _textBox = new TextBox();

            // スペルチェック機能を有効にする
            this.SpellCheck.IsEnabled = true;
            // KeyDownイベントの登録
            this.PreviewKeyDown += OnKeyDown;

            // PopupとListBoxの初期化
            _popup = new Popup
            {
                PlacementTarget = this,
                Placement = PlacementMode.Bottom,
                StaysOpen = false,
                IsOpen = false,

            };

            _dropdownList = new ListBox
            {
                Width = this.Width,
                ItemsSource = new[] { "Option1", "Option2", "Option3" }  // サンプル項目
            };
            _dropdownList.SelectionChanged += DropdownList_SelectionChanged;

            _popup.Child = _dropdownList;

            this.Loaded += PulldownTextBoxExUltimetSheep_Loaded;
            this.LostFocus += OnLostFocus;
        }

     


        private void OnLostFocus(object sender, RoutedEventArgs e)
            {
                _popup.IsOpen = false;
            }

        private void PulldownTextBoxExUltimetSheep_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                // ↓キーでプルダウンリストを表示
                ShowDropdown();
            }
        }

        private void ShowDropdown()
        {

            _popup.IsOpen = true;
        }

        private void DropdownList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (_dropdownList.SelectedItem != null)
            {
                if(_textBox != null)
                this._textBox.Text = _dropdownList.SelectedItem.ToString();


            }
        }


            public override void OnApplyTemplate()
            {
            base.OnApplyTemplate();
            _textBox = new TextBox();
            _textBox = GetTemplateChild("PART_TextBox") as TextBox;
            

            if (_textBox != null)
            {
                // ここでPART_TextBoxに対して何か処理を行う
                

                _textBox.Language = System.Windows.Markup.XmlLanguage.GetLanguage("en-US"); // 英語でスペルチェック


                string dictionaryPath = Path.Combine(Directory.GetCurrentDirectory(), "customDictionary.lex");
                _textBox.SpellCheck.CustomDictionaries.Add(new Uri(dictionaryPath));


                //var spellcheck = new CustomSpellCheck(_textBox);
                //spellcheck.AddCustomDictionary(new Uri("customDictionary.lex", UriKind.Relative));

                //_textBox.SpellCheck.IsEnabled = true; // 例: スペルチェックを有効にする

                //spellcheck.IsEnabled = true;

            }


        }
    }
}

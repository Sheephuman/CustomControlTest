using System.Collections;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace WpfApp11
{
   public partial class CustomSpellCheck
    {

        // TextBoxBaseを参照するためのフィールド
        private readonly TextBoxBase _owner;

        // カスタム辞書のリスト
        private readonly IList _customDictionaries;


        public CustomSpellCheck(TextBoxBase owner)
        {
            _owner = owner ?? throw new ArgumentNullException(nameof(owner));
            _customDictionaries = new List<Uri>(); // カスタム辞書リストの初期化
        }


        public bool IsEnabled
        {
            get => (bool)_owner.GetValue(IsEnabledProperty);
            set => _owner.SetValue(IsEnabledProperty, value);
        }

        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached(
                "IsEnabled",
                typeof(bool),
                typeof(CustomSpellCheck),
                new FrameworkPropertyMetadata(true, OnIsEnabledChanged));


        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBoxBase textBoxBase)
            {
                var spellCheck = new CustomSpellCheck(textBoxBase);
                spellCheck.ApplyCustomDictionaries((bool)e.NewValue);
            }
        }


        private void ApplyCustomDictionaries(bool enableSpellCheck)
        {

            if (enableSpellCheck)
            {
                // カスタム辞書を追加
                foreach (Uri dictionaryUri in _customDictionaries)
                {
                    if (!_owner.SpellCheck.CustomDictionaries.Contains(dictionaryUri))
                    {
                        _owner.SpellCheck.CustomDictionaries.Add(dictionaryUri);
                    }

                }
            }
            else
            {
                // カスタム辞書を削除
                foreach (Uri dictionaryUri in _customDictionaries)
                {
                    _owner.SpellCheck.CustomDictionaries.Remove(dictionaryUri);
                }

            }



        }

        /// <summary>
        /// カスタム辞書を追加するメソッド
        /// </summary>
        /// <param name="dictionaryUri">追加する辞書のURI</param>
        public void AddCustomDictionary(Uri dictionaryUri)
        {
            if (dictionaryUri == null)
                throw new ArgumentNullException(nameof(dictionaryUri));

            if (!_customDictionaries.Contains(dictionaryUri))
            {
                _customDictionaries.Add(dictionaryUri);
                ApplyCustomDictionaries(IsEnabled);

            }

        }

        /// <summary>
        /// カスタム辞書を削除するメソッド
        /// </summary>
        /// <param name="dictionaryUri">削除する辞書のURI</param>
        public void RemoveCustomDictionary(Uri dictionaryUri)
        {
            if (dictionaryUri == null)
                throw new ArgumentNullException(nameof(dictionaryUri));
            if (_customDictionaries.Contains(dictionaryUri))
            {
                _customDictionaries.Remove(dictionaryUri);
                ApplyCustomDictionaries(IsEnabled);
            }
        }
    }
}
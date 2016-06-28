using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QLKH
{
    /// <summary>
    /// Interaction logic for IntegerTextBox.xaml
    /// </summary>
    public partial class PhoneTextBox : UserControl
    {
        public uint Limit
        {
            get { return (uint)GetValue(LimitProperties); }
            set { SetValue(LimitProperties, value); }
        }

        public static readonly DependencyProperty
            LimitProperties = DependencyProperty.Register("Limit", typeof(uint), typeof(PhoneTextBox),new PropertyMetadata((uint)15));

        public string Text
        {
            // Property này sẽ truy xuất đến DependencyProperty trên kia
            get { return (string)GetValue(TextBoxTextproperty); }
            set { SetValue(TextBoxTextproperty, value); }
        }

        public static readonly DependencyProperty
            TextBoxTextproperty = DependencyProperty.Register(
                "Text", typeof(string), typeof(PhoneTextBox),
                new UIPropertyMetadata(""));

        public PhoneTextBox()
        {
            try
            {
                InitializeComponent();
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("error");
            }
        }

        private void textBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            // kiểm tra xem có phải người dùng đang dán string hay ko
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                // lấy string từ data
                String text = (String)e.DataObject.GetData(typeof(String));
                if (textBox.Text.Length + text.Length > Limit)
                {
                    e.CancelCommand();
                }
                else
                {
                    for (int i = 0; i < text.Length; i++)
                    {
                        if (!char.IsDigit(text[i]))
                        {
                            e.CancelCommand();
                            return;
                        }
                    }
                }
            }
            else
            { // ko dán string thì hủy lệnh
                e.CancelCommand();
            }
        }

        private void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (textBox.Text.Length == Limit|| !char.IsDigit(e.Text[0]))
            {
                e.Handled = true;
            }
        }
    }
}

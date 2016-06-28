using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QLKH
{
    /// <summary>
    /// Interaction logic for IntegerTextBox.xaml
    /// </summary>
    public partial class IntegerTextBox : UserControl
    {
        public int Value
        {
            get
            {
                int value = 0;
                int.TryParse(textBox.Text, out value);
                return value;
            }
            set
            {
                textBox.Text = value.ToString();
            }
        }
        public IntegerTextBox()
        {
            InitializeComponent();
        }

        private void textBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            // kiểm tra xem có phải người dùng đang dán string hay ko
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                // lấy string từ data
                String text = (String)e.DataObject.GetData(typeof(String));
                int value = 0;
                if (!int.TryParse(textBox.Text.Insert(textBox.CaretIndex,text), out value))
                {
                    e.CancelCommand();
                }
            }
            else
            { // ko dán string thì hủy lệnh
                e.CancelCommand();
            }
        }

        private void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text[0]=='-'&& textBox.Text.Length == 0)
            {
                return;
            }
            int value1 = 0;
            if (!int.TryParse(textBox.Text.Insert(textBox.CaretIndex, e.Text[0].ToString()), out value1))
            {
                // nếu sau khi nhập giá trị không hợp lệ
                e.Handled = true;
            }
        }

        private void textBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}

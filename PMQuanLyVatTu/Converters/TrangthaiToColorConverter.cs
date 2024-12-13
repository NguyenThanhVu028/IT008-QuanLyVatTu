using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace PMQuanLyVatTu.Converters
{
    public class TrangthaiToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string TrangThai = (string)value;
            if (TrangThai == "Bị từ chối" || TrangThai == "Thiếu hàng" || TrangThai == "Lỗi không tồn tại") return new SolidColorBrush(Colors.Red);
            if (TrangThai == "Đã duyệt" || TrangThai == "Đã tiếp nhận") return new SolidColorBrush(Colors.Green);
            if(TrangThai == "Kế toán đã duyệt") return new SolidColorBrush(Colors.Blue);
            return new SolidColorBrush(Colors.DarkOrange);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
}

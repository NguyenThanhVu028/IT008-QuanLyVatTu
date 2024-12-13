using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PMQuanLyVatTu.Converters
{
    class TrangThaiConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value.ToString() != "Thiếu hàng" && value.ToString() != "Lỗi không tồn tại" && value.ToString() != "Đã duyệt" && value.ToString() != "Đã tiếp nhận" && value.ToString() != "Bị từ chối") return Visibility.Visible;
            else return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
}

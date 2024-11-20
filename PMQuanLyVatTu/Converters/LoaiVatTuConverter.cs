using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PMQuanLyVatTu.Converters
{
    class LoaiVatTuConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch(value)
            {
                case "NVL":
                    return "Nguyên vật liệu";
                    break;
                case "TB":
                    return "Thiết bị";
                    break;
                case "CC":
                    return "Công cụ";
                    break;
                default:
                    return "Null";
                    break;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case "Nguyên vật liệu":
                    return "NVL";
                    break;
                case "Thiết bị":
                    return "TB";
                    break;
                case "Công cụ":
                    return "CC";
                    break;
                default:
                    return "Null";
                    break;
            }
        }
    }
}

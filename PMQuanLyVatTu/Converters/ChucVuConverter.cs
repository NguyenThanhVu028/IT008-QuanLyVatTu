using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PMQuanLyVatTu.Converters
{
    class ChucVuConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case "QL":
                    return "Quản lý";
                    break;
                case "KT":
                    return "Kế toán";
                    break;
                case "NX":
                    return "Nhân viên nhập xuất";
                    break;
                case "TN":
                    return "Nhân viên tiếp nhận yêu cầu";
                    break;
                default:
                    return "Null";
                    break;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            {
                switch (value)
                {
                    case "Quản lý":
                        return "QL";
                        break;
                    case "Kế toán":
                        return "KT";
                        break;
                    case "Nhân viên nhập xuất":
                        return "NX";
                        break;
                    case "Nhân viên tiếp nhận yêu cầu":
                        return "TN";
                        break;
                    default:
                        return "Null";
                        break;
                }
            }
        }
    }
}

﻿using Microsoft.IdentityModel.Tokens;
using PMQuanLyVatTu.ErrorMessage;
using PMQuanLyVatTu.Models;
using PMQuanLyVatTu.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PMQuanLyVatTu.ViewModel
{
    public class ThemSuaVatTuWindowViewModel : BaseViewModel
    {
        public ThemSuaVatTuWindowViewModel(string makho = "", string mancc = "")
        {
            Makho = makho; MaNCC = mancc; ReturnValue = false;
            
            LoadVatTu();

            MoveWindowCommand = new RelayCommand<Window>(MoveWindow);
            CancelCommand = new RelayCommand<Window>(Cancel);
            ConfirmCommand = new RelayCommand<Window>(Confirm);
        }
        #region Info
        private string _maKho = "";
        public string Makho
        {
            get { return _maKho; }
            set { _maKho = value; OnPropertyChanged(); }
        }
        private string _maNCC = "";
        public string MaNCC
        {
            get { return _maNCC; }
            set { _maNCC = value; OnPropertyChanged(); }
        }
        private string _maVT = "";
        public string MaVT
        {
            get { return _maVT; }
            set { _maVT = value; OnPropertyChanged(); }
        }
        private int _soLuong = 0;
        public int SoLuong
        {
            get { return _soLuong; }
            set { _soLuong = value; OnPropertyChanged(); }
        }
        private double _chietKhau = 0;
        public double ChietKhau
        {
            get { return _chietKhau; }
            set { _chietKhau = value; OnPropertyChanged(); }
        }
        private double _vAT = 0;
        public double VAT
        {
            get { return _vAT; }
            set { _vAT = value; OnPropertyChanged(); }
        }
        #endregion
        #region ReturnValue
        private bool _returnValue = false;
        public bool ReturnValue
        {
            get { return _returnValue; }
            set { _returnValue  = value; OnPropertyChanged(); }
        }
        #endregion
        #region Data for SelectionList
        private ObservableCollection<string> _danhSachVatTu = new ObservableCollection<string>();
        public ObservableCollection<string> DanhSachVatTu
        {
            get { return _danhSachVatTu; }
            set { _danhSachVatTu = value; }
        }
        #endregion
        #region Command
        public ICommand MoveWindowCommand { get; set; }
        void MoveWindow(Window window)
        {
            window.DragMove();
        }
        public ICommand CancelCommand { get; set; }
        void Cancel(Window window)
        {
            window.Close();
        }
        public ICommand ConfirmCommand { get; set; }
        void Confirm(Window t) {
            if(MaVT.IsNullOrEmpty())
            {
                CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng chọn mã vật tư.", false);
                msg1.ShowDialog();
                return;
            }

            if(SoLuong < 0 || ChietKhau < 0 || VAT < 0)
            {
                CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Không được nhập số âm.", false);
                msg1.ShowDialog();
                return;
            }
            var supplies = DataProvider.Instance.DB.Supplies.Find(MaVT);
            if (supplies == null)
            {
                CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vật tư đã không còn trên hệ thống.", false);
                msg1.ShowDialog();
                return;
            }
            else
            {
                if(supplies.SoLuongTonKho != null)
                    if(SoLuong > supplies.SoLuongTonKho)
                    {
                        CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Số lượng tồn kho không đủ. Tồn kho: " + supplies.SoLuongTonKho, false);
                        msg1.ShowDialog();
                        return;
                    }
            }
            //CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Đã lưu thông tin.", false);
            //msg.ShowDialog();
            ReturnValue = true;
            t.Close();

        }
        #endregion
        #region Function
        void LoadVatTu()
        {
            var ListFromDB = DataProvider.Instance.DB.Supplies.Where(p=>p.DaXoa == false).ToList();
            foreach (var item in ListFromDB)
            {
                if((item.MaKho == null ||item.MaKho.Contains(Makho)) && (item.MaNcc == null || item.MaNcc.Contains(MaNCC))) DanhSachVatTu.Add(item.MaVt);
            }
        }
        #endregion  
    }
}

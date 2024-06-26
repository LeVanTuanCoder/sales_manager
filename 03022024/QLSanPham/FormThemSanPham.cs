﻿using BLL;
using System;
using System.Data;
using System.Windows.Forms;
using DAL.Entity;
using _03022024.QLTaiKhoan;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _03022024
{
    public partial class FormThemSanPham : Form
    {
        private SanPhamManager manager = null;
        SanPhamEntity sanPham = new SanPhamEntity();

        public FormThemSanPham()
        {
            InitializeComponent();
            manager = new SanPhamManager();
        }
        private void FormThemSanPham_Load(object sender, EventArgs e)
        {
            DataTable categories = manager.LayDanhMuc();
            DataTable units = manager.LayDVT();

            cbbDanhMuc.DisplayMember = "TenDanhMuc";
            cbbDanhMuc.ValueMember = "MaDanhMuc";
            cbbDanhMuc.DataSource = categories;
            cbbDanhMuc.SelectedIndex = -1;

            cbbDonViTinh.DisplayMember = "TenDVT";
            cbbDonViTinh.ValueMember = "MaDVT";
            cbbDonViTinh.DataSource = units;
            cbbDonViTinh.SelectedIndex = -1;

            this.KeyPreview = true;
        }

        private void txtTenSanPham_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void KiemTraKyTu(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtMaSanPham_KeyPress(object sender, KeyPressEventArgs e)
        {
            KiemTraKyTu(e);
        }      

        private void txtMaDonViTinh_KeyPress(object sender, KeyPressEventArgs e)
        {
            KiemTraKyTu(e);
        }

        private void txtMaDanhMuc_KeyPress(object sender, KeyPressEventArgs e)
        {
            KiemTraKyTu(e);
        }

        private void txtDonGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void XacNhan()
        {
            if(!string.IsNullOrEmpty(txtMaSanPham.Text) || !string.IsNullOrEmpty(txtTenSanPham.Text) || !string.IsNullOrEmpty(cbbDonViTinh.SelectedValue.ToString()) || !string.IsNullOrEmpty(cbbDanhMuc.SelectedValue.ToString()) || !string.IsNullOrEmpty(txtDonGia.Text))
            {
                sanPham.MaSanPham = txtMaSanPham.Text;
                sanPham.TenSanPham = txtTenSanPham.Text;
                sanPham.MaDVT = cbbDonViTinh.SelectedValue.ToString();
                sanPham.MaDanhMuc = cbbDanhMuc.SelectedValue.ToString();
                sanPham.DonGia = decimal.Parse(txtDonGia.Text);

                if (string.IsNullOrEmpty(txtMaSanPham.Text) || string.IsNullOrEmpty(txtTenSanPham.Text) || string.IsNullOrEmpty(cbbDonViTinh.Text) || string.IsNullOrEmpty(cbbDanhMuc.Text) || string.IsNullOrEmpty(txtDonGia.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                    return;
                }
                string errorMessage;
                try
                {
                    manager.ThemSanPham(sanPham, out errorMessage);
                    MessageBox.Show(errorMessage);
                    if (errorMessage.StartsWith("Sản phẩm đã được thêm thành công"))
                    {
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
            }
        }
        private void btnXacNhan_Click(object sender, System.EventArgs e)
        {
            XacNhan();
        }

        private void ptbXacNhan_Click(object sender, System.EventArgs e)
        {
            XacNhan();
        }

        private void Thoat()
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }
        private void btnThoat_Click(object sender, System.EventArgs e)
        {
            Thoat();
        }

        private void ptbThoat_Click(object sender, System.EventArgs e)
        {
            Thoat();
        }

        private void FormThemSanPham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnXacNhan_Click(sender, e);
            }
        }
    }
}

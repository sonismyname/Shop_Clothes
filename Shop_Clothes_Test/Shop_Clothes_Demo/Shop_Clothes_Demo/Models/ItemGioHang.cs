using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Clothes_Demo.Models
{
    public class ItemGioHang
    {
        public string TenSP { get; set; }
        public int MaSP { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public string HinhAnh { get; set; }
        public decimal ThanhTien { get; set; }
        public string Size { get; set; }
        public ItemGioHang() { }
        public ItemGioHang(int MaSP)
        {
            
            using(ShopModel db = new ShopModel())
            {
                this.MaSP = MaSP;
                SanPham sp = db.SanPhams.Single(n => n.MaSanPham == MaSP);
                this.TenSP = sp.TenSanPham;
                this.HinhAnh = sp.HinhAnh;
                this.DonGia = sp.DonGia.Value;
                if(this.SoLuong==0)
                {
                    this.SoLuong = 1;
                }    
                this.ThanhTien = this.DonGia * this.SoLuong;

            }    
        }
        public ItemGioHang(int MaSP, int SoLuong, string Size)
        {

            using (ShopModel db = new ShopModel())
            {
                this.MaSP = MaSP;
                SanPham sp = db.SanPhams.Single(n => n.MaSanPham == MaSP);
                this.TenSP = sp.TenSanPham;
                this.HinhAnh = sp.HinhAnh;
                this.DonGia = sp.DonGia.Value;
                this.SoLuong = SoLuong;
                this.Size = Size;
                this.ThanhTien = this.DonGia * this.SoLuong;

            }
        }
    }
}
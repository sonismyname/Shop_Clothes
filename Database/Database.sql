create database Websitebanhang
go
use Websitebanhang
go

create table LoaiThanhVien(
	MaLoaiThanhVien int identity(1,1) primary key,
	TenLoaiThanhVien nvarchar(255),
	UuDai int
)
create table ThanhVien(
	MaThanhVien int identity(1,1) primary key,
	TaiKhoan nvarchar(255),
	MatKhau nvarchar(255),
	HoTen nvarchar(MAX),
	SoDienThoai nvarchar(50),
	DiaChi nvarchar(MAX), 
	Email nvarchar(MAX),
	MaLoaiThanhVien int,
	foreign key(MaLoaiThanhVien) references dbo.LoaiThanhVien(MaLoaiThanhVien) on delete cascade
)
create table NhaSanXuat(
	MaNhaSanXuat int identity(1,1) primary key,
	TenNhaSanXuat nvarchar(255),
	ThongTin nvarchar(MAX),
	Logo nvarchar(MAX)
)
create table LoaiSanPham(
	MaLoaiSanPham int identity(1,1) primary key,
	TenLoaiSanPham nvarchar(255),
	Icon nvarchar(MAX)
)
create table NhomMua(
	MaNhom int identity(1,1) primary key,
	TenNhom nvarchar(255)
)
create table SanPham(
	MaSanPham int identity(1,1) primary key,
	TenSanPham nvarchar(255),
	DonGia decimal(18,0),
	MoTa nvarchar(MAX),
	NgayCapNhat datetime,
	HinhAnh nvarchar(MAX),
	HinhAnh2 nvarchar(MAX),
	HinhAnh3 nvarchar(MAX),
	SoLuongMua int,
	SoLuongTon int,
	MaNhaSanXuat int, 
	MaLoaiSanPham int,
	MaNhom int,
	foreign key(MaLoaiSanPham) references dbo.LoaiSanPham(MaLoaiSanPham) on delete cascade,
	foreign key(MaNhaSanXuat) references dbo.NhaSanXuat(MaNhaSanXuat) on delete cascade,
	foreign key(MaNhom) references dbo.NhomMua(MaNhom) on delete cascade
)
create table KhachHang(
	MaKhachHang int identity(1,1) primary key,
	TenKhachHang nvarchar(255),
	DiaChi nvarchar(MAX),
	Email nvarchar(MAX),
	SoDienThoai nvarchar(50),
	MaThanhVien int,
	foreign key(MaThanhVien) references dbo.ThanhVien(MaThanhVien) on delete cascade
)
create table DonDatHang(
	MaDonDatHang int identity(1,1) primary key,
	NgayDat date,
	NgayGiaoDuKien date,
	TinhTrangGiaoHang nvarchar(MAX),
	DaThanhToan bit default 0,
	MaKhachHang int,
	UuDai int,
	foreign key(MaKhachHang) references dbo.KhachHang(MaKhachHang) on delete cascade
)
create table ChiTietDonHang(
	MaChiTietDDH int identity(1,1) primary key,
	MaDonDatHang int,
	MaSanPham int,
	TenSanPham nvarchar(255),
	SoLuong int,
	DonGia decimal(18,0),
	foreign key(MaDonDatHang) references dbo.DonDatHang(MaDonDatHang) on delete cascade,
	foreign key(MaSanPham) references dbo.SanPham(MaSanPham) on delete cascade
)
create table PhanHoi(
	MaPhanHoi int identity(1,1) primary key,
	NoiDung nvarchar(MAX),
	MaSanPham int,
	MaThanhVien int,
	foreign key(MaThanhVien) references dbo.ThanhVien(MaThanhVien) on delete cascade,
	foreign key(MaSanPham) references dbo.SanPham(MaSanPham) on delete cascade
)

use Websitebanhang
go

insert into NhaSanXuat ( TenNhaSanXuat) values ( N'Gucci')
insert into NhaSanXuat ( TenNhaSanXuat) values ( N'Louis Vuitton')
insert into NhaSanXuat ( TenNhaSanXuat) values ( N'Zara')
insert into NhaSanXuat ( TenNhaSanXuat) values ( N'H&M')
insert into NhaSanXuat ( TenNhaSanXuat) values ( N'Hermès')
insert into NhaSanXuat ( TenNhaSanXuat) values ( N'Chanel')
insert into NhaSanXuat ( TenNhaSanXuat) values ( N'Burberry')
insert into NhaSanXuat ( TenNhaSanXuat) values ( N'Prada')
insert into NhaSanXuat ( TenNhaSanXuat) values ( N'Fendi')
insert into NhaSanXuat ( TenNhaSanXuat) values ( N'Armani')

insert into LoaiSanPham( TenLoaiSanPham) values ( N'Áo Thun')
insert into LoaiSanPham( TenLoaiSanPham) values ( N'Áo Vest')
insert into LoaiSanPham( TenLoaiSanPham) values ( N'Áo Sơ Mi')
insert into LoaiSanPham( TenLoaiSanPham) values ( N'Áo Khoác')
insert into LoaiSanPham( TenLoaiSanPham) values ( N'Quần Thun')
insert into LoaiSanPham( TenLoaiSanPham) values ( N'Quần Đùi')
insert into LoaiSanPham( TenLoaiSanPham) values ( N'Quần Kaki')
insert into LoaiSanPham( TenLoaiSanPham) values ( N'Quần Jean')
insert into LoaiSanPham( TenLoaiSanPham) values ( N'Đồ Thể Thao')

insert into NhomMua( TenNhom) values( N'Nam')
insert into NhomMua( TenNhom) values( N'Nữ')
insert into NhomMua( TenNhom) values( N'Trẻ Em')

insert into SanPham(TenSanPham, DonGia, HinhAnh, HinhAnh2, HinhAnh3, SoLuongTon, SoLuongMua, MaLoaiSanPham, MaNhaSanXuat, MaNhom)
values (N'Áo Khoác Trắng Gucci', 3200000, N'Gucci_1_1', N'Gucci_1_2', N'Gucci_1_3', 100, 0, 4, 1, 2 )
insert into SanPham(TenSanPham, DonGia, HinhAnh, HinhAnh2, HinhAnh3, SoLuongTon, SoLuongMua, MaLoaiSanPham, MaNhaSanXuat, MaNhom)
values (N'Áo Khoác Nâu Gucci', 3000000, N'Gucci_2_1', N'Gucci_2_2', N'Gucci_2_3', 100, 0, 4, 1, 2 )
insert into SanPham(TenSanPham, DonGia, HinhAnh, HinhAnh2, HinhAnh3, SoLuongTon, SoLuongMua, MaLoaiSanPham, MaNhaSanXuat, MaNhom)
values (N'Áo Khoác Lông Gucci', 2200000, N'Gucci_3_1', N'Gucci_3_2', N'Gucci_3_3', 100, 0, 4, 1, 2 )
insert into SanPham(TenSanPham, DonGia, HinhAnh, HinhAnh2, HinhAnh3, SoLuongTon, SoLuongMua, MaLoaiSanPham, MaNhaSanXuat, MaNhom)
values (N'Áo Thun Xám H&M', 1200000, N'H&M_1_1', N'H&M_1_2', N'H&M_1_3', 100, 0, 1, 4, 1 )
insert into SanPham(TenSanPham, DonGia, HinhAnh, HinhAnh2, HinhAnh3, SoLuongTon, SoLuongMua, MaLoaiSanPham, MaNhaSanXuat, MaNhom)
values (N'Áo Thun Xanh Thẫm H&M', 1000000, N'H&M_2_1', N'H&M_2_2', N'H&M_2_3', 100, 0, 1, 4, 1 )
insert into SanPham(TenSanPham, DonGia, HinhAnh, HinhAnh2, HinhAnh3, SoLuongTon, SoLuongMua, MaLoaiSanPham, MaNhaSanXuat, MaNhom)
values (N'Áo Thun Xanh H&M', 1600000, N'H&M_3_1', N'H&M_3_2', N'H&M_3_3', 100, 0, 1, 4, 1 )
insert into SanPham(TenSanPham, DonGia, HinhAnh, HinhAnh2, HinhAnh3, SoLuongTon, SoLuongMua, MaLoaiSanPham, MaNhaSanXuat, MaNhom)
values (N'Áo Thun Xanh Rêu H&M', 900000, N'H&M_4_1', N'H&M_4_2', N'H&M_4_3', 100, 0, 1, 4, 1 )
insert into SanPham(TenSanPham, DonGia, HinhAnh, HinhAnh2, HinhAnh3, SoLuongTon, SoLuongMua, MaLoaiSanPham, MaNhaSanXuat, MaNhom)
values (N'Quần Jean H&M', 1900000, N'H&M_5_1', N'H&M_5_2', N'H&M_5_3', 100, 0, 8, 4, 1 )
insert into SanPham(TenSanPham, DonGia, HinhAnh, HinhAnh2, HinhAnh3, SoLuongTon, SoLuongMua, MaLoaiSanPham, MaNhaSanXuat, MaNhom)
values (N'Quần Kaki H&M', 2100000, N'H&M_6_1', N'H&M_6_2', N'H&M_6_3', 100, 0, 7, 4, 1 )
insert into SanPham(TenSanPham, DonGia, HinhAnh, HinhAnh2, HinhAnh3, SoLuongTon, SoLuongMua, MaLoaiSanPham, MaNhaSanXuat, MaNhom)
values (N'Quần Jean Nữ H&M', 2200000, N'H&M_7_1', N'H&M_7_2', N'H&M_7_3', 100, 0, 8, 4, 2 )
insert into SanPham(TenSanPham, DonGia, HinhAnh, HinhAnh2, HinhAnh3, SoLuongTon, SoLuongMua, MaLoaiSanPham, MaNhaSanXuat, MaNhom)
values (N'Quần Jean H&M', 2150000, N'H&M_8_1', N'H&M_8_2', N'H&M_8_3', 100, 0, 8, 4, 2 )

﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GiaCongThienStore.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GiaCongStoreEntities : DbContext
    {
        public GiaCongStoreEntities()
            : base("name=GiaCongStoreEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BANVE> BANVEs { get; set; }
        public virtual DbSet<CALAMVIEC> CALAMVIECs { get; set; }
        public virtual DbSet<CHEDOCAT> CHEDOCATs { get; set; }
        public virtual DbSet<CHITIETNGUOITHAMGIABANVE> CHITIETNGUOITHAMGIABANVEs { get; set; }
        public virtual DbSet<CHITIETNGUOITHAMGIAQUYTRINH> CHITIETNGUOITHAMGIAQUYTRINHs { get; set; }
        public virtual DbSet<CHITIETPHIEUNHAP> CHITIETPHIEUNHAPs { get; set; }
        public virtual DbSet<CHITIETPHIEUXUAT> CHITIETPHIEUXUATs { get; set; }
        public virtual DbSet<CHITIETSANPHAMDUKIENBANVE> CHITIETSANPHAMDUKIENBANVEs { get; set; }
        public virtual DbSet<CHITIETSANPHAMQUYTRINH> CHITIETSANPHAMQUYTRINHs { get; set; }
        public virtual DbSet<CHUCVU> CHUCVUs { get; set; }
        public virtual DbSet<KHACHHANG> KHACHHANGs { get; set; }
        public virtual DbSet<LOAIQUYTRINH> LOAIQUYTRINHs { get; set; }
        public virtual DbSet<LOAISANPHAM> LOAISANPHAMs { get; set; }
        public virtual DbSet<NHACUNGCAP> NHACUNGCAPs { get; set; }
        public virtual DbSet<NHANVIEN> NHANVIENs { get; set; }
        public virtual DbSet<PHIEUNHAP> PHIEUNHAPs { get; set; }
        public virtual DbSet<PHIEUXUAT> PHIEUXUATs { get; set; }
        public virtual DbSet<QUYTRINH> QUYTRINHs { get; set; }
        public virtual DbSet<SANPHAM> SANPHAMs { get; set; }
        public virtual DbSet<TAIKHOAN> TAIKHOANs { get; set; }
        public virtual DbSet<TRANGTHAINHAPXUAT> TRANGTHAINHAPXUATs { get; set; }
        public virtual DbSet<LOGHISTORY> LOGHISTORies { get; set; }
    }
}

//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class NHANVIEN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NHANVIEN()
        {
            this.BANVEs = new HashSet<BANVE>();
            this.CHITIETNGUOITHAMGIABANVEs = new HashSet<CHITIETNGUOITHAMGIABANVE>();
            this.CHITIETNGUOITHAMGIAQUYTRINHs = new HashSet<CHITIETNGUOITHAMGIAQUYTRINH>();
            this.PHIEUNHAPs = new HashSet<PHIEUNHAP>();
            this.PHIEUXUATs = new HashSet<PHIEUXUAT>();
            this.QUYTRINHs = new HashSet<QUYTRINH>();
        }
    
        public string MNV { get; set; }
        public string TAIKHOAN { get; set; }
        public string TENNV { get; set; }
        public bool GIOITINH { get; set; }
        public System.DateTime NGAYSINH { get; set; }
        public string CMND { get; set; }
        public string DIACHI { get; set; }
        public string SDT { get; set; }
        public string EMAIL { get; set; }
        public System.DateTime NGAYVAOLAM { get; set; }
        public string CHUCVU { get; set; }
        public string CALAMVIEC { get; set; }
        public int TIENLUONG { get; set; }
        public bool ACTIVATE { get; set; }
        public Nullable<System.DateTime> NGAYKHOITAO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BANVE> BANVEs { get; set; }
        public virtual CALAMVIEC CALAMVIEC1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETNGUOITHAMGIABANVE> CHITIETNGUOITHAMGIABANVEs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETNGUOITHAMGIAQUYTRINH> CHITIETNGUOITHAMGIAQUYTRINHs { get; set; }
        public virtual CHUCVU CHUCVU1 { get; set; }
        public virtual TAIKHOAN TAIKHOAN1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUNHAP> PHIEUNHAPs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUXUAT> PHIEUXUATs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QUYTRINH> QUYTRINHs { get; set; }
    }
}

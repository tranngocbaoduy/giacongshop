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
    
    public partial class PHIEUNHAP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PHIEUNHAP()
        {
            this.CHITIETPHIEUNHAPs = new HashSet<CHITIETPHIEUNHAP>();
        }
    
        public string MPN { get; set; }
        public string TRANGTHAINHAPXUAT { get; set; }
        public string NHANVIEN { get; set; }
        public Nullable<System.DateTime> NGAYNHAP { get; set; }
        public int TONGSOLUONGNHAP { get; set; }
        public string GHICHU { get; set; }
        public string LICHSUTRANGTHAINHAP { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETPHIEUNHAP> CHITIETPHIEUNHAPs { get; set; }
        public virtual NHANVIEN NHANVIEN1 { get; set; }
        public virtual TRANGTHAINHAPXUAT TRANGTHAINHAPXUAT1 { get; set; }
    }
}

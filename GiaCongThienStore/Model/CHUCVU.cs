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
    
    public partial class CHUCVU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CHUCVU()
        {
            this.CHITIETNGUOITHAMGIABANVEs = new HashSet<CHITIETNGUOITHAMGIABANVE>();
            this.CHITIETNGUOITHAMGIAQUYTRINHs = new HashSet<CHITIETNGUOITHAMGIAQUYTRINH>();
            this.NHANVIENs = new HashSet<NHANVIEN>();
        }
    
        public string MCV { get; set; }
        public string TENCHUCVU { get; set; }
        public bool ACTIVATE { get; set; }
        public Nullable<System.DateTime> NGAYKHOITAO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETNGUOITHAMGIABANVE> CHITIETNGUOITHAMGIABANVEs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETNGUOITHAMGIAQUYTRINH> CHITIETNGUOITHAMGIAQUYTRINHs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NHANVIEN> NHANVIENs { get; set; }
    }
}

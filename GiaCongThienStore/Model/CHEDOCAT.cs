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
    
    public partial class CHEDOCAT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CHEDOCAT()
        {
            this.QUYTRINHs = new HashSet<QUYTRINH>();
        }
    
        public string MCDC { get; set; }
        public string MSP { get; set; }
        public string TENCHEDOCAT { get; set; }
        public Nullable<double> SOVONGQUAY { get; set; }
        public Nullable<double> BUOCTIEN { get; set; }
        public Nullable<double> CHIEUSAUCAT { get; set; }
        public Nullable<double> CHIEUDAILAMVIEC { get; set; }
        public Nullable<double> THOIGIANHOANTHANH { get; set; }
        public Nullable<System.DateTime> NGAYKHOITAO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QUYTRINH> QUYTRINHs { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConnectSQLServer.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class pemesanan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public pemesanan()
        {
            this.orderdetails = new HashSet<orderdetail>();
        }
    
        public int id { get; set; }
        public System.DateTime order_date { get; set; }
        public string status { get; set; }
        public int total_price { get; set; }
        public int user_id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<orderdetail> orderdetails { get; set; }
        public virtual user user { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcFirmaCagri.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class TblCagriDetay
    {
        public int ID { get; set; }
        public Nullable<int> cagri { get; set; }
        public string aciklama { get; set; }
        public Nullable<System.DateTime> tarih { get; set; }
        public string saat { get; set; }
    
        public virtual TblCagri TblCagri { get; set; }
    }
}

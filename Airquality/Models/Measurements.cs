//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Airquality.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Measurements
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> DatoMaerke { get; set; }
        public Nullable<int> OpstillingsId { get; set; }
        public Nullable<double> Resultat { get; set; }
        public Nullable<int> EnhedId { get; set; }
        public Nullable<int> StofId { get; set; }
    
        public virtual Compounds Compounds { get; set; }
        public virtual Instruments Instruments { get; set; }
        public virtual Units Units { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InsuranceCompany.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Accident
    {
        public Accident()
        {
            this.Clients = new HashSet<Clients>();
        }
    
        public int IdAccident { get; set; }
        public int IdClient { get; set; }
        public System.DateTime Date { get; set; }
        public string Description { get; set; }
        public string PolicyNumber { get; set; }
        public string PolicyType { get; set; }
    
        public virtual ICollection<Clients> Clients { get; set; }
    }
}

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
    
    public partial class Status
    {
        public Status()
        {
            this.Claims = new HashSet<Claims>();
            this.Policies = new HashSet<Policies>();
        }
    
        public int IdStatus { get; set; }
        public string StatusName { get; set; }
    
        public virtual ICollection<Claims> Claims { get; set; }
        public virtual ICollection<Policies> Policies { get; set; }
    }
}

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
    
    public partial class InsuranceRisks
    {
        public InsuranceRisks()
        {
            this.Policies = new HashSet<Policies>();
        }
    
        public int IdRisk { get; set; }
        public string RiskName { get; set; }
    
        public virtual ICollection<Policies> Policies { get; set; }
    }
}

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
    
    public partial class Drivers
    {
        public Drivers()
        {
            this.Policies = new HashSet<Policies>();
        }
    
        public int IdDriver { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public System.DateTime BirthDate { get; set; }
        public string DriverLicenseSeries { get; set; }
        public string DriverLicenseNumber { get; set; }
        public System.DateTime DataDriverLicense { get; set; }
        public int IdGender { get; set; }

        public int Number { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual ICollection<Policies> Policies { get; set; }
    }
}

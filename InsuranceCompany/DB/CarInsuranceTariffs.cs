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
    
    public partial class CarInsuranceTariffs
    {
        public int idCarInsuranceTariffs { get; set; }
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public string Generation { get; set; }
        public decimal Tariff { get; set; }
    }
}

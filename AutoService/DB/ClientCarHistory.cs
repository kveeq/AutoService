//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AutoService.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClientCarHistory
    {
        public int Id { get; set; }
        public Nullable<int> IdClient { get; set; }
        public Nullable<int> IdCar { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    
        public virtual Car Car { get; set; }
        public virtual Client Client { get; set; }
    }
}

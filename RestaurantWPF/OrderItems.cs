//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RestaurantWPF
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderItems
    {
        public int dish_id { get; set; }
        public int quantity { get; set; }
        public int oi_id { get; set; }
    
        public virtual MenuItems MenuItems { get; set; }
    }
}

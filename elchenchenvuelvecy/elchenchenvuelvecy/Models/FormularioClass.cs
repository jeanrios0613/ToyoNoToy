﻿namespace elchenchenvuelvecy.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
        [NotMapped]


    public class FormularioClass
    { 
        public Contact Contact { get; set; } = new Contact();
        public Enterprise Enterprise { get; set; } = new Enterprise();
        public Request Request { get; set; } = new Request();
        public RequestDetail RequestDetail { get; set; } = new RequestDetail();



    }


}

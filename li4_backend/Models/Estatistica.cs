//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace li4_backend.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Estatistica
    {
        public int id_estatistica { get; set; }
        public int utilizador { get; set; }
        public System.DateTime data { get; set; }
        public int id_escala { get; set; }
        public byte intervencao_imediata { get; set; }
        public string info_adicional { get; set; }
        public string localizacao_latitude { get; set; }
        public string localizacao_longitude { get; set; }
    
        public virtual Escala Escala { get; set; }
        public virtual Utilizador Utilizador1 { get; set; }
    }
}
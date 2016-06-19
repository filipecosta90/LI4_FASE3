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
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    public partial class Evento
    {
        [DisplayName("Id Evento")]
        
        public int id_evento { get; set; }
        [Required(ErrorMessage = "Por favor introduza a data do evento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Data")]
        public System.DateTime data { get; set; }
        [DisplayName("Registado por")]
        public int utilizador { get; set; }
        [DisplayName("Gravidade")]
        public int gravidade { get; set; }
        [DisplayName("Tipo")]
        public string tipo { get; set; }
        [DisplayName("Foto")]
        public byte[] foto { get; set; }
        [DisplayName("Resumo Textual")]
        public string resumo_textual { get; set; }

        [DisplayName("Latitude")]
        public string localizacao_latitude { get; set; }
        [DisplayName("Longitude")]
        public string localizacao_longitude { get; set; }
    
        public virtual Utilizador Utilizador1 { get; set; }
    }
}

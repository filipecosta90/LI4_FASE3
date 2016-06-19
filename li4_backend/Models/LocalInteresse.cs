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
    public partial class LocalInteresse
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LocalInteresse()
        {
            this.Utilizadors = new HashSet<Utilizador>();
            this.RegistoCampoes = new HashSet<RegistoCampo>();
        }

        [DisplayName("Id do local de interesse")]
        public int id_local_interesse { get; set; }
        [Required(ErrorMessage = "Por favor introduza a data do registo")]
        [DisplayName("Data do registo")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime data_registo { get; set; }
        [Required(ErrorMessage = "Por favor introduza a data para intervenção")]
        [DisplayName("Data para intervenção")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime data_intervencao { get; set; }
        [DisplayName("Informação adicional")]
        public string inf_adicional { get; set; }
        [DisplayName("Número de voluntários")]
        public Nullable<int> numero_voluntarios { get; set; }
        [DisplayName("Latitude")]
        public string latitude { get; set; }
        [DisplayName("Longitude")]
        public string longitude { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Utilizador> Utilizadors { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegistoCampo> RegistoCampoes { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Examen_Axsis.Models
{
    [DataContract(IsReference = true)]
    public class usuarios_axsis_Metadatos
    {
        public int id { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Correo electrónico")]
        [EmailAddress(ErrorMessage = "Agrega un correo electrónico válido")]
        public string correo { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Usuario")]
        [Required(ErrorMessage ="Introduce tu nombre de usuario")]
        [MinLength(7, ErrorMessage = "Debe tener minimo 7 digitos")]
        public string usuario { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [Compare("contrasena_confirm", ErrorMessage = "Las constraseñas no son iguales")]
        [StringLength(250, ErrorMessage = "La contraseña debe tener minimo 10 digitos", MinimumLength = 10)]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", 
            ErrorMessage = "Las contraseñas deben tener al menos 10 caracteres y contener minimamente 1 de los siguientes: mayúsculas(A - Z), minúsculas(a - z), números(0 - 9) " +
            "y un carácter especial(por ejemplo, @ # $% ^ & *!)")]

        public string contrasena { get; set; }

        [Display(Name = "Estatus")]
        public Nullable<bool> estatus { get; set; }
        
        [Display(Name = "Sexo")]
        [Required]
        public string sexo { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de registro")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> fecha_creacion { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        public string contrasena_confirm { get; set; }
    }
}

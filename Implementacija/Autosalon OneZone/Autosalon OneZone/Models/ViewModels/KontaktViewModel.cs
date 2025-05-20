// Autosalon OneZone/Models/ViewModels/KontaktViewModel.cs
using System.ComponentModel.DataAnnotations;

namespace Autosalon_OneZone.ViewModels
{
    public class KontaktViewModel
    {
        [Required(ErrorMessage = "Naslov je obavezan")]
        [MaxLength(200, ErrorMessage = "Naslov ne može biti duži od 200 karaktera")]
        public string Naslov { get; set; }

        [Required(ErrorMessage = "Sadržaj poruke je obavezan")]
        public string Sadrzaj { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;


namespace MecHub.ViewModel
{
    public class SharedResource
    {
    }

    public class LoginViewModel
    {
        [Required(
            ErrorMessageResourceName = "CampoObrigatorio",
            ErrorMessageResourceType = typeof(SharedResource)
        )]
        [EmailAddress]
        public required string Email { get; set; }

        [Required(
            ErrorMessageResourceName = "CampoObrigatorio",
            ErrorMessageResourceType = typeof(SharedResource)
        )]
        [DataType(DataType.Password)]
        public required string Senha { get; set; }

        public bool LembrarMe { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace UsersApi.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Укажите имя")]
        [MaxLength(20, ErrorMessage = "Имя должно иметь длину меньше 20 символов")]
        [MinLength(3, ErrorMessage = "Имя должно иметь длину больше 3 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите пароль")]
        [MinLength(6, ErrorMessage = "Пароль должен иметь длину больше 6 символов")]
        public string Email { get; set; }
        public string Password { get; set; } 



    }
}

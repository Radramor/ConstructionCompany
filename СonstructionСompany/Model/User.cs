using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СonstructionСompany.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }  // Пароль (зашифрованный)
        public string Role { get; set; }  // Роль пользователя
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Exceptions
{
    public class EmailNotFoundException (string email): 
        NotFoundException($" User with Email {email} not found..!!")
    {
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Exceptions
{
    // Abstract base class for not found exceptions
    public abstract class NotFoundException(string message) : Exception(message)
    {

    }
}

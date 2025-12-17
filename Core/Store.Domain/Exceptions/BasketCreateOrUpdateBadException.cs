using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Exceptions
{
    public class BasketCreateOrUpdateBadException() :
        BadException($"Invalid Operation On Create Or Update Basket..!!")
    {
    }
}

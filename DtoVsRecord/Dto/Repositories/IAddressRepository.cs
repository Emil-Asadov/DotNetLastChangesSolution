using DtoVsRecord.Dto.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoVsRecord.Dto.Repositories
{
    internal interface IAddressRepository
    {
        bool ValidateAddressEquality(AddressDto userInputAddress, AddressDto apiResponseAddress);
    }
}

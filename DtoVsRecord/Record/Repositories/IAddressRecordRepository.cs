using DtoVsRecord.Dto.Entities;
using DtoVsRecord.Record.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoVsRecord.Record.Repositories
{
    public interface IAddressRecordRepository
    {
        bool ValidateAddressEquality(AddressRecord userInputAddress, AddressRecord apiResponseAddress);
    }
}

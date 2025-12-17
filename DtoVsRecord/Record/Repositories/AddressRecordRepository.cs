using DtoVsRecord.Dto.Entities;
using DtoVsRecord.Record.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoVsRecord.Record.Repositories
{
    public class AddressRecordRepository : IAddressRecordRepository
    {
        public bool ValidateAddressEquality(AddressRecord userInputAddress, AddressRecord apiResponseAddress)
        {
            #region Bu yazilisda isdiyir
            //var streetMatches = userInputAddress.street.Equals(apiResponseAddress.street);
            //var cityMatches = userInputAddress.city.Equals(apiResponseAddress.city);
            //var res = streetMatches && cityMatches;
            #endregion
            var res = userInputAddress.Equals(apiResponseAddress);

            return res;
        }
    }
}

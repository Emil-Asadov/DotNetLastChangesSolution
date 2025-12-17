using DtoVsRecord.Dto.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoVsRecord.Dto.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        public bool ValidateAddressEquality(AddressDto userInputAddress, AddressDto apiResponseAddress)
        {
            var streetMatches = userInputAddress.Street.Equals(apiResponseAddress.Street);
            var cityMatches = userInputAddress.City.Equals(apiResponseAddress.City);
            var res = streetMatches && cityMatches;

            //res = userInputAddress.Equals(apiResponseAddress);//Bu yoxlama DTO ucun isdemir

            return res;
        }
    }
}

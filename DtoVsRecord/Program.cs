using DtoVsRecord.Dto.Entities;
using DtoVsRecord.Dto.Repositories;
using DtoVsRecord.Record.Entities;
using DtoVsRecord.Record.Repositories;

namespace DtoVsRecord
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IAddressRepository addressDto = new AddressRepository();
            var resDto = addressDto.ValidateAddressEquality(new AddressDto
            {
                Street = "123 Main st",
                City = "Anytown"
            }, new AddressDto
            {
                Street = "123 Main st",
                City = "Anytown"
            });
            Console.WriteLine($"Dto result: {resDto}");
            Console.WriteLine();

            IAddressRecordRepository addressRecordDto = new AddressRecordRepository();
            var resRecord = addressRecordDto.ValidateAddressEquality(new AddressRecord("123 Main st", "Anytown"), new AddressRecord("123 Main st", "Anytown"));
            Console.WriteLine($"Record result: {resRecord}");

            Console.ReadKey();
        }
    }
}

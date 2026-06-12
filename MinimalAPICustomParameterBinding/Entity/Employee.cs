namespace MinimalAPICustomParameterBinding.Entity
{
    public sealed class Employee
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public static bool TryParse(string? value, out Employee? result)
        {
            try
            {
                var splitValue = value?.Split(";")/*.Select(double.Parse)*/.ToArray();
                result = new Employee()
                {
                    Name = splitValue![0],
                    SurName = splitValue[1]
                };

                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }
    }
}

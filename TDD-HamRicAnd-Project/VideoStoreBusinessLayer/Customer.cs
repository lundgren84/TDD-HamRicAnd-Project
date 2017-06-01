namespace VideoStoreBusinessLayer
{
    public class Customer
    {
        public string Name { get; set; }
        public string SSN { get; set; }
        public Customer(string ssn, string name)
        {
            this.Name = name;
            this.SSN = ssn;
        }
        public Customer()
        {
                
        }
    }
}
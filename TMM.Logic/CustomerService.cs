using TMM.Database;

namespace TMM.Logic
{
    public class CustomerService : ICustomerService
    {
        private CustomerRepository customerRepository;
        private AddressRepository addressRepository;

        public CustomerService(CustomerRepository customerRepository, AddressRepository addressRepository)
        {
            this.customerRepository = customerRepository;
            this.addressRepository = addressRepository;
        }

        public ReponseResult AddCustomer(CompleteCustomerModel customerModel)
        {
            try
            {
                if (Validate(out List<string> Messages))
                {
                    Customer customer = new()
                    {
                        Title = customerModel.Title,
                        Forename = customerModel.Forename,
                        SureName = customerModel.SureName,
                        EmailAddress = customerModel.EmailAddress,
                        MobileNo = customerModel.MobileNo,
                        Active = true,
                        Addresses = new List<Address>()
                    };

                    foreach (CompleteAddressModel _add in customerModel.Addresses)
                    {
                        customer.Addresses.Add(new Address()
                        {
                            AddressLine1 = _add.AddressLine1,
                            Country = _add.Country,
                            County = _add.County,
                            Postcode = _add.Postcode,
                            Town = _add.Town,
                            MainAddress = _add.MainAddress
                        });
                    }

                    customerRepository.Create(customer);

                    return ReponseResult.Success(customer.Id);
                }

                return ReponseResult.ValidaitionFailed(Messages.ToArray());
            }
            catch (Exception ex)
            {
                //TODO : log here
                return ReponseResult.Exception(ex);
            }

            bool Validate(out List<string> Messages)
            {
                Messages = new List<string>();

                if (customerRepository.GetByPredicate(a => a.Forename == customerModel.Forename && a.SureName == customerModel.SureName).Any())
                {
                    Messages.Add("Customer Already Exists");
                }

                if (!customerModel.Addresses?.Any() ?? false)
                {
                    Messages.Add("Customer is required to have at lease one address");
                }
                else if (customerModel.Addresses.Count(a => a.MainAddress) != 1)
                {
                    Messages.Add("Customer is required to have only 1 main address");
                }

                //TODO : complete email address, Mobile No, null checks

                return Messages.Count == 0;
            }
        }

        public IEnumerable<Customer> GetCustomers(bool ActiveOnly)
        {
            try
            {
                if (ActiveOnly)
                {
                    return customerRepository.GetByPredicate(a => a.Active, true);
                }

                return customerRepository.GetAll();
            }
            catch (Exception ex)
            {
                //TODO : log here
                return null;
            }
        }

        public Customer GetCustomer(int ID)
        {
            try
            {
                return customerRepository.GetById(ID);
            }
            catch (Exception ex)
            {
                //TODO : log here
                return null;
            }
        }

        public ReponseResult DeleteAddress(int CustomerID, int AddressID)
        {
            try
            {
                if (Validate(out string Message))
                {
                    Customer c = customerRepository.GetById(CustomerID);

                    Address toDelete = c.Addresses.Single(a => a.Id == AddressID);

                    addressRepository.Delete(toDelete);

                    Address toUpdate = c.Addresses.First();

                    toUpdate.MainAddress = true;

                    addressRepository.Update(toUpdate);

                    return ReponseResult.Success();
                }

                return ReponseResult.ValidaitionFailed(Message);
            }
            catch (Exception ex)
            {
                //TODO : log here
                return ReponseResult.Exception(ex);
            }

            bool Validate(out string Messages)
            {
                Messages = string.Empty;

                if (!GetSingleCustomer(CustomerID, out string Message, out Customer c))
                {
                    Messages = Message;
                }
                else if (!c.Addresses.Any(a => a.Id == AddressID)) // address does not exists
                {
                    Messages = "Invalid Adddress id passed in";
                }
                else if (c.Addresses.Count == 1)
                {
                    Messages = "Address is the last address for the customer, cannot be deleted";
                }

                return string.IsNullOrEmpty(Messages);
            }
        }

        public ReponseResult SetMainAddress(int CustomerID, int AddressID)
        {
            try
            {
                if (!Validate(out string Message))
                {
                    return ReponseResult.ValidaitionFailed(Message);
                }
                else
                {
                    Customer c = customerRepository.GetById(CustomerID);

                    foreach (Address address in c.Addresses)
                    {
                        if (address.Id == AddressID)
                            address.MainAddress = true;
                        else
                            address.MainAddress = false; //TODO : this should be locked down on db level

                        addressRepository.Update(address);
                    }


                    return ReponseResult.Success();
                }
            }
            catch (Exception ex)
            {
                //TODO : log here
                return ReponseResult.Exception(ex);
            }

            bool Validate(out string Messages)
            {
                Messages = string.Empty;

                if (!GetSingleCustomer(CustomerID, out string Message, out Customer c))
                {
                    Messages = Message;
                }
                else if (!c.Addresses.Any(a => a.Id == AddressID)) // address does not exists
                {
                    Messages = "Invalid Adddress id passed in";
                }

                return string.IsNullOrEmpty(Messages);
            }
        }

        public ReponseResult MarkCustomerAsInactive(int CustomerID)
        {
            try
            {
                if (!BasicValidate(CustomerID, out string Message))
                {
                    return ReponseResult.ValidaitionFailed(Message);
                }
                else
                {
                    Customer c = customerRepository.GetById(CustomerID);

                    c.Active = false;

                    customerRepository.Update(c);

                    return ReponseResult.Success();
                }
            }
            catch (Exception ex)
            {
                //TODO : log here
                return ReponseResult.Exception(ex);
            }


        }

        public ReponseResult DeleteCustomer(int CustomerID)
        {
            try
            {
                if (!BasicValidate(CustomerID, out string Messages))
                {
                    return ReponseResult.ValidaitionFailed(Messages);
                }
                else
                {
                    Customer c = customerRepository.GetById(CustomerID);

                    customerRepository.Delete(c);

                    return ReponseResult.Success();
                }
            }
            catch (Exception ex)
            {
                //TODO : log here
                return ReponseResult.Exception(ex);
            }
        }

        private bool BasicValidate(int CustomerID, out string Messages)
        {
            Messages = string.Empty;

            if (!GetSingleCustomer(CustomerID, out string Message, out Customer c))
            {
                Messages = Message;
            }

            return string.IsNullOrEmpty(Messages);
        }

        private bool GetSingleCustomer(int CustomerID, out string Message, out Customer customer)
        {
            customer = customerRepository.GetById(CustomerID);
            Message = "Invalid CustomerID Passed in";

            return customer != null;

        }

    }


    public interface ICustomerService
    {
        ReponseResult AddCustomer(CompleteCustomerModel customerModel);
        IEnumerable<Customer> GetCustomers(bool ActiveOnly);
        Customer GetCustomer(int ID);
        ReponseResult DeleteAddress(int CustomerID, int AddressID);
        ReponseResult SetMainAddress(int CustomerID, int AddressID);
        ReponseResult MarkCustomerAsInactive(int CustomerID);
        ReponseResult DeleteCustomer(int CustomerID);
    }
}
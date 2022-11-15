namespace VestEngine.Infastructure.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IAccountRepository AccountRepository { get; }
        ICountryRepository CountryRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        ILedgerRepository LedgerRepository { get; }
        ILedgerTypeRepository LedgerTypeRepository { get; }
        IMemberRepository MemberRepository { get; }
        IProductRepository ProductRepository { get; }
        ISupportedCountryRepository SupportedCountryRepository { get; }
        ITransactionRepository TransactionRepository { get; }
        ITransactionTypeRepository TransactionTypeRepository { get; }
        public IBusinessRepository BusinessRepository { get; }
        public IUserRepository UserRepository { get; }
        IChargeRepository ChargeRepository { get; }
        ITaxRepository TaxRepository { get; }
    }
}
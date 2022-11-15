using VestEngine.Infastructure.Data;
using VestEngine.Infastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestEngine.Infastructure.Repositories.Implementation
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppDbContext _appDbContext;
		private IAccountRepository _accountRepository;
		private IProductRepository _productRepository;
		private ILedgerRepository _ledgerRepository;
		private ILedgerTypeRepository _ledgerTypeRepository;
		private ITransactionTypeRepository _transactionTypeRepository;
		private ITransactionRepository _transactionRepository;
		private IMemberRepository _memberRepository;
		private ICustomerRepository _customerRepository;
		private ISupportedCountryRepository _supportedCountryRepository;
		private ICountryRepository _countryRepository;
		private IBusinessRepository _businessRepository;
		private IUserRepository _userRepository;
		private IChargeRepository _chargeRepository;
		private ITaxRepository _taxRepository;

		public UnitOfWork(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}
		public IAccountRepository AccountRepository => _accountRepository ??= new AccountRepository(_appDbContext);
		public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_appDbContext);
		public ILedgerRepository LedgerRepository => _ledgerRepository ??= new LedgerRepository(_appDbContext);
		public ILedgerTypeRepository LedgerTypeRepository => _ledgerTypeRepository ??= new LedgerTypeRepository(_appDbContext);
		public ITransactionTypeRepository TransactionTypeRepository => _transactionTypeRepository ??= new TransactionTypeRepository(_appDbContext);
		public ITransactionRepository TransactionRepository => _transactionRepository ??= new TransactionRepository(_appDbContext);
		public IMemberRepository MemberRepository => _memberRepository ??= new MemberRepository(_appDbContext);
		public ICustomerRepository CustomerRepository => _customerRepository ??= new CustomerRepository(_appDbContext);
		public ISupportedCountryRepository SupportedCountryRepository => _supportedCountryRepository ??= new SupportedCountryRepository(_appDbContext);
		public ICountryRepository CountryRepository => _countryRepository ??= new CountryRepository(_appDbContext);
		public IBusinessRepository BusinessRepository => _businessRepository ??= new BusinessRepository(_appDbContext);
		public IUserRepository UserRepository => _userRepository ??= new UserRepository(_appDbContext);
		public IChargeRepository ChargeRepository => _chargeRepository ??= new ChargeRepository(_appDbContext);
		public ITaxRepository TaxRepository => _taxRepository ??= new TaxRepository(_appDbContext);
	}
}
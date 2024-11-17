﻿using MilkSt.Data.Models;
using MilkSt.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkSt.Data
{
    public class UnitOfWork
    {
        private Net1804_212_3_MilkStContext _unitOfWorkContext;

        private BillDetailRepository _billDetail;
        private MilkRepository _milk;
        private CustomerRepository _customer;
        private BillRepository _bill;
        private VoucherRepository _voucher;

        public UnitOfWork()
        {
            _unitOfWorkContext ??= new Net1804_212_3_MilkStContext();
        }

        public BillDetailRepository BillDetailRepository { get { return _billDetail ??= new Repository.BillDetailRepository(_unitOfWorkContext); } }

        public MilkRepository MilkRepository { get { return _milk ??= new Repository.MilkRepository(_unitOfWorkContext); } }

        public CustomerRepository CustomerRepository {get { return _customer ??= new Repository.CustomerRepository(_unitOfWorkContext); } }

        public BillRepository BillRepository { get { return _bill ??= new Repository.BillRepository(_unitOfWorkContext); } }

        public VoucherRepository VoucherRepository { get { return _voucher ??= new Repository.VoucherRepository(_unitOfWorkContext); } }



        #region Set transaction isolation levels

        /*
        Read Uncommitted: The lowest level of isolation, allows transactions to read uncommitted data from other transactions. This can lead to dirty reads and other issues.

        Read Committed: Transactions can only read data that has been committed by other transactions. This level avoids dirty reads but can still experience other isolation problems.

        Repeatable Read: Transactions can only read data that was committed before their execution, and all reads are repeatable. This prevents dirty reads and non-repeatable reads, but may still experience phantom reads.

        Serializable: The highest level of isolation, ensuring that transactions are completely isolated from one another. This can lead to increased lock contention, potentially hurting performance.

        Snapshot: This isolation level uses row versioning to avoid locks, providing consistency without impeding concurrency. 
         */

        public int SaveChangesWithTransaction()
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (var dbContextTransaction = _unitOfWorkContext.Database.BeginTransaction())
            {
                try
                {
                    result = _unitOfWorkContext.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }

        public async Task<int> SaveChangesWithTransactionAsync()
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (var dbContextTransaction = _unitOfWorkContext.Database.BeginTransaction())
            {
                try
                {
                    result = await _unitOfWorkContext.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }

        #endregion
    }
}

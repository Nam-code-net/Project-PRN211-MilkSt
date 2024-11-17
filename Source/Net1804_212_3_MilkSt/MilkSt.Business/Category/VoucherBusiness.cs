using MilkSt.Business.Base;
using MilkSt.Common;
using MilkSt.Data.Models;
using MilkSt.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MilkSt.Business.Category
{
    public interface IVoucherBusiness
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> Save(Voucher voucher);
        Task<IBusinessResult> Update(Voucher voucher);
        Task<IBusinessResult> DeleteById(int id);
        Task<IBusinessResult> SearchByCriteria(string name, string discount, string expiry, string quantity);
    }

    public class VoucherBusiness : IVoucherBusiness
    {
        private readonly UnitOfWork _unitOfWork;

        public VoucherBusiness()
        {
            _unitOfWork ??= new();
        }

        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                var voucher = await _unitOfWork.VoucherRepository.GetAllAsync();

                if (voucher == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, voucher);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetById(int id)
        {
            try
            {
                var voucher = await _unitOfWork.VoucherRepository.GetByIdAsync(id);

                if (voucher == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, voucher);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> Save(Voucher voucher)
        {
            try
            {
                int result = await _unitOfWork.VoucherRepository.CreateAsync(voucher);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IBusinessResult> Update(Voucher voucher)
        {
            try
            {
                int result = await _unitOfWork.VoucherRepository.UpdateAsync(voucher);

                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.ToString());
            }
        }

        public async Task<IBusinessResult> DeleteById(int id)
        {
            try
            {
                var voucher = await _unitOfWork.VoucherRepository.GetByIdAsync(id);
                if (voucher != null)
                {
                    var result = await _unitOfWork.VoucherRepository.RemoveAsync(voucher);
                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
                    }
                }
                else
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.ToString());
            }
        }

        public async Task<IBusinessResult> SearchByCriteria(string name, string discount, string expiry, string quantity)
        {
            try
            {
                var query = _unitOfWork.VoucherRepository.Query();

                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(v => v.Name.Contains(name));
                }

                if (!string.IsNullOrEmpty(discount))
                {
                    if (double.TryParse(discount, out double disc))
                    {
                        query = query.Where(v => v.Discount == disc);
                    }
                }

                if (!string.IsNullOrEmpty(expiry))
                {
                    if (DateTime.TryParse(expiry, out DateTime exp))
                    {
                        var expDateOnly = DateOnly.FromDateTime(exp);
                        query = query.Where(v => v.Expiry == expDateOnly);
                    }
                }

                if (!string.IsNullOrEmpty(quantity))
                {
                    if (int.TryParse(quantity, out int qty))
                    {
                        query = query.Where(v => v.Quantity == qty);
                    }
                }

                var vouchers = await query.ToListAsync();

                if (vouchers.Count == 0)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, vouchers);
                }
            }
            catch (Exception e)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, e.Message);
            }
        }
    }
}

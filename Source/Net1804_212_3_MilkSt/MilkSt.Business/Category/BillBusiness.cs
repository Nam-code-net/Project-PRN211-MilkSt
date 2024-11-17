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
    public interface IBillBusiness
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> Save(Bill bill);
        Task<IBusinessResult> Update(Bill bill);
        Task<IBusinessResult> DeleteById(int id);
        Task<IBusinessResult> SearchByCriteria(string billId, string billDetailId, string customerId, string date);
    }

    public class BillBusiness : IBillBusiness
    {
        private readonly UnitOfWork _unitOfWork;

        public BillBusiness()
        {
            _unitOfWork ??= new();
        }

        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                var bill = await _unitOfWork.BillRepository.GetAllAsync();

                if (bill == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, bill);
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
                var bill = await _unitOfWork.BillRepository.GetByIdAsync(id);

                if (bill == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, bill);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> Save(Bill bill)
        {
            try
            {
                int result = await _unitOfWork.BillRepository.CreateAsync(bill);
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

        public async Task<IBusinessResult> Update(Bill bill)
        {
            try
            {
                int result = await _unitOfWork.BillRepository.UpdateAsync(bill);

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
                var bill = await _unitOfWork.BillRepository.GetByIdAsync(id);
                if (bill != null)
                {
                    var result = await _unitOfWork.BillRepository.RemoveAsync(bill);
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

        public async Task<IBusinessResult> SearchByCriteria(string subTotal, string discountAmount, string totalAmount, string status)
        {
            try
            {
                var query = _unitOfWork.BillRepository.Query();

                if (!string.IsNullOrEmpty(subTotal))
                {
                    if (double.TryParse(subTotal, out double result))
                    {
                        query = query.Where(v => v.SubTotal == result);
                    }
                }
                if (!string.IsNullOrEmpty(discountAmount))
                {
                    if (double.TryParse(discountAmount, out double result))
                    {
                        query = query.Where(v => v.DiscountAmount == result);
                    }
                }
                if (!string.IsNullOrEmpty(totalAmount))
                {
                    if (double.TryParse(totalAmount, out double result))
                    {
                        query = query.Where(v => v.TotalAmount == result);
                    }
                }
                if (!string.IsNullOrEmpty(status))
                {
                    query = query.Where(v => v.Status.Contains(status));
                }

                    var bill = await query.ToListAsync();

                if (bill.Count == 0)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, bill);
                }
            }
            catch (Exception e)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, e.Message);
            }
        }
    }
}

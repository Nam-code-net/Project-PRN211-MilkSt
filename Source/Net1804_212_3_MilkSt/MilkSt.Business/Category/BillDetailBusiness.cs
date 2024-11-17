using Microsoft.EntityFrameworkCore;
using MilkSt.Business.Base;
using MilkSt.Common;
using MilkSt.Data;
using MilkSt.Data.Models;
using MilkSt.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkSt.Business.Category
{
    public interface IBillDetailBusiness
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int code);
        Task<IBusinessResult> Save(BillDetail billDetail);
        Task<IBusinessResult> Update(BillDetail billDetailcurrency);
        Task<IBusinessResult> DeleteById(int code);
        Task<IBusinessResult> SearchByStatus(string status, string discount, string tax, string minTotal, string maxTotal);
    }

    public class BillDetailBusiness : IBillDetailBusiness
    {
        private readonly UnitOfWork _unitOfWork;

        public BillDetailBusiness()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> DeleteById(int code)
        {
            try
            {
                var billDetail = await _unitOfWork.BillDetailRepository.GetByIdAsync(code);
                if (billDetail != null)
                {
                    var result = await _unitOfWork.BillDetailRepository.RemoveAsync(billDetail);
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

        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                var billDetails = await _unitOfWork.BillDetailRepository.GetAllAsync();

                if (billDetails == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, billDetails);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetById(int code)
        {
            try
            {
                var billDetails = await _unitOfWork.BillDetailRepository.GetByIdAsync(code);

                if (billDetails == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, billDetails);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> Save(BillDetail billDetail)
        {
            try
            {
                int result = await _unitOfWork.BillDetailRepository.CreateAsync(billDetail);
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

        public async Task<IBusinessResult> SearchByStatus(string status, string discount, string tax, string minTotal, string maxTotal)
        {
            try
            {
                var query = _unitOfWork.BillDetailRepository.Query();

                if (!string.IsNullOrEmpty(discount) && double.TryParse(discount, out double discountResult))
                {
                        query = query.Where(v => v.Discount == discountResult);
                }

                if (!string.IsNullOrEmpty(tax) && double.TryParse(tax, out double taxResult))
                {
                        query = query.Where(v => v.Tax == taxResult);
                }

                if (!string.IsNullOrEmpty(minTotal) && double.TryParse(minTotal, out double minResult))
                {
                    query = query.Where(v => v.Total >= minResult);
                }


                if (!string.IsNullOrEmpty(maxTotal) && double.TryParse(maxTotal, out double maxResult))
                {
                    query = query.Where(v => v.Total <= maxResult);
                }

                if (!string.IsNullOrEmpty(status))
                {
                    query = query.Where(v => v.Status.Contains(status));
                }

                var billDetail = await query.ToListAsync();

                if (billDetail.Count == 0)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, billDetail);
                }
            }
            catch (Exception e)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, e.Message);
            }
        }

        public async Task<IBusinessResult> Update(BillDetail billDetailcurrency)
        {
            try
            {
                int result = await _unitOfWork.BillDetailRepository.UpdateAsync(billDetailcurrency);

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
    }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Shop.Application.Utilities;
using Shop.Domain.DataTransferObjects.GeneralDataTransferObjects;

namespace Shop.Application.IServices
{
    public interface IService<TModel, TDto> where TModel : class where TDto : class
    {
        List<TModel> GetAll(Expression<Func<TModel, bool>> conditions = null);
        List<TDto> GetAllDto(Expression<Func<TModel, bool>> conditions = null);
        TModel GetById(int id);
        TDto GetDtoById(int id);
        TModel GetByQuery(Expression<Func<TModel, bool>> conditions);
        TDto GetDtoByQuery(Expression<Func<TModel, bool>> conditions);
        RequestResult<TDto> Add(TDto dto);
        RequestResult AddRangeByModel(List<TModel> models);
        RequestResult<TDto> Update(TDto dto);
        RequestResult<TModel> UpdateByModel(TModel model);
        RequestResult UpdateRangeByModel(List<TModel> models);
        RequestResult Delete(int id);
        List<OptionDto> GetSelectList(Expression<Func<TModel, bool>> conditions = null, bool hasDefault = true);
        bool IsExist(Expression<Func<TModel, bool>> conditions);

        #region Async Methods

        Task<List<TModel>> GetAllAsync(CancellationToken cancellationToken, Expression<Func<TModel, bool>> conditions = null);
        Task<List<TDto>> GetAllDtoAsync(CancellationToken cancellationToken, Expression<Func<TModel, bool>> conditions = null);
        Task<TModel> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<TDto> GetDtoByIdAsync(int id, CancellationToken cancellationToken);
        Task<TModel> GetByQueryAsync(Expression<Func<TModel, bool>> conditions, CancellationToken cancellationToken);
        Task<TDto> GetDtoByQueryAsync(Expression<Func<TModel, bool>> conditions, CancellationToken cancellationToken);
        Task<RequestResult<TDto>> AddAsync(TDto dto, CancellationToken cancellationToken);
        Task<RequestResult> AddRangeByModelAsync(List<TModel> models, CancellationToken cancellationToken);
        Task<RequestResult<TDto>> UpdateAsync(TDto dto, CancellationToken cancellationToken);
        Task<RequestResult<TModel>> UpdateByModelAsync(TModel model, CancellationToken cancellationToken);
        Task<RequestResult> UpdateRangeByModelAsync(List<TModel> models, CancellationToken cancellationToken);
        Task<RequestResult> DeleteAsync(int id, CancellationToken cancellationToken);
        Task<List<OptionDto>> GetSelectListAsync(CancellationToken cancellationToken, Expression<Func<TModel, bool>> conditions = null, bool hasDefault = true);
        Task<bool> IsExistAsync(Expression<Func<TModel, bool>> conditions, CancellationToken cancellationToken);
        
        #endregion

        #region Additional Methods

        void SetDefaultValueIfMatch<TClass, TProperty>(TClass dto, string propertyName, TProperty defaultValue, TProperty expectedValue) where TClass : class;

        #endregion
    }
}
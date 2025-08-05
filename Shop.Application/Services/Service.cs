using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Shop.Application.IServices;
using Shop.Application.Mappings;
using Shop.Application.Utilities;
using Shop.Domain.DataTransferObjects;
using Shop.Domain.DataTransferObjects.GeneralDataTransferObjects;
using Shop.Domain.IRepositories;
using Shop.Domain.Models;

namespace Shop.Application.Services
{
    public class Service<TModel, TDto> : IService<TModel, TDto>
        where TModel : class, IEntity, new() 
        where TDto : class, IDto, new()
    {
        protected readonly IRepository<TModel> Repository;

        public Service(IRepository<TModel> repository)
        {
            Repository = repository;
        }

        public virtual List<TModel> GetAll(Expression<Func<TModel, bool>> conditions = null)
        {
            return Repository.GetAll(conditions);
        }

        public virtual List<TDto> GetAllDto(Expression<Func<TModel, bool>> conditions = null)
        {
            var models = GetAll(conditions);
            return models.Select(model => model.ToDto<TModel, TDto>().Map()).ToList();
        }

        public virtual TModel GetById(int id)
        {
            return Repository.GetById(id);
        }

        public virtual TDto GetDtoById(int id)
        {
            var model = GetById(id);
            return model.ToDto<TModel, TDto>().Map();
        }

        public virtual TModel GetByQuery(Expression<Func<TModel, bool>> conditions)
        {
            return Repository.GetByQuery(conditions);
        }

        public virtual TDto GetDtoByQuery(Expression<Func<TModel, bool>> conditions)
        {
            var model = GetByQuery(conditions);
            return model.ToDto<TModel, TDto>().Map();
        }

        public virtual RequestResult<TDto> Add(TDto dto)
        {
            if (dto == null)
                return new RequestResult<TDto>(false, RequestResultStatusCode.NotFound, null);
            
            SetDefaultValueIfMatch(dto, nameof(BaseDto.CreateDate), DateTime.Now, DateTime.MinValue);
            
            var model = dto.ToModel<TModel, TDto>().Map();
            Repository.Add(model);
            
            return new RequestResult<TDto>(true, RequestResultStatusCode.Success, dto);
        }

        public RequestResult AddRangeByModel(List<TModel> models)
        {
            if (models is { Count: <= 0 })
                return new RequestResult(false, RequestResultStatusCode.BadRequest);

            foreach (var model in models)
            {
                SetDefaultValueIfMatch(model, nameof(BaseEntity.CreateDate), DateTime.Now, DateTime.MinValue);
            }
            
            Repository.AddRange(models);
            return new RequestResult(true, RequestResultStatusCode.Success);
        }

        public virtual RequestResult<TDto> Update(TDto dto)
        {
            if (dto == null)
                return new RequestResult<TDto>(false, RequestResultStatusCode.NotFound, null);

            var idProp = typeof(TDto).GetProperty("Id");

            if (idProp == null)
                return new RequestResult<TDto>(false, RequestResultStatusCode.BadRequest, null, "محصول مورد نظر یافت نشد!");

            var id = (int)idProp.GetValue(dto)!;
            if (id == 0)
                return new RequestResult<TDto>(false, RequestResultStatusCode.BadRequest, null, "محصول مورد نظر یافت نشد!");
            
            SetDefaultValueIfMatch(dto, nameof(BaseDto.UpdateDate), (DateTime?)DateTime.Now, DateTime.MinValue);

            var model = GetById(id);

            if (model == null)
                return new RequestResult<TDto>(false, RequestResultStatusCode.BadRequest, null, "محصول مورد نظر یافت نشد!");
            
            dto.ToModel(model).Ignore("CreateDate").Map();
            
             Repository.Update(model);
            
            return new RequestResult<TDto>(true , RequestResultStatusCode.Success, dto);
        }

        public RequestResult<TModel> UpdateByModel(TModel model)
        {
            SetDefaultValueIfMatch(model, nameof(BaseEntity.UpdateDate), DateTime.Now, DateTime.MinValue);
            
            model = Repository.Update(model);
            
            return new RequestResult<TModel>(true, RequestResultStatusCode.Success, model);
        }

        public RequestResult UpdateRangeByModel(List<TModel> models)
        {
            if (models is { Count: <= 0 })
                return new RequestResult(false, RequestResultStatusCode.BadRequest);

            foreach (var model in models)
            {
                SetDefaultValueIfMatch(model, nameof(BaseEntity.UpdateDate), DateTime.Now, DateTime.MinValue);
            }
            
            Repository.UpdateRange(models);
            return new RequestResult(true, RequestResultStatusCode.Success);
        }

        public virtual RequestResult Delete(int id)
        {
            var model = GetById(id);

            if (model == null)
                return new RequestResult(false, RequestResultStatusCode.NotFound);

            var entityType = typeof(TModel);
            var deleteDateProp = entityType.GetProperty(nameof(BaseEntity.DeleteDate));

            if (deleteDateProp != null && deleteDateProp.CanWrite)
            {
                deleteDateProp.SetValue(model, DateTime.Now);
                Repository.Update(model);
                return new RequestResult(true, RequestResultStatusCode.Success);
            }
            
            Repository.Delete(model);
            return new RequestResult(true, RequestResultStatusCode.Success);
        }

        public virtual List<OptionDto> GetSelectList(Expression<Func<TModel, bool>> conditions = null, bool hasDefault = true)
        {
            var result = new List<OptionDto>();
            if (hasDefault)
                result.Add(new OptionDto()
                {
                    Value = null,
                    Text = "لطفا انتخاب کنید"
                });
            
            var options = GetAll(conditions); 
            
            result.AddRange(options.ToOptions());

            return result;
        }

        public bool IsExist(Expression<Func<TModel, bool>> conditions)
        {
            return Repository.IsExist(conditions);
        }

        #region Async Methods

        public virtual async Task<List<TModel>> GetAllAsync(CancellationToken cancellationToken, Expression<Func<TModel, bool>> conditions = null)
        {
            return await Repository.GetAllAsync(cancellationToken, conditions);
        }

        public virtual async Task<List<TDto>> GetAllDtoAsync(CancellationToken cancellationToken, Expression<Func<TModel, bool>> conditions = null)
        {
            var models = await GetAllAsync(cancellationToken, conditions);
            return models.Select(model => model.ToDto<TModel, TDto>().Map()).ToList();
        }

        public virtual async Task<TModel> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await Repository.GetByIdAsync(id, cancellationToken);
        }

        public virtual async Task<TDto> GetDtoByIdAsync(int id, CancellationToken cancellationToken)
        {
            var model = await GetByIdAsync(id, cancellationToken);
            return model.ToDto<TModel, TDto>().Map();
        }

        public virtual async Task<TModel> GetByQueryAsync(Expression<Func<TModel, bool>> conditions, CancellationToken cancellationToken)
        {
            return await Repository.GetByQueryAsync(conditions, cancellationToken);
        }

        public virtual async Task<TDto> GetDtoByQueryAsync(Expression<Func<TModel, bool>> conditions, CancellationToken cancellationToken)
        {
            var model = await GetByQueryAsync(conditions, cancellationToken);
            return model.ToDto<TModel, TDto>().Map();
        }

        public virtual async Task<RequestResult<TDto>> AddAsync(TDto dto, CancellationToken cancellationToken)
        {
            if (dto == null)
                return new RequestResult<TDto>(false, RequestResultStatusCode.NotFound, null);

            SetDefaultValueIfMatch(dto, nameof(BaseDto.CreateDate), DateTime.Now, DateTime.MinValue);
            
            var model = dto.ToModel<TModel, TDto>().Map();
            /*var result =*/ await Repository.AddAsync(model, cancellationToken);

            // if (result == null)
            //     return new RequestResult<TDto>(false, RequestResultStatusCode.InternalServerError, null, "متاسفانه خطایی رخ داده است!");
            
            return new RequestResult<TDto>(true, RequestResultStatusCode.Success, dto);
        }

        public async Task<RequestResult> AddRangeByModelAsync(List<TModel> models, CancellationToken cancellationToken)
        {
            if (models is { Count: <= 0 })
                return new RequestResult(false, RequestResultStatusCode.BadRequest);

            foreach (var model in models)
            {
                SetDefaultValueIfMatch(model, nameof(BaseEntity.CreateDate), DateTime.Now, DateTime.MinValue);
            }
            
            await Repository.AddRangeAsync(models, cancellationToken);
            return new RequestResult(true, RequestResultStatusCode.Success);
        }

        public virtual async Task<RequestResult<TDto>> UpdateAsync(TDto dto, CancellationToken cancellationToken)
        {
            if (dto == null)
                return new RequestResult<TDto>(false, RequestResultStatusCode.NotFound, null);

            var idProp = typeof(TDto).GetProperty("Id");

            if (idProp == null)
                return new RequestResult<TDto>(false, RequestResultStatusCode.BadRequest, null, "محصول مورد نظر یافت نشد!");

            var id = (int)idProp.GetValue(dto)!;
            if (id == 0)
                return new RequestResult<TDto>(false, RequestResultStatusCode.BadRequest, null, "محصول مورد نظر یافت نشد!");
            
            SetDefaultValueIfMatch(dto, nameof(BaseDto.UpdateDate), (DateTime?)DateTime.Now, DateTime.MinValue);
            
            var model = await GetByIdAsync(id, cancellationToken);

            if (model == null)
                return new RequestResult<TDto>(false, RequestResultStatusCode.BadRequest, null, "محصول مورد نظر یافت نشد!");
            
            dto.ToModel(model).Ignore("CreateDate").Map();
            
            //var model = dto.ToModel<TModel, TDto>().Ignore(nameof(BaseEntity.CreateDate)).Map();
            await Repository.UpdateAsync(model, cancellationToken);
            
            return new RequestResult<TDto>(true , RequestResultStatusCode.Success, dto);
        }

        public async Task<RequestResult<TModel>> UpdateByModelAsync(TModel model, CancellationToken cancellationToken)
        {
            SetDefaultValueIfMatch(model, nameof(BaseDto.UpdateDate), DateTime.Now, DateTime.MinValue);
            
            model = await Repository.UpdateAsync(model, cancellationToken);
            
            return new RequestResult<TModel>(true, RequestResultStatusCode.Success, model);
        }

        public async Task<RequestResult> UpdateRangeByModelAsync(List<TModel> models, CancellationToken cancellationToken)
        {
            if (models is { Count: <= 0 })
                return new RequestResult(false, RequestResultStatusCode.BadRequest);

            foreach (var model in models)
            {
                SetDefaultValueIfMatch(model, nameof(BaseDto.UpdateDate), DateTime.Now, DateTime.MinValue);
            }
            
            await Repository.UpdateRangeAsync(models, cancellationToken);
            return new RequestResult(true, RequestResultStatusCode.Success);
        }

        public virtual async Task<RequestResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var model = await GetByIdAsync(id, cancellationToken);

            if (model == null)
                return new RequestResult(false, RequestResultStatusCode.NotFound);
            
            var entityType = typeof(TModel);
            var deleteDateProp = entityType.GetProperty(nameof(BaseEntity.DeleteDate));

            if (deleteDateProp != null && deleteDateProp.CanWrite)
            {
                deleteDateProp.SetValue(model, DateTime.Now);
                await Repository.UpdateAsync(model, cancellationToken);
                return new RequestResult(true, RequestResultStatusCode.Success);
            }
            
            await Repository.DeleteAsync(model, cancellationToken);
            return new RequestResult(true, RequestResultStatusCode.Success);
        }

        public virtual async Task<List<OptionDto>> GetSelectListAsync(CancellationToken cancellationToken, Expression<Func<TModel, bool>> conditions = null, bool hasDefault = true)
        {
            var result = new List<OptionDto>();
            
            if (hasDefault)
                result.Add(new OptionDto()
                {
                    Value = null,
                    Text = "لطفا انتخاب کنید"
                });
            
            var options = await GetAllAsync(cancellationToken, conditions);
            
            result.AddRange(options.ToOptions());

            return result;
        }

        public async Task<bool> IsExistAsync(Expression<Func<TModel, bool>> conditions, CancellationToken cancellationToken)
        {
            return await Repository.IsExistAsync(conditions, cancellationToken);
        }

        #endregion

        #region Additional Methods
        
        public void SetDefaultValueIfMatch<TClass, TProperty>(TClass dto, string propertyName, TProperty defaultValue, TProperty expectedValue) where TClass : class
        {
            var property = dto.GetType().GetProperty(propertyName);
            
            if (property == null || property.PropertyType != typeof(TProperty)) return;
            
            var value = (TProperty)property.GetValue(dto);
            if (EqualityComparer<TProperty>.Default.Equals(value, expectedValue) || value is null)
            {
                property.SetValue(dto, defaultValue);
            }
        }

        #endregion
    }
}
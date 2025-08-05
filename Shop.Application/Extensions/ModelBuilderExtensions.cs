using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.DataTransferObjects.SuperSettingDataTransferObjects;
using Shop.Domain.Enums.Orders;
using Shop.Domain.Models;
using Shop.Domain.Models.Orders;
using Shop.Domain.Models.SuperSettings;

namespace Shop.Application.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void RegisterAllEntities<TBaseType>(this ModelBuilder modelBuilder)
        {
            var baseType = typeof(TBaseType);
            var excludedTypes = new[] { typeof(BaseEntity), typeof(BaseEntity<>), typeof(BasePureEntity), typeof(BasePureEntity<>)};

            var entities = baseType
                .Assembly
                .GetTypes()
                .Where(t =>
                    t.IsClass &&
                    !t.IsAbstract &&
                    t.IsPublic &&
                    baseType.IsAssignableFrom(t)&&
                    !excludedTypes.Contains(t)); 
            
            foreach (var entity in entities)
            {
                var entityBuilder = modelBuilder.Entity(entity);
                
                // var tableName = entity.Name + "s"; // مثلا User => Users
                // entityBuilder.ToTable(tableName);
                
                var deleteDateProp = entity.GetProperty(nameof(BaseEntity.DeleteDate));
                if (deleteDateProp != null && deleteDateProp.PropertyType == typeof(DateTime?))
                {
                    var parameter = Expression.Parameter(entity, "e");
                    var propertyAccess = Expression.Call(
                        typeof(EF),
                        nameof(EF.Property),
                        new [] {typeof(DateTime?)},
                        parameter,
                        Expression.Constant(nameof(BaseEntity.DeleteDate))
                    );

                    var nullConstant = Expression.Constant(null, typeof(DateTime?));
                    var equalExpression = Expression.Equal(propertyAccess, nullConstant);
                    
                    var lambda = Expression.Lambda(equalExpression, parameter);

                    entityBuilder.HasQueryFilter(lambda);
                }
            }

            modelBuilder.ApplyConfigurationsFromAssembly(baseType.Assembly);
        }
        
        public static void ConfigureEnumValues(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SuperSetting>().Property(r => r.Type)
                .HasConversion(r=> r.ToString(), r => (SuperSettingTypeEnum)Enum.Parse(typeof(SuperSettingTypeEnum), r));
            modelBuilder.Entity<Order>().Property(r => r.Status)
                .HasConversion(r => r.ToString(), r => (OrderStatusEnum)Enum.Parse(typeof(OrderStatusEnum), r));
        }
    }
}

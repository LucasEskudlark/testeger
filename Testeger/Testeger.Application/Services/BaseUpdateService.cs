using System.Reflection;
using AutoMapper;
using Testeger.Infra.UnitOfWork;

namespace Testeger.Application.Services;

public abstract class BaseUpdateService<TEntity, TUpdateRequest> : BaseService
    where TEntity : class
    where TUpdateRequest : class
{
    protected static readonly HashSet<string> IgnoredProperties = ["Id"];

    public BaseUpdateService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public virtual async Task UpdateEntityAsync(TUpdateRequest request, string id)
    {
        var existingEntity = await FindEntityByIdAsync(id);
        var updatedProperties = UpdateEntityPropertiesAsync(existingEntity, request);

        if (updatedProperties.Count > 0)
        {
            await _unitOfWork.CompleteAsync();
        }
    }

    protected virtual List<string> UpdateEntityPropertiesAsync(TEntity entity, TUpdateRequest request)
    {
        var updatedProperties = new List<string>();
        var dtoProperties = typeof(TUpdateRequest).GetProperties();
        var entityType = typeof(TEntity);

        foreach (var dtoProp in dtoProperties)
        {
            if (ShouldUpdateProperty(dtoProp, entityType, out var entityProp))
            {
                var oldValue = entityProp.GetValue(entity);
                if (TryUpdateProperty(entity, request, dtoProp, entityProp))
                {
                    updatedProperties.Add(dtoProp.Name);
                    var newValue = entityProp.GetValue(entity);
                    OnPropertyUpdated(entity, dtoProp.Name, oldValue, newValue);
                }
            }
        }
        return updatedProperties;
    }

    protected virtual void OnPropertyUpdated(TEntity entity, string propertyName, object oldValue, object newValue) { }

    protected static bool ShouldUpdateProperty(PropertyInfo dtoProp, Type entityType, out PropertyInfo entityProp)
    {
        entityProp = entityType.GetProperty(dtoProp.Name);
        return !IgnoredProperties.Contains(dtoProp.Name) && entityProp != null && entityProp.CanWrite;
    }

    protected bool TryUpdateProperty(TEntity entity, TUpdateRequest request, PropertyInfo dtoProp, PropertyInfo entityProp)
    {
        var dtoValue = dtoProp.GetValue(request);
        var entityValue = entityProp.GetValue(entity);

        if (dtoProp.PropertyType.IsEnum && entityProp.PropertyType.IsEnum && dtoValue != null)
        {
            dtoValue = Enum.ToObject(entityProp.PropertyType, (int)dtoValue);
        }

        if (dtoProp.PropertyType.IsClass && dtoProp.PropertyType != typeof(string))
        {
            var mappedValue = _mapper.Map(dtoValue, dtoProp.PropertyType, entityProp.PropertyType);
            if (!object.Equals(mappedValue, entityValue))
            {
                entityProp.SetValue(entity, mappedValue);
                return true;
            }
        }
        else if (!object.Equals(dtoValue, entityValue))
        {
            entityProp.SetValue(entity, dtoValue);
            return true;
        }

        return false;
    }

    protected abstract Task<TEntity> FindEntityByIdAsync(string id);
}

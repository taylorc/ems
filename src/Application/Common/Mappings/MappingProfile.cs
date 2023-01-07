using System.Reflection;
using AutoMapper;
using Ems.Domain.Enums;

namespace Ems.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<int, Gender>().ConvertUsing(x=> Gender.FromValue(x));
        
        CreateMap<int, Title>().ConvertUsing(x=> Title.FromValue(x));

        CreateMap<int, EmployeeType>().ConvertUsing(x => EmployeeType.FromValue(x));
        
        CreateMap<int, State>().ConvertUsing(x => State.FromValue(x));
        
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
       
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        ProcessMappings(assembly, typeof(IMapFrom<>), nameof(IMapFrom<object>.Mapping));
        ProcessMappings(assembly, typeof(IMapTo<>), nameof(IMapTo<object>.Mapping));
    }

    private void ProcessMappings(Assembly assembly, Type mapFromType, string mappingMethodName)
    {
        
        bool HasInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;

        var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();

        var argumentTypes = new Type[] { typeof(Profile) };

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);

            var methodInfo = type.GetMethod(mappingMethodName);

            if (methodInfo != null)
            {
                methodInfo.Invoke(instance, new object[] { this });
            }
            else
            {
                var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

                if (interfaces.Count <= 0)
                {
                    continue;
                }

                foreach (var @interface in interfaces)
                {
                    var interfaceMethodInfo = @interface.GetMethod(mappingMethodName, argumentTypes);

                    interfaceMethodInfo?.Invoke(instance, new object[] { this });
                }
            }
        }
    }
}

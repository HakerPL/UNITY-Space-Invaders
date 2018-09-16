using System;
using System.Collections.Generic;
using Unity.Entities;


public abstract class EcsSystem : ComponentSystem
{
    protected abstract override void OnUpdate();

    private static Dictionary<string, EntityDefinition> defs
      = new Dictionary<string, EntityDefinition>();

    protected EntityDefinition DefineEntity(String name)
    {
        var def = new EntityDefinition();
        defs.Add(name, def);
        return def;
    }

    public EcsSystem Spawn(string name)
    {
        EntityDefinition def = defs[name];
        PostUpdateCommands.CreateEntity(def.GetArchetype(EntityManager));
        def.Components.ForEach(cpi => cpi.PostUpdateSet(this));
        return this;
    }

    public void Set<T>(T cp) where T : struct, IComponentData
    {
        PostUpdateCommands.SetComponent(cp);
    }

}


public class EntityDefinition
{
    private EntityArchetype _archetype;
    public List<ComponentType> Types = new List<ComponentType>();
    public readonly List<IComponentDataWrapper> Components = new List<IComponentDataWrapper>();

    public EntityArchetype GetArchetype(EntityManager em)
    {
        if (_archetype == default(EntityArchetype)) _archetype = em.CreateArchetype(Types.ToArray());
        return _archetype;
    }

    public EntityDefinition Add<T>() where T : struct, IComponentData
    {
        Types.Add(ComponentType.Create<T>());
        return this;
    }

    public EntityDefinition Add<T>(T cd) where T : struct, IComponentData
    {
        Types.Add(ComponentType.Create<T>());
        Components.Add(new ComponentDataWrapper<T>(cd));
        return this;
    }
}

public interface IComponentDataWrapper
{
    void PostUpdateSet(EcsSystem system);
}

class ComponentDataWrapper<T> : IComponentDataWrapper where T : struct, IComponentData
{
    public T _Component;
    public ComponentDataWrapper(T component) { _Component = component; }
    public void PostUpdateSet(EcsSystem sys) { sys.Set(_Component); }
}
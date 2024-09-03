using System.ComponentModel.DataAnnotations;
namespace ChallengeN5.Command.Domain.Architecture.SeedWork;

/// <summary>
/// Base entity with an abstract key which implements IEquatable.
/// </summary>
/// <remarks>
/// Derived from SharpArch.Core.EntityWithTypedId.
/// For a discussion of this object, see
/// http://devlicio.us/blogs/billy_mccafferty/archive/2007/04/25/using-equals-gethashcode-effectively.aspx
/// </remarks>
public abstract class BaseEntity<TEntity, TKey> : IEntity<TKey>
    where TEntity : BaseEntity<TEntity, TKey>
    where TKey : struct, IEquatable<TKey>
{
    /// <summary>
    /// Id may be of type string, int, custom type, etc.
    /// Setter is protected to allow unit tests to set this property via reflection and to allow
    /// domain objects more flexibility in setting this for those objects with assigned Ids.
    /// It's virtual to allow NHibernate-backed objects to be lazily loaded.
    ///
    /// This is ignored for XML serialization because it does not have a public setter (which is very much by design).
    /// See the FAQ within the documentation if you'd like to have the Id XML serialized.
    /// </summary>
    public virtual TKey Id
    {
        get;
    }

    [ConcurrencyCheck]
    public Guid Version { get; set; }
}
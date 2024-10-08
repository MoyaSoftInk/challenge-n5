﻿namespace ChallengeN5.Command.Domain.Architecture.SeedWork;

public interface IAuditableEntity
{
    DateTime CreatedAt
    {
        get;
        set;
    }

    string? CreatedBy
    {
        get;
        set;
    }

    DateTime UpdatedAt
    {
        get;
        set;
    }

    string? UpdatedBy
    {
        get;
        set;
    }
}
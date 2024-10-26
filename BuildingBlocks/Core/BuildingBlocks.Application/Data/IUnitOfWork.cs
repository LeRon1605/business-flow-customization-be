﻿namespace BuildingBlocks.Application.Data;

public interface IUnitOfWork
{
    Task CommitAsync();

    Task BeginTransactionAsync();

    Task CommitTransactionAsync(bool autoRollbackOnFail = false);
    
    Task RollbackTransactionAsync();
}
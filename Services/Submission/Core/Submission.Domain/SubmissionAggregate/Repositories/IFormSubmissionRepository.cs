﻿using BuildingBlocks.Domain.Repositories;
using Submission.Domain.SubmissionAggregate.Entities;

namespace Submission.Domain.SubmissionAggregate.Repositories;

public interface IFormSubmissionRepository : IRepository<FormSubmission>
{
    Task<TOut?> FindByTrackingTokenAsync<TOut>(string trackingToken, IProjection<FormSubmission, TOut> projection);
}
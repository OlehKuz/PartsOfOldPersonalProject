using System;
using System.Collections.Generic;
using Payment.Services;
using Payment.Services.DeploymentPlans;

namespace Payment.Domain
{
    public class TaskDeploymentLogEntry
    {
        public Guid TransactionId { get; set; }
        public int UnderlyingEntityId { get; set; }
        public Guid? LockedBy { get; set; } = null;

        public ProcessState ProcessState { get; set; }
        public DateTime? CompleteByUtc { get; set; } = null;
        public int FailureCount { get; set; }
        public string EventName { get; set; }

        public IEnumerable<StageLog> TaskStages { get; set; } 
        ///public TaskDeploymentPlan TaskDeploymentPlan { get; set; }
    }
}

//using LearnMUSIC.Core.Application._Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace LearnMUSIC.Core.Application.Base
//{
//    public class RequestHandlerBase
//    {
//        private readonly IAppDbContext dbContext;

//        public RequestHandlerBase(IAppDbContext dbContext)
//        {
//            this.dbContext = dbContext;
//        }

//        /******************************************************************************/

//        protected void LogCustomAction(long userId, string module, string action, DateTime timeStamp)
//        {
//            var systemAuditTrail = new SystemAudit
//            {
//                UserId = userId,
//                Module = module,
//                Action = action,
//                TimeStamp = timeStamp
//            };

//            this.dbContext.SystemAudits.Add(systemAuditTrail);
//        }

//        protected void LogCustomSystemAudit(long userId, string module, string action, string recordIdentifier, List<string> propertyNames, object newEntity, object originalEntity, DateTime timeStamp)
//        {
//            var changes = this.GetEntityChanges(propertyNames, newEntity, originalEntity);

//            if (!changes.Any())
//            {
//                return;
//            }

//            var systemAuditTrail = new SystemAudit
//            {
//                UserId = userId,
//                Module = module,
//                Action = this.GenerateCustomAction(action, recordIdentifier, changes),
//                TimeStamp = timeStamp
//            };

//            this.dbContext.SystemAudits.Add(systemAuditTrail);
//        }

//        protected void LogRecordCreation(long userId, string module, string recordIdentifier, DateTime timeStamp)
//        {
//            var systemAuditTrail = new SystemAudit
//            {
//                UserId = userId,
//                Module = module,
//                Action = $"Created: {recordIdentifier}",
//                TimeStamp = timeStamp
//            };

//            this.dbContext.SystemAudits.Add(systemAuditTrail);
//        }

//        protected void LogRecordDeletion(long userId, string module, string recordIdentifier, DateTime timeStamp)
//        {
//            var systemAuditTrail = new SystemAudit
//            {
//                UserId = userId,
//                Module = module,
//                Action = $"Deleted: {recordIdentifier}",
//                TimeStamp = timeStamp
//            };

//            this.dbContext.SystemAudits.Add(systemAuditTrail);
//        }

//        protected void LogRecordUpdate(long userId, string module, string recordIdentifier, List<string> propertyNames, object newEntity, object originalEntity, DateTime timeStamp)
//        {
//            var changes = this.GetEntityChanges(propertyNames, newEntity, originalEntity);

//            if (!changes.Any())
//            {
//                return;
//            }

//            var systemAuditTrail = new SystemAudit
//            {
//                UserId = userId,
//                Module = module,
//                Action = this.GenerateActionForRecordUpdate(recordIdentifier, changes),
//                TimeStamp = timeStamp
//            };

//            this.dbContext.SystemAudits.Add(systemAuditTrail);
//        }

//        /******************************************************************************/

//        private string GenerateActionForRecordUpdate(string identifier, List<EntityChange> changes)
//        {
//            var action = new StringBuilder();

//            action.AppendLine($"Updated: {identifier}");
//            foreach (var change in changes)
//            {
//                action.AppendLine($"    {change.PropertyName}: [{change.OriginalValue}] -> [{change.NewValue}]");
//            }

//            return action.ToString();
//        }

//        private string GenerateCustomAction(string action, string identifier, List<EntityChange> changes)
//        {
//            var actions = new StringBuilder();

//            actions.AppendLine($"{action}: {identifier}");
//            foreach (var change in changes)
//            {
//                actions.AppendLine($"    {change.PropertyName}: [{change.OriginalValue}] -> [{change.NewValue}]");
//            }

//            return actions.ToString();
//        }

//        private List<EntityChange> GetEntityChanges(List<string> propertyNames, object newEntity, object originalEntity)
//        {
//            var changes = new List<EntityChange>();

//            foreach (var propertyName in propertyNames)
//            {
//                try
//                {
//                    var originalValue = originalEntity.GetType().GetProperty(propertyName).GetGetMethod().Invoke(originalEntity, null)?.ToString();
//                    var newValue = newEntity.GetType().GetProperty(propertyName).GetGetMethod().Invoke(newEntity, null)?.ToString();

//                    if (originalValue != newValue)
//                    {
//                        changes.Add(new EntityChange(propertyName, originalValue, newValue));
//                    }
//                }
//                catch
//                {
//                }
//            }

//            return changes;
//        }

//        /******************************************************************************/

//        public class EntityChange
//        {
//            public EntityChange(
//                string propertyName,
//                string originalValue,
//                string newValue)
//            {
//                this.PropertyName = propertyName;
//                this.OriginalValue = originalValue;
//                this.NewValue = newValue;
//            }

//            public string PropertyName { get; private set; }

//            public string OriginalValue { get; private set; }

//            public string NewValue { get; private set; }
//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Extensions.Options;
using owp_web.Models;


namespace owp_web.Helpers
{
    public class GraphAPI
    {
        private GraphServiceClient _graphServiceClient;

        public GraphAPI()
        {
            var clientSecretCredential = new ClientSecretCredential(
                "ecfdcfc9-91a9-4555-9a6e-ad249494c760", // tenantId
                "4a42c3ed-c598-478c-8e47-16da252fa026", // clientId
                "X.XP[W?FBHxnsb3BK:UX6uVJTsQq8sF0"   // clientSecret
            );

            _graphServiceClient = new GraphServiceClient(clientSecretCredential);
        }

        public async Task<List<Worker>> GetWorkersListAsync()
        {
            List<Worker> Workers = new List<Worker>();
            var _assignments = await _graphServiceClient.ServicePrincipals["4454926f-6463-4e95-a416-48d6d8bf86a6"].AppRoleAssignedTo.GetAsync();

            if (_assignments?.Value != null)
            {
                foreach (AppRoleAssignment assigment in _assignments.Value)
                {
                    if (assigment.AppRoleId.ToString() == "1b4f816e-5eaf-48b9-8613-7923830595ad")
                    {
                        Workers.Add(
                            new Worker()
                            {
                                AssignmentId = assigment.Id,
                                PrincipalDisplayName = assigment.PrincipalDisplayName,
                                PrincipalId = assigment.PrincipalId
                            }
                        );
                    }
                }
            }

            return Workers;
        }

        public async Task<Worker> GetWorkerByAssignmentIdAsync(string assignmentId)
        {
            var _assignment = await _graphServiceClient.ServicePrincipals["4454926f-6463-4e95-a416-48d6d8bf86a6"].AppRoleAssignedTo[assignmentId].GetAsync();

            return new Worker()
            {
                AssignmentId = _assignment.Id,
                PrincipalDisplayName = _assignment.PrincipalDisplayName,
                PrincipalId = _assignment.PrincipalId
            };
        }

        public async Task SendEmailByPrincipalIdAsync(Guid? principalId, Message message)
        {
            List<Recipient> recipientList = new List<Recipient>();

            var user = await _graphServiceClient.Users[principalId.ToString()].GetAsync();

            recipientList.Add(new Recipient
            {
                EmailAddress = new EmailAddress
                {
                    Address = user.Mail
                }
            });

            message.ToRecipients = recipientList;

            var sendMailPostRequestBody = new Microsoft.Graph.Users.Item.SendMail.SendMailPostRequestBody
            {
                Message = message
            };

            await _graphServiceClient.Users["8716bb10-935f-4ae0-a4f0-0313994fd41e"].SendMail.PostAsync(sendMailPostRequestBody);
        }

        public async Task SendEmailByEmailAsync(EmailAddress emailAddress, Message message)
        {
            List<Recipient> recipientList = new List<Recipient>();

            recipientList.Add(new Recipient
            {
                EmailAddress = emailAddress
            });

            message.ToRecipients = recipientList;

            var sendMailPostRequestBody = new Microsoft.Graph.Users.Item.SendMail.SendMailPostRequestBody
            {
                Message = message
            };

            await _graphServiceClient.Users["8716bb10-935f-4ae0-a4f0-0313994fd41e"].SendMail.PostAsync(sendMailPostRequestBody);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Extensions.Options;
using owp_web.Models;


namespace owp_web.Helpers
{
    public class GraphAPI
    {
        private GraphServiceClient _graphServiceClient;

        public GraphAPI()
        {
            IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
                .Create("4a42c3ed-c598-478c-8e47-16da252fa026")
                .WithTenantId("ecfdcfc9-91a9-4555-9a6e-ad249494c760")
                .WithClientSecret("X.XP[W?FBHxnsb3BK:UX6uVJTsQq8sF0")
                .Build();

            ClientCredentialProvider authProvider = new ClientCredentialProvider(confidentialClientApplication);
            _graphServiceClient = new GraphServiceClient(authProvider);
        }

        public async Task<List<Worker>> GetWorkersListAsync()
        {
            List<Worker> Workers = new List<Worker>();
            IServicePrincipalAppRoleAssignmentsCollectionPage _assignments = await _graphServiceClient.ServicePrincipals["4454926f-6463-4e95-a416-48d6d8bf86a6"].AppRoleAssignments.Request().GetAsync();

            foreach (AppRoleAssignment assigment in _assignments)
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

            return Workers;
        }

        public async Task<Worker> GetWorkerByPrincipalIdAsync(string assignmentId)
        {
            AppRoleAssignment _assignment = await _graphServiceClient.ServicePrincipals["4454926f-6463-4e95-a416-48d6d8bf86a6"].AppRoleAssignments[assignmentId].Request().GetAsync();

            return new Worker()
            {
                AssignmentId = _assignment.Id,
                PrincipalDisplayName = _assignment.PrincipalDisplayName,
                PrincipalId = _assignment.PrincipalId
            };
        }
    }
}

import http from 'k6/http';
import { check, group, sleep, fail } from 'k6';

export let options = {
  stages: [
    { duration: '5m', target: 100 }, // simulate ramp-up of traffic from 1 to 100 users over 5 minutes.
    { duration: '10m', target: 100 }, // stay at 100 users for 10 minutes
    { duration: '5m', target: 0 }, // ramp-down to 0 users
  ],
  thresholds: {
    http_req_duration: ['p(99)<1500'], // 99% of requests must complete below 1.5s
    'listed work items': ['p(99)<1500'], // 99% of requests must complete below 1.5s
  },
};
 

export default () => { 
  response = http.get(`https://owp-appservice.azurewebsites.net/Workitems/Index`);
  check(response, {
    'status is 200': (r) => r.status == 200,
  })
  sleep(1);
};
